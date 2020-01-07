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
using GsmComm;

namespace SmsMon
{
    public partial class PollGens : Form        // 25/03/2016
    {
        static int GEN_ID_DIGITS = 13;     // Minimum generator ID length
        String SqlPGenPoll1 = @"UPDATE PollGenerators SET poll = 1";
        String SqlPGen = @"INSERT INTO PollGenerators (Cust_Id,Gen_Id,Location,Operational) 
                                        SELECT Cust_Id,Gen_Id,Location,Operational FROM Generators  WHERE Operational =1";
        String SqlTrunkPollGens = @"TRUNCATE TABLE PollGenerators";
        String SqlCustomers = @"SELECT Cust_Id,Organization FROM Customers WHERE Active =1";
        String SqlPGensAll = @"SELECT Cust_Id,Gen_id,Location,Poll FROM PollGenerators WHERE operational =1";


        private bool PollChk = true;
        List<int> c_cuidList = new List<int>();         // poll customer table  id
         List<int> g_cuidList  = new List<int>();       // Poll Gens table id
        List<string> genidList = new List<string>();    // poll gen table
        List<string> locList   = new List<string>();    // poll gen table
        List<String> g_pollList = new List<String>();       // list of current active gens to be polled
        List<String> g_IntensivePoll = new List<string>();  // Intensive poll generators
        
        SqlConnection conn = null;
        SqlDataReader dr = null;

        private int port = 0;
       SerialPort usbPollport = new SerialPort();
        // private Boolean ModemSts = false;
        private static Boolean EMU = true;      // using Emulator(=1, No Network status)
        //public  GsmCommMain gprs = null;
        String Modem = null;
        int BaudRate = 19200;  // default
         public  UsbPollModem  gprsPoll = null;
         AEISmsAlert pollmodem = null;
        string com = "COM3";
        bool OK2SendMsg = false;
        param inst;
        TabControl tc;
        String org;
        int orgindx = 0;
        int custid;
        int indx;
        bool Configured = false;          // ConfigSystem data
        int  PollTimer = 0;
        String ComPollModem = null;
        bool Toggle = true;

        public PollGens(param p_inst) //int id, DateTime dt, String org, int indx = 0)
        {
            InitializeComponent();
            Config(p_inst);
        }


