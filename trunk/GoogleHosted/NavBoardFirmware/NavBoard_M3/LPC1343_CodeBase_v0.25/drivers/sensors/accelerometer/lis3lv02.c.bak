/**************************************************************************/
/*! 
    @file     lis3lv02.c
    @author   M. Ryan (ryanmechatronics.com)
    @date     8 Aug 2010
    @version  0.10

    @section DESCRIPTION

    Driver for ST Microelectronics 3 axis accelerometer
	
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

#include "lis3lv02.h"

extern volatile uint8_t   I2CMasterBuffer[I2C_BUFSIZE];
extern volatile uint8_t   I2CSlaveBuffer[I2C_BUFSIZE];
extern volatile uint32_t  I2CReadLength, I2CWriteLength;

uint32_t i;
uint8_t tilt_init;

static LIS3LV02Error_e LIS3LV02Write8 (uint8_t reg, uint32_t value)
{
  // Clear write buffers
  for ( i = 0; i < I2C_BUFSIZE; i++ )
  {
    I2CMasterBuffer[i] = 0x00;
  }

  I2CWriteLength = 3;
  I2CReadLength = 0;
  I2CMasterBuffer[0] = LIS3LV02_ADDRESS;             // I2C device address
  I2CMasterBuffer[1] = reg;                       // Command register
  I2CMasterBuffer[2] = (value & 0xFF);            // Value to write
  i2cEngine();
  return LIS3LV02_ERROR_OK;
}


static LIS3LV02Error_e LIS3LV02Read8(uint8_t reg, uint8_t *value)
{
  // Clear write buffers
  for ( i = 0; i < I2C_BUFSIZE; i++ )
  {
    I2CMasterBuffer[i] = 0x00;
  }

  I2CWriteLength = 0; 
  I2CReadLength = 1;
  I2CMasterBuffer[0] = LIS3LV02_ADDRESS;             // I2C device address
  I2CMasterBuffer[1] = reg;                       // Command register
  // Append address w/read bit
  I2CMasterBuffer[2] = LIS3LV02_ADDRESS | LIS3LV02_READBIT;  
  i2cEngine();

  // Shift values to create properly formed integer
  *value = I2CSlaveBuffer[0];

  return LIS3LV02_ERROR_OK;
}


/**************************************************************************/
/*! 
    @brief  Initialises the I2C block
*/
/**************************************************************************/
LIS3LV02Error_e LIS3LV02Init(void)
{
  LIS3LV02Error_e response_error= LIS3LV02_ERROR_OK;
  // Initialise I2C
  if (i2cInit(I2CMODE_MASTER) == FALSE)
  {
    return LIS3LV02_ERROR_I2CINIT;    /* Fatal error */
  }
  
  response_error = LIS3LV02Write8 (LIS3LV02_REGISTER_CONFIG_0, 0x87);

  if (response_error == LIS3LV02_ERROR_OK)
	{
		tilt_init = 1;
	} else {
		tilt_init = 0;
	}
   return response_error;
  
}

/**************************************************************************/
/*! 
    @brief  Reads the desired acceleration from the indicated channel

    @note   This method will assign a signed 16-bit value (int16) to 'data',
            where each unit represents XXXX m/s.  To convert the numeric
            value to m/s you must divide the value of 'data'
            by 8.  This conversion is not done automatically, since you may
            or may not want to use floating point math for the calculations.
*/
/**************************************************************************/
LIS3LV02Error_e LIS3LV02GetChannel (uint8_t channel, int16_t *data)
{
  // Read channel
  LIS3LV02Error_e error = LIS3LV02_ERROR_OK;
  uint8_t lbyte, hbyte, ch;
  int16_t ix;
    
    if (!tilt_init)
        LIS3LV02Init();


    switch(channel) {
        case 1:  // x axis
            ch = LIS3LV02_REGISTER_XAxis;
            break;  
        case 2:  // y axis
            ch = LIS3LV02_REGISTER_YAxis;
            break;  
        case 3:  // z axis
            ch = LIS3LV02_REGISTER_ZAxis;
            break;  
        default:
            return 0;  // invalid channel
    }

	lbyte = 0;
        hbyte = 0;
	error = LIS3LV02Read8 (ch, &lbyte);
	//MIGHT HAVE TO PUT DELAY HERE
        //systickDelay(1);
        error = LIS3LV02Read8 (ch+1, &hbyte);

        *data = (int16_t) ((int) hbyte << 8) | ((int) lbyte);
	
  return error;
}

/**************************************************************************/
/*! 
    @brief  Reads the entire accelerometer structure

    @note   fills entire structure both with raw and scaled values.
*/
/**************************************************************************/
LIS3LV02Error_e LIS3LV02GetData (AccData *data)
{
  // Read all channels
  LIS3LV02Error_e error = LIS3LV02_ERROR_OK;
  if (!tilt_init)
        LIS3LV02Init();

  uint8_t lbyte, cnt;
  int16_t ix;

  error = LIS3LV02Read8 (LIS3LV02_REGISTER_WHOAMI, &lbyte);
    data->status = (short) ((int) error << 8) | ((int) lbyte);//Populate who am i field
  
  for (cnt=1;cnt<4;cnt++)
  {
    LIS3LV02GetChannel (cnt, &ix);
    switch(cnt) {
        case 1:  // x axis
            data->raw_X = ix;
            data->X = (float) ix / 1024.0 * 9.8; //1024 LSB per g per datasheet at 2 g
            break;  
        case 2:  // y axis
            data->raw_Y = ix;
            data->Y = (float) ix / 1024.0 * 9.8; //1024 LSB per g per datasheet at 2 g
            break;  
        case 3:  // z axis
            data->raw_Z = ix;
            data->Z = (float) ix / 1024.0 * 9.8; //1024 LSB per g per datasheet at 2 g;
            break;  
        default:
            return 0;  // invalid 
    }
  }
}
/**************************************************************************/
/*! 
    @brief  Writes the supplied 8-bit value to the LIS3LV02 config register
*/
/**************************************************************************/
LIS3LV02Error_e LIS3LV02ConfigWrite (uint8_t configValue)
{
  return LIS3LV02Write8 (LIS3LV02_REGISTER_CONFIG_0, configValue);
}

/**************************************************************************/
/*! 
    @brief  Reads the WHO_AM_I only

    @note   BIT for accel
*/
/**************************************************************************/
uint8_t LIS3LV02_Get_WhoAmI (void)
{
  LIS3LV02Error_e error = LIS3LV02_ERROR_OK;
  if (!tilt_init)
        LIS3LV02Init();

  uint8_t lbyte;

  error = LIS3LV02Read8 (LIS3LV02_REGISTER_WHOAMI, &lbyte);
  if(lbyte == 0x3A) 
  {
    return 1;
  } else {
    return 0;
  }
    
  return (lbyte);//Populate who am i field
}
