﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.Threading;

namespace SmsMon
{
    public partial class StatusForm : Form            
    {
        public String genid;
        public int custid;
        SqlConnection conn = null;
        //SqlDataReader gens = null;
        CultureInfo culture = new CultureInfo("en-GB");
        List<decimal> RxList = new List<decimal>();    // Rx'd Alarm Status display
        List<decimal> RxList2 = new List<decimal>();    // Rx'd Alarm ListBox
        List<PictureBox> alarmList = new List<PictureBox>();
        List<String> CustGenList = new List<String>();  //To update combo box and Alarm list box
        List<String> CustLocList = new List<String>();  //To update combo box and Alarm Location list
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        param inst;
        TabControl tc;

        public StatusForm(param p_inst) //String gen_id, int cust_id)
        {
            InitializeComponent();
            Config(p_inst);
        }

        public void Config(param p_inst)
        {
            inst = p_inst;
            conn = inst.conn;   // db connection
            genid = inst.gen_id;
            custid = inst.cust_id;
            tc = inst.tc;             // for backbutton
            // Update combo box
            String SqlGens = String.Format(@"SELECT Gen_id,Location FROM Generators WHERE Cust_id ={0}", custid);
            String SqlOrg = String.Format(@"SELECT c.Organization,g.Location FROM Customers c,Generators g WHERE c.Cust_id = g.Cust_id AND c.Cust_Id= {0} AND g.Gen_id ='{1}'", custid, genid);

           // String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlDataReader drg = null;

            //conn = new SqlConnection(dbconn);
            try
            {

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmdg = new SqlCommand(SqlOrg, conn))
                {
                    cmdg.CommandTimeout = 60;   // 60 ms time out
                    drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                    String org = "";
                    String loc = "";
                    while (drg.Read())
                    {
                        org = drg.GetString(0);
                        loc = drg.GetString(1);
                    }
                    lblLocation.Text = "Location: " + loc;
                    lblOrganization.Text = "Organization: " + org;
                    lblGenerator.Text = "Genertor No: " + genid;
                    lblDateTime.Text = DateTime.Now.ToString("G", culture);
                    timer1.Interval = 10000; //10 sec alarm poll time
                    timer1.Enabled = true;
                }
                drg.Close();

                using (SqlCommand cmdg = new SqlCommand(SqlGens, conn))
                {
                    cmdg.CommandTimeout = 60;   // 60 ms time out
                    drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                    CustGenList.Clear();
                    comboBox1.Items.Clear();
                    CustLocList.Clear();
                    while (drg.Read())
                    {
                        CustGenList.Add(drg.GetString(0).Trim());
                        comboBox1.Items.Add(drg.GetString(0).Trim());
                        CustLocList.Add(drg.GetString(1));
                    }

                    int indx = CustGenList.IndexOf(genid);
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(genid); 
                    comboBox1.Show();

                }
                alarmList.Add(this.pictureBox1);
                alarmList.Add(this.pictureBox2);
                alarmList.Add(this.pictureBox3);
                alarmList.Add(this.pictureBox4);
                alarmList.Add(this.pictureBox5);
                alarmList.Add(this.pictureBox6);
                alarmList.Add(this.pictureBox7);
                alarmList.Add(this.pictureBox8);               
                alarmList.Add(this.pictureBox9);
                alarmList.Add(this.pictureBox10);
                alarmList.Add(this.pictureBox11);
                alarmList.Add(this.pictureBox12);
                alarmList.Add(this.pictureBox13);
                alarmList.Add(this.pictureBox14);
                alarmList.Add(this.pictureBox15);
                alarmList.Add(this.pictureBox16);
            }
            catch (Exception ex)
            {
                if (!drg.IsClosed) drg.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            finally
            {
                if (!drg.IsClosed) drg.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // back button
        private void button1_Click(object sender, EventArgs e)
        {
            
             inst.gen_id = genid;

             inst.indx = CustGenList.IndexOf(genid);
            tc.SelectedIndex = 1;  // go to Sms Main page
            //if (backgroundWorker1.IsBusy) _resetEvent.Set();  // terminate thread
            //this.Hide();
        }
           
        

        private void StatusForm_Load(object sender, EventArgs e)
        {

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text =  DateTime.Now.ToString("G", culture);
            timer1.Enabled = false;    //dissable timer
            if (!backgroundWorker1.IsBusy)
              backgroundWorker1.RunWorkerAsync();
              
           _resetEvent.WaitOne();      // wait here for doWork to complete
             
           // DisplayAlarms();
            timer1.Interval = 10000;     // next poll in 10 sec
            timer1.Enabled = true;
        }

        private void StatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (backgroundWorker1.IsBusy) _resetEvent.Set();  // terminate thread
            timer1.Enabled = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            if (backgroundWorker1.IsBusy) backgroundWorker1.CancelAsync();  // stop background  thread

           
        }

         //private void DisplayAlarms()
         private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
         {
            // Do work here
            String SqlLastAlarm = String.Format(@"SELECT TOP 1 * FROM Alarms WHERE Gen_id = '{0}' ORDER BY Id DESC", genid);
             String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlDataReader drg = null;

            conn = new SqlConnection(dbconn);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmdg = new SqlCommand(SqlLastAlarm, conn))
                {
                    cmdg.CommandTimeout = 60;   // 60 ms time out
                    drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                    RxList.Clear();
                    String date = null;
                    String time = null;
                    
                    while (drg.Read())
                    {   //  0-4 other data inc id,type,date,time & genid
                        date = drg.GetString(2);
                        time = drg.GetString(3);
                        RxList.Add(drg.GetDecimal(5));
                        RxList.Add(drg.GetDecimal(6));
                        RxList.Add(drg.GetDecimal(7));
                        RxList.Add(drg.GetDecimal(8));
                        RxList.Add(drg.GetDecimal(9));
                        RxList.Add(drg.GetDecimal(10));
                        RxList.Add(drg.GetDecimal(11));
                        RxList.Add(drg.GetDecimal(12));
                        RxList.Add(drg.GetDecimal(13));
                        RxList.Add(drg.GetDecimal(14));
                        RxList.Add(drg.GetDecimal(15));
                        RxList.Add(drg.GetDecimal(16));
                        RxList.Add(drg.GetDecimal(17));
                        RxList.Add(drg.GetDecimal(18));
                        RxList.Add(drg.GetDecimal(19));
                        RxList.Add(drg.GetDecimal(20));
                    }
                    drg.Close();
                    //String alarmdate = date + " " + time;
                    DisplayStatus(RxList);
                   String dt = "Last Polled: " + date + "," + time;
                   this.BeginInvoke(new Action<string>(DisplayAlarmLastDate), dt);

                    // Process other gens Alarms
                    if (CustGenList.Count > 1)
                    {
                        this.BeginInvoke(new Action(UpdateListBoxClear)); 
                        for (int i = 0; i < CustGenList.Count; i++)
                        {
                            String SqlAlarm = String.Format(@"SELECT TOP 1 * FROM Alarms WHERE Gen_id = '{0}' ORDER BY Id DESC", CustGenList[i]);
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
                            using (SqlCommand cmd = new SqlCommand(SqlAlarm, conn))
                            {
                                cmd.CommandTimeout = 60;   // 60 ms time out
                                drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                                
                                
                                while (drg.Read())
                                {   //  0-4 other data inc id,type,date,time & genid
                                    RxList2.Clear();
                                  String  date2 = drg.GetString(2);
                                   String time2 = drg.GetString(3);

                                    RxList2.Add(drg.GetDecimal(5));
                                    RxList2.Add(drg.GetDecimal(6));
                                    RxList2.Add(drg.GetDecimal(7));
                                    RxList2.Add(drg.GetDecimal(8));
                                    RxList2.Add(drg.GetDecimal(9));
                                    RxList2.Add(drg.GetDecimal(10));
                                    RxList2.Add(drg.GetDecimal(11));
                                    RxList2.Add(drg.GetDecimal(12));
                                    RxList2.Add(drg.GetDecimal(13));
                                    RxList2.Add(drg.GetDecimal(14));
                                    RxList2.Add(drg.GetDecimal(15));
                                    RxList2.Add(drg.GetDecimal(16));
                                    RxList2.Add(drg.GetDecimal(17));
                                    RxList2.Add(drg.GetDecimal(18));
                                    RxList2.Add(drg.GetDecimal(19));
                                    RxList2.Add(drg.GetDecimal(20));
                                }
                                drg.Close();

                               if (CheckAlarmCondition(RxList2))
                                {
                                    String  gen = CustGenList[i].ToString();
                                  /*  this.Invoke((MethodInvoker)delegate
                                    {
                                        this.listGenAlarms.Items.Add(gen); // runs on UI thread
                                    });
                                    */ 
                                    this.BeginInvoke(new Action<string>(UpdateListBox),gen);
                                 }  
                            }                    
                        }  //for
                    }  // if   */
                    if (conn.State == ConnectionState.Open) conn.Close();
                    // Done, release thread
                }  // using
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alarm ERROR: :" + ex.Message);
                if (!drg.IsClosed) drg.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            _resetEvent.Set(); 
         }

