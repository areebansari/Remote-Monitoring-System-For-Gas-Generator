/*==================================================================================*/
/***
   Funtion     :  int get_rsp(int LF_char_count, char *command, long timeout_val,
                              int error_type)
   Parameters  :  LF_char_count, *command, timeout_val, error_type.
   
   Return value:  return value is one of these
                  [] MODULE_OK 
                  [] MODULE_ERROR
                  [] MODULE_CME_ERROR
                  [] MODULE_TIMEOUT.
   
   Purpose     :  This function will compare the received stream with the desired
                  string and retrun the value.
                  
                  This function gets parameters like
                  [] LF_char_count-> Indicates how many new line characters will be there.
                  [] *command     -> Indicates with which command the received stream
                                     will be compared.
                  [] timeout_val  -> Timeout value.
                  [] error_type   -> Compare the received stream with possible errors 
                                     if any.
                  
                  It will return the value which is the response of the received stream.
                  [] MODULE_OK         -> It means received response is OK.
                  [] MODULE_ERROR      -> It means received response have an ERROR.
                  [] MODULE_CME_ERROR  -> It means received response have an CME ERROR.
                  [] MODULE_TIMEOUT    -> It means timeout.
***/   
/*==================================================================================*/
int get_rsp(int LF_char_count, char *command, long timeout_val, int error_type)
{
   while(new_line_char_rcv<LF_char_count && response_timeout<=timeout_val)
   {
      delay_ms(1);   response_timeout++;
   }
   
   if(LF_char) delay_ms(2);
   _APPEND_NULL_CHARACTER; // This will add a NULL character at the end of the stream 
                           // and it is compulsory if we want to use the strstr function.
   
   if(strstr(gsm_buffer,command)!=NULL)   return(MODULE_OK);
   else if(error_type==MODULE_ERROR)
   {
      if(strstr(gsm_buffer,error_rsp)!=NULL) return(MODULE_ERROR);
   }
   else if(error_type==MODULE_CME_ERROR)
   {
      if(strstr(gsm_buffer,CME_error_rsp)!=NULL)   return(MODULE_CME_ERROR);
   }
   return(MODULE_TIMEOUT);
}



/*==================================================================================*/
/***
   Funtion     :  void reset_buffers(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  This function clear some variables.
***/   
/*==================================================================================*/
void reset_buffers()
{
   new_line_char_rcv=0;
   response_timeout=0;
   memset(gsm_buffer,0,index);
   index=0;
}



/*==================================================================================*/
/***
   Funtion     :  void get_date_and_time(int rcv_index)
   Parameters  :  rcv_index
   Return value:  None
   Purpose     :  This function gets the current date and time form GSM Modem.
***/   
/*==================================================================================*/
void get_date_and_time(int rcv_index)
{
   int rsp;
   reset_buffers();
   fprintf(PC_PORT,"AT+CCLK?\r");      // Command for date and time.
   rsp=get_rsp(4,cclk_rsp,5000,MODULE_CME_ERROR);
   if(rsp==MODULE_OK)
   {                                   // Store the time and date values in an array.
      memcpy(date,gsm_buffer+10,8);
      date[8] = "\0";  // Append null in clock string.
      memcpy(time,gsm_buffer+19,8);
      time[8] = "\0";  // Append null in clock string.
   } 
   reset_buffers();
}



/*==================================================================================*/
/***
   Funtion     :  void check_rcvd_data(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  This function checks what is received?
                  New SMS or Incoming Call.
***/   
/*==================================================================================*/
void check_rcvd_data(void)
{
   if(strstr(gsm_buffer,new_sms_indication)!=NULL)             // SMS Received. 
   {
      new_sms_received=TRUE;output_high(PIN_C4);output_low(PIN_C4);
   }   
   else if(strstr(gsm_buffer,ring)!=NULL) incoming_call=TRUE;  // Ring.
//!   else if(strstr(gsm_buffer,cgreg_rsp)!=NULL)  network_reg_status=TRUE;
   something_new_received=FALSE;counter=0;
}



