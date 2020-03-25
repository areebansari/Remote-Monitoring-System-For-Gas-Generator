#include <18f26k22.h>
#device PASS_STRINGS=IN_RAM
#fuses HSH,PUT,NOWDT,NOBROWNOUT                     
#use delay(crystal=20M)
//!#use delay(xtal=20M, clock=80M)
#use RS232(baud=38400,   UART2,   parity=N,   stream=PC_PORT)
#use RS232(baud=38400,   UART1,   parity=N,   stream=MODBUS_PORT)

//-----------------------------MODBUS DEFINES
#define MODBUS_SLAVE_DSE               0x08
#define MODBUS_SLAVE_PIC               0x02
#define MODBUS_TYPE                    MODBUS_TYPE_MASTER
#define MODBUS_SERIAL_RX_BUFFER_SIZE   128
#define MODBUS_SERIAL_BAUD             38400
#define MODBUS_SERIAL_TIMEOUT          500000
#define MODBUS_SERIAL_INT_SOURCE       MODBUS_INT_RDA
#define MODBUS_SERIAL_ENABLE_PIN       PIN_C5

//-----------------------------Misc DEFINES
#define  _MODEM_INIT_TIMEOUT_TICKS   2    // in seconds
#define  _GPRS_FUNCTION_HALT_TICKS   60   // in seconds
#define  _MAX_RETRIES   5

#define  _TIME_TO_CHECK_FOR_ALARM_TICKS   6  // in seconds
#define  _TIME_TO_READ_PARAMETERS_TICKS   10 // in seconds
#define  _TIME_TO_SEND_PARAMETERS_TICKS   50 // in seconds
#define  _TIME_TO_CHECK_SMS_MEMORY        15 // in seconds
#define  _TIME_TO_GET_FUEL_LEVEL          3  // in seconds
#define  _TIME_TO_CHECK_FUEL_THEFT        4  // in seconds
#define  _TIME_TO_CHECK_GEN_STATUS        2  // in seconds
#define  _TIME_TO_SEND_ALARM_CONDITIONS   14 // in seconds  (alarm alert = time req-6) e.g alarm alert = 20-6 = 14
#define  _FIVE_MIN_LOOP                   5  // in seconds
#define  _TIME_TO_SEND_DATA_TICKS_1       60 // in seconds (After 1 min send data report)
//!#define  _TIME_TO_SEND_DATA_TICKS_2       600// in seconds (After 10min send data report)


#define  _APPEND_NULL_CHARACTER  {index++; gsm_buffer[index]='\0';}

#define TO_INT(C)   (C-'0')

#define  MODULE_TIMEOUT     0 
#define  MODULE_OK          1
#define  MODULE_ERROR       2 
#define  MODULE_CME_ERROR   3

#define  NETWORK_NOT_DETECTED 0
#define  UFONE                1
#define  TELENOR              2

#define  GSM_GPRS_BUFFER_SIZE 255

#define  MINUTE_ARRAY_SIZE 10

#define  NORMAL_CONDITION        1
#define  WARNING_ALARM           2
#define  SHUTDOWN_ALARM          3
#define  ELECTRICAL_TRIP_ALARM   4
#define  UNIMPLEMENTED_ALARM     15

#define  NON          0
#define  HOST         1
#define  CLIENT_NO_1  2

#define  MAX_ALARM_COUNT   5
//!#define ENABLE_ALARM_CHECKING

//!#define DISPLAY_FUEL_ARRAY
//!#define DISPLAY_INPUT_BUFFER_ARRAY
//!#define DISPLAY_SORTED_ARRAY
//!#define DISPLAY_SAMPLES_ARRAY
//!#define DISPLAY_DEBUG_PARAMETERS

//!#define DEBUG_ENABLED

//!#define TESTING_MODBUS

#define FUEL_ARRAY_SIZE 30    // fuel samples history

#define MEDIAN_FILTER_WIDTH  19  // size of array for storing values for taking median

#define gen_door_state        input(PIN_C0)
#define fuel_tank_lid_state   input(PIN_C1)

#define LED_1  PIN_A0
#define LED_2  PIN_A1
#define LED_3  PIN_A2
#define LED_4  PIN_A3
#define LED_5  PIN_A4
//-----------------------------Include Files
#include "modbus_hay.c"
#include <string.h>
#include <filters.c>

//-----------------------------Macros
#define NIBBLE_0(R)  (make8(R,0) & 0x0F)
#define NIBBLE_1(R)  ((make8(R,0) & 0xF0) >> 4)
#define NIBBLE_2(R)  (make8(R,1) & 0x0F)
#define NIBBLE_3(R)  ((make8(R,1) & 0xF0) >> 4)

