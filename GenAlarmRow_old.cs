using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsMon
{
    class GenAlarmRow
    {
        //The field delimiter used in the input file.
		private static char C_FIELD_DELIMITER = ',';
		//The number of fields expected in the input file.
		public static int C_EXPECTED_FIELD_COUNT = 20;
		
		//Consts which define the location of data in the array produced from splitting the input line.
        public static int C_TYPEDATA_INDEX          = 0;       // 'A'     Alarm
        public static int C_DATE_INDEX              = 1;
        public static int  C_TIME_INDEX             = 2;
        public static int C_GEN_ID_INDEX            = 3;
        public static int C_EMERGENCYSTOP_INDEX     = 4;
        public static int C_FAILTOSTART_INDEX       = 5;
        public static int C_FAILTOSTOP_INDEX        = 6;
        public static int C_GENLOWFREQ_INDEX        = 7;
        public static int C_GENHIGHVOLTS_INDEX      = 8;
        public static int C_GENLOWVOLTS_INDEX       = 9;
        public static int C_GENREVERSEPWR_INDEX     = 10;
        public static int C_EARTHFAULT_INDEX        = 11;
        public static int C_GENHIGHCURRENT_INDEX    = 12;
        public static int C_GENHIGHFREQ_INDEX       = 13;
        public static int C_LOWBATTERYVOLT_INDEX    = 14;
        public static int C_HIGHBATTERYVOLT_INDEX   = 15;
        public static int C_PANELDOOROPEN_INDEX     = 16;
        public static int C_FUELTHEFT_INDEX         = 17;
        public static int C_FUELLEVELLOW_INDEX      = 18;
        public static int C_FUELLEVELHIGH_INDEX     = 19;
        
        //Field Instance variables.
        private String      f_TypeData;
        private String      f_MeasurementDate;
        private String      f_MeasurementTime;
        private String      f_Gen_id;
        private decimal     f_Emgy_Stop;
        private decimal     f_Fail_Start;
        private decimal     f_Fail_Stop;
        private decimal     f_Low_Freq;
        private decimal     f_High_Volts;
        private decimal     f_Low_Volts;
        private decimal     f_Rev_PWR;
        private decimal     f_Earth_Fault;
        private decimal     f_High_Current;
        private decimal     f_High_Freq;
        private decimal     f_Low_Battery;
        private decimal     f_High_Battery;
        private decimal     f_Panel_Door;
        private decimal     f_Fuel_Theft;
        private decimal     f_Fuel_Low;
        private decimal     f_Fuel_High;



        public String TypeData          { get { return f_TypeData; } }
        public String MeasurementDate   { get { return f_MeasurementDate; } }
        public String MeasurementTime   { get { return f_MeasurementTime; } }
        public String GeneratorID       { get { return f_Gen_id; } }  // mobile
        public decimal EmergencyStop    { get { return f_Emgy_Stop; } }
        public decimal FailToStart      { get { return f_Fail_Start; } }
        public decimal FailToStop       { get { return f_Fail_Stop; } }
        public decimal LowFreq          { get { return f_Low_Freq; } }
        public decimal HighVoltage      { get { return f_High_Volts; } }
        public decimal LowVoltage       { get { return f_Low_Volts;; } }
        public decimal ReversePower     { get { return f_Rev_PWR; } }
        public decimal EarthFault       { get { return f_Earth_Fault; } }
        public decimal HighCurrent      { get { return f_High_Current; }}
        public decimal HighFReq         { get { return f_High_Freq; }}
        public decimal LowBattery       { get { return f_Low_Battery; } }
        public decimal HighBattery      { get { return f_High_Battery; } }
        public decimal PanelDoorOPen    { get { return f_Panel_Door; } }
        public decimal FuelTheft        { get { return f_Fuel_Theft; } }
        public decimal FuelLow          { get { return f_Fuel_Low; } }
        public decimal FuelHigh         { get { return f_Fuel_High; } }

        public GenAlarmRow(String InputRow)
        {
            if(InputRow == null)
                throw new ArgumentNullException("Input Row can not be null");

            if (InputRow.Length == 0)
                throw new ArgumentException("Input Row can not be empty");

            string[] fields = InputRow.Split(C_FIELD_DELIMITER);
            if(fields.Length != C_EXPECTED_FIELD_COUNT)
                throw new ArgumentException(string.Format("The input field count {0}, do not match expected {1}.",fields.Length,C_EXPECTED_FIELD_COUNT));

            string s_TypeData         = fields[C_TYPEDATA_INDEX].Trim();                                                  
            string s_MeasurementDate  = fields[C_DATE_INDEX].Trim();
            string s_MeasurementTime = fields[C_TIME_INDEX].Trim();
            string s_Generator        = fields[C_GEN_ID_INDEX].Trim();
            string s_EmergencyStop    = fields[C_EMERGENCYSTOP_INDEX].Trim();
            string s_FailToStart      = fields[C_FAILTOSTART_INDEX].Trim();
            string s_FailToStop       = fields[C_FAILTOSTOP_INDEX].Trim();
            string s_GenLowFreq       = fields[C_GENLOWFREQ_INDEX].Trim();
            string s_GenHighVolts     = fields[C_GENHIGHVOLTS_INDEX].Trim();
            string s_GenLowVolts      = fields[C_GENLOWVOLTS_INDEX].Trim();
            string s_GenReversePwr    = fields[C_GENREVERSEPWR_INDEX].Trim();
            string s_EarthFault       = fields[C_EARTHFAULT_INDEX].Trim();
            string s_GenHighCurrent   = fields[C_GENHIGHCURRENT_INDEX].Trim();
            string s_GenHighFreq      = fields[C_GENHIGHFREQ_INDEX].Trim();
            string s_LowBatteryVolt   = fields[C_LOWBATTERYVOLT_INDEX].Trim();
            string s_HighBatteryVolt  = fields[C_HIGHBATTERYVOLT_INDEX].Trim();
            string s_PanelDoorOpen    = fields[C_PANELDOOROPEN_INDEX].Trim();
            string s_FuelTheft        = fields[C_FUELTHEFT_INDEX].Trim();
            string s_FuelLevelLow     = fields[C_FUELLEVELLOW_INDEX].Trim();
            string s_FuelLevelHigh    = fields[C_FUELLEVELHIGH_INDEX].Trim();
            


             string[] daterx  = s_MeasurementDate.Split('/');
            //  Convert & Validate generator InputRow data.
            // (To do: Search db for this generatorID and get its Type,
            //  use Limits table to check upper & lower Measured limits).
            try
            {
                 f_TypeData             = Convert.ToString(s_TypeData);
                 f_MeasurementDate      = Convert.ToString(s_MeasurementDate);
                 f_MeasurementTime      = Convert.ToString(s_MeasurementTime);
                 f_Gen_id               = Convert.ToString(s_Generator);
                 f_Emgy_Stop            = Convert.ToDecimal(s_EmergencyStop);
                 f_Fail_Start           = Convert.ToDecimal(s_FailToStart);
                 f_Fail_Stop            = Convert.ToDecimal(s_FailToStop);
                 f_Low_Freq             = Convert.ToDecimal(s_GenLowFreq);
                 f_High_Volts           = Convert.ToDecimal(s_GenHighVolts);
                 f_Low_Volts            = Convert.ToDecimal(s_GenLowVolts);
                 f_Rev_PWR              = Convert.ToDecimal(s_GenReversePwr);
                 f_Earth_Fault          = Convert.ToDecimal(s_EarthFault);
                 f_High_Current         = Convert.ToDecimal(s_GenHighCurrent);
                 f_High_Freq            = Convert.ToDecimal(s_GenHighFreq);
                 f_Low_Battery          = Convert.ToDecimal(s_LowBatteryVolt);
                 f_High_Battery         = Convert.ToDecimal(s_HighBatteryVolt);
                 f_Panel_Door           = Convert.ToDecimal(s_PanelDoorOpen);
                 f_Fuel_Theft           = Convert.ToDecimal(s_FuelTheft);
                 f_Fuel_Low             = Convert.ToDecimal(s_FuelLevelLow);
                 f_Fuel_High            = Convert.ToDecimal(s_FuelLevelHigh);
                    
            }
            catch(FormatException)

            {
                throw new ArgumentException("InputRow Error: Generator data is not in valid format");
            }
        } // constr

    }  //alarm
}