/*==================================================================================*/
/***
   Funtion     :  void disconnect_call(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  This function Disconnect Call.
***/   
/*==================================================================================*/
void disconnect_call(void)
{
   reset_buffers();
   fprintf(PC_PORT,"ATH\r");     // Command for disconnecting call.
   if(get_rsp(2,ok_rsp,5000,MODULE_ERROR)==MODULE_OK) incoming_call=FALSE;counter=0;
   reset_buffers();
}



/*==================================================================================*/
/***
   Funtion     :  void read_sms(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  This function read the SMS. First it will check the Number, if 
                  number matches then check the text, if text matches then send sms
                  to the sender.
***/   
/*==================================================================================*/
void read_sms(void)
{
   int1  authorized_no=FALSE;
   int   response;
//!   output_toggle(GREEN_LED);
   if(index==17)        sms_number = (TO_INT(gsm_buffer[14]));
   else if(index==18)   sms_number = ((TO_INT(gsm_buffer[14]))*10) + 
                                     (TO_INT(gsm_buffer[15]));
   reset_buffers();
   LF_char=TRUE;        // This will compare 0x0D instead of 0x0A because of the format.
   
   fprintf(PC_PORT,"AT+CMGR=%u\r",sms_number);           // Read sms command.
   
//!   if(get_rsp(5,HOST_NO,5000,MODULE_ERROR)==MODULE_OK)   // Check if HOST NO Match.
//!   {
//!      output_high(PIN_A0);output_low(PIN_A0);            // for testing purpose.
//!      
//!      if(strstr(gsm_buffer,report_str))                  // Is report req?
//!      {
//!         if(CHECK_MSG_BODY(index,14,report_str)==65)     // Is received msg body is in 
//!         {                                               // correct format
//!            output_high(PIN_A1);output_low(PIN_A1);      // for testing purpose.
//!            send_sms_to=HOST;                            // |
//!            normal_data=TRUE;                            // | set some variables.
//!            get_modbus_data=TRUE;                        // |
//!         }
//!      }
//!   }
   
//!   response = get_rsp(5,HOST_NO_1,5000,MODULE_ERROR);
//!   
//!   if(response == MODULE_TIMEOUT)   {output_high(LED_1);output_high(LED_4);}
//!   else if(response == MODULE_OK)   {output_high(LED_1);output_low(LED_4);authorized_no=TRUE;}
//!   else if(response == MODULE_ERROR){output_low(LED_1);output_high(LED_4);}
//!   else if(response == MODULE_CME_ERROR)  {output_low(LED_1);output_low(LED_4);}

   if(get_rsp(5,HOST_NO_1,5000,MODULE_ERROR)==MODULE_OK)
   {
      authorized_no=TRUE;
   }
//!   else if(get_rsp(5,HOST_NO_2,5000,MODULE_ERROR)==MODULE_OK) 
//!   { 
//!      authorized_no=TRUE; //output_toggle(RED_LED);
//!      index--; // In _"APPEND_NULL_CHARACTER" macro index is incremented by 1 for adding
//!               // NULL character. Here two times index was incremented first in the "if"
//!               // loop and second in the "else if" loop. So index is decremented by 1
//!               // b/c this extra increment will disturb the next process.
//!   }
   
   if(authorized_no)
   {
//!      output_high(PIN_A0);output_low(PIN_A0);            // for testing purpose.
      if(strstr(gsm_buffer,report_str))                  // Is report req?
      {
         output_toggle(LED_1);
         if(CHECK_MSG_BODY(index,14,report_str)==65)     // Is received msg body is in
         {                                               // correct format
            output_toggle(LED_2);
            if(index==82)
            {  
               output_high(LED_3);
               time_to_send_data_2 = (TO_INT(gsm_buffer[72]));
            }   
            
            else if(index==83)
            {
               output_high(LED_4);
               time_to_send_data_2 = ((TO_INT(gsm_buffer[72]))*10) +
                                      (TO_INT(gsm_buffer[73]));
            }
            
            else if(index==84)   
            {
               output_high(LED_5);
               time_to_send_data_2 = (TO_INT(gsm_buffer[72]));
               time_to_send_data_2 = time_to_send_data_2*100;
               time_to_send_data_2 = time_to_send_data_2 + ((TO_INT(gsm_buffer[73]))*10)
                                     + (TO_INT(gsm_buffer[74]));
            }
//!            output_high(PIN_A1);output_low(PIN_A1);      // for testing purpose.
//!            output_toggle(RED_LED);
//!            send_sms_to=HOST;                            // |
//!            normal_data=TRUE;                            // | set some variables.
//!            get_modbus_data=TRUE;                        // |
            
            authorized_no=FALSE;                         // |
         }
      }
   }
   reset_buffers();
   LF_char=FALSE;                        // Now onwards 0x0A will be compared again. 
   new_sms_received=FALSE;
}



