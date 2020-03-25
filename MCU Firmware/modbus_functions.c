
//*****************************************************************************
//**************************MODBUS RELATED FUNCTIONS***************************
//*****************************************************************************
void read_holding_regs(int mbslave_address, long start_address,long length_of_data)
{
   if(!(modbus_read_holding_registers(mbslave_address,start_address,
         length_of_data)))
   {                                                        
//!      //started at 1 since 0 is quantity of coils/
//!      for(i=1; i < (modbus_rx.len); ++i)               
//!      fprintf(pc,"%u ", modbus_rx.data[i]);                         
//!      fprintf(pc,"\r\n\r\n");
//!      read_register_exception=FALSE;
      modbus_data_read = TRUE;
   }
   else
   {                                                            
//!      fprintf(PC_PORT,"<-**Exception %x**->\r\n\r\n", modbus_rx.error);
      modbus_data_read = FALSE;
      output_high(PIN_B4); output_low(PIN_B4);
//!      read_register_exception=TRUE;   
   }
}



/*==================================================================================*/
/***
   Funtion     :  void check_alarms(long start_address,long length_of_data)
   Parameters  :  start_address, length_of_data
   Return value:  None
   Purpose     :  This function gets the data of particular alarm register.
***/   
/*==================================================================================*/
void check_alarms(long start_address,long length_of_data)
{
   if(!(modbus_read_holding_registers(MODBUS_SLAVE_DSE,start_address,
         length_of_data)))
   {                                                        
      output_high(PIN_B5);output_low(PIN_B5);   // for testing purpose only.
   }                                                 
   else
   {                                                            
      output_high(PIN_B3); output_low(PIN_B3);  // for testing purpose only.
      alarm_exception=TRUE;
   }
}

int1 read_coils(long start_address,long quantity)
{
   if(!(modbus_read_coils(MODBUS_SLAVE_PIC,start_address,quantity)))
   {
      /*Started at 1 since 0 is quantity of coils*/
//!      for(i=1; i < (modbus_rx.len); ++i)
      if(modbus_rx.data[1]) return(1);
      else return(0);
   }
   else
   {
   }
}

void read_input_regs(long start_address,long length_of_data)
{
   if(!(modbus_read_input_registers(MODBUS_SLAVE_PIC,start_address,length_of_data)))
   {
      /*Started at 1 since 0 is quantity of coils*/
//!      for(i=1; i < (modbus_rx.len); ++i)
      error_read_input_regs = FALSE;
      output_high(PIN_C3);output_low(PIN_C3);
   }
   else 
   {  
      error_read_input_regs = TRUE;
      output_high(PIN_B1);output_low(PIN_B1);
//!      fprintf(PC_PORT,"<-**READ INPUT Exception %X**->\r\n\r\n", modbus_rx.error);
   }
}



/*==================================================================================*/
/***
   Funtion     :  void get_data_from_deepsea()
   Parameters  :  None
   Return value:  None
   Purpose     :  This function gets the parameters value.
***/   
/*==================================================================================*/
void get_data_from_deepsea()
{
   get_date_and_time(rcv_ptr);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1024,1);
   oil_pressure = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1025,1);
   coolant_temp = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1026,1);
   oil_temp = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1029,1);
   battery_voltage = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1030,1);
   engine_speed = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1031,1);
   g_freq = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1032,2);
   g_l1_v = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1034,2);
   g_l2_v = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
                   
   read_holding_regs(MODBUS_SLAVE_DSE,1036,2);
   g_l3_v = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1044,2);
   g_l1_i = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
   
   read_holding_regs(MODBUS_SLAVE_DSE,1046,2);
   g_l2_i = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
                   
   read_holding_regs(MODBUS_SLAVE_DSE,1048,2);
   g_l3_i = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
                   
   read_holding_regs(MODBUS_SLAVE_DSE,1794,2);
   maintenance_due = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
                                      
   read_holding_regs(MODBUS_SLAVE_DSE,1798,2);
   engine_run_time = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
                                      
   read_holding_regs(MODBUS_SLAVE_DSE,1800,2);
   g_kwh = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);
                
   read_holding_regs(MODBUS_SLAVE_DSE,1808,2);
   no_of_starts = make32(modbus_rx.data[1],modbus_rx.data[2],
                                      modbus_rx.data[3],modbus_rx.data[4]);

   get_modbus_data=FALSE;
   send_sms_report=TRUE;
}



