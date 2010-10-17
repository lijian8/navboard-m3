/**************************************************************************/
/*! 
    @file     ublox_lea5.c
    @author   M. Ryan (ryanmechatronics.com)
    @date     8 Aug 2010
    @version  0.10

    @section DESCRIPTION

    Driver for Ublox LEA 5 GPS unit
	
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

#include "ublox_lea5.h"
#include "core/systick/systick.h"
#include "stdio.h"



extern volatile uint8_t   I2CMasterBuffer[I2C_BUFSIZE];
extern volatile uint8_t   I2CSlaveBuffer[I2C_BUFSIZE];
extern volatile uint32_t  I2CReadLength, I2CWriteLength;

uint32_t i;
GPS_Data gps_gga;
uint8_t gps_init;

static ublox5Error_e uBlox5Read8(uint8_t reg, unsigned char *value)
{
  // Clear write buffers
  for ( i = 0; i < I2C_BUFSIZE; i++ )
  {
    I2CMasterBuffer[i] = 0x00;
  }

  I2CWriteLength = 2;//was 2.  should be??
  I2CReadLength = 1;
  I2CMasterBuffer[0] = ublox5_ADDRESS;             // I2C device address
  I2CMasterBuffer[1] = reg;                       // Command register
  // Append address w/read bit
  I2CMasterBuffer[2] = ublox5_ADDRESS | ublox5_READBIT;  
  i2cEngine();

  // Shift values to create properly formed integer
  *value = I2CSlaveBuffer[0];

  return ublox5Error_OK;
}


static ublox5Error_e uBlox5Write8(uint8_t reg,  unsigned char *value)
{
  // Clear write buffers
  for ( i = 0; i < I2C_BUFSIZE; i++ )
  {
    I2CMasterBuffer[i] = 0x00;
  }

  I2CWriteLength = 2;
  I2CReadLength = 8;
  I2CMasterBuffer[0] = ublox5_ADDRESS;             // I2C device address
  I2CMasterBuffer[1] = 0xFD;                       // Command register
  i2cEngine();

  *value = I2CSlaveBuffer[0];

 
}




/**************************************************************************/
/*! 
    @brief  Initialises the I2C block
*/
/**************************************************************************/
ublox5Error_e ublox5Init(void)
{
    unsigned char dat;
    int ix;
    ublox5Error_e response_error= ublox5Error_OK;
    // Initialise I2C
    if (i2cInit(I2CMODE_MASTER) == FALSE)
    {
      return ublox5Error_I2CINIT;    /* Fatal error */
    }

    //Init the GPS structure so it is obvious if we don't have a lock
    gps_gga.fix = 0;
    gps_gga.lat = 99;
    gps_gga.lon = 99;
    gps_gga.alt = 99;
    gps_gga.sat = 99;
    gps_gga.utc = 99;
    gps_gga.latitude = 99.0;
    gps_gga.longitude = 99.0;
    gps_gga.altitude = 99.0;
    gps_gga.newdata = 0;

    if (response_error == ublox5Error_OK)
    {
		gps_init = 1;
	} else {
		gps_init = 0;
    }

    return response_error;
}

/**************************************************************************/
/*! 
    @brief  Handles Ublox reading

    @note   Simple method for ublox, just keep reading until we get non 0xFF.  See
	http://www.u-blox.com/images/downloads/Product_Docs/DDC_Implementation_AppNote(GPS.G5-X-08023).pdf
*/
/**************************************************************************/


unsigned char read_ublox() {
    unsigned char dat;
    int ix;

    for (ix=0; ix<10; ix++) {  
       dat = 0xff; 
	//Might have to go to low speed?
	uBlox5Read8(ublox_REGISTER_FF, &dat);

        if (dat != 0xff)   // if there's no data, i2cread() reads 0xff
        {  
             //printf("%d\r\n", (int) dat);
            return dat;
        }
    }
    return 0xff;  // no data
}

/**************************************************************************/
/*! 
    @brief  Reads the GPS data set

    @note   This method will read some NMEA sentences from the GPS and fill the data.
*/
/**************************************************************************/


