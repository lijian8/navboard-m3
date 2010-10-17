/**************************************************************************/
/*! 
    @file     lis3lv02.h
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

#ifndef _LIS3LV02_H_
#define _LIS3LV02_H_

#include "projectconfig.h"
#include "core/i2c/i2c.h"

#define LIS3LV02_ADDRESS (0x3A) // device ID
#define LIS3LV02_READBIT (0x01)

#define LIS3LV02_REGISTER_CONFIG_0 (0x20) //Config byte required

#define LIS3LV02_REGISTER_WHOAMI		(0x0F)

#define LIS3LV02_REGISTER_XAxis			(0x28)
#define LIS3LV02_REGISTER_YAxis			(0x2A)
#define LIS3LV02_REGISTER_ZAxis			(0x2C)



typedef enum
{
  LIS3LV02_ERROR_OK = 0,               // Everything executed normally
  LIS3LV02_ERROR_I2CINIT,              // Unable to initialise I2C
  LIS3LV02_ERROR_I2CBUSY,              // I2C already in use
  LIS3LV02_ERROR_LAST
}
LIS3LV02Error_e;


#pragma pack (push)
#pragma pack (1)
typedef struct
{
        short status; //low byte is "whoami".  High is pending
        short raw_X;
	short raw_Y;
	short raw_Z;
        float X;
        float Y;
        float Z;
}
AccData;


LIS3LV02Error_e LIS3LV02Init(void);
LIS3LV02Error_e LIS3LV02GetChannel (uint8_t channel, int16_t *data);
LIS3LV02Error_e LIS3LV02GetData (AccData *data);
LIS3LV02Error_e LIS3LV02ConfigWrite (uint8_t configValue);
uint8_t LIS3LV02_Get_WhoAmI (void);

#endif


