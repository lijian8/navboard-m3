/**************************************************************************/
/*! 
    @file     cmd_NavBoard.c
    @author   M. Ryan (ryanmechatronics.com)
    @date     14 Sept 2010
    @version  0.10

    @section DESCRIPTION
     
    Handles simple typed in characters on serial or USB port and returns them to the calling function

    Core from both Christopher Wang (Freaklabs) and K. Townsend (microBuilder.eu)
    See cmd.c for more information


    @section LICENSE

    Software License Agreement (BSD License)

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

#include <stdio.h>
#include <string.h>

//#include "cmd.h"
#include "cmd_NavBoard.h"

#ifdef CFG_PRINTF_UART
#include "core/uart/uart.h"
#endif

#ifdef CFG_PRINTF_USBCDC
  #include "core/usbcdc/cdcuser.h"
  static char usbcdcBuf [32];
#endif

/**************************************************************************/
/*! 
    @brief  Polls the relevant incoming message queue and returns the value if it is a number
*/
/**************************************************************************/
uint8_t cmdPoll_NavBoard(void)
{
  uint8_t c;

  c = 0xFF;
   
  #if defined CFG_PRINTF_UART
  while (uartRxBufferDataPending())
  {
    c = uartRxBufferRead();
  }
  #endif

  #if defined CFG_PRINTF_USBCDC
    int  numBytesToRead, numBytesRead, numAvailByte;
  
    CDC_OutBufAvailChar (&numAvailByte);
    if (numAvailByte > 0) 
    {
        numBytesToRead = numAvailByte > 32 ? 32 : numAvailByte; 
        numBytesRead = CDC_RdOutBuf (&usbcdcBuf[0], &numBytesToRead);
        int i;
        //for (i = numBytesToRead; i > 0; --i)
        //{
        //  c = usbcdcBuf[i-1]; //Last character in buffer is used
        //}

        for (i = 0; i < numBytesRead; i++) 
        {  
          c = usbcdcBuf[i]; //Last character in buffer is used  
        }  

    }
  #endif

  return (c - 0x30); //returns a value, not the hex ASCII value
}


