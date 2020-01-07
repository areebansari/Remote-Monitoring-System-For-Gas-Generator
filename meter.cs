using System;
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

namespace SmsMon
{
    public partial class meter : Form
    {
        private decimal[] volts = new decimal[3];
        Decimal engRunhrs = 0;
        decimal status = 0;
        private decimal maint_due = 0, days = 0, months = 0, hours = 0;
        public String genid = null;
        SqlConnection conn;
        CultureInfo culture = new CultureInfo("en-GB");
        int indx = 0;
        param inst;
        int custid;
        List<String> GeneratorList = new List<String>();
        //private StringBuilder sb_err = null;      // holds error files
        TabControl tc;

        public meter(param p_inst)
        {
            InitializeComponent();
            inst = p_inst;
            genid = inst.gen_id;
            custid = inst.cust_id;
            conn = inst.conn;   // db connection
            indx = inst.indx;
            tc = inst.tc;   // used to select next index
            //Config(p_inst);
            timer1.Interval = 5000; //timer interval 5 secs
            timer1.Start();
            timer1.Tick += new EventHandler(TimerEventProcessor);
            String SqlGens = String.Format(@"SELECT Gen_id FROM Generators WHERE Cust_id ={0}", custid);
            String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
            conn = new SqlConnection(dbconn);
            SqlDataReader drg = null;

#region initialization
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmdg = new SqlCommand(SqlGens, conn))
                {
                    //cmdg.CommandTimeout = 60;   // 60 ms time out
                    drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                    gen_id.Items.Clear();
                    while (drg.Read())
                    {
                        genid = drg.GetString(0).Trim();
                        GeneratorList.Add(genid);
                        gen_id.Items.Add(genid);
                    }

                    drg.Close();

                    indx = GeneratorList.IndexOf(genid.Trim());
                    gen_id.SelectedIndex = indx; // comboBox1.Items.IndexOf(genid.Trim()); 
                    gen_id.Show();

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
#endregion
        }

        private void aquaGauge1_Load(object sender, EventArgs e) //Update the value of gauge accordingly when program is executed
        {
            aquaGauge1.Refresh();
            aquaGauge1.Value = (float)volts[0];
            lblvolt0.Text = volts[0].ToString() + "V";
            lblvolt0.ForeColor = System.Drawing.Color.Black;
        }

#region updatevalues 
        private void TimerEventProcessor(object sender, EventArgs e) //after every 5 secs the value will be updated if there is any change in sql database
        {                                                           //note that 5sec time is only for testing 
                                                                   //update time can or will be changed according to the need     
                                     
            String SQLVolData = String.Format(@"SELECT TOP 1 V1,V2,V3 FROM Data WHERE Gen_Id=(SELECT MAX({0})) ORDER BY Id DESC", gen_id.Text); /* Command line here are just for testing */
            String SQLMaintData = String.Format(@"SELECT TOP 1 MaintDue From Data WHERE Gen_Id=(SELECT MAX({0})) ORDER BY Id DESC", gen_id.Text); /*   will be updated/improvised */
            String SQLAlarm = String.Format(@"SELECT TOP 1 PanelDoorOpen From Alarms WHERE Gen_Id=(SELECT MAX({0})) ORDER BY Id DESC", gen_id.Text); /*  later in the project      */
            String SQLEngData = String.Format(@"SELECT TOP 1 EngRunTime From Data WHERE Gen_Id=(SELECT MAX({0})) ORDER BY Id DESC", gen_id.Text); 
            String dbconn = ConfigurationManager.AppSettings["ConnectionString"];
            conn = new SqlConnection(dbconn);
            SqlDataReader drg = null;

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmdg = new SqlCommand(SQLVolData, conn))
                {
                    cmdg.CommandTimeout = 60;
                    drg = cmdg.ExecuteReader();
                    while (drg.Read())
                    {
                        volts[0] = drg.GetDecimal(0);
                        volts[1] = drg.GetDecimal(1);
                        volts[2] = drg.GetDecimal(2);
                    }

                    drg.Close();

                    aquaGauge1.Value = (float)volts[0];
                    lblvolt0.Text = volts[0].ToString() + "V";
                    aquaGauge1.Refresh();
                    
                }

                using (SqlCommand cmdg = new SqlCommand(SQLMaintData, conn))
                {
                    cmdg.CommandTimeout = 60;
                    drg = cmdg.ExecuteReader();
                    while (drg.Read())
                    {
                        maint_due = drg.GetDecimal(0);
                    }

                    drg.Close();

                    months = DaysMonths(Convert.ToInt32(maint_due)).Item1;
                    days = DaysMonths(Convert.ToInt32(maint_due)).Item2;
                    hours = DaysMonths(Convert.ToInt32(maint_due)).Item3;
                    maintDue.Text = String.Format("Months: {0}, Days: {1}, Hours: {2}", months, days, hours);
                    maintDays.Text = days.ToString();
                    maintHours.Text = hours.ToString();
                    maintMonths.Text = months.ToString();
                }

                using (SqlCommand cmdg = new SqlCommand(SQLAlarm, conn))
                {
                    cmdg.CommandTimeout = 60;
                    drg = cmdg.ExecuteReader();
                    while (drg.Read())
                    {
                        status = drg.GetDecimal(0);
                    }

                    drg.Close();

                    if (status == 2) maintDue.BackColor = System.Drawing.Color.Red;
                    else maintDue.BackColor = System.Drawing.SystemColors.Window;
                }

                using (SqlCommand cmdg = new SqlCommand(SQLEngData, conn))
                {
                    cmdg.CommandTimeout = 60;
                    drg = cmdg.ExecuteReader();
                    while (drg.Read())
                    {
                        engRunhrs = drg.GetDecimal(0);
                    }
                }

                drg.Close();

                engRhrs.Text = engRunhrs.ToString().Trim();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
#endregion
        private static Tuple<Decimal, Decimal, Decimal> DaysMonths(Int32 hours)
        {
            float Days = hours / 24;
            Decimal Months = 0;

            while (Days > 30)
            {
                Months++; 
                Days-=30;
            }

            Decimal Hours = 0;
            Hours = hours - (Months * 30 * 24);
            Hours = Hours - ((Decimal)Days*24);

            var val = new Tuple<Decimal, Decimal, Decimal>(Months, Convert.ToDecimal(Days), Hours);
            return val;
        }

        private void meter_closing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }
}