/*==================================================================================*/
/***
   Funtion     :  void send_sms(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  This function will send an SMS to the predefined number.
***/   
/*==================================================================================*/
/*void send_sms(void)
{
   int cell_index=0;
   int1 normal_flag = FALSE, alarm_flag = FALSE, system_power_flag = FALSE;
   reset_buffers();
   
//!   if(send_sms_to==HOST)   memcpy(temp_array,HOST_NO,13);//cpy host no into temp 
//!                                                   //array without null character
   
   memcpy(temp_array,HOST_NO_1,13);   // Copy Host No into temp array without Null character.
   
   fprintf(PC_PORT,"AT+CMGS=\"");   // sending AT command for SMS to modem.
   
   do
   {
      fprintf(PC_PORT,"%c",temp_array[cell_index]);   // sending HOST No to Modem.
//!      output_high(PIN_A2);output_low(PIN_A2);         // for testing purpose only.
      cell_index++;
   }while(cell_index<13);
   
   fprintf(PC_PORT,"\"\r");         // sending Enter command to indicate the stream is 
                                    // completed.
   while(index<=3);                 // 0x0D, 0x0A and > , wait for these 3 characters.
   
   if(gsm_buffer[2]=='>')
   {
      reset_buffers();
      
//!      if(normal_data)   { send_normal_data(); normal_flag = TRUE; }
//!      else if(alarm_counter>=MAX_ALARM_COUNT)   { send_alarms_data(); alarm_flag = TRUE; }
//!      else if(system_power_up)       
      if(system_power_up)
      {                             // when system starts, it will send 'S' to indicate
                                    // the system is started, the start time & sim no
                                    // to the server.
         fprintf(PC_PORT,"S,");
         fprintf(PC_PORT,"%s,",date);
         fprintf(PC_PORT,"%s,",time);
         fprintf(PC_PORT,"num.");
         system_power_flag = TRUE;
      }
      else if (gen_status_flag)
      {
         fprintf(PC_PORT,"G,");
         fprintf(PC_PORT,"%s,",date);
         fprintf(PC_PORT,"%s,",time);
         fprintf(PC_PORT,"num.");
      }
      else if(normal_data)   { send_normal_data(); normal_flag = TRUE; }
      else if(alarm_counter>=MAX_ALARM_COUNT)   { send_alarms_data(); alarm_flag = TRUE; }
      
      fprintf(PC_PORT,"%c",ctrl_z);
      
      // after sending sms, wait for the response. If response is OK then reset some
      // variables.
      if(get_rsp(4,ok_rsp,10000,MODULE_ERROR)==MODULE_OK)      
      {
         send_sms_report=FALSE;
         if(normal_flag) { normal_data=FALSE; normal_flag=FALSE;}
         
         if(alarm_flag) { alarm_counter=0; alarm_flag=FALSE;
                          time_to_send_alarm_conditions=_TIME_TO_SEND_ALARM_CONDITIONS;}
         
         if(system_power_flag) { system_power_up=FALSE; system_power_flag=FALSE; }
         
         if(gen_status_flag)  gen_status_flag=FALSE;
         output_toggle(PIN_A1);
//!         output_high(PIN_A3);output_low(PIN_A3);   // for testing purpose only.
      }   
//!      else  output_high(PIN_A4);output_low(PIN_A4);// for testing purpose only.
   }
   reset_buffers();
}*/

