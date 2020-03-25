/*==================================================================================*/
/***
   Funtion     :  void modem_initialization()
   Parameters  :  None
   Return value:  None
   Purpose     :  This function do several tasks like 
                  [] Check modem status ON/OFF
                  [] Check Communication 
                  [] Check SIM Presence
                  [] Check SIM Network etc..more funciton can be added.
***/   
/*==================================================================================*/
void modem_initialization()
{
   if(!powered_on_flag)
   {
      if(gsm_power_down()==MODULE_OK)   powered_off_flag=TRUE;
      reset_buffers();
   }
   
   
   if(powered_off_flag)
   {
      if(gsm_power_up()==MODULE_OK)  { powered_on_flag=TRUE;
                                       powered_off_flag=FALSE; }
      reset_buffers();
   }
   
   
   if(check_auto_date_time)
   {
      
   }
   if(check_comm_flag)              // This flag will be TRUE in gsm_power_up() function.
   {
      if(check_comm()==MODULE_OK)   // If communication established then check sim status
      {                             // in next step.
         check_comm_flag=FALSE;
         check_sim_status=TRUE;
      }
      else modem_init_timeout = _MODEM_INIT_TIMEOUT_TICKS;
      reset_buffers();
   }
   
   
   if(check_sim_status)
   {
      if(check_sim()==MODULE_OK)    // If sim is detected then check the sim network in     
      {                             // next step.
         check_sim_status=FALSE;
         check_sim_network=TRUE;
      }
      else modem_init_timeout = _MODEM_INIT_TIMEOUT_TICKS;
      reset_buffers();
   }
   
   
   if(check_sim_network)
   {
      if(check_network()==MODULE_OK)   // If sim network is detected then set/reset some   
      {                                // variables and deactivate the 
                                       // modem_initialization() function.
         check_sim_network=FALSE;
         gprs_conn_status=TRUE;
         modem_init_status=FALSE;
         modem_init_ok=TRUE;
      }
      else modem_init_timeout = _MODEM_INIT_TIMEOUT_TICKS;
      reset_buffers();
   }
}



/*==================================================================================*/
/***
   Funtion     :  int check_comm()
   Parameters  :  None
   Return value:  response
   Purpose     :  This function checks if the communication between PIC and Modem is
                  Established or not. AT will be send to the Modem and save its response
                  in a 'response' variable. This response will be returned.
***/   
/*==================================================================================*/
int check_comm()
{
   int response;
   fprintf(PC_PORT,"AT\r");                        // send AT to GSM Modem
   response=get_rsp(2,ok_rsp,5000,MODULE_ERROR);   // get repsonse of the modem.
   reset_buffers();
   return(response);
}



/*==================================================================================*/
/***
   Funtion     :  int gsm_power_up()
   Parameters  :  None
   Return value:  0 or 1.
   Purpose     :  This function will turn the Modem ON and capture the initial stream.
                  It will look for the stream "call ready". If it is captured then the 
                  retrun value will be "TRUE" otherwise "FALSE".
***/   
/*==================================================================================*/
int gsm_power_up()
{
   output_low(PIN_C2);  delay_ms(20);
   output_high(PIN_C2); delay_ms(2000);   // Min delay of 2 sec is required to turn the
                                          // modem ON.
   output_low(PIN_C2);  delay_ms(20);
   
   
   if(get_rsp(8,call_ready,10000,MODULE_TIMEOUT)==MODULE_OK)
   {
//!      check_auto_date_time=TRUE;
      check_comm_flag=TRUE;   return(TRUE);
   }
   else  return(FALSE);
}


int gsm_power_down()//--------------------------------------------------------------
{
   output_low(PIN_C2);  delay_ms(20);
   output_high(PIN_C2); delay_ms(2000);
   output_low(PIN_C2);  delay_ms(20);
   
   if(get_rsp(2,power_down,10000,MODULE_TIMEOUT)==MODULE_OK) return(TRUE);
   else  return(FALSE);
}


int check_sim()//-------------------------------------------------------------------
{
   reset_buffers();
   fprintf(PC_PORT,"AT+CSMINS?\r");
   if(get_rsp(4,sim_status_rsp,5000,MODULE_TIMEOUT)==MODULE_OK)
   {
      if(gsm_buffer[13]=='1') return(TRUE);
      else return(FALSE);
   }  
}


int check_network()//---------------------------------------------------------------
{
   reset_buffers();
   fprintf(PC_PORT,"AT+COPS?\r");//AT command to check SIM Network
   if(get_rsp(4,cops_rsp,7000,MODULE_TIMEOUT)==MODULE_OK)
   {
      if(strstr(gsm_buffer,ufone_sim)!=NULL)
      {
         network_operator=UFONE; return(TRUE);
      }
      else if(strstr(gsm_buffer,telenor_sim)!=NULL)
      {
         network_operator=TELENOR; return(TRUE);
      }
      else return(FALSE);
   }
}
