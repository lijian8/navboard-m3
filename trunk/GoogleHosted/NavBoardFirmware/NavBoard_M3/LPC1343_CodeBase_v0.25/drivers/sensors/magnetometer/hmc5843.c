/**************************************************************************/
/*! 
    @file     hmc5843.c
    @author   M. Ryan (ryanmechatronics.com)
    @date     8 Aug 2010
    @version  0.10

    @section DESCRIPTION

    Driver for Honeywell 3 axis magnetometer
	
    @section Example

    @code 

    @endcode
	
    @section LICENSE

    Software License Agreement 

    Copyright (c) 2010, Ryan Mechatronics LLC
    All rights reserved.

    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:
    1. Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.
    2. Redistributions in binary form must reproduce the above copyright
    notice, this list of conditions and the following disclaimer in the
    documentation and/or other materials provided with the distribution.
    3. Neither the name of the copyright holders nor the
    names of its contributors may be used to endorse or promote products
    derived from this software without specific prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS ''AS IS'' AND ANY
    EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
    DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
    (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
    LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
    ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
    (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
    SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
/**************************************************************************/

#include "hmc5843.h"

extern volatile uint8_t   I2CMasterBuffer[I2C_BUFSIZE];
extern volatile uint8_t   I2CSlaveBuffer[I2C_BUFSIZE];
extern volatile uint32_t  I2CReadLength, I2CWriteLength;

uint32_t i;
uint8_t mag_init;

static HMC5843Error_e HMC5843Write8 (uint8_t reg, uint32_t value)
{
  // Clear write buffers
  for ( i = 0; i < I2C_BUFSIZE; i++ )
  {
    I2CMasterBuffer[i] = 0x00;
  }

  I2CWriteLength = 3;
  I2CReadLength = 0;
  I2CMasterBuffer[0] = HMC5843_ADDRESS;             // I2C device address
  I2CMasterBuffer[1] = reg;                       // Command register
  I2CMasterBuffer[2] = (value & 0xFF);            // Value to write
  i2cEngine();
  return HMC5843_ERROR_OK;
}


static HMC5843Error_e HMC5843Read8(uint8_t reg, uint8_t *value)
{
  // Clear write buffers
  for ( i = 0; i < I2C_BUFSIZE; i++ )
  {
    I2CMasterBuffer[i] = 0x00;
  }

  I2CWriteLength = 0; //was 2.  should be?
  I2CReadLength = 1;
  I2CMasterBuffer[0] = HMC5843_ADDRESS;             // I2C device address
  I2CMasterBuffer[1] = reg;                       // Command register
  // Append address w/read bit
  I2CMasterBuffer[2] = HMC5843_ADDRESS | HMC5843_READBIT;  
  i2cEngine();

  // Shift values to create properly formed integer
  *value = I2CSlaveBuffer[0];

  return HMC5843_ERROR_OK;
}



/**************************************************************************/
/*! 
    @brief  Initialises the I2C block
*/
/**************************************************************************/
HMC5843Error_e HMC5843Init(void)
{
  HMC5843Error_e response_error= HMC5843_ERROR_OK;
  uint8_t whoami[3], tmpbyte;
  
  // Initialise I2C
  if (i2cInit(I2CMODE_MASTER) == FALSE)
  {
    return HMC5843_ERROR_I2CINIT;    /* Fatal error */
  }
    
   // Write some bytes to check proper com
  
   // Write the value 0x18 to Config Register A in the HMC
   //	This represents a 50Hz data output rate, normal measurement rate
   response_error = HMC5843Write8 (HMC_CR_A, 0x18);

   //Setup register B
   response_error = HMC5843Write8 (HMC_CR_B, 0x20); //Gain of 1 on internal device gain (1 Ga default)

   //Mode register that sets operating mode.  0x00 = continuous conversion
   response_error = HMC5843Write8 (HMC_MR, 0x00); 


   // As a check, read the ID bits now...should return H43 ascii equivalent
   HMC5843Read8(HMC_ID_A, &tmpbyte);
   whoami[0] = tmpbyte;
   HMC5843Read8(HMC_ID_B, &tmpbyte);
   whoami[1] = tmpbyte;
   HMC5843Read8(HMC_ID_C, &tmpbyte);
   whoami[2] = tmpbyte;
   
   //Now ready to fill the incoming data buffer with data starting at 0x03
  

  if (response_error == HMC5843_ERROR_OK)
	{
		mag_init = 1;
	} else {
		mag_init = 0;
	}
   return response_error;
  
}


