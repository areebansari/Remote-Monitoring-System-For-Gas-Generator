#include "sms_modbus_prog.h"
#include "modem_init.c"
#include "modbus_functions.c"
#include "gsm_functions.c"
#include "fuel_functions.c"



/*==================================================================================*/
/***
   Funtion     :  void serial_isr(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  Serial interrupt for capturing the GSM Modem Data.
***/   
/*==================================================================================*/
#INT_RDA2
void serial_isr(void)                                    
{  
   static char c=0;
   
   c=fgetc(PC_PORT);             // store received character in a variable.   
   gsm_buffer[index]=c;          // Save stream in an array.
   
   
   if(LF_char)                   // If reading sms then count characters in this routine.
   {
      if(c==0x0D) { new_line_char_rcv++; response_timeout=0; }
   }
   else                          // Count characters in this routine except reading sms.
   {
      if(c==0x0A)
      {
         new_line_char_rcv++; response_timeout=0;
         counter++;
         if(counter>=2) something_new_received=TRUE;
      }
   }
   index++;
}



/*==================================================================================*/
/***
   Funtion     :  void timer0_isr
   Parameters  :  None
   Return value:  None
   Purpose     :  Timer0 is used for timing purpose.
***/   
/*==================================================================================*/
#int_timer0
void timer0_isr()
{
//!   if(time_to_read_parameters)   time_to_read_parameters--;
//!   if(time_to_send_parameters)   time_to_send_parameters--;
   if(time_to_check_alarm)       time_to_check_alarm--;
   if(modem_init_timeout)        modem_init_timeout--;
   if(time_to_check_sms_memory)  time_to_check_sms_memory--;
   if(get_fuel_level)            get_fuel_level--;
   if(check_fuel_theft)          check_fuel_theft--;
//!   if(gprs_function_halt)        gprs_function_halt--;
   if(time_to_check_gen_status)  time_to_check_gen_status--;
   if(time_to_send_alarm_conditions) time_to_send_alarm_conditions--;
   if (delta_timeout)   delta_timeout--;
   else delta_count=0;
   
   if(time_to_send_data)   time_to_send_data--;
   
   set_timer0(46004);            // value for 1 sec interrupt.
}



/*==================================================================================*/
/***
   Funtion     :  void main (void)
   Parameters  :  None
   Return value:  None
   Purpose     :  Main loop of the program.Performs different tasks.
***/   
/*==================================================================================*/
void main()
{
   clear_interrupt(INT_RDA2);                // Clear Serial Interrupt (GSM Modem).
   enable_interrupts(INT_RDA2);              // Enable Serial Interrupt (GSM Modem).
   
   modbus_init();                            // Initialize Modbus settings.
   
   set_timer0(46004);                        // Value for 1 sec interrupt.
   setup_timer_0(T0_INTERNAL | T0_DIV_256);  // 16-bit timer, tick time : 51.2usec.
   enable_interrupts(INT_TIMER0);            // Enable timer0 interrupt.
                                                             
   enable_interrupts(GLOBAL);                // Enable global interrupts.
   
   memset(&alarm1,0,sizeof(alarm1));         // Initialize parameters structure to 0
   
   signed_delta_off = integer_delta_off * -1; 
   signed_delta_on = integer_delta_on * -1; 
   
   memset(fuel_array,0,sizeof(fuel_array));  // Reset all elements of fuel_array to 0

   if(check_comm()==MODULE_OK)               // if response of check_comm() is OK then
   {                                         // it means Modem is ON and working.
      powered_on_flag=TRUE;
      check_sim_status=TRUE;
   }
   else if (gsm_power_up()==MODULE_OK)       // else TURN ON the modem and check if 
   {                                         // gsm_power_up() is TRUE or not.
      powered_on_flag=TRUE;                  // Modem is ON.
   }
   
   system_power_up=TRUE;
   
   reset_buffers();
   
   while(TRUE)
   {
//!      output_toggle(PIN_B2);

      // if system starts or need to initialize modem in middle.
      if(modem_init_status && !modem_init_timeout)   modem_initialization();
      
      // check generator status if it is ON/OFF.
      if(!time_to_check_gen_status)    check_gen_status();
      
      // if Generator is ON then send data according to desired time.
      if(gen_status)                   send_data_to_server();
      
      // if something is received on GSM.
      if(something_new_received)       check_rcvd_data();
      
      // if incoming call.
      if(incoming_call)                disconnect_call();
      
      // if new sms received.
      if(new_sms_received)             read_sms();
      
      // if report is requested.
//!      if(get_modbus_data)                    get_data_from_deepsea();
 
      if(system_power_up && modem_init_ok) { get_date_and_time(0); 
                                             send_sms(1);}
      if(gen_turn_on)                      { get_date_and_time(0);
                                             send_sms(2);}
      if(normal_data)                        send_sms(3);
      if(alarm_counter >= MAX_ALARM_COUNT)   send_sms(4);
      
      if(!time_to_check_sms_memory)          check_sms_memory();
      if(delete_sms)                         del_sms();
      
      if(!get_fuel_level)              read_fuel_level();      // read after every 3 secs.
      if(!check_fuel_theft)            func_check_fuel_theft();// read after every 4 secs.
      if(!time_to_check_alarm)         read_alarm_status();    // read after every 6 secs.
   }
}