//----------------------------Error Msgs

#if _TIME_TO_CHECK_FOR_ALARM_TICKS >= _TIME_TO_READ_PARAMETERS_TICKS
   #error "_TIME_TO_CHECK_FOR_ALARM_TICKS" should be less than\
   "_TIME_TO_READ_PARAMETERS_TICKS"
#endif

#if _TIME_TO_READ_PARAMETERS_TICKS >= _TIME_TO_SEND_PARAMETERS_TICKS
   #error "_TIME_TO_READ_PARAMETERS_TICKS" should be less than \
            "_TIME_TO_SEND_PARAMETERS_TICKS"
#endif

#if MINUTE_ARRAY_SIZE%5
   #error "MINUTE_ARRAY_SIZE" Must be a multiple of 5
#endif

//----------------------------Variables

//!char  HOST_NO_1[]="+923312107273";  // Raees number.  (Ufone)
//!char  HOST_NO_2[]="+923312107273";  // Raees number.  (Ufone)

char  HOST_NO_1[]="+923332383079";  // Abdul hayee number.  (Ufone)
//!char  HOST_NO_2[]="+923332383079";  // Abdul hayee number.  (Ufone)

//!char  HOST_NO_2[]="+923312107273";  // Raees number.        (Ufone)

//!char  CLIENT_1[]="+923123456789";   // AEI no 1.            (Ufone)
//!char CLIENT 2[]="+923322683401"     // AEI no 2.            (Ufone)
char  temp_array[]="xxxxxxxxxxxxx";

//!char  HOST_NO_2[]="+923312551725";  // Polling Number (Given by Sir Pervaiz).
//!char  HOST_NO_1[]="+923363111563";  // Receiver Number (Given by Sir Pervaiz).

//!char  HOST_NO_2[]="+923363111563";  // Polling Number (Given by Sir Pervaiz).
//!char  HOST_NO_1[]="+923324180194";  // Receiver Number (Given by Sir Pervaiz).

//!volatile int   time_to_read_parameters    = _TIME_TO_READ_PARAMETERS_TICKS;
//!volatile int   time_to_send_parameters    = _TIME_TO_SEND_PARAMETERS_TICKS;
volatile int   time_to_check_alarm        = _TIME_TO_CHECK_FOR_ALARM_TICKS;
volatile int   modem_init_timeout         = _MODEM_INIT_TIMEOUT_TICKS;
volatile int   time_to_check_sms_memory   = _TIME_TO_CHECK_SMS_MEMORY;
volatile int   get_fuel_level             = _TIME_TO_GET_FUEL_LEVEL;
volatile int   check_fuel_theft           = _TIME_TO_CHECK_FUEL_THEFT;
volatile int   delta_timeout = 0;
volatile int   time_to_check_gen_status   = _TIME_TO_CHECK_GEN_STATUS;
volatile int   time_to_send_alarm_conditions = _TIME_TO_SEND_ALARM_CONDITIONS;
volatile int16 time_to_send_data          = _TIME_TO_SEND_DATA_TICKS_1;
volatile int16 time_to_send_data_2=600;
//!volatile int   gprs_function_halt = 0;

int1  alarm_occured = FALSE, normal_data = FALSE, 
      get_modbus_data = FALSE, system_power_up = FALSE,
      modem_init_ok = FALSE, theft_check_enable = FALSE, fuel_averaging = TRUE, 
      error_read_input_regs = FALSE, modbus_data_read = FALSE, repeat_alarm = FALSE;
      
int   alarm_counter=0, rcv_ptr=0, delta_count=0;

int32 low_fuel_threshold = 40000;    // four digits from right are fractional part e.g. 
                                 //   40000 means 4.0000

int32 high_fuel_threshold = 980000;  // four digits from right are fractional part
                                 //   980000 means 98.0000

int32 integer_delta_off = 1454;     // corresponds to 0.1454 % in real world

int32 integer_delta_on = 2000;      // corresponds to 0.2000 % in real world

int32 fuel_array[FUEL_ARRAY_SIZE];

signed int32 fuel_delta, signed_delta_off, signed_delta_on;

//!char  date_and_time[18];
char date[9],time[9];
int1  gen_status=0;
int16 g_freq=0;
int32 g_l1_v=0;
int32 g_l2_v=0;
int32 g_l3_v=0;
int32 g_l1_i=0;
int32 g_l2_i=0;
int32 g_l3_i=0;
int32 g_kwh=0;
int16 amb_temp=0;
//!int16 cool_temp=0;
int16 engine_speed=0;
int32 maintenance_due=0;
int16 battery_voltage=0;
int32 engine_run_time=0;
int32 no_of_starts=0;
int32 fuel_level=0; 