         private bool CheckAlarmCondition(List<Decimal> RxListx)
         {   
             bool sts = false;
             for (int i = 0; i < RxListx.Count; i++)
             {
                 if (RxListx[i] > 1) return true;
              }
             return sts;
         } //chk alarm 

         public void UpdateListBoxClear()
         {
             this.listGenAlarms.Items.Clear();
         }

         private void UpdateListBox(string gen)
         {
             this.listGenAlarms.Items.Add(gen); // runs on UI thread
             this.listGenAlarms.Enabled = true;
             this.listGenAlarms.Show();
          }
               
         private void DisplayAlarmLastDate(String dt)
         {
                                   
             this.lblLastPollDate.Text = dt;
         }

         private bool DisplayStatus(List<Decimal> RxList)
         {
            /*  4 Bit Alarm
            *   0 = not implemented         1 = Normal
            *   2 = Warning                 3 = Shutdown Alarm
            *   4 = Electrical trip
            */
            int i=0;
            bool alarm_detected = false;
            //Display images
            for (i = 0; i < RxList.Count; i++)
            {
                                          
                if (RxList[i] == 1)    // No Alarm
                {
                    alarmList[i].ImageLocation = @"..\..\images\led_grn12x12.jpg";
                }
                else
                if (RxList[i] > 1)   // Alarm
                {
                    alarmList[i].ImageLocation = @"..\..\images\red.bmp";
                    alarm_detected = true;
                 }
                else
                if (RxList[i] == 0)   // Disabled digital input
                {

                    alarmList[i].ImageLocation = @"..\..\images\gray.bmp"; 
                }
                else
                if (RxList[i] == 15)   // Alarm Not Implemented
                {

                    alarmList[i].ImageLocation = @"..\..\images\blue.bmp";
                }
            }
            return alarm_detected;
        }

         private void btnShowAlarm_Click(object sender, EventArgs e)
         {

             if (backgroundWorker1.IsBusy)
             {
                 backgroundWorker1.CancelAsync();
            }
              _resetEvent.Set();   //
             genid = (String)comboBox1.SelectedItem;
             lblGenerator.Text = "Generator: ";
             lblGenerator.Text = lblGenerator.Text  + genid;
             lblLocation.Text = "Location: ";
             lblLocation.Text = lblLocation.Text  + CustLocList[comboBox1.SelectedIndex].ToString(); 
         }

         private void panel1_Paint(object sender, PaintEventArgs e)
         {

         }

         private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
         {
             genid = comboBox1.SelectedItem.ToString();

         }


    }   //class
}  //ns
                                                                                               