using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.IO.Ports;

namespace SmsMon
{
    public partial class PollGens : Form
    {
        static int GEN_ID_DIGITS = 13;     // Minimum generator ID length
        String SqlPCust = @"INSERT INTO PollCustomers (Cust_Id,Organization,Active)
                                        SELECT Cust_Id,Organization,Active FROM Customers  WHERE Active =1";
        String SqlPCustPoll0 = @"UPDATE PollCustomers SET poll = 0";
        String SqlPCustPoll1 = @"UPDATE PollCustomers SET poll = 1";
        String SqlPGenPoll0 = @"UPDATE PollGenerators SET poll = 0";
        String SqlPGenPoll1 = @"UPDATE PollGenerators SET poll = 1";
        String SqlPGen = @"INSERT INTO PollGenerators (Cust_Id,Gen_Id,Location,Operational)
                                        SELECT Cust_Id,Gen_Id,Location,Operational FROM Generators  WHERE Operational =1";
        String SqlTrunkPollCust = @"TRUNCATE TABLE PollCustomers";
        String SqlTrunkPollGens = @"TRUNCATE TABLE PollGenerators";
        String SqlPCustAll = @"SELECT Cust_Id,Organization,Poll FROM PollCustomers WHERE Active =1";
        String SqlPGensAll = @"SELECT Cust_Id,Gen_id,Location,Poll FROM PollGenerators WHERE operational =1";
        String SqlPoll = @"SELECT pg.Gen_Id FROM PollCustomers pc,PollGenerators pg WHERE pc.Cust_Id = pg.Cust_Id AND 
                                pc.Active =1 AND pg.Poll=1";
        
        List<int> c_cuidList = new List<int>();         // poll customer table  id
         List<int> g_cuidList  = new List<int>();       // Poll Gens table id
        List<string> genidList = new List<string>();    // poll gen table
        List<string> locList   = new List<string>();    // poll gen table
        List<bool> c_pollList = new List<bool>();       // list of current active Customers to be polled
        List<String> g_pollList = new List<String>();       // list of current active gens to be polled
        SqlConnection conn = null;
        SqlDataReader dr = null;

        private int port = 0;
       SerialPort usbPollport = new SerialPort();
        // private Boolean ModemSts = false;
        private static Boolean EMU = true;      // using Emulator(=1, No Network status)
        //public  GsmCommMain gprs = null;
        String Modem = null;
        int BaudRate = 19200;  // default
        UsbPollModem gprsPoll = null;
        string com = "COM3";
        bool OK2SendMsg = false;
        param inst;
        TabControl tc;
        String org;
        int custid;
        int indx;



        public PollGens(param p_inst) //int id, DateTime dt, String org, int indx = 0)
        {
            InitializeComponent();
            Config(p_inst);
        }
        public void Config(param p_inst)
        {
            inst = p_inst;
            custid = inst.cust_id;
            conn = inst.conn;   // db connection
            //selectedDate = inst.dt;
            org = inst.org;
            indx = inst.indx;
            tc = inst.tc;   // used to select next index


            String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
            conn = new SqlConnection(dbconn);
            try
            {
                // conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd2 = new SqlCommand(SqlPCustAll, conn);
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    c_cuidList.Clear();
                    c_pollList.Clear();
                    // OrgList.Clear();
                    while (reader.Read())
                    {
                        c_cuidList.Add(Convert.ToInt32(reader[0])); // p cust id
                        this.listBoxOrgs.Items.Add(Convert.ToString(reader[1]));
                        c_pollList.Add(Convert.ToBoolean(reader[2]));  
                    }
                    reader.Close();
                }
                if (c_cuidList.Count > 0)
                {
                    listBoxOrgs.SelectedIndex = 0;
                    // gen event
                    object sender = new object();
                    EventArgs e = new EventArgs();
                    this.listBoxOrgs_SelectedIndexChanged(sender, e);
                }
                lblPollSeconds.Text = Convert.ToString(ConfigurationManager.AppSettings["GenPollTime"]) + " Seconds"; ;
                 // Modem Set up
                 BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["GPRSBaudRate"]);
            try
            {
                //com = "COM10";
                com = Convert.ToString(ConfigurationManager.AppSettings["PollMODEM"]);
                if (com != null)
                {
                    //gprs = new GsmCommMain(port, BaudRate, 1000);              //
                     gprsPoll = new UsbPollModem();
                    // gprsPoll.smsEvent += new SmsMessageIndication(gprs_smsEvent);
                    // gprsPoll.MsgRxdEnableInterrupt(this.usbPollport);
                    try
                    {
                        this.usbPollport = gprsPoll.OpenPort(com, BaudRate, 8, 300, 300);
                        if (gprsPoll != null)  //if (ModemSts)
                        {
                           // Attach SMS message event handler
                           // gprsPoll.smsEvent +=new SmsMessageIndication(gprs_smsEvent);
                            gprsPoll.ModemStatus = true;
                        }    

                    }
                    catch (Exception ex)
                    {
                       // ModemSts = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //ModemSts = false;
                // EventLog.WriteEntry("AeiGenAlert: Comm  is Closed");
            }

            this.usbPollport.DataReceived += new SerialDataReceivedEventHandler(gprs_dataReceived);
            // Attach SMS message event handler                                                     
            // gprs.smsEvent +=new SmsMessageIndication(gprs_smsEvent);
            gprsPoll.MsgRxdEnableInterrupt(this.usbPollport);
        //; ;............ end Modem .....
        timer1.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["GenPollTime"]) * 1000;
        timer1.Enabled = true;
        timer1.Start();

        }
        catch(Exception ex)
        {
            MessageBox.Show(" PollGens - Initz error: "  + ex.Message);                     
        }
     }     // End of init/config