/**************************************************************************/
/*! 
    @brief  Reads the entire magnetometer structure

    @note   fills entire structure both with raw and scaled values.
*/
/**************************************************************************/
HMC5843Error_e HMC5843GetData (MagData *data)
{
	uint8_t rxbytes[6], tmpbyte, whoami[3];
	int16_t ix;
 

	// Read all channels
	HMC5843Error_e error = HMC5843_ERROR_OK;
	if (!mag_init)
        HMC5843Init();


	// As a check, read the ID bits now...should return H43 ascii equivalent
	HMC5843Read8(HMC_ID_A, &tmpbyte);
	whoami[0] = tmpbyte;
	HMC5843Read8(HMC_ID_B, &tmpbyte);
	whoami[1] = tmpbyte;
	HMC5843Read8(HMC_ID_C, &tmpbyte);
	whoami[2] = tmpbyte;  

        data->status = (short) ((int) error << 8) | ((int) tmpbyte);//Populate who am i field - only using last bit
  
        for (ix = 0;ix<6; ix++)
        {
            HMC5843Read8((HMC_DXM+ix), &tmpbyte);
            rxbytes[ix]=tmpbyte;
        }

        data->raw_X = (short) ((int) rxbytes[0] << 8) | ((int) rxbytes[1]);
        data->raw_Y = (short) ((int) rxbytes[2] << 8) | ((int) rxbytes[3]);
        data->raw_Z = (short) ((int) rxbytes[4] << 8) | ((int) rxbytes[5]);

	data->X = data->raw_X * HMC_1_pt_0_Gauss_Scaling;
	data->Y = data->raw_Y * HMC_1_pt_0_Gauss_Scaling;
	data->Z = data->raw_Z * HMC_1_pt_0_Gauss_Scaling;

        return error;

}

/**************************************************************************/
/*! 
    @brief  Reads the entire magnetometer structure

    @note   fills entire structure both with raw and scaled values.
*/
/**************************************************************************/
unsigned char HMC5843_Get_WhoAmI (void)
{
	uint8_t tmpbyte, whoami[3];
 
	// Read all channels
	HMC5843Error_e error = HMC5843_ERROR_OK;
	if (!mag_init)
          HMC5843Init();


	// As a check, read the ID bits now...should return H43 ascii equivalent
	HMC5843Read8(HMC_ID_A, &tmpbyte);
	whoami[0] = tmpbyte;
	HMC5843Read8(HMC_ID_B, &tmpbyte);
	whoami[1] = tmpbyte;
	HMC5843Read8(HMC_ID_C, &tmpbyte);
	whoami[2] = tmpbyte;  

        if((whoami[0] == 0x48) && (whoami[1] == 0x34) && (whoami[2] == 0x33)) 
          {
            return (1);
          }
        return(0);
}


int HMC5843CheckAll (void)
{
	uint8_t i;
	static uint8_t tmpbyte, dataholder[15];

	// Read all channels
	HMC5843Error_e error = HMC5843_ERROR_OK;
	if (!mag_init)
          HMC5843Init();

        for(i=0;i<15;i++)
        {
          HMC5843Read8(i, &tmpbyte);
          dataholder[i]=tmpbyte;
        }

        if (dataholder[HMC_ID_A] == 'H') return 1;
        return 0;

}