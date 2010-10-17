/**************************************************************************/
/*! 
    @file     ublox_lea5.h
    @author   M. Ryan (ryanmechatronics.com)
    @date     8 Aug 2010
    @version  0.10

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

#ifndef _ublox5_H_
#define _ublox5_H_

#include "projectconfig.h"
#include "core/i2c/i2c.h"

#define ublox5_ADDRESS (0x84) // device ID
#define ublox5_READBIT (0x01)

#define ublox_REGISTER_FD (0xFD) 
#define ublox_REGISTER_FE (0xFE) 
#define ublox_REGISTER_FF (0xFF) 


typedef enum
{
  ublox5Error_OK = 0,               // Everything executed normally
  ublox5Error_I2CINIT,              // Unable to initialise I2C
  ublox5Error_I2CBUSY,              // I2C already in use
  ublox5Error_LAST
}
ublox5Error_e;

#pragma pack (push)
#pragma pack (1)
typedef struct {
    int32_t lat;  // lat degrees x 10^6
    int32_t lon;  // lon degrees x 10^6
    int32_t alt;
    int16_t fix;
    int16_t sat;
    uint32_t utc;
    //float values
    float latitude;
    float longitude;
    float altitude;
    //
    int16_t newdata;
} GPS_Data;




void gps_show();
int gps_parse();
unsigned char read_ublox();

ublox5Error_e ublox5Init(void);
ublox5Error_e ublox5GetData (GPS_Data *data);

int gps_GetData (GPS_Data *gpsdata);

#endif