void send_sms(int report_type)
{
   int cell_index=0;
//!   int1 normal_flag = FALSE, alarm_flag = FALSE, system_power_flag = FALSE;
   reset_buffers();
   
   memcpy(temp_array,HOST_NO_1,13);   // Copy Host No into temp array without Null character.
   
   fprintf(PC_PORT,"AT+CMGS=\"");   // sending AT command for SMS to modem.
   
   do
   {
      fprintf(PC_PORT,"%c",temp_array[cell_index]);   // sending HOST No to Modem.
//!      output_high(PIN_A2);output_low(PIN_A2);         // for testing purpose only.
      cell_index++;
   }while(cell_index<13);
   
   fprintf(PC_PORT,"\"\r");         // sending Enter command to indicate the stream is 
                                    // completed.
   while(index<=3);                 // 0x0D, 0x0A and > , wait for these 3 characters.
   
   if(gsm_buffer[2]=='>')
   {
      reset_buffers();
      
      if(report_type==1)
      {                             // when system starts, it will send 'S' to indicate
                                    // the system is started, the start time & sim no
                                    // to the server.
         fprintf(PC_PORT,"S,");
         fprintf(PC_PORT,"%s,",date);
         fprintf(PC_PORT,"%s,",time);
         fprintf(PC_PORT,"num.");
//!         system_power_flag = TRUE;
      }
      else if(report_type==2)
      {
         fprintf(PC_PORT,"G,");
         fprintf(PC_PORT,"%s,",date);
         fprintf(PC_PORT,"%s,",time);
         fprintf(PC_PORT,"num.");
      }
      else if(report_type==3)   
      { 
         send_normal_data(); 
//!         normal_flag = TRUE;
      }
      else if(report_type==4)
      { 
         send_alarms_data(); 
//!         alarm_flag = TRUE;
      }
      
      fprintf(PC_PORT,"%c",ctrl_z);
      
      // after sending sms, wait for the response. If response is OK then reset some
      // variables.
      if(get_rsp(4,ok_rsp,10000,MODULE_ERROR)==MODULE_OK)      
      {
         send_sms_report=FALSE;
         if(report_type==1)      { system_power_up=FALSE; }
         else if(report_type==2) { gen_turn_on=FALSE; }
         else if(report_type==3) { normal_data=FALSE;     }
         else if(report_type==4) { alarm_counter=0;
                                   time_to_send_alarm_conditions=
                                   _TIME_TO_SEND_ALARM_CONDITIONS;}
         
//!         if(normal_flag) { normal_data=FALSE; normal_flag=FALSE;}
         
//!         if(alarm_flag) { alarm_counter=0; alarm_flag=FALSE;
//!                          time_to_send_alarm_conditions=_TIME_TO_SEND_ALARM_CONDITIONS;}
         
//!         if(system_power_flag) { system_power_up=FALSE; system_power_flag=FALSE; }
         
//!         if(gen_status_flag)  gen_status_flag=FALSE;
         output_toggle(PIN_A1);
//!         output_high(PIN_A3);output_low(PIN_A3);   // for testing purpose only.
      }   
//!      else  output_high(PIN_A4);output_low(PIN_A4);// for testing purpose only.
   }
   reset_buffers();
}