    private void UpdatePollList()
    {
        try
        {
            // conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd2 = new SqlCommand(SqlPoll, conn);
            using (SqlDataReader reader = cmd2.ExecuteReader())
            {
                g_pollList.Clear();
                while (reader.Read())
                {
                    g_pollList.Add(Convert.ToString(reader[0]));
                 }
                reader.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }

        void gprs_dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                Thread.Sleep(100);  // give time to rx sms >= 114 Alarm/Data bytes
                String portRead = this.usbPollport.ReadExisting();
                this.usbPollport.DiscardOutBuffer();     // clear buffers
                this.usbPollport.DiscardInBuffer();
                if (portRead == "" || portRead.Length < 10) return;      // ignore

                String msg = portRead;
                // Handover to delegate to process in a separate thread
            }
        
        }

    public void gprs_smsEvent(String msg)
    {
         
         String[] sms = Regex.Split(msg, "\r\n");                     
        // Save rxd values in dB.
         //gprsPoll.DiscardRxtBuffer();
         foreach (String ms in sms)
         {
             //process rxd msg
             String msgrx = ms;
             if (msgrx.Length > 4)
             {
                // gprsPoll.DiscardRxtBuffer();
                 return;
             }

             OK2SendMsg = msgrx.Contains("> ") ? true : false;
        }
    }


        private void comboOrgs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
                
        private void PollGens_Load(object sender, EventArgs e)
        {

        }

        private void btnLocGenReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to delete ALL Polling Data?", "Remove All Polling", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // update db poll bit from Gen/Gen tables
                try
                {
                    // conn.Open();
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }


                    using (SqlCommand cmd = new SqlCommand(SqlTrunkPollCust, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(SqlTrunkPollGens, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }                                        

                    using (SqlCommand cmd = new SqlCommand(SqlPCust, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(SqlPGen, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }
                     // Clear  & Populate List Box and Combo
                    this.comboGenLoc.Items.Clear();
                    this.listBoxOrgs.Items.Clear();
                    

                    SqlCommand cmd2 = new SqlCommand(SqlPCustAll, conn);
                    using (SqlDataReader reader = cmd2.ExecuteReader())
                    {
                        c_cuidList.Clear();
                        c_pollList.Clear();
                        this.listBoxOrgs.Items.Clear();
                        while (reader.Read())
                        {
                            c_cuidList.Add(Convert.ToInt32(reader[0])); // p cust id
                            this.listBoxOrgs.Items.Add(Convert.ToString(reader[1]));
                            c_pollList.Add(Convert.ToBoolean(0));   //  0
                        }
                        reader.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand(SqlPCustPoll0, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }
                     

                    this.listBoxOrgs.SelectedIndex = 0;
                    
                    
                    //this.listBoxOrgs.DisplayMember = "City Bank";
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in Copying table: " + ex.Message);
                }

            }   // if yes
        }

        private void listBoxOrgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = listBoxOrgs.SelectedIndex;
            String SqlOrgSel = String.Format(@"SELECT Cust_Id,Location,Gen_id FROM Generators WHERE Cust_id={0}", c_cuidList[id]);
            comboGenLoc.Items.Clear();
            try
            {
                // conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd2 = new SqlCommand(SqlOrgSel, conn);
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    g_cuidList.Clear();
                    g_pollList.Clear();
                    locList.Clear();
                    genidList.Clear();
                    comboGenLoc.Items.Clear();
                    while (reader.Read())
                    {
                        g_cuidList.Add(Convert.ToInt32(reader[0])); // p cust id
                        this.comboGenLoc.Items.Add(Convert.ToString(reader[1]));
                        genidList.Add(Convert.ToString(reader[2]));
                        // 3rd arg ?
                    }
                    reader.Close();
                }

                UpdatePollList();   // update Gen polls list

                if(this.comboGenLoc.Items.Count > 0)
                    this.comboGenLoc.SelectedIndex = 0;  // select first element
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ListBox index change : " + ex.Message);

            }

        }