        public void Config(param p_inst)
        {
            inst = p_inst;
            custid = inst.cust_id;
            //conn = inst.conn;   // db connection
            //selectedDate = inst.dt;
            org = inst.org;
            indx = inst.indx;
            PollChk = inst.PollChk;
            if (PollChk)
            {
                rbPoll.Checked = true;
                rbTimer.Checked = false;
            }
            else
            {
                rbPoll.Checked = false;
                rbTimer.Checked = true;
            }
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

                UpdateOrgListBox();  // populate ListBoxOrg
                lblPollSeconds.Text = Convert.ToString(ConfigurationManager.AppSettings["GenPollTime"]) + " Seconds"; 
                // Modem Set up
                BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["GPRSBaudRate"]);
               string modem = ConfigurationManager.AppSettings["GPRSPollModem"];
                try
                {
                    pollmodem = AEISmsAlert.getInstance(modem, BaudRate); //(modem, BaudRate);
                    //EventHandler PhoneConnected += new EventHandler(comm_phoneconnected);
                    //EventHandler PhoneDisconnected += new EventHandler(comm_phoneDisconnected);

                    if (pollmodem.OK)
                    {
                        btnGsmModule.Text = "ON";
                        btnGsmModule.BackColor = Color.Green;
                        try
                        {
                            btnNetworkStatus.Text = pollmodem.GetOPerator();
                            btnNetworkStatus.BackColor = Color.Green;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Operator not found: " + ex.Message); 
                        }
                    }

           
                   
                  // com = Convert.ToString(ConfigurationManager.AppSettings["PollMODEM"]);
                   String SqlConfig = "SELECT * FROM ConfigSystem";
                   SqlCommand cmd = new SqlCommand(SqlConfig, conn);
                    
                  try
                   {
                       if (conn.State != ConnectionState.Open)
                       {
                           conn.Open();
                       }
                       using (SqlDataReader rd = cmd.ExecuteReader())
                       {
                           while (rd.Read())
                           {
                               // ComRxModem = Convert.ToString(rd[0]);
                              // ComPollModem = Convert.ToString(rd[1]).Trim();
                               PollTimer = Convert.ToInt32(rd[2]);
                               Configured = Convert.ToBoolean(rd[3]);
                               // DateTime  = Convert.ToString(rd[4]);
                           }
                           rd.Close();
                       }
                       if (Configured)
                       {
                          // com = ComPollModem;   // assign com from db
                           lblPollSeconds.Text = PollTimer + " Secs";
                       }
                       
                   }    
                   catch (Exception ex)
                   {
                       //ModemSts = false;
                       // EventLog.WriteEntry("AeiGenAlert: Comm  is Closed");
                   }

      
                    // reanable timer
                    if (Configured)
                    {
                        timer1.Interval = PollTimer * 1000;
                    }
                    else
                    {
                        timer1.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["GenPollTime"]) * 1000;
                    }
                    timer1.Enabled = true;
                    timer1.Start();
                     
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" PollGens - Initz error: " + ex.Message);
                }
             
            }
            catch (Exception ex)
            {


            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        
       }

        private void comm_phoneDisconnected(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void comm_phoneconnected(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if ( != null)
               
        }     // End of init/config

       

    private void UpdatePollList()
    {
        
        String SqlPoll = @"SELECT pg.Gen_Id,pg.Location FROM Customers c,PollGenerators pg WHERE c.Cust_Id = pg.Cust_Id AND 
                                c.Active =1 AND pg.Poll=1";

        if (!inst.admin)
        {
            SqlPoll = String.Format(@"SELECT pg.Gen_Id,pg.Location FROM Customers c,PollGenerators pg WHERE c.Cust_Id = pg.Cust_Id AND 
                                c.Active =1 AND pg.Poll=1 AND c.Cust_id ={0}",custid);

        }
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
                g_IntensivePoll.Clear();
                locList.Clear();
                while (reader.Read())
                {
                    g_IntensivePoll.Add(Convert.ToString(reader[0]).Trim());
                    g_pollList.Add(Convert.ToString(reader[0]).Trim());
                    locList.Add(Convert.ToString(reader[1]));
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
        }
         finally
        {

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }

    #region Oldmodem data rxd
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
  #endregion

    private void comboOrgs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
                
        private void PollGens_Load(object sender, EventArgs e)
        {

        }

        private void UpdatePollGenerator(int custid, String genid, String loc)
        {
            Int32 cnt = 0;
            try
            {                                                                                              
                // conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                 // check if user exist update poll bit else insert new Plollgenerator  entry
                String SQLCount = String.Format(@"SELECT Count(Gen_Id) FROM PollGenerators  WHERE Cust_Id={0} AND Gen_Id='{1}'", custid, genid);
                using (SqlCommand cmd = new SqlCommand(SQLCount, conn))
                {
                    cmd.CommandTimeout = 60;
                    cnt = (Int32)cmd.ExecuteScalar();
                }
                if (cnt > 0)  // poll bit
                {
                     String SqlUpdatePoll = String.Format(@"UPDATE PollGenerators SET poll = 1 WHERE Cust_Id = {0} AND Gen_Id ='{1}'",custid,genid);
                     using (SqlCommand cmd2 = new SqlCommand(SqlUpdatePoll, conn))
                     {
                         cmd2.CommandTimeout = 60;
                         cmd2.ExecuteNonQuery();
                     }
                }
                else   // insert new row
                {
                    String SqlPollGenInsertRow =String.Format(@"INSERT INTO PollGenerators (Cust_Id,Gen_Id,Location,Operational,Poll) 
                                        VALUES({0},'{1}','{2}','{3}','{4}')", custid, genid, loc, '1','1');
                    using (SqlCommand cmd3 = new SqlCommand(SqlPollGenInsertRow, conn))
                    {
                        cmd3.CommandTimeout = 60;
                        cmd3.ExecuteNonQuery();
                    }
                }                                                                   
                
            }
            catch (Exception ex)
            {


            }
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


        }
        private void btnLocGenReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to delete ALL Intensive Monitoring?", "Remove All Polling", MessageBoxButtons.YesNo);
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

        #region trunkate admin/user tables
                    if (!inst.admin)
                    {
                        SqlPGen = String.Format(@"INSERT INTO PollGenerators (Cust_Id,Gen_Id,Location,Operational) 
                                        SELECT Cust_Id,Gen_Id,Location,Operational FROM Generators  WHERE Operational =1 AND Cust_id={0}",custid);
                       SqlTrunkPollGens = String.Format(@"DELETE PollGenerators WHERE Cust_id={0}",custid);  
                    }
                    using (SqlCommand cmd = new SqlCommand(SqlTrunkPollGens, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }                                        

                     using (SqlCommand cmd = new SqlCommand(SqlPGen, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }

        #endregion
                     // Clear  & Populate List Box and Combo
                    this.listGenLoc.Items.Clear();     //rep combobox
                    this.listBoxOrgs.Items.Clear();
                    UpdateOrgListBox();  // populate ListBoxOrg
                   this.listBoxOrgs.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in Copying table: " + ex.Message);
                }
                {

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }

            }   // if yes
        }

        private void UpdateOrgListBox()
        {
            #region update poll list
            c_cuidList.Clear();
            this.listBoxOrgs.Items.Clear();

            if (!inst.admin)
            {
                c_cuidList.Add(custid); // p cust id
                this.listBoxOrgs.Items.Add(org);
                UpdatePollList();
                UpdateLocInCombo(custid);
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand(SqlCustomers, conn);
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    int cnt = 0;
                    while (reader.Read())
                    {
                        c_cuidList.Add(Convert.ToInt32(reader[0])); // p cust id
                        this.listBoxOrgs.Items.Add(Convert.ToString(reader[1]));
                        if (org.Equals(Convert.ToString(reader[1])))
                        {
                            orgindx = cnt;
                        }
                        else
                        {
                            cnt += 1;
                        }
                    }
                    reader.Close();
                }
                if (c_cuidList.Count > 0)
                {
                    // listBoxOrgs.SelectedIndex = 0;
                    listBoxOrgs.SelectedIndex = orgindx;
                    // gen event
                    object sender = new object();
                    EventArgs e = new EventArgs();
                    this.listBoxOrgs_SelectedIndexChanged(sender, e);
                }
            }
            #endregion

        }
        private void UpdateLocInCombo(int org_id)
        {

            String SqlOrgSel = String.Format(@"SELECT Cust_Id,Location,Gen_id FROM Generators WHERE Cust_id={0}", org_id);
            //comboGenLoc.Items.Clear();
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
                   // g_pollList.Clear();
                    //locList.Clear();
                    genidList.Clear();
                    listGenLoc.Items.Clear();   // rem combo
                    while (reader.Read())
                    {
                        g_cuidList.Add(Convert.ToInt32(reader[0])); // p cust id
                        String loc = Convert.ToString(reader[1]);
                        String gid = Convert.ToString(reader[2]);
                        if (locList.Contains(loc) && g_pollList.Contains(gid.Trim()))
                            loc = loc + "  => Intensive";
                        this.listGenLoc.Items.Add(loc);
                        genidList.Add(gid);
                        
                    }
                    reader.Close();
                }
                
                    if (this.listGenLoc.Items.Count > 0)    // rep combobox
                    this.listGenLoc.SelectedIndex = 0;  // select first element

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ListBox index change : " + ex.Message);

            }
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
     }


        

        private void listBoxOrgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = listBoxOrgs.SelectedIndex + 1;
            if (!inst.admin)
                id = custid;
            UpdatePollList();   // update Gen polls list
            UpdateLocInCombo(id);
 
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

               /* using (SqlCommand cmd = new SqlCommand(SqlPCustPoll1, conn))
                {
                    cmd.CommandTimeout = 60;
                    cmd.ExecuteNonQuery();
                }
               */
                UpdatePollList();   // update Gen polls list
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Enabled = false;
            timer1.Stop();
            //if (this.usbPollport.IsOpen)    // if modem OK
           // {
                if (Toggle && PollChk)
                {
                    picPollGreenLed.Enabled = true;
                    picPollGreenLed.Show();
                    picPollStopedLed.Enabled = false;
                    picPollStopedLed.Hide();
                    lblPolling.Show();
                    lblStoppedLed.Hide();
                    Toggle = false;
                }
                else
                    if (!Toggle && PollChk)
                    {
                        picPollGreenLed.Enabled = false;
                        picPollGreenLed.Hide();
                        picPollStopedLed.Enabled = true;
                        picPollStopedLed.Show();
                        lblPolling.Hide();
                        lblStoppedLed.Show();
                        Toggle = true;
                    }
                    else
                        if (rbTimer.Checked)
                        {
                            picPollGreenLed.Enabled = true;
                            picPollGreenLed.Show();
                            lblPolling.Show();
                            picPollStopedLed.Enabled = false;
                            picPollStopedLed.Hide();
                            lblStoppedLed.Hide();
                        }


                // Do  Poll
                for (int x = 0; x < g_IntensivePoll.Count; x++)   // g_pollList.Count; x++)
                {

                    if (g_IntensivePoll[x] != null)  //                
                    {
                        String message = null;
                        String Gen_mobile = (String)g_IntensivePoll[x];
                        if (Gen_mobile.Contains("+") && Gen_mobile.Length >= GEN_ID_DIGITS)   //
                        {
                            if (!PollChk)
                            {
                                message = "REPORT=" + ConfigurationManager.AppSettings["GenPollTime"];
                                if (Configured)
                                    message = "REPORT=" + PollTimer;
                            }
                            else
                            {
                                message = "REPORT=P";
                            }

                            if (pollmodem.OK)
                            {
                                bool ok = pollmodem.Alert(message, Gen_mobile);
                                // gprsPoll.sendSMSMsg(this.usbPollport, Gen_mobile, message);
                                Thread.Sleep(100);
                            }     //Gen_mobile = "+923363111563";     //"+923332383079" A Hayee;
                        }
                    }
                }// for gx count 
                // gprsPoll.ClosePort();
                // Thread.Sleep(1000);
                // picPollGreenLed.Enabled = false;

            // }
              if (Configured)
              {
                  timer1.Interval = PollTimer * 1000;
              }
              else
              {
                  timer1.Interval  = Convert.ToInt32(ConfigurationManager.AppSettings["GenPollTime"]) * 1000;
              }
              if (!PollChk)
              {
                  g_IntensivePoll.Clear();   // clear Intensive Timer based poll once
              }
               timer1.Enabled = true;
               timer1.Start();    
        }

        private void btnAddPollGen_Click(object sender, EventArgs e)
        {
            List<int> cid = new List<int>();
            List<String> gid = new List<string>();
            List<string> loc = new List<string>();
            List<string> orgs = new List<string>();  // all orgs

            //String SqlPSetEGenPoll1 = String.Format(@"UPDATE PollGenerators SET poll = '1' WHERE Cust_Id ={0} AND Gen_Id='{1}'", g_cuidList[comboGenLoc.SelectedIndex], genidList[comboGenLoc.SelectedIndex]);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                orgs.Clear();
               // g_IntensivePoll.Clear();
                if (checkBoxAllOrgs.Checked)
                {
                    String SqlAllOrgs = @"SELECT Organization FROM Customers";  // add all orgs
                    SqlCommand cmd_org = new SqlCommand(SqlAllOrgs,conn);
                    using(SqlDataReader rdr = cmd_org.ExecuteReader())
                    {
                       while(rdr.Read())
                       {
                           orgs.Add(Convert.ToString(rdr[0]));
                       }
                        rdr.Close();
                    }
                    
                }
                else
                {
                    if (!inst.admin)
                    {
                        listBoxOrgs.SelectedIndex = 0;
                    }
                    orgs.Add(listBoxOrgs.SelectedItem.ToString()); // add selected org
                }

                if (checkBoxLocGen.Checked)
                {
                    for (int y = 0; y < orgs.Count; y++)     // get current org and get its members
                    {
                        cid.Clear();
                        gid.Clear();
                        loc.Clear();
                        String SqlLocs = String.Format(@"SELECT g.Cust_id,g.Gen_id,g.Location FROM Customers c,Generators g WHERE c.Cust_Id = g.Cust_Id AND c.Organization ='{0}'", orgs[y]);
                        SqlCommand cmd = new SqlCommand(SqlLocs, conn);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cid.Add(Convert.ToInt32(reader[0]));
                                gid.Add(Convert.ToString(reader[1]));
                                loc.Add(Convert.ToString(reader[2]));
                            }
                            reader.Close();
                            for (int x = 0; x < cid.Count; x++)
                            {
                                UpdatePollGenerator(cid[x], gid[x], loc[x]);    // copy Gen to Poll gen as tables have been truncated
                                g_IntensivePoll.Add(gid[x]);    // used by timer to intensive poll
                            }
                        }
                    }
                }
                else  // update single loc
                {
                    int cid0 = g_cuidList[listGenLoc.SelectedIndex];
                    String gid0 = genidList[listGenLoc.SelectedIndex];
                    String loc0 = listGenLoc.SelectedItem.ToString();
                    UpdatePollGenerator(cid0, gid0, loc0);    // copy Gen to Poll gen as tables have been truncated
                    g_IntensivePoll.Add(gid0.Trim());
                }
                 object sender2 = new object();
                 EventArgs e2 = new EventArgs();
                listBoxOrgs_SelectedIndexChanged(sender2, e2);  // refresh display
                UpdatePollList();   // update Gen polls list i.e only with those that are to be polled
            }
            catch (Exception ex)
            {

            }
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

       
        private void btnLocGenRemove_Click(object sender, EventArgs e)
        {
            // Remove  poll gen set  PollCust & PollGen Poll bit = 0'
           // String SqlPGenPoll0 = String.Format(@"UPDATE PollGenerators SET poll = 0 WHERE Cust_Id = {0}",g_cuidList[comboGenLoc.SelectedIndex]); 
           // String SqlPGenSetPoll0 = String.Format(@"UPDATE PollGenerators SET poll = 0  WHERE Cust_Id ={0} AND Gen_Id='{1}'", g_cuidList[comboGenLoc.SelectedIndex], genidList[comboGenLoc.SelectedIndex]);
            String SqlPGenPoll0 = String.Format(@"UPDATE PollGenerators SET poll = 0 WHERE Cust_Id = {0}", g_cuidList[listGenLoc.SelectedIndex]);
            String SqlPGenSetPoll0 = String.Format(@"UPDATE PollGenerators SET poll = 0  WHERE Cust_Id ={0} AND Gen_Id='{1}'", g_cuidList[listGenLoc.SelectedIndex], genidList[listGenLoc.SelectedIndex]);
            
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
                    if (rbTimer.Checked)
                    {
                        // Send REPORT=0  to stop intensive monitoring
                        for (int x = 0; x < genidList.Count; x++)
                        {
                            String Gen_mobile = genidList[x];
                            if (Gen_mobile.Contains("+") && Gen_mobile.Length >= GEN_ID_DIGITS)   //
                            {
                                String message = "REPORT=0";      // Stop Intensive Monitoring
                                gprsPoll.sendSMSMsg(this.usbPollport, Gen_mobile, message);
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand(SqlPGenSetPoll0, conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                    }

                    // Send : REPORT=0
                     String Gen_mobile = genidList[listGenLoc.SelectedIndex];  // 
                    if (Gen_mobile.Contains("+") && Gen_mobile.Length >= GEN_ID_DIGITS)   //
                    {
                        if (rbTimer.Checked)  // only send report=0 in timed polling mode.
                        {
                            String message = "REPORT=0";      // Stop Intensive Monitoring
                            try
                            {
                                gprsPoll.sendSMSMsg(this.usbPollport, Gen_mobile, message);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Modem error");
                            }

                            Thread.Sleep(1000);
                        }
                    }     //Gen_mobile = "+923363111563";     //"+923332383079" A Hayee;
                    //
                    UpdatePollList();   // update Gen polls list
                }
                object sender2 = new object();
                EventArgs e2 = new EventArgs();
                listBoxOrgs_SelectedIndexChanged(sender2, e2);  // refresh display
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void checkBoxLocGen_CheckedChanged(object sender, EventArgs e)
        {
          // status checked else where 
        }      

       
        private void btnBack_Click(object sender, EventArgs e)
        {
            inst.org = org;
            inst.cust_id = custid;
            inst.indx = indx;
           // inst.sts = true;   ***** PA
            TabControl tc = inst.tc;
            tc.SelectedIndex = 0;    // admin page
        }

       
        private void comboGenLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void rbPoll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPoll.Checked)
            {
                inst.PollChk =true;
                PollChk = true;
            }
           
        }

        private void rbTimer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTimer.Checked)
            {
                inst.PollChk = false;
                PollChk = false;
            } 
         }

        private void PollGens_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gprsPoll != null) gprsPoll.ClosePort(this.usbPollport);
            if (conn.State == ConnectionState.Open) conn.Close();
        }

     }     //  class
}      // form
                                        