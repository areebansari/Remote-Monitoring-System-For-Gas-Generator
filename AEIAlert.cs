using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Text;

namespace SmsMon
{
    public class AEIAlert
    {
       /* private string s_AlarmsTable;
        private string s_AlarmContactTable;
        private string s_DataBaseConnectionString;
        AEISmsAlert Alert = null;
        public AEIAlert(string Alarms, string AlarmContact, string DataBaseConnectionString,AEISmsAlert alert)
        {
            if(alert == null)
                throw new ArgumentNullException("GPRS Modem is not functioning");
            this.Alert = alert;
            if (Alarms == null)
                throw new ArgumentNullException("Alarms table");
            if (Alarms.Length == 0)
                throw new ArgumentException("Alarms Parameter may not be an empty string");
            s_AlarmsTable = Alarms;

            if (AlarmContact == null)
                throw new ArgumentNullException("AlarmContact table");
            if (AlarmContact.Length == 0)
                throw new ArgumentException("AlarmContact Parameter may not be an empty string");
            s_AlarmContactTable = AlarmContact;

            if (DataBaseConnectionString == null)
                throw new ArgumentNullException("DataBaseConnectionString");
            if (DataBaseConnectionString.Length == 0)
                throw new ArgumentException("DataBaseConnectionString Parameter may not be an empty string");
            s_DataBaseConnectionString = DataBaseConnectionString;
        }
        
        public bool SmsAlert(string period,Decimal ScanInterval,int interval,string sp)
        {
            //string SQLAlarms = string.Format(@"SELECT * FROM {0} WHERE AlarmDate > DATEADD({1},-{2},CURRENT_TIMESTAMP) AND AlarmDate < CURRENT_TIMESTAMP", s_AlarmsTable, period, ScanInterval);
            string SQLAlarms = string.Format(@"SELECT * FROM {0} WHERE (Processed = 'False') AND (AlarmDate < CURRENT_TIMESTAMP)", s_AlarmsTable);
            
            try
            {
                // Execute the SQL			
                using (SqlConnection conn = new SqlConnection(s_DataBaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand AlarmCommand = new SqlCommand(SQLAlarms, conn))
                    {
                        try
                        {
                            AlarmCommand.CommandTimeout = 60;   // 60 ms time out
                            SqlDataReader dra = AlarmCommand.ExecuteReader(CommandBehavior.CloseConnection);
                            while (dra.Read())  // process each Alarms row 
                            {
                                int alarmid = dra.GetInt32(0);   // alarm_id index
                                Decimal  GeneratorNo = dra.GetDecimal(2);
                                DateTime AlarmDate = dra.GetDateTime(3);
                                // use above to find contact id
                                string SQLSms = string.Format(@"SELECT c.con_id,c.FirstName,c.LastName,c.Mobile1 FROM Contact c
                                                    join Location l on l.loc_id = c.loc_id
                                                    join Generator g on l.loc_id = g.Loc_id
                                                    join Measurement m on g.gen_id = m.gen_id
                                                    join {0} a on m.meas_id = a.meas_id
                                                    WHERE a.alarm_id = {1}  and c.ContactVia='sms'", s_AlarmsTable, alarmid);

                                using (SqlConnection conn2 = new SqlConnection(s_DataBaseConnectionString))
                                {
                                    conn2.Open();
                                    using (SqlCommand ContCommand = new SqlCommand(SQLSms, conn2))
                                    {
                                        try
                                        {
                                            ContCommand.CommandTimeout = 60;   // 60 ms time out
                                            SqlDataReader drc = ContCommand.ExecuteReader(CommandBehavior.CloseConnection);

                                            while (drc.Read())  // process each Contact row 
                                            {
                                                int SendMsg = 0;
                                                string SQLAlarmContactInsert = string.Format("EXEC {0} {1},{2},{3},{4},{5}", sp, alarmid, drc.GetInt32(0), GeneratorNo, interval, SendMsg); 

                                                using (SqlConnection conn3 = new SqlConnection(s_DataBaseConnectionString))
                                                {
                                                    conn3.Open();
                                                    using (SqlCommand InsertCommand = new SqlCommand(SQLAlarmContactInsert, conn3))
                                                    {
                                                        try
                                                        {
                                                            InsertCommand.CommandTimeout = 60;   // 60 ms time out
                                                            SendMsg = InsertCommand.ExecuteNonQuery();
                                                            if(SendMsg > 0)
                                                            {
                                                                //Now send SMS to Mobile1 contact
                                                                StringBuilder destno = new StringBuilder("0");          // add leading '0' to dial digits
                                                                destno = destno.Append(Convert.ToString(drc.GetDecimal(3)));
                                                                StringBuilder SMSMsg = new StringBuilder("FAO: ");
                                                                SMSMsg.Append(drc.GetString(1));                        // First name
                                                                SMSMsg.Append(" ");
                                                                SMSMsg.Append(drc.GetString(2));                        // last name
                                                                SMSMsg.Append(", Dated: ");
                                                                SMSMsg.Append(AlarmDate.ToString("HH:mm:ss dd/MM/yyyy"));
                                                                SMSMsg.Append("  Generator No: ");
                                                                SMSMsg.Append(GeneratorNo.ToString());                  // Generator
                                                                SMSMsg.Append(", Status: ");
                                                                SMSMsg.Append(dra.GetString(4));              // Fault 
                                                                if (SMSMsg.Length > 150)
                                                                    SMSMsg = SMSMsg.Remove(150, SMSMsg.Length - 150);
                                                                string AlertMsg = SMSMsg.ToString();
                                                                this.Alert.SendMessage(AlertMsg, destno.ToString());
                                                            }
                                                        }
                                                        catch (SqlException ex)
                                                        {
                                                            string msg = ex.Message;
                                                        }
                                                    }
                                                }
                                            } //drc.read
                                        }
                                        catch (SqlException ex)
                                        {
                                        }
                                    }
                                }
                            }  //while dra
                        }
                        catch (SqlException ex)
                        {
                        }
                    }
                }
            }
            catch (SqlException exp)
            {
            }

            return true;
        }  // SmsAlert()


        public int EmailAlert(string period,int PollTime)
        {
            string SqlEmailAlert = string.Format(@"select al.Alarm_id,al.Gen_id,al.Fault,ct.FirstName,ct.Email from  contact ct
                                                    join AlarmContact ac on ac.Con_id = ct.Con_id
                                                    join Alarms al on al.Alarm_id = ac.Alarm_id
                                                    where ac.Active = 1  AND ct.ContactVia ='email'
                                                    AND DATEPART('{0}',ac.Contacted) > {1}", period,PollTime);

            try
            {
                // Execute the SQL			
                using (SqlConnection conn = new SqlConnection(s_DataBaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand EmailCommand = new SqlCommand(SqlEmailAlert, conn))
                    {
                        try
                        {
                            EmailCommand.CommandTimeout = 60;   // 60 ms time out
                            SqlDataReader dre = EmailCommand.ExecuteReader(CommandBehavior.CloseConnection);

                            while (dre.Read())  // process each contact row 
                            {
                                int alarmid = dre.GetInt32(0);   // alarm_id index
                                //   Email
                            }
                        }
                        catch (SqlException ex)
                        {
                        }
                    }  // inner using
                } //outer
            }
            catch (SqlException ex2)
            {
            }

            return 1;
        }
        */ 
    }
   
}