int gps_parse() {
    unsigned char buf[100];
    unsigned char ch;
    uint32_t t0;
    unsigned int sum, pow10, div10;
    int i1, i2, ilast, ix;
    int latdeg, londeg, latmin, lonmin;
    
    i1 = i2 = ilast = 0;  // to get rid of compiler warnings

    t0 = SysTick_Get();

    while (1) {
        //
        if ( SysTick_DeltaTime_ms(t0) > 100) { // check for timeout at 100 msec intervals
            return 0;
        }
            ch = read_ublox();
            if (ch == 0xff)
                continue;
        if (ch != '$')   // look for "$GPGGA," header
            continue;
        for (i1=0; i1<6; i1++) { 
                buf[i1] = read_ublox();
        }
        if ((buf[2] != 'G') || (buf[3] != 'G') || (buf[4] != 'A'))
            continue;
        for (i1=0; i1<100; i1++) {
                buf[i1] = read_ublox();


            if (buf[i1] == '\r') {
                buf[i1] = 0;
                ilast = i1;
                break;
            }
        }
        //printf("$GPGGA,%s\r\n", buf);

        // i1 = start of search, i2 = end of search (comma), ilast = end of buffer

        // parse utc
        i1 = 0;
        sum = 0;
        pow10 = 1;
        div10 = 0;
        for (ix=0; ix<ilast; ix++) {
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }
        for (ix=(i2-1); ix>=i1; ix--) {
            if (buf[ix] == '.') {
                div10 = 1;
                continue;
            }
            sum += pow10 * (buf[ix] & 0x0F);
            pow10 *= 10;
            div10 *= 10;
        }
        div10 = pow10 / div10;
        gps_gga.utc = sum / div10;
        
        // parse lat
        i1 = i2+1;
        sum = 0;
        pow10 = 1;
        div10 = 0;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }
        for (ix=(i2-1); ix>=i1; ix--) {
            if (buf[ix] == '.') {
                div10 = 1;
                continue;
            }
            sum += pow10 * (buf[ix] & 0x0F);
            if (ix>i1) {  // to prevent overflow
                pow10 *= 10;
                div10 *= 10;
            }
        }
        div10 = pow10 / div10;
        latdeg = sum / (div10*100);
        latmin = sum - (latdeg*div10*100);
        latmin = (latmin * 100) / 60;  // convert to decimal minutes
        gps_gga.lat = (latdeg*div10*100) + latmin;
        if (div10 > 10000)
            gps_gga.lat /= (div10 / 10000);  // normalize lat minutes to 6 decimal places
            
        // skip N/S field
        i1 = i2+1;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == 'S')
                gps_gga.lat = -gps_gga.lat;
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }


        // parse lon
        i1 = i2+1;
        sum = 0;
        pow10 = 1;
        div10 = 0;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }
        for (ix=(i2-1); ix>=i1; ix--) {
            if (buf[ix] == '.') {
                div10 = 1;
                continue;
            }
            sum += pow10 * (buf[ix] & 0x0F);
            if (ix>i1) {  // to prevent overflow
                pow10 *= 10;
                div10 *= 10;
            }
        }
        div10 = pow10 / div10;
        londeg = sum / (div10*100);
        lonmin = sum - (londeg*div10*100);
        lonmin = (lonmin * 100) / 60;  // convert to decimal minutes
        gps_gga.lon = (londeg*div10*100) + lonmin;        
        if (div10 > 10000)
            gps_gga.lon /= (div10 / 10000);  // normalize lon minutes to 6 decimal places

        // skip E/W field
        i1 = i2+1;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == 'W')
                gps_gga.lon = -gps_gga.lon;
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }

        // parse fix
        i1 = i2+1;
        sum = 0;
        pow10 = 1;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }
        gps_gga.fix = buf[i2-1] & 0x0F;
        
        // parse satellites
        i1 = i2+1;
        sum = 0;
        pow10 = 1;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }
        for (ix=(i2-1); ix>=i1; ix--) {
            sum += pow10 * (buf[ix] & 0x0F);
            pow10 *= 10;
        }
        gps_gga.sat = sum;
        
        // skip horz-precision field
        i1 = i2+1;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }

        // parse alt
        i1 = i2+1;
        sum = 0;
        pow10 = 1;
        for (ix=i1; ix<ilast; ix++) {
            if (buf[ix] == ',') {
                i2 = ix;
                break;
            }
        }
        for (ix=(i2-1); ix>=i1; ix--) {
            if (buf[ix] == '.')
                continue;
            sum += pow10 * (buf[ix] & 0x0F);
            pow10 *= 10;
        }
        gps_gga.alt = sum / 10;
        

                
        gps_gga.latitude = (float) gps_gga.lat / 1000000.0; //Scale to float
        gps_gga.longitude = (float) gps_gga.lon / 1000000.0; //Scale to float
        gps_gga.altitude = (float) gps_gga.alt; //Scale to float
        return 1;
    }
}


int gps_GetData (GPS_Data *gpsdata)
{
    if (!gps_init)
        ublox5Init();
		
		//DEBUG
		if (!gps_parse())
		{
                    //printf("no response from gps\n\r");
                    gps_gga.newdata = 0;
                    *gpsdata = gps_gga;
                    return 0;

		} else {
                    gps_gga.newdata = 1;
                    *gpsdata = gps_gga; //Copy current data set over to response
                    return 1;
                }
}

