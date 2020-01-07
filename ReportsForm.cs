using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmsMon
{

    public partial class ReportsForm : Form
    {
        private param inst;
        public String genid;
        public int custid;
        SqlConnection conn = null;
        List<decimal> RxList2 = new List<decimal>();    // Rx'd Alarm ListBox
        private String org = null;
        List<String> CustGenList = new List<String>();  //To update combo box and Alarm list box
        List<String> CustLocList = new List<String>();  //To update combo box and Alarm Location list
        String genparam = "*";

        public ReportsForm(param p_inst)
        {
            InitializeComponent();
            Config(p_inst);
        }

        private void Config(param p_inst)
        {
            inst = p_inst;
            conn = inst.conn;   // db connection
            genid = inst.gen_id;
            custid = inst.cust_id;
            //tc = inst.tc;             // for backbutton
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
                    org = "";
                    String loc = "";
                    while (drg.Read())
                    {
                        org = drg.GetString(0);
                        loc = drg.GetString(1);
                    }
                    lblLocation.Text = "Location: " + loc;
                    lblOrganization.Text = "Organization: " + org;
                    //blGenerator.Text = "Genertor No: " + genid;
                    //blDateTime.Text = DateTime.Now.ToString("G", new CultureInfo("en-GB"));

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
                   
                    int indx = CustGenList.IndexOf(genid.Trim());
                    comboBox1.SelectedIndex = indx; // comboBox1.Items.IndexOf(genid.Trim()); 
                    comboBox1.Show();
                }
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
        /// <summary>
        /// ////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.Data' table. You can move, or remove it, as needed.
          //  this.DataTableAdapter.Fill(this.DataSet1.Data,);

          //  this.reportViewer1.RefreshReport();
            // this.reportViewer2.RefreshReport();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            genid = (String)comboBox1.SelectedItem;
            lblGenerator.Text = "Generator: ";
            lblGenerator.Text = lblGenerator.Text + genid;
            lblLocation.Text = "Location: ";
            lblLocation.Text = lblLocation.Text + CustLocList[comboBox1.SelectedIndex].ToString();

            DateTime dateTime = dateTimePicker1.Value;
            String dt = dateTime.ToShortDateString(); 
            this.DataTableAdapter.Fill(this.DataSet1.Data, genid,dt);

            this.reportViewer1.RefreshReport();

        }

       
    }
}