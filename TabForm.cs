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
using System.Threading;

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
        ReportsForm ctrlReportsPage = null;
        ConfigForm ctrlConfigPage = null;
        meter ctrlMeterPage = null;
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
           // inst.admin = true;
            inst.sts = true;    //***** PA
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

                case 5:   // Reports
                     if (inst.sts)
                    {
                        ctrlReportsPage = null; // destroy previous form
                        if (ctrlReportsPage == null)
                        {
                            ctrlReportsPage = new ReportsForm(inst); //(2, DateTime.Now, "Aei", 0);   //aei =3
                        }

                        if (ctrlReportsPage != null)
                        {
                            ctrlReportsPage.TopLevel = false;
                            panel6.Controls.Add(ctrlReportsPage);
                            ctrlReportsPage.Show();
                        }
                        //inst.admin = false;
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
                        //inst.admin = false;
                    }
                    break;

                case 7:
                    if (inst.sts)
                    {
                        if (ctrlMeterPage == null)
                        {
                            ctrlMeterPage = new meter(inst); 
                        }

                        if (ctrlMeterPage != null)
                        {
                            ctrlMeterPage.TopLevel = false;
                            panel8.Controls.Add(ctrlMeterPage);
                            ctrlMeterPage.Show();
                        }
                    }
                    break;

                default: break;
            }
        }

        private void TabForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
         }

    }    //class
}       //ns