/*==================================================================================*/
/***
   Funtion     :  void read_alarm_status()
   Parameters  :  None
   Return value:  None
   Purpose     :  This function reads the status of the alarm conditions.
***/   
/*==================================================================================*/
void read_alarm_status()
{
   output_high(LED_4);
   int16 alarm_reg_1, alarm_reg_2, alarm_reg_3, alarm_reg_4, alarm_reg_5;
   
   alarm_exception=FALSE;
   get_date_and_time(rcv_ptr);         // get current date and time.
   
   check_alarms(2049,1);
   alarm_reg_1 = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   check_alarms(2050,1);
   alarm_reg_2 = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   check_alarms(2051,1);
   alarm_reg_3 = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   check_alarms(2052,1);
   alarm_reg_4 = make16(modbus_rx.data[1],modbus_rx.data[2]);
   
   check_alarms(2055,1);
   alarm_reg_5 = make16(modbus_rx.data[1],modbus_rx.data[2]);     
   
   if(!alarm_exception)       // if no exception in reading above parameters from 
   {                          // DSE panel then check the alarm conditions.
      alarm2.high_oil_temp       = NIBBLE_0(alarm_reg_1);
      alarm2.high_cool_temp      = NIBBLE_1(alarm_reg_1);
      alarm2.low_oil_pressure    = NIBBLE_2(alarm_reg_1);
      alarm2.emergency_stop      = NIBBLE_3(alarm_reg_1);
            
      
      alarm2.fail_to_come_to_rest= NIBBLE_0(alarm_reg_2);
      alarm2.fail_to_start       = NIBBLE_1(alarm_reg_2);
      alarm2.over_speed          = NIBBLE_2(alarm_reg_2);
      alarm2.under_speed         = NIBBLE_3(alarm_reg_2);
      
      alarm2.gen_low_freq        = NIBBLE_0(alarm_reg_3);
      alarm2.gen_high_voltage    = NIBBLE_1(alarm_reg_3);
      alarm2.gen_low_voltage     = NIBBLE_2(alarm_reg_3);
      
      alarm2.gen_reverse_power   = NIBBLE_0(alarm_reg_4);
      alarm2.gen_earth_fault     = NIBBLE_1(alarm_reg_4);
      alarm2.gen_high_current    = NIBBLE_2(alarm_reg_4);
      alarm2.gen_high_freq       = NIBBLE_3(alarm_reg_4);
      
      alarm2.high_battery_volt   = NIBBLE_2(alarm_reg_5);
      alarm2.low_battery_volt    = NIBBLE_3(alarm_reg_5);
      
      
      if(!gen_door_state)        alarm2.gen_door_status=2;
      else                       alarm2.gen_door_status=1;
      
      if(!fuel_tank_lid_state)   alarm2.fuel_tank_lid=2;
      else                       alarm2.fuel_tank_lid=1;
      
      /*-------------------------------------------------------------------High Oil Temp*/
      if(alarm1.high_oil_temp != alarm2.high_oil_temp)
      {
         alarm_occured = TRUE;   alarm1.high_oil_temp = alarm2.high_oil_temp;
      }
//!      else if(alarm2.high_oil_temp > NORMAL_CONDITION &&
//!              alarm2.high_oil_temp < UNIMPLEMENTED_ALARM )  repeat_alarm = TRUE;
      
      /*------------------------------------------------------------------High_Cool_Temp*/
      if(alarm1.high_cool_temp != alarm2.high_cool_temp)
      {
         alarm_occured = TRUE;   alarm1.high_cool_temp = alarm2.high_cool_temp;
      }
//!      else if(alarm2.high_cool_temp > NORMAL_CONDITION &&
//!              alarm2.high_cool_temp < UNIMPLEMENTED_ALARM ) repeat_alarm = TRUE;
      
      /*----------------------------------------------------------------Low Oil Pressure*/
      if(alarm1.low_oil_pressure != alarm2.low_oil_pressure)            
      {
         alarm_occured = TRUE;   alarm1.low_oil_pressure = alarm2.low_oil_pressure;   
      }
//!      else if(alarm2.low_oil_pressure > NORMAL_CONDITION &&
//!              alarm2.low_oil_pressure < UNIMPLEMENTED_ALARM) repeat_alarm =  TRUE;
              
      /*------------------------------------------------------------------Emergency_stop*/        
      if( alarm1.emergency_stop != alarm2.emergency_stop )
      { 
         alarm_occured = TRUE;   alarm1.emergency_stop = alarm2.emergency_stop;
      }
//!      else if(alarm2.emergency_stop > NORMAL_CONDITION &&
//!              alarm2.emergency_stop < UNIMPLEMENTED_ALARM)  repeat_alarm = TRUE;

      /*------------------------------------------------------------Fail_to_come_to_rest*/
      if(alarm1.fail_to_come_to_rest != alarm2.fail_to_come_to_rest)
      {
         alarm_occured = TRUE; alarm1.fail_to_come_to_rest = alarm2.fail_to_come_to_rest;
      }
//!      else if(alarm2.fail_to_come_to_rest > NORMAL_CONDITION &&
//!              alarm2.fail_to_come_to_rest < UNIMPLEMENTED_ALARM)  repeat_alarm = TRUE;
              
      /*-------------------------------------------------------------------Fail_to_start*/
      if(alarm1.fail_to_start != alarm2.fail_to_start)
      {
         alarm_occured = TRUE;   alarm1.fail_to_start = alarm2.fail_to_start;
      }
//!      else if(alarm2.fail_to_start > NORMAL_CONDITION &&
//!              alarm2.fail_to_start < UNIMPLEMENTED_ALARM)   repeat_alarm = TRUE;
              
      /*----------------------------------------------------------------------Over_speed*/
      if(alarm1.over_speed != alarm2.over_speed)
      {
         alarm_occured = TRUE;   alarm1.over_speed = alarm2.over_speed;
      }
//!      else if(alarm2.over_speed > NORMAL_CONDITION &&
//!              alarm2.over_speed < UNIMPLEMENTED_ALARM)   repeat_alarm = TRUE;
              
      /*---------------------------------------------------------------------Under_speed*/
      if(alarm1.under_speed != alarm2.under_speed)
      {
         alarm_occured = TRUE;   alarm1.under_speed = alarm2.under_speed;
      }
//!      else if(alarm2.under_speed > NORMAL_CONDITION &&
//!              alarm2.under_speed < UNIMPLEMENTED_ALARM)  repeat_alarm = TRUE;
              
      /*--------------------------------------------------------------------Gen_low_freq*/
      if(alarm1.gen_low_freq != alarm2.gen_low_freq)
      {
         alarm_occured = TRUE;   alarm1.gen_low_freq = alarm2.gen_low_freq;
      }
//!      else if(alarm2.gen_low_freq > NORMAL_CONDITION &&
//!              alarm2.gen_low_freq < UNIMPLEMENTED_ALARM) repeat_alarm = TRUE;
              
      /*-------------------------------------------------------------------Gen_high_freq*/
      if(alarm1.gen_high_freq!=alarm2.gen_high_freq)
      {
         alarm_occured = TRUE;   alarm1.gen_high_freq = alarm2.gen_high_freq;
      }
//!      else if(alarm2.gen_high_freq > NORMAL_CONDITION &&
//!              alarm2.gen_high_freq < UNIMPLEMENTED_ALARM)   repeat_alarm = TRUE;
              
      /*----------------------------------------------------------------Gen_high_voltage*/
      if(alarm1.gen_high_voltage != alarm2.gen_high_voltage)
      {
         alarm_occured = TRUE;   alarm1.gen_high_voltage = alarm2.gen_high_voltage;
      }
//!      else if(alarm2.gen_high_voltage > NORMAL_CONDITION &&
//!              alarm2.gen_high_voltage < UNIMPLEMENTED_ALARM)   repeat_alarm = TRUE;
              
      /*-----------------------------------------------------------------Gen_low_voltage*/
      if(alarm1.gen_low_voltage != alarm2.gen_low_voltage)
      {
         alarm_occured = TRUE;   alarm1.gen_low_voltage = alarm2.gen_low_voltage;
      }
//!      else if(alarm2.gen_low_voltage > NORMAL_CONDITION &&
//!              alarm2.gen_low_voltage < UNIMPLEMENTED_ALARM)    repeat_alarm = TRUE;
              
      /*---------------------------------------------------------------Gen_reverse_power*/
      if(alarm1.gen_reverse_power != alarm2.gen_reverse_power)
      {
         alarm_occured = TRUE;   alarm1.gen_reverse_power = alarm2.gen_reverse_power;
      }
//!      else if(alarm2.gen_reverse_power > NORMAL_CONDITION &&
//!              alarm2.gen_reverse_power < UNIMPLEMENTED_ALARM)  repeat_alarm = TRUE;
              
      /*-----------------------------------------------------------------Gen_earth_fault*/
      if(alarm1.gen_earth_fault != alarm2.gen_earth_fault)
      {
         alarm_occured = TRUE;   alarm1.gen_earth_fault = alarm2.gen_earth_fault;
      }
//!      else if(alarm2.gen_earth_fault > NORMAL_CONDITION &&
//!              alarm2.gen_earth_fault < UNIMPLEMENTED_ALARM)    repeat_alarm = TRUE;
              
      /*----------------------------------------------------------------Gen_high_current*/
      if(alarm1.gen_high_current != alarm2.gen_high_current)
      {
         alarm_occured = TRUE;   alarm1.gen_high_current = alarm2.gen_high_current;
      }
//!      else if(alarm2.gen_high_current > NORMAL_CONDITION &&
//!              alarm2.gen_high_current < UNIMPLEMENTED_ALARM)   repeat_alarm = TRUE;
              
      /*---------------------------------------------------------------High_battery_volt*/
      if(alarm1.high_battery_volt != alarm2.high_battery_volt)
      {
         alarm_occured = TRUE;   alarm1.high_battery_volt = alarm2.high_battery_volt;
      }
      else if(alarm2.high_battery_volt > NORMAL_CONDITION &&
              alarm2.high_battery_volt < UNIMPLEMENTED_ALARM)  repeat_alarm = TRUE;
              
      /*----------------------------------------------------------------Low_battery_volt*/
      if(alarm1.low_battery_volt != alarm2.low_battery_volt)
      {
         alarm_occured = TRUE;   alarm1.low_battery_volt = alarm2.low_battery_volt;
      }
      else if(alarm2.low_battery_volt > NORMAL_CONDITION &&
              alarm2.low_battery_volt < UNIMPLEMENTED_ALARM)   repeat_alarm = TRUE;
              
      /*-----------------------------------------------------------------Gen_door_status*/
      if(alarm1.gen_door_status != alarm2.gen_door_status)
      {
         alarm_occured = TRUE;   alarm1.gen_door_status = alarm2.gen_door_status;
      }
      else if(alarm2.gen_door_status > NORMAL_CONDITION &&
              alarm2.gen_door_status < UNIMPLEMENTED_ALARM) repeat_alarm=TRUE;
      
      /*-------------------------------------------------------------------Fuel_tank_lid*/
      if(alarm1.fuel_tank_lid != alarm2.fuel_tank_lid)
      {
         alarm_occured = TRUE;   alarm1.fuel_tank_lid = alarm2.fuel_tank_lid;
      }
      else if(alarm2.fuel_tank_lid > NORMAL_CONDITION &&
              alarm2.fuel_tank_lid < UNIMPLEMENTED_ALARM)   repeat_alarm = TRUE;
      
      /*-----------------------------------------------------------------Fuel Level High*/
      if(alarm1.fuel_level_high != alarm2.fuel_level_high)
      {
         alarm_occured = TRUE;   alarm1.fuel_level_high = alarm2.fuel_level_high;
      }
      
      /*------------------------------------------------------------------Fuel Level Low*/
      if(alarm1.fuel_level_low != alarm2.fuel_level_low)
      {
         alarm_occured = TRUE;   alarm1.fuel_level_low = alarm2.fuel_level_low;
      }
   }
   
   if(repeat_alarm && !time_to_send_alarm_conditions)
   {
      repeat_alarm = FALSE;
      alarm_occured = TRUE;
      time_to_send_alarm_conditions = _TIME_TO_SEND_ALARM_CONDITIONS;
   }
   
   if(alarm_occured)                // if any of the above listed alarm occured
   {
      get_date_and_time(rcv_ptr);   // get current date and time.

      alarm_occured=FALSE;
      alarm_counter=MAX_ALARM_COUNT; 
   }
   
//!   if(repeat_alarm && !time_to_send_alarm_conditions)
//!   {
//!      repeat_alarm = FALSE;
//!      alarm_occured = TRUE;
//!      time_to_send_alarm_conditions = _TIME_TO_SEND_ALARM_CONDITIONS;
//!   }
   time_to_check_alarm = _TIME_TO_CHECK_FOR_ALARM_TICKS;
   output_low(LED_4);
}



