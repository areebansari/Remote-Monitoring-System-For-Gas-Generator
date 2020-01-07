using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;


namespace SmsMon
{
    public partial class TabForm : Form
    {
        int curTab = 0;

        public int custid = 0;
        public String Genid = null;
        public DateTime dt = DateTime.Now;
        String organization = null;
        public int index = 0;
        public bool OK_STS = false;
        
        // declare forms as control
        Admin ctrlAdminPage = null;
        Form1 ctrlMainPage = null;
        StatusForm ctrlAlarmsPage = null;
        LogsForm ctrlLogsPage = null;
        PollGens ctrlPollPage = null;
        ConfigForm ctrlConfigPage = null;
        SqlConnection conn = null;

        public param inst = param.instance; // used to pass paramters to each form
        String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
        
    public TabForm()
    {
        InitializeComponent();
        inst.tc = tabControl1;   // tabcontrol to be used in other forms
            
        conn = new SqlConnection(dbconn);
        try
        {
            // conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
             }
            inst.conn = conn;            
            // Make control Page
                          
            ctrlAdminPage = new Admin(inst);   // add to Panel1 & use button2 to show 
            ctrlAdminPage.TopLevel = false;
            panel1.Controls.Add(ctrlAdminPage);
            ctrlAdminPage.Show();

            tabControl1.SelectedIndexChanged += new EventHandler(SelectedIndex_changed); //(sender Object,EventArgs e);
 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in form: " + ex.Message);
            }

       }   // const

        private void TabForm_Load(object sender, EventArgs e)
        {
            
        }

        private void SelectedIndex_changed(object sender, EventArgs e)
        {
            // if(curTab == (int)(sender as TabControl).SelectedIndex)
            //  return;

            int curTab = (int)(sender as TabControl).SelectedIndex;

            switch (curTab)
            {
                case 0:
                    if (ctrlAdminPage != null)
                    {
                        ctrlAdminPage.TopLevel = false;
                        panel1.Controls.Add(ctrlAdminPage);
                        ctrlAdminPage.Show();
                    }

                    break;
                case 1:
                    if (inst.sts)
                    {
                        if (ctrlMainPage == null)
                        {
                            ctrlMainPage = new Form1(inst); //(2, DateTime.Now, "Aei", 0);
                        }
                        else
                        {
                            ctrlMainPage.Config(inst);
                        }
                        if (ctrlMainPage != null)
                        {
                            ctrlMainPage.TopLevel = false;
                            panel2.Controls.Add(ctrlMainPage);
                            ctrlMainPage.Show();
                        }
                    }
                    break;
                case 2:
                    if (inst.sts)
                    {
                        if (ctrlAlarmsPage == null)
                        {
                            ctrlAlarmsPage = new StatusForm(inst); //("+923312551725", 3);    //aei =3
                        }
                        else
                        {
                            ctrlAlarmsPage.Config(inst);
                        }
                        if (ctrlAlarmsPage != null)
                        {
                            ctrlAlarmsPage.TopLevel = false;

                            panel3.Controls.Add(ctrlAlarmsPage);
                            ctrlAlarmsPage.Show();
                        }
                    }
                    break;
                case 3:
                    if (inst.sts)
                    {
                        if (ctrlLogsPage == null)
                        {
                            ctrlLogsPage = new LogsForm(inst); //("+923312551725", DateTime.Now.ToShortDateString());    //aei =3
                        }
                        else
                        {
                            ctrlLogsPage.Config(inst);
                        }
                        if (ctrlLogsPage != null)
                        {
                            ctrlLogsPage.TopLevel = false;
                            panel4.Controls.Add(ctrlLogsPage);
                            ctrlLogsPage.Show();
                        }
                    }
                    break;

                case 4:
                    if (inst.sts)
                    {
                        if(ctrlPollPage == null)
                        {
                            ctrlPollPage = new PollGens(inst); //(2, DateTime.Now, "Aei", 0);   //aei =3
                        }
                        else
                        {
                            ctrlPollPage.Config(inst);
                        }
                        if (ctrlPollPage != null)
                        {
                            ctrlPollPage.TopLevel = false;
                            panel5.Controls.Add(ctrlPollPage);
                            ctrlPollPage.Show();
                        }
                    }
                    break;

                case 5:
                    if (inst.sts)
                    {
                        // to do
                    }
                    break;

                case 6:
                    if (inst.sts)
                    {
                        if (ctrlConfigPage == null)
                        {
                            ctrlConfigPage = new ConfigForm(inst); //(2, DateTime.Now, "Aei", 0);   //aei =3
                        }
                       
                        if (ctrlConfigPage != null)
                        {
                            ctrlConfigPage.TopLevel = false;
                            panel7.Controls.Add(ctrlConfigPage);
                            ctrlConfigPage.Show();
                        }
                    }
                    break;

                default: break;
            }
        }

        
        private void TabForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ctrlAlarmsPage != null) ctrlAlarmsPage.Dispose();
            if (ctrlLogsPage != null) ctrlLogsPage.Dispose();
            if (ctrlPollPage != null) ctrlPollPage.Dispose();  // make sure COM  port is released.
            if (ctrlAdminPage != null) ctrlAdminPage.Dispose();
            if (ctrlMainPage != null) ctrlMainPage.Dispose();  // make sure COM  port is released.
        }

        

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxRxModem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TabForm_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

              

    }    //class
}       //ns
