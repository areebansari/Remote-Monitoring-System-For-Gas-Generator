using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
 using System.Data.SqlClient;
using System.Configuration;

namespace SmsMon
{
    public partial class LogsForm : Form
    {

        private SqlConnection conn = null;
        private String Sqldbstr = null;
        private BindingSource bindingSource = new BindingSource();
        //private DataTable dt = new DataTable();
        private String gen_id = null;
        private String rx_date = null;
        private String org = null;
        param inst;
        TabControl tc;

        public LogsForm(param p_inst) //(String genid,String cd)
        {

            InitializeComponent();
            Config(p_inst);
        }

         public void Config(param p_inst)
        {
            inst = p_inst;
            conn = inst.conn;   // db connection 
            gen_id = inst.gen_id;
            rx_date = dateTimePicker1.Value.ToString("dd/MM/yyyy"); //inst.dt;
            org = inst.org;
            tc = inst.tc;

            lblHistoricalData.Text = org + " Historical Data.  Generator: " + gen_id + ",  Date: " + rx_date;
            ClearButtons();
            btnGenData.BackColor = Color.Green;
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            String SQLGenData = String.Format(@"SELECT * FROM Data WHERE Gen_Id='{0}' AND Date='{1}' ORDER BY Time ASC",gen_id,rx_date);
            Sqldbstr = ConfigurationManager.AppSettings["connectionString"];
            conn = new SqlConnection(Sqldbstr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQLGenData, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    bindingSource.DataSource = dt;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    dataGridView1.DataSource = bindingSource;
                    dataGridView1.Enabled = true;
                    dataGridView1.Show();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tc.SelectedIndex = 1; // SMS main
            //this.Hide();
        }

        private void LogsForm_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }

       /* private void reportViewer1_Load(object sender, EventArgs e)
        {

        }  */



        private void bGenData_Click(object sender, EventArgs e)
        {
            String SQLGenData = String.Format(@"SELECT * FROM Data WHERE Gen_Id='{0}' AND Date='{1}' ORDER BY Time ASC", gen_id, rx_date);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataGridView1.Columns.Clear();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQLGenData, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    bindingSource.DataSource = dt;
                    //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    dataGridView1.Width = 700;
                    dataGridView1.Anchor = AnchorStyles.Left;
                    dataGridView1.DataSource = bindingSource;
                    dataGridView1.Enabled = true;
                    dataGridView1.Show();
                }
                ClearButtons();
                btnGenData.BackColor = Color.Green;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            String SQLGenData = String.Format(@"SELECT * FROM Alarms WHERE Gen_Id='{0}' AND Date='{1}' ORDER BY Time ASC", gen_id, rx_date);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataGridView1.Columns.Clear();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQLGenData, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    bindingSource.DataSource = dt;
                    //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    dataGridView1.Width = 800; // AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                    dataGridView1.Anchor = AnchorStyles.Left;
                    dataGridView1.DataSource = bindingSource;
                    dataGridView1.Enabled = true;
                    dataGridView1.Show();
                }
                ClearButtons();
                btnAlarm.BackColor = Color.Green;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnGenStart_Click(object sender, EventArgs e)
        {
            String SQLGenData = String.Format(@"SELECT * FROM GenStart WHERE Gen_Id='{0}' AND Date='{1}' ORDER BY Time ASC", gen_id, rx_date);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataGridView1.Columns.Clear();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQLGenData, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    bindingSource.DataSource = dt;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    dataGridView1.DataSource = bindingSource;
                    dataGridView1.Enabled = true;
                    dataGridView1.Show();
                }
                ClearButtons();
                btnGenStart.BackColor = Color.Green;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSmsStart_Click(object sender, EventArgs e)
        {
            String SQLGenData = String.Format(@"SELECT * FROM GsmStart WHERE Gen_Id='{0}' AND Date='{1}' ORDER BY Time ASC", gen_id, rx_date);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataGridView1.Columns.Clear();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQLGenData, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    bindingSource.DataSource = dt;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    dataGridView1.DataSource = bindingSource;
                    dataGridView1.Enabled = true;
                    dataGridView1.Show();
                }
                ClearButtons();
                btnSmsStart.BackColor = Color.Green;
            }
            catch (Exception ex)
            {

            }
        }

        private void ClearButtons()
        {
            btnAlarm.BackColor = Color.Red;
            btnGenData.BackColor = Color.Red;
            btnGenStart.BackColor = Color.Red;
            btnSmsStart.BackColor = Color.Red;
            btnGenFuel.BackColor = Color.Red;
            btnService.BackColor = Color.Red;
        }

        private void btnGenFuel_Click(object sender, EventArgs e)
        {
            String SQLGenData = String.Format(@"SELECT gf.typeData,g.location,gf.Gen_id,g.MgrMob,gf.Date,gf.time,gf.Fuel as Litres
                                                FROM Generators g,genFuel gf
                                                WHERE g.mgrmob = gf.mob_id AND g.Gen_id = gf.Gen_id
                                                AND gf.Gen_id ='{0}' AND  gf.Date ='{1}'", gen_id, rx_date);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataGridView1.Columns.Clear();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQLGenData, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    bindingSource.DataSource = dt;
                    //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                    dataGridView1.DataSource = bindingSource;
                    dataGridView1.Enabled = true;
                    dataGridView1.Show();
                }
                ClearButtons();
               btnGenFuel.BackColor = Color.Green;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {

            String SQLGenData = String.Format(@"SELECT gs.typeData,g.location,gs.Gen_id,g.MgrMob,gs.Date,gs.time
                FROM Generators g,genService gs WHERE g.mgrmob = gs.mob_id AND g.Gen_id = gs.Gen_id
                AND gs.Gen_id ='{0}' AND  gs.Date ='{1}'", gen_id, rx_date);

            //String SQLGenData = String.Format(@"SELECT * FROM GenService WHERE Gen_Id='{0}' AND Date='{1}'", gen_id, rx_date);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataGridView1.Columns.Clear();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQLGenData, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    bindingSource.DataSource = dt;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    dataGridView1.DataSource = bindingSource;
                    dataGridView1.Enabled = true;
                    dataGridView1.Show();
                }
                ClearButtons();
                btnService.BackColor = Color.Green;
            }
            catch (Exception ex)
            {

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            rx_date = dateTimePicker1.Value.ToString("dd/MM/yyyy");
        }

        
    }
}
