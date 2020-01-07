using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;

namespace SmsMon
{
    public partial class ConfigForm : Form
    {

        private param inst;
        private string SqlAllDb = "select [name] from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')";
        private SqlConnection conn = null;
        int STEP_INTERVAL = 40;
        String ComRxModem = ConfigurationManager.AppSettings["RxModem"];
        String ComPollModem = ConfigurationManager.AppSettings["PollModem"];
        Int32 PollTimer = Convert.ToInt32(ConfigurationManager.AppSettings["GenPollTime"]);
        bool Configured = false;
        String DateModified = DateTime.Now.ToShortDateString();
        SqlConnection conn2 = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        public ConfigForm(param p_inst)
        {
            InitializeComponent();
            inst = p_inst;
             //////////////////////////
             UpdateLabels();
                        //////////////////////////////
             int indx = 0;
            int rxport =0;
            int pollport = 0;

            foreach (String prt in SerialPort.GetPortNames())
            {
                comboBoxRxModem.Items.Add(prt);      // assign ports to combobox
                comboBoxPollModem.Items.Add(prt);
                if (prt == ComRxModem)
                {
                    rxport = indx;
                }
                if (prt == ComPollModem)
                {
                    pollport = indx;
                }
                indx++;
            }
            if (comboBoxRxModem.Items.Count > 0)
            {
                comboBoxRxModem.SelectedIndex = rxport;
                comboBoxPollModem.SelectedIndex = pollport;
            }
            int polindx = 0;
           for(int t=0;t<10;t++)  
            {   
               int temppoll = (t + 1) * STEP_INTERVAL; 
                comboBoxPollTimer.Items.Add(temppoll + " Secs");
                if (temppoll == PollTimer)
               {
                   polindx = t;
               }
            }
           comboBoxPollTimer.SelectedIndex = polindx;
            DataTable dtDatabaseSources = null;
            // Retrieve the enumerator instance, and then retrieve the data sources.
            try
            {
                if (conn2.State != ConnectionState.Open)
                {
                    conn2.Open();
                }
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                dtDatabaseSources = instance.GetDataSources();

                //// Populate the data sources into DropDownList.            
                foreach (DataRow row in dtDatabaseSources.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["InstanceName"].ToString()))
                        comboBoxServerName.Items.Add(row["ServerName"].ToString() + "\\" + row["InstanceName"].ToString());

                }
                if (comboBoxServerName.Items.Count > 0)
                    comboBoxServerName.SelectedIndex = 0;
            }
            catch (ArgumentNullException ex)
            {
            }
            finally
            {
                if (conn2.State == ConnectionState.Open) conn2.Close();
            }

        }  //const

      #region update labels
        private void UpdateLabels()
        {
            // Init db with Com ports/Polltimer
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
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
                        ComRxModem = Convert.ToString(rd[0]).Trim();
                        ComPollModem = Convert.ToString(rd[1]).Trim();
                        PollTimer = Convert.ToInt32(rd[2]);
                        Configured = Convert.ToBoolean(rd[3]);
                        DateModified = Convert.ToString(rd[4]);
                    }
                    rd.Close();
                }
                if (Configured)  // read values from App.config
                {
                    lblRxModem.Text = "Rx Modem [" + ComRxModem.Trim() + "]";
                    lblPollModem.Text = "Poll Modem [" + ComPollModem.Trim() + "]";
                    lblTimer.Text = "Poll Time [" + PollTimer + "]";
                }
                else  // read values from App.config
                {
                    lblRxModem.Text = "Rx Modem [" + ConfigurationManager.AppSettings["RxModem"] + "]";
                    lblPollModem.Text = "Poll Modem [" + ConfigurationManager.AppSettings["PollModem"] + "]";
                    lblTimer.Text = "Poll Time [" + ConfigurationManager.AppSettings["GenPollTime"] + "]";

                }
            }

            catch (Exception ex)
            {


            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
        #endregion

        private void btnModemUpdate_Click(object sender, EventArgs e)
        {
                String rxm = Convert.ToString(comboBoxRxModem.SelectedItem);
               String polm = Convert.ToString(comboBoxPollModem.SelectedItem);
               Int32 interval = Convert.ToInt32((comboBoxPollTimer.SelectedIndex  + 1) * STEP_INTERVAL);
               String date = DateTime.Now.ToShortDateString();
               String SqlConfigModem = "UPDATE ConfigSystem SET RxModem = '"+ rxm + "',PollModem='" + polm + "',PollTimer=" + interval + ",Changed='1',DateChanged='" + date + "'";
              
          
             conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
             try
             {
                 // conn.Open();
                 if (conn.State != ConnectionState.Open)
                 {
                     conn.Open();
                 }
                 using (SqlCommand cmd2 = new SqlCommand(SqlConfigModem, conn))
                 {
                     cmd2.ExecuteNonQuery();     // update Config
                 }

                 UpdateLabels(); //also update labels
             }
             catch (Exception ex)
             {
             }
         }

        private void ConfigForm_Load(object sender, EventArgs e)
        {

        }

             
        // populate list with  db Names using the 'Server' connection selected
        private void PopulateDbList()
        {
            String server = Convert.ToString(comboBoxServerName.SelectedItem);
            String ServerConn = String.Format(@"Data Source='{0}';Initial Catalog=master;Integrated Security=True", server);
            conn = new SqlConnection(ServerConn);
            SqlDataReader drg = null;
            try
            {
                // conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmdg = new SqlCommand(SqlAllDb, conn))
                {
                    //cmdg.CommandTimeout = 60;   // 60 ms time out
                    drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
                    comboBoxDBNames.Items.Clear();
                    while (drg.Read())
                    {
                        String genid = drg.GetString(0);
                        comboBoxDBNames.Items.Add(genid);
                    }
                    drg.Close();
                    if (comboBoxDBNames.Items.Count > 1)
                        comboBoxDBNames.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB con error: " + ex.Message);
            }
        }  // update db

        private void btnUpdateConn_Click(object sender, EventArgs e)
        {
            //  <add key="connectionString" value="Data Source=PARVAIZ-PC\ADNAN;Initial Catalog=SmsMonitor;Integrated Security=True" />
            //  <add key="DestinationDatabase" value="Initial Catalog=SmsMonitor;Data Source=PARVAIZ-PC\Localhost; Integrated Security=SSPI" />
            try
            {
                String server = Convert.ToString(comboBoxServerName.SelectedItem);
                String machine = server.Substring(0,server.IndexOf('\\'));
                String db =     Convert.ToString(comboBoxDBNames.SelectedItem);
                if (server != null && db != null)
                {
                    String ServerConn = String.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", server,db);
                    String dbName = String.Format(@"Initial Catalog={0};Data Source={1}\localhost; Integrated Security=SSPI", db, machine);
                  // insert into App.config file
               /*     string app_Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    String appPath = app_Path.Substring(0, app_Path.IndexOf("bin"));
                    string configFile = System.IO.Path.Combine(appPath, "App.config");
                    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                    configFileMap.ExeConfigFilename = configFile;
                    System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                    config.AppSettings.Settings["connectionString"].Value = ServerConn;  // update Conn & Db String
                    config.Save();
                    config.AppSettings.Settings["DestinationDatabaseg"].Value = dbName;
                    config.Save();
                  */
                }

            }
            catch (Exception ex)
            {
            }
        }

       

        private void comboBoxServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDbList();
        }

        private void lblRxModem_Click(object sender, EventArgs e)
        {

        }

   }                                       
}
