/**************************************************************************/
/*! 
    @file     hmc5843.c
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

#ifndef _HMC5843_H_
#define _HMC5843_H_

#include "projectconfig.h"
#include "core/i2c/i2c.h"

#define HMC5843_ADDRESS (0x3C) // device ID
#define HMC5843_READBIT (0x01)

#define HMC5843_REGISTER_CONFIG_0 (0x20) //Config byte required

#define HMC5843_REGISTER_WHOAMI		(0x0F)

#define  HMC_CR_A		0x00           	//Configuration Register A
#define  HMC_CR_B		0x01           	//Configuration Register B
#define  HMC_MR			0x02           	//Mode Register
#define  HMC_DXM		0x03           	//Data Output X MSB
#define  HMC_DXL		0x04           	//Data Output X LSB
#define  HMC_DYM		0x05           	//Data Output Y MSB
#define  HMC_DYL		0x06           	//Data Output Y LSB
#define  HMC_DZM		0x07           	//Data Output Z MSB
#define  HMC_DZL		0x08           	//Data Output Z LSB
#define  HMC_SR			0x09           	//Status register
#define  HMC_ID_A		0x0A           	//ID Register A
#define  HMC_ID_B		0x0B           	//ID Register B
#define  HMC_ID_C		0x0C           	//ID Register C

//HMC scaling
#define HMC_0_pt_7_Gauss_Scaling (1.0 / 1620.0) //From HMC datasheet
#define HMC_1_pt_0_Gauss_Scaling (1.0 / 1300.0) //From HMC datasheet
#define HMC_1_pt_5_Gauss_Scaling (1.0 / 970.0) //From HMC datasheet
#define HMC_2_pt_0_Gauss_Scaling (1.0 / 780.0) //From HMC datasheet
#define HMC_3_pt_2_Gauss_Scaling (1.0 / 530.0) //From HMC datasheet
#define HMC_3_pt_8_Gauss_Scaling (1.0 / 460.0) //From HMC datasheet

typedef enum
{
  HMC5843_ERROR_OK = 0,               // Everything executed normally
  HMC5843_ERROR_I2CINIT,              // Unable to initialise I2C
  HMC5843_ERROR_I2CBUSY,              // I2C already in use
  HMC5843_ERROR_LAST
}
HMC5843Error_e;


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
MagData;


HMC5843Error_e HMC5843Init(void);
HMC5843Error_e HMC5843GetChannel (uint8_t channel, int16_t *data);
HMC5843Error_e HMC5843GetData (MagData *data);
unsigned char HMC5843_Get_WhoAmI (void);

int HMC5843CheckAll (void);


#endif