/*==================================================================================*/
/***
   Funtion     :  void check_sms_memory(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  This function checks if the SMS Memory is FULL or NOT.
***/   
/*==================================================================================*/
void check_sms_memory(void)
{
   int total_sms=0, used_sms=0, remaining_sms=0;
   
   reset_buffers();
   fprintf(PC_PORT,"AT+CPMS?\r");      // Command for checking sms memory.
   if(get_rsp(4,sms_memory_rsp,5000,MODULE_ERROR)==MODULE_OK)  // if response is OK
   {
      if(index==47)        // if nos of sms are < 10
      {
         used_sms  = (TO_INT(gsm_buffer[14]));
         total_sms = ((TO_INT(gsm_buffer[16]))*10) + (TO_INT(gsm_buffer[17]));
         remaining_sms = total_sms - used_sms;
      }
      else if(index==50)   // if nos of sms are >= 10
      {
         used_sms  = ((TO_INT(gsm_buffer[14]))*10) + (TO_INT(gsm_buffer[15]));
         total_sms = ((TO_INT(gsm_buffer[17]))*10) + (TO_INT(gsm_buffer[18]));
         remaining_sms = total_sms - used_sms;
      }
      if(!remaining_sms)   delete_sms=TRUE;
      time_to_check_sms_memory = _TIME_TO_CHECK_SMS_MEMORY;
   }
   reset_buffers();
}



/*==================================================================================*/
/***
   Funtion     :  void del_sms(void)
   Parameters  :  None
   Return value:  None
   Purpose     :  This function delete all the sms from memory.
***/   
/*==================================================================================*/
void del_sms(void)
{
   reset_buffers();
   fprintf(PC_PORT,"AT+CMGDA=\"DEL ALL\"\r");            // Command for deleting sms.
   if(get_rsp(2,ok_rsp,5000,MODULE_ERROR) == MODULE_OK)  // if response is OK
   {  
      sms_number=0; delete_sms=FALSE;                    // reset variables.   
//!      output_high(PIN_A5);output_low(PIN_A5);            // for testing purpose only.
   }
   else  output_high(PIN_A6);output_low(PIN_A6);         // for testing purpose only.
   reset_buffers();
}



/*==================================================================================*/
/***
   Funtion     :  int CHECK_MSG_BODY(int _index, int _COUNT, char *command)
   Parameters  :  _index, _COUNT, *command
   Return value:  
   Purpose     :  This function checks if the received text is in correct/defined
                  format or not.
***/   
/*==================================================================================*/
int CHECK_MSG_BODY(int _index,int _COUNT, char *command)//--------------------------
{
   char *ret;
   int count=0,starting_location=0;
   
   ret=strstr(gsm_buffer,command);
   while(*ret++ !=NULL) count++;
   
   return(starting_location=_index-count);
//!   if(count == _COUNT)  return(starting_location=index-1-count);
//!   else return(0);
}



/*==================================================================================*/
/***
   Funtion     :  void send_normal_data()
   Parameters  :  None.
   Return value:  None.
   Purpose     :  This function send normal data to GSM Modem.
***/   
/*==================================================================================*/
void send_normal_data()
{
   /* NOTE: If need to add more DATA parameters, then add those parameters at the end not 
            in the starting or middle because at the receiving end it will disturb the
            database. Also the database have to be update in order to receive and store
            new changes.*/
   fprintf(PC_PORT,"D,");                    // 1        /*Indication for Data Stream*/
   fprintf(PC_PORT,"%s,",date);              // 2
   fprintf(PC_PORT,"%s,",time);              // 3
   fprintf(PC_PORT,"num,");                  // 4
   fprintf(PC_PORT,"%u,",gen_status);        // 5
   fprintf(PC_PORT,"%lu,",g_l1_v);           // 6
   fprintf(PC_PORT,"%lu,",g_l2_v);           // 7
   fprintf(PC_PORT,"%lu,",g_l3_v);           // 8
   fprintf(PC_PORT,"%lu,",g_l1_i);           // 9
   fprintf(PC_PORT,"%lu,",g_l2_i);           // 10
   fprintf(PC_PORT,"%lu,",g_l3_i);           // 11
   fprintf(PC_PORT,"%lu,",g_kwh);            // 12
   fprintf(PC_PORT,"%lu,",amb_temp);         // 13
   fprintf(PC_PORT,"%ld,",coolant_temp);     // 14
   fprintf(PC_PORT,"%lu,",engine_speed);     // 15
   fprintf(PC_PORT,"%lu,",maintenance_due);  // 16
   fprintf(PC_PORT,"%lu,",battery_voltage);  // 17
   fprintf(PC_PORT,"%lu,",engine_run_time);  // 18
   fprintf(PC_PORT,"%lu,",no_of_starts);     // 19
   fprintf(PC_PORT,"%lu,",fuel_level);       // 20
   fprintf(PC_PORT,"100,");                  // 21      /*Dummy Fuel theft in Liters*/
   fprintf(PC_PORT,"%lu,",g_freq);           // 22
   fprintf(PC_PORT,"%lu,",oil_pressure);     // 23
   fprintf(PC_PORT,"100");                   // 24      /*Dummy oil temp value*/
//!   fprintf(PC_PORT,"%ld",oil_temp);          // 24 

}