int16 oil_pressure=0;

signed int16 coolant_temp=0;
signed int16 oil_temp=0;

struct _parameters
{
   int   gen_door_status      : 4;
   int   fuel_tank_lid        : 4;
   
   int   low_battery_volt     : 4;
   int   high_battery_volt    : 4;
   int   emergency_stop       : 4;
   int   under_speed          : 4;
   int   over_speed           : 4;
   int   high_cool_temp       : 4;
   
   int   fail_to_start        : 4;
   int   fail_to_come_to_rest : 4;
   int   gen_low_voltage      : 4;
   int   gen_high_voltage     : 4;
   int   gen_low_freq         : 4;
   int   gen_high_freq        : 4;
   int   gen_high_current     : 4;
   int   gen_earth_fault      : 4;
   int   gen_reverse_power    : 4;
   int   fuel_theft           : 4;
   int   fuel_level_low       : 4;
   int   fuel_level_high      : 4;
   
   int   low_oil_pressure     : 4;
   int   high_oil_temp        : 4;
   
} alarm1,alarm2;

//#define clock_string     minute[rcv_index].date_and_time

union modbus_u
{
   int i8_val[4];
   int16 i16_val[2];
   int32 i32_val;
} percent_fuel_new;

//--------------------------gsm/gprs modem variables---------------------------
volatile int   index=0,   new_line_char_rcv=0, counter=0;
volatile long  response_timeout=0;
volatile char  gsm_buffer[GSM_GPRS_BUFFER_SIZE];
volatile int1  new_sms_received=FALSE, something_new_received;

int1  LF_char=FALSE,  powered_on_flag=FALSE,  check_comm_flag=FALSE,
      powered_off_flag=FALSE,  check_sim_status=FALSE,  check_sim_network=FALSE,
      gprs_conn_status=FALSE,  modem_init_status=TRUE,   send_sms_report=FALSE,
      alarm_exception=FALSE,
      check_auto_date_time=FALSE, delete_sms=FALSE, gen_turn_on=FALSE;
      
int   network_operator=0, sms_number=0, incoming_call=FALSE;

int five_min_loop = _FIVE_MIN_LOOP;

const char  ok_rsp[]="OK";
const char  ring[]="RING";
const char  cops_rsp[]="+COPS:";
const char  error_rsp[]="ERROR";
const char  call_ready[]="Call Ready";
const char  power_down[]="POWER DOWN";
const char  sim_status_rsp[]="+CSMINS:";
const char  CME_error_rsp[]="+CME ERROR:";
const char  sms_memory_rsp[]="\r\n+CPMS:";
const char  new_sms_indication[]="\r\n+CMTI:";

const char  cclk_rsp[]="+CCLK:";
//!const char  ufone_apn[]="ufone.internet";
//!const char  telenor_apn[]="";

const char  ufone_sim[]="UFONE";
const char  telenor_sim[]="Telenor";

const char  ctrl_z=0x1A;
//!char  report_str[]="Report";
char  report_str[]="REPORT=";
//!char  auth_no[]="Auth no: ";
//!const char  sim_no[]="03363111563";

//---------------------------Functions

void del_sms(void);
void read_sms(void);
void send_sms(void);
void check_sms_memory();
void reset_buffers(void);
void check_rcvd_data(void);
void disconnect_call(void);
//!void check_rcvd_data(void);
void check_gprs_conn(void);
void check_server_ack(void);
void send_normal_data(void);
void send_alarms_data(void);
void check_apn_setting(void);
void check_wireless_conn(void);
void modem_initialization(void);
void send_data_using_tcpip(void);
void get_data_from_deepsea(void);
void get_date_and_time(int rcv_index);
void check_alarms(long start_address,long length_of_data);

#ifndef TESTING_MODBUS
void read_holding_regs(int mbslave_address, long start_address,long length_of_data);
#else
void read_register(long start_address,long length_of_data);
#endif

void read_input_regs(long start_address,long length_of_data);
int1 read_coils(long start_address,long quantity);

void func_check_fuel_theft(void);
void read_fuel_level(void);
int16 get_adc(void);

int check_sim(void);
int check_comm(void);
int gsm_power_up(void);
int check_network(void);
int gsm_power_down(void);
int CHECK_MSG_BODY(int _index,int _COUNT, char *command);
int get_rsp(int LF_char_count, char *command, long timeout_val, int error_type);

void check_gen_status(void);
void send_data_to_server(void);
