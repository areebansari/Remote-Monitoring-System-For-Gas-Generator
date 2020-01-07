﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
//using SmsMonTab;
                   
namespace SmsMon
{
    public delegate void SmsMessageIndication(String msg);

    public partial class Form1 : Form
    {
        private int port = 0;
        SerialPort usbport = new SerialPort();
       // private Boolean ModemSts = false;
        private static Boolean EMU = false;      // using Emulator(=1, No Network status)
        //public  GsmCommMain gprs = null;
        String Modem = null;
        Int32 BaudRate = 19200;  // default
        //UsbModem gprs = null;
        UsbModem gprs = null;
        string com = null;
        SqlConnection conn;
        SqlDataReader gens = null;
        CultureInfo culture = new CultureInfo("en-GB");
        List<String> listLocation = new List<string>();
        List<String> listGenerators = new List<string>();
        String queryGen = null;
        bool QueryOK = false;
        String[] smstype;
        String org = null;
        int indx = 0;
        param inst;
        int custid;
        public SmsMessageIndication SmsMsgInstance;
        private StringBuilder sb_err = null;      // holds error files
         TabControl tc;

        public Form1(param p_inst) //(int id,DateTime dt,String orgz,int index=0)
        {
            InitializeComponent();
            inst = p_inst;
            custid = inst.cust_id;
            conn = inst.conn;   // db connection
            //selectedDate = inst.dt;
            org = inst.org;
            indx = inst.indx;
            tc = inst.tc;   // used to select next index
            btQuery.BackColor = Color.Red;
            String SqlGenId = String.Format(@"Select Gen_Id,Location From Generators Where Cust_Id='{0}'", custid);
            String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
            conn = new SqlConnection(dbconn);
          

            SqlDataReader drg = null;
            try
            {
                // conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmdg = new SqlCommand(SqlGenId, conn))
                {
                    //cmdg.CommandTimeout = 60;   // 60 ms time out
                    drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                    listGenerators.Clear();
                    listLocation.Clear();
                    while (drg.Read())
                    {
                        String genid = drg.GetString(0);
                        comboBox1.Items.Add(genid);
                        listGenerators.Add(genid);
                        listLocation.Add(drg.GetString(1));
                    }
                    drg.Close();
                    if (comboBox1.Items.Count > 1)
                    {
                        comboBox1.Items.Add("All");
                        listLocation.Add(org +" [All]");
                    }

                    comboBox1.SelectedIndex =indx;
                    inst.gen_id = listGenerators[comboBox1.SelectedIndex].ToString();    //assign Gen_id
                    lblLoc.Text = "Location: " + listLocation[indx];
                    lblOrg.Text = "Organization: " + org;
                   
                    comboBox1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error .. No Database", "Data Base", MessageBoxButtons.OK);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return;
            }
            

            
           // Modem = ConfigurationManager.AppSettings["GPRSMODEM"];
            BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["GPRSBaudRate"]);
            try
            {
              /*  ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    String mod = queryObj["Name"].ToString();
                    if (queryObj["Name"].ToString().Contains(Modem))   // i.e "ST-Ericsson ... "
                    {
                        com = queryObj["DeviceID"].ToString();
                        //int index = com.LastIndexOf('M');
                        //if (index > 0 && index <= 5)                     // Check for valid COMx port
                        //    port = Convert.ToInt32(com.Substring(index + 1, com.Length - index - 1));
                    }
                }  */
               // com = "COM3";
                com = Convert.ToString(ConfigurationManager.AppSettings["RxMODEM"]);
                if (com != null)
                {
                    //gprs = new GsmCommMain(port, BaudRate, 1000);              //
                     gprs = new UsbModem();

                     
                    try
                    {
                        this.usbport = gprs.OpenPort(com, BaudRate, 8, 300, 300);
                        if (this.usbport.IsOpen)
                        {
                        // String ok =  gprs.ConfigModem();    // set modem defaults
                        }
                        else
                        {
                            MessageBox.Show("Error: Unable to init Modem:");
                        }
                    }
                    catch (Exception ex)
                    {
                       // ModemSts = false;
                        MessageBox.Show("Error: Unable to init Modem:" + ex.Message); 
                    }
                }
            }
            catch (Exception ex)
            {
                     MessageBox.Show("Error: Comm port is Closed");
            }

            if (gprs != null)  //if (ModemSts)
            {
                btnGsmModule.Text = "ON";
                btnGsmModule.BackColor = Color.Green;
                
                String network = gprs.GetCurrentOperator(this.usbport);
                if (network.Length > 0)
                {
                    btnNetworkStatus.Text = network;
                    btnNetworkStatus.BackColor = Color.Green;
                }
            }
            if (gprs == null) //if (!ModemSts)
            {
                // modem not connected
                MessageBox.Show("Modem Failure", "Modem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    this.usbport.DataReceived += new SerialDataReceivedEventHandler(gprs_dataReceived);    // intrrupt handling only
                    SmsMsgInstance = new SmsMessageIndication(ProcessSmsData);   // Delegate to process & update SQL db
                    gprs.MsgRxdEnableInterrupt(this.usbport);   // Now enable Rx interrupt
                    // String ok = gprs.ConfigModem();    // set modem defaults

                    // gprs.PhoneConnected += new EventHandler(gprs_ModemConnected);
                    //gprs.MessageReceived += new MessageReceivedEventHandler(gprs_SmsMessageReceived);
                    // gprs.ReceiveComplete += new  ProgressEventHandler(gprs_ReceiveComplete);
                    // gprs.PhoneDisconnected += new EventHandler(gprs_PhoneDisconnected);  
                    // gprs.ModemStatus = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR = Mdem Config: " + ex.Message);
                }
                //Check that the InvalidFilesDirectory exists
                if (!Directory.Exists(ConfigurationManager.AppSettings["DiscardedSMS"]))
                {
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["DiscardedSMS"]);
                }
            }
         } // form constr  

        
    void gprs_dataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (e.EventType == SerialData.Chars)
        {
           // gprs.receiveNow.Set();    // allow thread to read
            try
            {
                Thread.Sleep(150);  // give time to rx sms >= 114 Alarm/Data bytes
                String portRead = this.usbport.ReadExisting();
                this.usbport.DiscardOutBuffer();     // clear buffers
                this.usbport.DiscardInBuffer();
                if (portRead == "" || portRead.Length < 10) return;      // ignore
                this.SmsMsgInstance(portRead); // Handover to delegate to process in a separate thread
            }
            catch (TimeoutException ex)
            {

            }

            
           
       }
    }   //  gprs event

    #region processsms and update SQl  
     public void ProcessSmsData(String portRead)
     {
         String[] smsrxd = Regex.Split(portRead, "\r\nOK\r\n");  // separate rxd  into SMS pkts. 
        //ShortMessageCollection messages = gprs.ParseMessages(msg);
        foreach (String sms in smsrxd)
        {
            if (!sms.Contains("+CMT:"))
            {  // not sms, ignore  this msg
                #region chk random port read
            /*    if( (sms == "") || sms.Contains("RING") || sms.Contains("NO CARRIER") || sms.Contains("AT+CNMI="))
                {
                        // next
                }
                else
                // if(sms.Contains())
                {

                    String cmd = sms;
                }
                */
                    #endregion
                return;
            }
            else
            {
                String Sqlsms = null;
                //clean data string
                if (sms.Contains("REPORT"))
                {
                    if (!backgroundWorker1.IsBusy)
                        backgroundWorker1.RunWorkerAsync(sms);
                    return;
                }
                // First + CMT
                String sms1 = sms.Remove(0, sms.IndexOf(':')+1);
                String msg = sms1.Substring(sms1.IndexOf('+') , sms1.LastIndexOf("\r\n") - sms1.IndexOf('+') -1);
                string[] cmdData = Regex.Split(msg, "\r\n");
                //if (cmdData.Count<String>() != 2) return;  // badly formed SMS msg
                String generator_id = cmdData[0].Substring(cmdData[0].IndexOf('+'), 13);   // get mob no from SMS
                    
                String ms = cmdData[1].ToString();   // msg part in SS
                if (ms.Length > 10)     // valid sms msg ?
                {
                    String[] smstype = ms.Split(',');
                    String dt = smstype[1];
                    dt = "20" + dt;

                    DateTime respDate = Convert.ToDateTime(dt);
                    String rxDate = respDate.ToString("d", culture);

                    smstype[3] = generator_id;          // replace gen id
                       
                    if (QueryOK)   // display incoming SMS msg(s)?
                    {
                        bool txtBox1 = false;
                        if (queryGen.Equals("All"))
                        {
                            if (listGenerators.Contains(smstype[3].ToString()))  // has req'd Generator?
                                txtBox1 = true;  //update textBox1 13/9/15

                        }
                        else
                        {
                            String mob = smstype[3].ToString();
                            if (queryGen.Equals(mob))
                                txtBox1 = true;   //update textBox1
                        }
                        if (txtBox1)
                        {
                            if (!backgroundWorker1.IsBusy)
                                backgroundWorker1.RunWorkerAsync(msg);
                        }
                    }
                    try
                    {
                        //if (!gens.IsClosed) gens.Close();
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        switch (smstype[0])
                        {

                            case "D":
                                GenDataRow row = new GenDataRow(ms);
                                Sqlsms = String.Format(@"INSERT INTO Data(TypeData,Date,Time,Gen_Id,Gen_Status,V1,V2,V3,I1,I2,I3,KWH,AmbTemp,CoolantTemp,EngSpeed,MaintDue,BattVoltage,EngRunTime,NoOfStarts,FuelLevel) VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19})",
                                    // Sqlsms = String.Format(@"INSERT INTO Data(TypeData,Date)  VALUES('{0}','{1}')", '{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19})",*/
                                    row.TypeData,
                                    rxDate, //row.MeasurementDate,
                                    row.MeasurementTime,
                                    generator_id, //row.GeneratorID,
                                    row.GenStatus,
                                    row.VoltageLine1,
                                    row.VoltageLine2,
                                    row.VoltageLine3,
                                    row.AmperesLine1,
                                    row.AmperesLine2,
                                    row.AmperesLine3,
                                    row.KWH,
                                    row.AmbTemp,
                                    row.CoolantTemp,
                                    row.EngSpeed,
                                    row.MaintDue,
                                    row.Battery,
                                    row.EngRunTime,
                                    row.NoOfStarts,
                                    row.FuelLevel
                                    );
                                break;

                            case "A":
                                GenAlarmRow arow = new GenAlarmRow(ms);
                                Sqlsms = String.Format(@"INSERT INTO Alarms(TypeData,Date,Time,Gen_Id,EmergencyStop,FailToStart,FailToStop,GenLowFreq,GenHighVoltage,GenLowVoltage,GenRevPower,GenEarthFault,HighCurrent,GenHighFreq,LowBatteryVolt,HighBatteryVolt,PanelDoorOpen,FuelTheft,FuelLow,FuelHigh) VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19})",
                                    arow.TypeData,
                                    rxDate, //arow.MeasurementDate,
                                    arow.MeasurementTime,
                                    generator_id, //arow.GeneratorID,
                                    arow.EmergencyStop,
                                    arow.FailToStart,
                                    arow.FailToStop,
                                    arow.LowFreq,
                                    arow.HighVoltage,
                                    arow.LowVoltage,
                                    arow.ReversePower,
                                    arow.EarthFault,
                                    arow.HighCurrent,
                                    arow.HighFReq,
                                    arow.LowBattery,
                                    arow.HighBattery,
                                    arow.PanelDoorOPen,
                                    arow.FuelTheft,
                                    arow.FuelLow,
                                    arow.FuelHigh
                                    );
                                break;

                            case "G":
                                Sqlsms = String.Format(@"INSERT INTO GenStart(TypeData,Date,Time,Gen_Id) VALUES('{0}','{1}','{2}','{3}')",
                                smstype[0],
                                rxDate,  // Displays en-Gb 15/3/2008,
                                smstype[2],
                                smstype[3]
                                );
                                break;
                            //MessageBox.Show("Generator Started: " + msg); break;
                            case "S":
                                Sqlsms = String.Format(@"INSERT INTO GSMStart(TypeData,Date,Time,Gen_Id) VALUES('{0}','{1}','{2}','{3}')",
                                smstype[0],
                                rxDate,  // Displays en-GB 15/3/2008,
                                smstype[2],
                                smstype[3]
                                );
                                break;
                            //MessageBox.Show("GSM Started: " + msg); break;

                            default: MessageBox.Show("Unknown SMS: " + msg); break;
                        }
                        if (Sqlsms != null)
                        {
                            using (SqlCommand cmdg = new SqlCommand(Sqlsms, conn))
                            {
                                cmdg.CommandTimeout = 60;   // 60 ms time out
                                cmdg.ExecuteNonQuery();
                            }
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        
                        if (sms.Length > 10)     // save bad SMS
                        {
                            String tyme = DateTime.Now.ToShortDateString().Replace('/','_');
                            String f_errs = String.Format("{0}\\{1}{2}", ConfigurationManager.AppSettings["DiscardedSMS"], tyme,".txt");
                          
                            
                            FileStream fe = null;

                            if (!File.Exists(f_errs))
                            {
                                // create file
                                fe = new FileStream(f_errs, FileMode.Create);
                            }
                            else
                            {
                                fe = new FileStream(f_errs, FileMode.Append);
                            }
                           
                           using (StreamWriter se = new StreamWriter(fe, Encoding.UTF8))
                           {
                               se.Write(sms);
                               se.Flush();
                               se.Close();
                            }
                           fe.Close();
                        }
                       // MessageBox.Show("SMS msg not in correct format: " + ex.Message);
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }
                    finally
                    {
                         if (conn.State == ConnectionState.Open)
                             conn.Close();
                    }
                }
            }
        }   //e.type
    } // for each sms
    #endregion


    public void Config(param p_inst)
    {
        inst = p_inst;
        custid = inst.cust_id;
        conn = inst.conn;   // db connection
        org = inst.org;
        indx = inst.indx;
        tc = inst.tc;   // used to select next index
        btQuery.BackColor = Color.Red;

        String SqlGenId = String.Format(@"Select Gen_Id,Location From Generators Where Cust_Id='{0}'", custid);
        /*  String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
            conn = new SqlConnection(dbconn);
        */

        SqlDataReader drg = null;
        try
        {
            // conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            using (SqlCommand cmdg = new SqlCommand(SqlGenId, conn))
            {
                //cmdg.CommandTimeout = 60;   // 60 ms time out
                drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                listGenerators.Clear();
                listLocation.Clear();
                comboBox1.Items.Clear();
                while (drg.Read())
                {
                    String genid = drg.GetString(0);
                    comboBox1.Items.Add(genid);
                    listGenerators.Add(genid);
                    listLocation.Add(drg.GetString(1));
                }
                drg.Close();
                if (comboBox1.Items.Count > 1)
                {
                    comboBox1.Items.Add("All");
                    listLocation.Add(org + " [All]");
                }                                       

                comboBox1.SelectedIndex = indx;
                inst.gen_id = listGenerators[comboBox1.SelectedIndex].ToString();    //assign Gen_id 
                lblLoc.Text = "Location: " + listLocation[indx];
                lblOrg.Text = "Organization: " + org;

                comboBox1.Show();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error .. No Database", "Data Base", MessageBoxButtons.OK);
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return;
        }
   }


        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            String genStr = comboBox1.SelectedItem.ToString().Trim();
            if (genStr.Equals("All"))
            {
                MessageBox.Show("Error..Please Select one Generator.", "Generator Selection");
                return;
            }

            inst.gen_id = genStr;
            tc = inst.tc;
            tc.SelectedIndex = 2;   //  Alarm StatusBar 
                 
           
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            String genStr = comboBox1.SelectedItem.ToString().Trim();
            if (genStr.Equals("All"))
            {
                MessageBox.Show("Error..Please Select one Generator.", "Generator Selection");
                return;
            }
            monthCalendar2.MaxSelectionCount = 1;
            String dateSelected = monthCalendar2.SelectionStart.ToShortDateString();
            inst.gen_id = genStr;
            inst.dt = dateSelected;
            //LogsForm logs = new LogsForm(inst); //(genStr,dateSelected);    //Padding todo
            tc = inst.tc;
            tc.SelectedIndex = 3;  // log page

            // logs.Owner = this;
            //logs.Show();
            //DialogResult dialogresult = logs.ShowDialog();
            
            //logs.Dispose();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;   // clear msg  box
        }

   
      // Poll Generators at regular intervals
 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           if (gprs != null) gprs.ClosePort(usbport);
           if (conn.State == ConnectionState.Open) conn.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btQuery_Click(object sender, EventArgs e)
        {    //
            if (QueryOK)
            {
                QueryOK = false;
                btQuery.BackColor = Color.Red;
                if (backgroundWorker1.IsBusy) backgroundWorker1.CancelAsync();
                
            }
            else
            {
                QueryOK = true;
                btQuery.BackColor = Color.Green;
                queryGen = (String)comboBox1.SelectedItem;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String qryStr = (String)comboBox1.SelectedItem;
            queryGen = qryStr.Trim();
            lblLoc.Text = "Location: " + listLocation[comboBox1.SelectedIndex];
         }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            String msg = (String)e.Argument;
            if (msg != null && msg.Length > 0)
            {

                this.Invoke(new Action<string>(UpdateTextBox1), msg);
            }
        }

        private void UpdateTextBox1(string value)
        {
            this.textBox1.AppendText(value);
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            TabControl tc = inst.tc;
            tc.SelectedIndex = 0;
            
            //this.Hide(); 
        }

        private void lblCustId_Click(object sender, EventArgs e)
        {

        }
   }     // form1  class

}
      
   