/*==================================================================================*/
/***
   Funtion     :  void send_alarms_data()
   Parameters  :  None.
   Return value:  None.
   Purpose     :  This function send alarm data to GSM Modem.
***/   
/*==================================================================================*/
void send_alarms_data()
{
   /* NOTE: If need to add more ALARM parameters, then add those parameters at the end not 
            in the starting or middle because at the receiving end it will disturb the
            database. Also the database have to be update in order to receive and store
            new changes.*/
   fprintf(PC_PORT,"A,");                                // 1  /*Indication for Alarm Stream*/
   fprintf(PC_PORT,"%s,",date);                          // 2
   fprintf(PC_PORT,"%s,",time);                          // 3
   fprintf(PC_PORT,"num,");                              // 4
   fprintf(PC_PORT,"%u,",alarm1.emergency_stop);         // 5
   fprintf(PC_PORT,"%u,",alarm1.fail_to_start);          // 6
   fprintf(PC_PORT,"%u,",alarm1.fail_to_come_to_rest);   // 7
   fprintf(PC_PORT,"%u,",alarm1.gen_low_freq);           // 8
   fprintf(PC_PORT,"%u,",alarm1.gen_high_voltage);       // 9
   fprintf(PC_PORT,"%u,",alarm1.gen_low_voltage);        // 10
   fprintf(PC_PORT,"%u,",alarm1.gen_reverse_power);      // 11
   fprintf(PC_PORT,"%u,",alarm1.gen_earth_fault);        // 12
   fprintf(PC_PORT,"%u,",alarm1.gen_high_current);       // 13
   fprintf(PC_PORT,"%u,",alarm1.gen_high_freq);          // 14
   fprintf(PC_PORT,"%u,",alarm1.low_battery_volt);       // 15
   fprintf(PC_PORT,"%u,",alarm1.high_battery_volt);      // 16
   fprintf(PC_PORT,"%u,",alarm1.gen_door_status);        // 17
   fprintf(PC_PORT,"%u,",alarm1.fuel_theft);             // 18
   fprintf(PC_PORT,"%u,",alarm1.fuel_level_low);         // 19
   fprintf(PC_PORT,"%u,",alarm1.fuel_level_high);        // 20
   fprintf(PC_PORT,"%u,",alarm1.high_cool_temp);         // 21
   fprintf(PC_PORT,"%u,",alarm1.over_speed);             // 22
   fprintf(PC_PORT,"%u,",alarm1.under_speed);            // 23
   fprintf(PC_PORT,"%u,",alarm1.fuel_tank_lid);          // 24
   fprintf(PC_PORT,"%u,", alarm1.low_oil_pressure);      // 25
   fprintf(PC_PORT,"%u", alarm1.high_oil_temp);          // 26
//!   fprintf(PC_PORT,"1,");                                // 25 /*Dummy Oil pressure */
//!   fprintf(PC_PORT,"1");                                 // 26 /*Dummy high Oil temp*/
}