/*==================================================================================*/
/***
   Funtion     :  void check_gen_status()
   Parameters  :  None
   Return value:  None
   Purpose     :  This function checks if the generator is ON/OFF by seeing the freq
                  parameter. If freq is > 20Hz, it mean's generator is ON. 20Hz value 
                  is taken randomly. value other than 20 can be selected.
***/   
/*==================================================================================*/
void check_gen_status()
{  
   static int1 one_time = TRUE;
   
   output_high(PIN_B2);output_low(PIN_B2);
   modbus_data_read=FALSE;
   read_holding_regs(MODBUS_SLAVE_DSE,1031,1);
   
   if(modbus_data_read)          // This check is to ensure that data is received by
   {                             // modbus without any exception
      g_freq = make16(modbus_rx.data[1],modbus_rx.data[2]);

      // This is used to monitor if the generator is ON/OFF.
                                             // if generator freq is greater than 20 Hz.
      if(g_freq >= 200)    gen_status = TRUE;     // generator is ON.
      else 
      {
         gen_status = FALSE;           // generator is OFF.
         one_time = TRUE;
         five_min_loop = _FIVE_MIN_LOOP;
         time_to_send_data = _TIME_TO_SEND_DATA_TICKS_1;
      }
      
      if(gen_status && one_time) // This check msg only one time when generator TURN ON.
      {
         one_time = FALSE;
         gen_turn_on=TRUE;
         get_date_and_time(0);   // get current date and time.
//!         send_sms();
      }
   }
   time_to_check_gen_status = _TIME_TO_CHECK_GEN_STATUS;
}



/*==================================================================================*/
/***
   Funtion     :  void send_data_to_server()
   Parameters  :  None
   Return value:  None
   Purpose     :  This function sends the data to server. When generator turn's ON, data 
                  will be send to server every min for defined loop (#define_FIVE_MIN_LOOP)
                  then send data to server according to defined time (time_to_send_data_2).
***/   
/*==================================================================================*/
void send_data_to_server(void)
{
   if(five_min_loop)
   {
      if(!time_to_send_data)
      {
         get_data_from_deepsea();
         normal_data=TRUE;
//!         send_sms();
         five_min_loop--;
         time_to_send_data = _TIME_TO_SEND_DATA_TICKS_1;
      }
   }
   else
   {
      if(!time_to_send_data)
      {
         get_data_from_deepsea();
         normal_data=TRUE;
//!         send_sms();
         time_to_send_data = time_to_send_data_2;
      }
   }
}