        private void checkBoxAllOrgs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAllOrgs.Checked)
            {
               // Set all org's gen to poll
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(SqlPCustPoll1, conn))
                {
                    cmd.CommandTimeout = 60;
                    cmd.ExecuteNonQuery();
                }

                UpdatePollList();   // update Gen polls list
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            picPollGreenLed.Enabled = true;
            picPollStopedLed.Enabled = false;
            picPollStopedLed.Hide();
            picPollGreenLed.Show();
            // Do  Poll
            for (int x = 0; x < g_pollList.Count; x++)
            {
                
                if(g_pollList[x] != null )  // is poll == true               
                {

                    String Gen_mobile = (String)g_pollList[x];
                    if (Gen_mobile.Contains("+") && Gen_mobile.Length >= GEN_ID_DIGITS)   //
                    {
                        String message = "REPORT";
                        gprsPoll.sendSMSMsg(this.usbPollport, Gen_mobile, message);
                        Thread.Sleep(300);
                    }     //Gen_mobile = "+923363111563";     //"+923332383079" A Hayee;
                }
            }//orgs
           // Thread.Sleep(1000);
            picPollGreenLed.Enabled = false;
            picPollStopedLed.Enabled = true;
            picPollStopedLed.Show();
            picPollGreenLed.Hide();
            Int32 Interval = Convert.ToInt32(ConfigurationManager.AppSettings["GenPollTime"]);
            timer1.Interval = Interval * 1000;  // seconds
            timer1.Enabled = true;
            timer1.Start();
        }

        private void btnAddPollGen_Click(object sender, EventArgs e)
        {
            // poll gen set  PollCust & PollGen Poll bit = 1'
            String SqlPSetEGenPoll1 = String.Format(@"UPDATE PollGenerators SET poll = 1WHERE Cust_Id ={0} AND Gen_Id='{1}'", g_cuidList[comboGenLoc.SelectedIndex], genidList[comboGenLoc.SelectedIndex]);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                if (checkBoxLocGen.Checked)
                {

                   using(SqlCommand cmd = new SqlCommand(SqlPGenPoll1,conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand(SqlPSetEGenPoll1, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }
                }

                UpdatePollList();   // update Gen polls list
            }
            catch (Exception ex)
            {

            }

        }

       
        private void btnLocGenRemove_Click(object sender, EventArgs e)
        {
            // Remove  poll gen set  PollCust & PollGen Poll bit = 0'
            String SqlPGenSetPoll0 = String.Format(@"UPDATE PollGenerators SET poll = 0  WHERE Cust_Id ={0} AND Gen_Id='{1}'", g_cuidList[comboGenLoc.SelectedIndex], genidList[comboGenLoc.SelectedIndex]);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                if (checkBoxLocGen.Checked)
                {

                    using (SqlCommand cmd = new SqlCommand(SqlPGenPoll0, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand(SqlPGenSetPoll0, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }

                    UpdatePollList();   // update Gen polls list
                }  
            }
            catch (Exception ex)
            {

            }
        }

        private void checkBoxLocGen_CheckedChanged(object sender, EventArgs e)
        {
          // status checked else where 
        }      

        private void btnLocGenStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            inst.org = org;
            inst.cust_id = custid;
            inst.indx = indx;
            inst.sts = true;
            TabControl tc = inst.tc;
            tc.SelectedIndex = 0;    // admin page
        }

        private void btnLocGenStart_Click(object sender, EventArgs e)
        {
            Int32 Interval = Convert.ToInt32(ConfigurationManager.AppSettings["GenPollTime"]);
            timer1.Interval = Interval * 1000;  // seconds
            timer1.Enabled = true;
            timer1.Start();
        }

    }     //  class
}      // form
                                        