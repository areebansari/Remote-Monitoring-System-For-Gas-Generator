using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsMon
{
    class GenDataRow
    {
        //The field delimiter used in the input file.
        private static char C_FIELD_DELIMITER = ',';
        //The number of fields expected in the input file.
        public static int C_EXPECTED_FIELD_COUNT = 24;

        //Consts which define the location of data in the array produced from splitting the input line.
        public static int C_TYPEDATA_INDEX = 0;       // 'D'     Not Saved
        public static int C_DATE_INDEX = 1;
        public static int C_TIME_INDEX = 2;
        public static int C_GEN_ID_INDEX = 3;
        public static int C_GEN_STS_INDEX = 4;
        public static int C_VL1_INDEX = 5;
        public static int C_VL2_INDEX = 6;
        public static int C_VL3_INDEX = 7;
        public static int C_AL1_INDEX = 8;
        public static int C_AL2_INDEX = 9;
        public static int C_AL3_INDEX = 10;
        public static int C_KWH_INDEX = 11;
        public static int C_AMBTEMP_INDEX = 12;
        public static int C_COOLANTTEMP_INDEX = 13;
        public static int C_ENG_SPEED_INDEX = 14;
        public static int C_MAINT_DUE_INDEX = 15;
        public static int C_BATTERY_INDEX = 16;
        public static int C_ENG_RUNTIME_INDEX = 17;
        public static int C_NO_OFSTARTS_INDEX = 18;
        public static int C_FUEL_LEVEL_INDEX = 19;
        public static int C_FUEL_THEFT_INDEX = 20;
        public static int C_GEN_FREQ_INDEX = 21;
        public static int C_OIL_PRESSURE_INDEX = 22;
        public static int C_HIGH_OILTEMP_INDEX = 23;


        //Field Instance variables.
        private String f_TypeData;
        private String f_MeasurementDate;
        private String f_MeasurementTime;
        private String f_Gen_id;
        private bool f_Gen_Sts;
        private decimal f_VL1;
        private decimal f_VL2;
        private decimal f_VL3;
        private decimal f_AL1;
        private decimal f_AL2;
        private decimal f_AL3;
        private decimal f_KWH;
        private decimal f_CoolantTemp;
        private decimal f_AmbTemp;
        private decimal f_EngSpeed;
        private decimal f_MaintDue;
        private decimal f_Battery;
        private decimal f_EngRunTime;
        private decimal f_NoOfStarts;
        private decimal f_FuelLevel;
        private decimal f_FuelTheft;
        private decimal f_GenFreq;
        private decimal f_OilPressure;
        private decimal f_HighOilTemp;
        


        public String TypeData { get { return f_TypeData; } }
        public String MeasurementDate { get { return f_MeasurementDate; } }
        public String MeasurementTime { get { return f_MeasurementTime; } }
        public String GeneratorID { get { return f_Gen_id; } }  // mobile
        public bool GenStatus { get { return f_Gen_Sts; } }
        public decimal VoltageLine1 { get { return f_VL1 / 10; } }
        public decimal VoltageLine2 { get { return f_VL2 / 10; } }
        public decimal VoltageLine3 { get { return f_VL3 / 10; } }
        public decimal AmperesLine1 { get { return f_AL1 / 10; } }
        public decimal AmperesLine2 { get { return f_AL2 / 10; } }
        public decimal AmperesLine3 { get { return f_AL3 / 10; } }
        public decimal KWH { get { return f_KWH; } }
        public decimal AmbTemp { get { return f_AmbTemp / 100; } }
        public decimal CoolantTemp { get { return f_CoolantTemp / 100; } }
        public decimal EngSpeed { get { return f_EngSpeed; } }
       
 
        public decimal MaintDue { get { return f_MaintDue; } }
        public decimal Battery { get { return f_Battery / 10; } }
        public decimal EngRunTime { get { return f_EngRunTime; } }
        public decimal NoOfStarts { get { return f_NoOfStarts; } }
        public decimal FuelLevel { get { return f_FuelLevel / 10000; } }
        public decimal FuelTheft { get { return f_FuelTheft / 100; } }  // 2 decimal place
        public decimal GenFreq { get { return f_GenFreq / 10; } }
        public decimal OilPressure { get { return f_OilPressure; } }
        public decimal OilTemp { get { return f_HighOilTemp; } }

        public GenDataRow(String InputRow)
        {
            if (InputRow == null)
                throw new ArgumentNullException("Input Row can not be null");

            if (InputRow.Length == 0)
                throw new ArgumentException("Input Row can not be empty");

            string[] fields = InputRow.Split(C_FIELD_DELIMITER);
            if (fields.Length != C_EXPECTED_FIELD_COUNT)
                throw new ArgumentException(string.Format("The input field count {0}, do not match expected {1}.", fields.Length, C_EXPECTED_FIELD_COUNT));

            string s_TypeData = fields[C_TYPEDATA_INDEX].Trim();
            string s_MeasurementDate = fields[C_DATE_INDEX].Trim();
            string s_MeasurementTime = fields[C_TIME_INDEX].Trim();
            string s_Generator = fields[C_GEN_ID_INDEX].Trim();
            string s_Gen_Sts = fields[C_GEN_STS_INDEX].Trim();
            string s_VL1 = fields[C_VL1_INDEX].Trim();
            string s_VL2 = fields[C_VL2_INDEX].Trim();
            string s_VL3 = fields[C_VL3_INDEX].Trim();
            string s_AL1 = fields[C_AL1_INDEX].Trim();
            string s_AL2 = fields[C_AL2_INDEX].Trim();
            string s_AL3 = fields[C_AL3_INDEX].Trim();
            string s_KWH = fields[C_KWH_INDEX].Trim();
            string s_AmbTemp = fields[C_AMBTEMP_INDEX].Trim();
            string s_CoolantTemp = fields[C_COOLANTTEMP_INDEX].Trim();
            string s_EngSpeed = fields[C_ENG_SPEED_INDEX].Trim();
            string s_MaintDue = fields[C_MAINT_DUE_INDEX].Trim();
            string s_Battery = fields[C_BATTERY_INDEX].Trim();
            string s_EngRunTime = fields[C_ENG_RUNTIME_INDEX].Trim();
            string s_NoOfStarts = fields[C_NO_OFSTARTS_INDEX].Trim();
            string s_FuelLevel = fields[C_FUEL_LEVEL_INDEX].Trim();
            string s_FuelTheft = fields[C_FUEL_THEFT_INDEX].Trim();
            string s_GenFreq = fields[C_GEN_FREQ_INDEX].Trim();
            string s_OilPressure = fields[C_OIL_PRESSURE_INDEX].Trim();
            string s_HighOilTemp = fields[C_HIGH_OILTEMP_INDEX].Trim();



            string[] daterx = s_MeasurementDate.Split('/');
            //  Convert & Validate generator InputRow data.
            // (To do: Search db for this generatorID and get its Type,
            //  use Limits table to check upper & lower Measured limits).
            try
            {
                f_TypeData = Convert.ToString(s_TypeData);
                f_MeasurementDate = Convert.ToString(s_MeasurementDate);
                f_MeasurementTime = Convert.ToString(s_MeasurementTime);
                f_Gen_id = Convert.ToString(s_Generator);
                f_Gen_Sts = s_Gen_Sts == "1" ? true : false;
                f_VL1 = Convert.ToDecimal(s_VL1);
                f_VL2 = Convert.ToDecimal(s_VL2);
                f_VL3 = Convert.ToDecimal(s_VL3);
                f_AL1 = Convert.ToDecimal(s_AL1);
                f_AL2 = Convert.ToDecimal(s_AL2);
                f_AL3 = Convert.ToDecimal(s_AL3);
                f_KWH = Convert.ToDecimal(s_KWH);
                f_AmbTemp = Convert.ToDecimal(s_AmbTemp);
                f_CoolantTemp = Convert.ToDecimal(s_CoolantTemp);
                f_EngSpeed = Convert.ToDecimal(s_EngSpeed);
                // Epoch 2 hours
                f_MaintDue = Epoch2Hours(Convert.ToInt32(s_MaintDue));
                f_Battery = Convert.ToDecimal(s_Battery);
                f_EngRunTime = Epoch2Hours(Convert.ToInt32(s_EngRunTime)); 
                f_NoOfStarts = Convert.ToDecimal(s_NoOfStarts);
                f_FuelLevel = Convert.ToDecimal(s_FuelLevel);
                f_FuelTheft = Convert.ToDecimal(s_FuelTheft);
                f_GenFreq   = Convert.ToDecimal(s_GenFreq);
                f_OilPressure   = Convert.ToDecimal(s_OilPressure);
                f_HighOilTemp     = Convert.ToDecimal(s_HighOilTemp);   


            }
            catch (FormatException)
            {
                throw new ArgumentException("InputRow Error: Generator data is not in valid format");
            }
        } // constr

        private Decimal Epoch2Hours(Int32 msecs)
        {
            TimeSpan t = DateTimeOffset.UtcNow - new DateTime(1970, 1, 1);
            Int32 Secs = msecs - (Int32)t.TotalSeconds;
            if (Secs > 0)
            {
                return Convert.ToDecimal(Secs / 3600); // GMT +5 for PK
            }
            return 0;
        }

    }
}
