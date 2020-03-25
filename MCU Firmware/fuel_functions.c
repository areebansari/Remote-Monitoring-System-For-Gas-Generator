int16 get_adc(void)
{
   read_input_regs(0,1);     // get adc value from slave PIC MCU (address 0x02)
   return(make16(modbus_rx.data[1],modbus_rx.data[2]));
}

void func_check_fuel_theft(void)
{
   if(!theft_check_enable) { check_fuel_theft = _TIME_TO_CHECK_FUEL_THEFT; return; }
   
   fuel_delta = fuel_array[FUEL_ARRAY_SIZE-1] - fuel_array[0];
   int k;
   for (k=1; k<FUEL_ARRAY_SIZE; k++) memmove(fuel_array+k-1,fuel_array+k,4);
      
   if(gen_status)
   {
      #ifdef DEBUG_ENABLED
//!         fprintf(PC,"Signed Delta_on : %2.4w",signed_delta_on);
      #endif
      if(fuel_delta < signed_delta_on)
      {    
        
         delta_count++;
         delta_timeout = 180;
         
         if(delta_count>=3)
         {            
//!            send_sms_report = TRUE;
            alarm_counter=MAX_ALARM_COUNT;
            alarm1.fuel_theft = 2;
            
            #ifdef DEBUG_ENABLED
               fprintf(PC,"<-** FUEL THEFT SUSPECTED **->\r\n\r\n");
            #endif
            delta_count=0;
         }
       }
       else
       {
         alarm1.fuel_theft = 1;
       }
   }
   else
   {
      #ifdef DEBUG_ENABLED
//!         fprintf(PC,"Signed Delta_Off : %2.4w",signed_delta_off);
      #endif
      if(fuel_delta < signed_delta_off)
      {            
         delta_count++;
         delta_timeout = 180;
         
         if(delta_count>=3)
         {
//!            send_sms_report = TRUE;
            alarm_counter=MAX_ALARM_COUNT;
            alarm1.fuel_theft = 2;
            
            #ifdef DEBUG_ENABLED
               fprintf(PC,"<-** FUEL THEFT SUSPECTED **->\r\n\r\n");
            #endif
            delta_count=0;
         }
       }
       else
       {
         alarm1.fuel_theft = 1;
       }
   }
   
   #ifdef DISPLAY_FUEL_ARRAY
   for (k=0; k<FUEL_ARRAY_SIZE; k++)
   {
      fprintf(PC,"fuel_array[%u] : %2.4w\r\n", k, fuel_array[k]);
   }
   #endif

   check_fuel_theft = _TIME_TO_CHECK_FUEL_THEFT;
}

void read_fuel_level(void)
{
   // 160 ms execution time
   output_high(LED_5);
   static int i=0;
//!   static int high_fuel_flag = 0;
//!   static int low_fuel_flag = 0;
//!   static int1 array_full = FALSE;
   
//!   int r=0;
//!   long avg_fuel_level_new;
//!   static long avg_fuel_level_old=0;
   
   long fuel_adc_val;
   
   if(fuel_averaging)
   {
//!      static int32 sum;
//!      static int n=0;
//!      int m;

      fuel_adc_val = median_filter(get_adc());
      percent_fuel_new.i32_val = fuel_adc_val*1000000;   // mul by 1000000 b/c 4 places
                                                         // are req after decimal.
   }
   else   
   {
      fuel_adc_val = get_adc();
      percent_fuel_new.i32_val = fuel_adc_val*1000000;   // mul by 1000000 b/c 4 places
                                                         // are req after decimal.
   }
   
   #ifdef DEBUG_ENABLED
   fprintf(PC,"percent_fuel_new.i32_val : %4.0w\r\n",percent_fuel_new.i32_val);
   #endif
   
   // suppose fuel_adc_val is 1912 then 
   // percent_fuel_new.i32_val will be 1912000000
   // percent_fuel_new.i32_val after div by 4095 will be 466910
   // in real world it will be 46.6910
   
   percent_fuel_new.i32_val /= 4095;
   
   fuel_array[i] = percent_fuel_new.i32_val;
   fuel_level = percent_fuel_new.i32_val;
   
                        // if the fuel is below the low level threshold then send sms.
   if(percent_fuel_new.i32_val < low_fuel_threshold)
   {
      alarm2.fuel_level_low = 2;
   }
   else 
   {
      alarm2.fuel_level_low = 1;
   }   
   
                        // if the fuel is above the high level threshold then send sms.
   if(percent_fuel_new.i32_val > high_fuel_threshold)
   {
      alarm2.fuel_level_high = 2;   output_toggle(LED_3);
   }
   else 
   {
      alarm2.fuel_level_high = 1;   output_toggle(LED_1);
   }   
   
   // first the fuel array will be filled then "theft_check_enable" will be activated.
   if (i<FUEL_ARRAY_SIZE-1) i++;
   else theft_check_enable = TRUE;
   
   #ifdef DEBUG_ENABLED
//!   fprintf(PC,"avg_fuel_level_new : %Lu\r\n",avg_fuel_level_new);
//!   fprintf(PC,"interrupt_count : %u\r\n",interrupt_count);

   fprintf(PC,"delta_count : %u\r\n",delta_count);
   fprintf(PC,"\r\nFuel Level : %2.4w %%\r\n",percent_fuel_new.i32_val);
   #endif
   
   get_fuel_level = _TIME_TO_GET_FUEL_LEVEL; // reload the scan time.
   output_low(LED_5);
}
