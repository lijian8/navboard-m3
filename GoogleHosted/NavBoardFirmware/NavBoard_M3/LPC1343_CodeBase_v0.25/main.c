/**************************************************************************/
/*! 
    @file     main.c
    @author   M. Ryan (ryanmechatronics.com)
    @date     02 Sept 2010
    @version  0.10

    @section DESCRIPTION

    Main program

    Original file structure developed by microbuilder.eu (www.micxrobuilder.eu)

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

#include "sysinit.h"

#ifdef CFG_INTERFACE
  #include "core/cmd/cmd.h"
#endif

#ifdef CFG_NAVBOARD
    #include "core/cmd/cmd_NavBoard.h"
    #include "core/adc/adc.h"
    #include "stdio.h"
    #include "string.h"
#endif

#ifdef CFG_LIS3LV02
  #include "drivers/sensors/accelerometer/lis3lv02.h"
  unsigned char bAccel_LIS3LV02; 
#endif

#ifdef CFG_LSM303
  #include "drivers/sensors/accelerometer/LSM303.h"
  unsigned char bAccel_LSM303;
#endif  

#if defined CFG_LIS3LV02 || defined CFG_LSM303
  AccData accdat;
#endif

#ifdef CFG_ITG3200
  #include "drivers/sensors/gyros/itg3200.h"
  GyroData gyrodat;
  unsigned char bGyro_ITG3200;
#endif

#ifdef CFG_HMC5843
  #include "drivers/sensors/magnetometer/hmc5843.h"
  MagData magdat;
  unsigned char bMag_HMC5843;
#endif

#ifdef CFG_UBLOX5
  #include "drivers/GPS/ublox/ublox_lea5.h"
      GPS_Data gpsdat;
      unsigned char bGPS_Present;
#endif
//<<<<<<<<<<<<<<<



/**************************************************************************/
/*! 
    Main program entry point.  After reset, normal code execution will
    begin here.
*/
/**************************************************************************/
int main (void)
{
  uint8_t mode = 2;
  uint8_t tmpmode = 0xFF;
  uint32_t mGPS_Timer =0;
  

  //Port pins
  uint8_t gpio_dat[8]; // GPIO values
                       /*
                          Port 0.11 (doubles as AD0)
                          Port 1.0 (doubles as AD1)
                          Port 1.1 (GPS Time Pulse signal, not user accessible via connectors)
                          Port 1.2 (Magnetometer data ready signal, not user accessible via connectors)
                          Port 1.9 (mislabeled as 1.8 on board)
                          Port 1.10 (mislabeled as 1.9 on board)
                          Port 2.6 - SP pin on Nav Board
                          final entry compiles all these into an output for the binary
                       */
  uint16_t adc_dat[2]; // ADC 0 and 1 values

   //Binary output array
  uint8_t index, mBinOut[80]; //65 + extra buffer bytes, scale this back
  
  // Configure cpu and mandatory peripherals
  systemInit();
  
  // Initialize AD channels 0 and 1
  adcInit();

  //Wait for 2.0 seconds on boot.  Look at ARM enable line (P2_6).  
  //    If low, go into the stand alone routines.
  //    If high, go into sensor package mode (i.e. safe the I2C lines and reduce power)

  systickDelay(3000);

  printf("Booting Unit\n");

  // BUILT IN TEST AND ID
  //  Do a built in test of all modules
  //  This determines what sensors are attached and if we use them later or not
#ifdef CFG_LIS3LV02
  bAccel_LIS3LV02 = (LIS3LV02_Get_WhoAmI());
    printf("LIS pass\n");
#else
  bAccel_LSM303 = (LSM303_Get_WhoAmI());
#endif

  bMag_HMC5843 = (HMC5843_Get_WhoAmI());
      printf("HMC pass\n");

  bGyro_ITG3200 = (itg3200_Get_WhoAmI());
    printf("ITG pass\n");
  ///GPS doesn't have a who am i though...
  bGPS_Present = gps_GetData(&gpsdat); 
  mGPS_Timer = SysTick_Get();
  //END BIT
      printf("GPS pass\n");


  while (1)
  {
    // SERVICE incoming requests
      //For more simple handling of user input
      tmpmode = cmdPoll_NavBoard();
      if ((tmpmode >=0) && (tmpmode <=2)) mode = tmpmode;
      // For more complex menu handling, use / modify the freaklabs parser found in cmdPoll();
    //END COM INPUT

    //GET INTERNAL DATA
      //Get GPS data
        if (SysTick_DeltaTime_ms( mGPS_Timer) >= 1000)
        {
          bGPS_Present = gps_GetData(&gpsdat);
          mGPS_Timer = SysTick_Get();
        }
      
#ifdef CFG_LIS3LV02
      LIS3LV02GetData(&accdat);
#else
      LSM303GetData(&accdat);
#endif

      itg3200GetData (&gyrodat);

      HMC5843GetData (&magdat);

      //Read ADC channels
      adc_dat[0] = (uint16_t) adcRead(0);
      adc_dat[1] = (uint16_t) adcRead(1);

      //Get GPIO states
      gpio_dat[0] = gpioGetValue (0, 11);   //P0.11, AD0
      gpio_dat[1] = gpioGetValue (0, 1);    //P0.1, AD1
      gpio_dat[2] = gpioGetValue (1, 1);    //P1.1, GPS TP
      gpio_dat[3] = gpioGetValue (1, 2);    //P1.2, MAG DR
      gpio_dat[4] = gpioGetValue (1, 9);    //P1.9 Mislabeled on board
      gpio_dat[5] = gpioGetValue (1, 10);   //P1.10 Mislabeled on board
      gpio_dat[6] = gpioGetValue (2, 6);    //P2.6 SP on board
      gpio_dat[7] = ((gpio_dat[6]<<6)||(gpio_dat[5]<<5)||(gpio_dat[4]<<4)||(gpio_dat[3]<<3)||(gpio_dat[2]<<2)||(gpio_dat[1]<<1)||(gpio_dat[0]<<0));

    //END GETTING DATA

    //OUTPUT DATA TO USER
      //Now, do something with it based on mode
      switch (mode)
      {
        case 0: //default mode, easy to read text out
            printf("G,%d,%d,%d,%d,%d,%d\r", gpsdat.fix, gpsdat.sat, gpsdat.utc, gpsdat.lat, gpsdat.lon, gpsdat.alt);
            printf("A,%d, %d, %d, %d\r", 99, accdat.raw_X, accdat.raw_Y, accdat.raw_Z);
            printf("R,%d, %d, %d, %d\r", (short) gyrodat.temp, gyrodat.raw_X, gyrodat.raw_Y, gyrodat.raw_Z);
            printf("M,%d, %d, %d, %d\r",  99, magdat.raw_X, magdat.raw_Y, magdat.raw_Z);
            printf("I,%d,%d,%d,%d,%d,%d,%d\r", 
                gpio_dat[0],gpio_dat[1],gpio_dat[2],gpio_dat[3],gpio_dat[4],gpio_dat[5],gpio_dat[6]);
            printf("V,%d,%d\r\r", adc_dat[0], adc_dat[1]);
          break;
        case 1: //binary mode
            mBinOut[0]=0xAE; mBinOut[1]=0xAE; //Repeated header
            mBinOut[2] = 0;  //Replaced later with index
            index = 3;
            memcpy(&mBinOut[index], &gpsdat, sizeof(GPS_Data)); //Note, if GPS structure changes, this will be invalid since we point partly into it and count bytes to get the rest
            index +=sizeof(GPS_Data);
            memcpy(&mBinOut[index], &accdat.X, 12); 
            index +=12;
            memcpy( &mBinOut[index], &gyrodat.temp, 16);
            index +=16;
            memcpy( &mBinOut[index], &magdat.X, 12);
            index +=12;
            memcpy( &mBinOut[index], &adc_dat, 4);
            index +=4;
            memcpy( &mBinOut[index], &gpio_dat[7], 1);
            index +=1;
            mBinOut[2] = index;
            putArray ((uint8_t *)mBinOut, index);
          break;
        case 2: //BIT mode
            printf("Ryan Mechatronics\r");
            printf("-----------------\r");
            printf("Nav Board M3 Built In Test \r\r");
            printf("Accel: ");
      
#ifdef CFG_LIS3LV02
            if (bAccel_LIS3LV02) {printf("LIS3LV02DQ\r");}else{printf("NONE / FAIL\r");};
#else
            if (bAccel_LSM303) {printf("LSM303\r");}else{printf("NONE / FAIL\r");};  
#endif

            printf("Gyro: ");
            if (bGyro_ITG3200) {printf("ITG-3200\r");}else{printf("NONE / FAIL\r");};
            printf("Mag: ");
            if (bMag_HMC5843) {printf("HMC 5843\r");}else{printf("NONE / FAIL\r");};
            printf("GPS: ");
            if (bGPS_Present) {printf("U-Blox\r\r");}else{printf("NONE / FAIL\r\r");};

            systickDelay(1000);
          break;
        default:
          mode = 0;
      }

      //END OUTPUT TO USER
  }
}


