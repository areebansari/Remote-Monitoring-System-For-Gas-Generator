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
          
    public partial class Admin : Form
    {
        private String Org = "Unknown";
        private SqlConnection conn = null;
        private String Sqldbstr = null;
      //  String SQLCurCust = @"SELECT IDENT_CURRENT('Customers')";   // return last index  used
       // private String SQLCurGen = @"SELECT IDENT_CURRENT('Generators')";   // return last index  used
        String SQLCustList = @"SELECT Cust_Id,Organization FROM Customers";   // Cus_id, Org list
        DataTable dt = new DataTable();
        private BindingSource bindingSource = new BindingSource();
        Int32 custindex = 0;
        Int32 locIndex = 0;
        String genindex = null;
        int genid = 0;
        int cust_id = 0;
        static int FUELTHEFT = 13; // in RxList
        int maxGens = 100;
        int genCount = 0;
        List<Int32> CustIdList = new List<int>();   // Customer Id  int list
        List<string> OrgList = new List<string>();     //  List of Organization names
        List<string> GenList = new List<string>();     //  Generator no int list
        List<string> LocList = new List<string>();     //  Loc list
        List<Label> lblList = new List<Label>();
        List<decimal> RxList = new List<decimal>();    // Rx'd Alarm Status display
        Int32 custid = 0;
        param inst;
        TabControl tc;

        #region constructor
        public Admin(param p_inst)
        {
            InitializeComponent();
            inst = p_inst;
            conn = inst.conn;   // db connection
            panel1.Enabled = false;
            panel1.Hide();
            Sqldbstr = ConfigurationManager.AppSettings["connectionString"];
            conn = new SqlConnection(Sqldbstr);
            btnSaveCustomer.Enabled = false;
            btnSaveLocation.Enabled = false;
            lblList.Clear();
                     
         }

      #endregion

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            String sdate = e.Start.ToShortDateString();
            String edate = e.End.ToShortDateString();
         
            
        }
                       
         private void Admin_Load(object sender, EventArgs e)
        {

        }

        // Log In button
         private void button1_Click(object sender, EventArgs e)
         {
             String user = txtUserLogin.Text.Trim();
             String pwd  = txtUserPassword.Text.Trim();
             
             String SQLUser = String.Format(@"Select Cust_Id,Organization From Customers where LoginName ='{0}' And Password = '{1}'", user, pwd);
             String SQLAdmin = String.Format(@"Select * From Customers Where Cust_Id = '{0}'", custid);

             try
             {
                 if (conn.State == ConnectionState.Open)
                 {
                     conn.Close();
                 }
             }
             catch (Exception ex) { }
 
             
             if ((user !=null) && (pwd != null) && (user.Length > 0) && (pwd.Length > 0))
             {                        
                  // chk if admin
                if (user.Equals(ConfigurationManager.AppSettings["admin"]) && pwd.Equals(ConfigurationManager.AppSettings["pwd"]))
                 { 
                                  
                 // Update Customer combo
                     btnPollGens.Enabled = true;
                     btnPollGens.Visible = true;
                     label20.Visible = true;
                     inst.admin = true;
                 try
                 {
                    if (conn.State == ConnectionState.Closed)
                    {                
                        conn.Open();
                    } 
 
                     // Update Combo with orgs & select last used
                     String SqlCusts = "SELECT Cust_Id,Organization FROM Customers";
                     SqlCommand cmd = new SqlCommand(SqlCusts, conn);
                     using (SqlDataReader reader = cmd.ExecuteReader())
                     {
                         CustIdList.Clear();
                         OrgList.Clear();
                         while (reader.Read())
                         {
                             CustIdList.Add(Convert.ToInt32(reader[0]));
                             OrgList.Add(Convert.ToString(reader[1]));
                             comboOrg.Items.Add(Convert.ToString(reader[1]));
                         }
                         reader.Close();
                     }

                     comboOrg.SelectedIndex = 0;
                     custindex = CustIdList[0];
                     Org = OrgList.ElementAt(comboOrg.SelectedIndex);
                     inst.org = Org;
                     genindex = AttachLoc2Combo(custindex);   // gen_id ?
                     inst.gen_id = genindex;
                     DisplayGenLocation(custindex, genindex);
                     txtLocation.Hide();
                     comboLoc.Enabled = true;
                     comboLoc.Show();
                     comboLoc.SelectedIndex = 0;
                     
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("Errr: " + ex.Message);
                 }
                 panel1.Enabled = true;
                 panel1.Show();       // Show Admin Panel
                 panelUser.Enabled = false;
                 panelUser.Hide();
                 btnMonitor.Enabled = true;
                 btnMonitor.Show();
                 this.Show();
               }                               
               else   // in USER MODE requires  user name & password 
               {
                   try
                   {
                         if (conn.State == ConnectionState.Closed)
                         {
                             conn.Open();
                         } 

                        //String Org = "Unknown";
                        cust_id=0;
                        SqlDataReader drCust = null;
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                         using (SqlCommand cmd = new SqlCommand(SQLUser, conn))
                         {
                             try
                             {
                                 drCust = cmd.ExecuteReader();
                                 while(drCust.Read())
                                 {
                                     cust_id = drCust.GetInt32(0);
                                     Org = drCust.GetString(1);
                                
                                 }
                                 drCust.Close();
                                 if (cust_id > 0)
                                 {
                                     btnPollGens.Enabled = true;
                                     btnPollGens.Visible = true;
                                     label20.Visible = true;
                                     btnMonitor.Enabled = true;
                                     btnMonitor.Show();
                                     label22.Show();
                                     inst.admin = false;
                                     
                                   /*  DateTime date = monthCalendar2.SelectionRange.Start;
                                                                  
                                     inst.cust_id = cust_id;
                                     inst.dt = date.ToString();
                                     inst.org = Org;
                                     inst.indx = locIndex;
                                     inst.sts = true;
                                     tc = inst.tc;
                                     //inst.gen_id = 
                                     // tc.SelectedIndex = 0;  // =1 Forms page
                                    */
                                     String SqlOrg = String.Format("SELECT Organization,City,District FROM Customers WHERE cust_id = {0}",cust_id);
                                    
                                     try
                                     {
                                         if (conn.State == ConnectionState.Closed)
                                         {
                                            conn.Open();
                                         }
                                          lblOrguser.Text="";
                                         using ( SqlCommand cmdorg = new SqlCommand(SqlOrg, conn))
                                         {
                                            
                                             SqlDataReader dr = cmdorg.ExecuteReader(CommandBehavior.CloseConnection);
                                             while(dr.Read())
                                             {
                                                  lblOrguser.Text = "Organization: " + dr.GetString(0);
                                                  lblAddress.Text = "Address: " + dr.GetString(1) + ", " + dr.GetString(2);
                                             }
                                             dr.Close();
                                         }
                                      }
                                    catch(Exception ex)
                                    {
                                    }
                                    // panel1.Controls.Add(tblPanel);
                                    
                                     panelUser.Enabled = true;
                                     panelUser.Show();
                                     tblPanel.Show();
                                     panel1.Enabled = true;
                                     panel1.Show();
                                     timerSummary.Interval = 500;
                                     timerSummary.Enabled = true;
                                     this.Show();
                                    //Form1 monitor = new Form1(ref p_inst); //(cust_id, date,Org,locIndex);
                                    // DialogResult dialogR = monitor.ShowDialog();
                                 }
                                 else
                                 {
                                     MessageBox.Show("Please Enter valid credentials", "Error in Login", MessageBoxButtons.OK);
                                 }
                             }
                             catch (NullReferenceException ex)
                             {
                                  MessageBox.Show("Enter Valid User/Password","Login Error",MessageBoxButtons.OK);
                             }
                         }
                     }
                     catch (SqlException ex)
                     {
                          if (conn.State == ConnectionState.Open)   conn.Close();
                     }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();

                    }                     
                 } 
             }
             else
             {
                MessageBox.Show("Enter User Name and Password","Login Error",MessageBoxButtons.OK);
             }
               DateTime date = monthCalendar2.SelectionRange.Start;
                                                                  
                inst.cust_id = cust_id;
                inst.dt = date.ToString();
                inst.org = Org;
                inst.indx = locIndex;
                inst.sts = true;
                tc = inst.tc;
                //inst.gen_id = "999xxx";
                tc.SelectedIndex = 0;  // =1 Forms page
                
         }

         private void btnMonitor_Click_1(object sender, EventArgs e)
         {
            if(inst.admin)
            {
             if (comboLoc.Items.Count == 0)
             {
                MessageBox.Show("Please enter Generator data.");
             }
            }
            else
             if(!inst.admin)
             {
                 //locIndex = 0;
                 custindex = cust_id;

             }

            try
            {
                DateTime date = monthCalendar2.SelectionRange.End;
                inst.dt = date.ToString();
                //if (!inst.sts)
                //{
                inst.cust_id = custindex;
                //inst.gen_id ="";
                inst.org = Org;
                inst.indx = locIndex;
                // }
                TabControl tc = inst.tc;
                inst.sts = true;
                tc.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Monitor =>: " + ex.Message);
            }
         }     // monitor.Dispose();
         


         private void btnResetData_Click(object sender, EventArgs e)
         {

         }
         #region SaveCustomer  Data
         private void btnNewCustomer_Click(object sender, EventArgs e)
         {
             comboOrg.Items.Clear();
             comboOrg.Hide();
             txtOrganztn.Show();
             txtOrganztn.Text = null;
             txtOwner.Text = null;
             txtBuildingNo.Text = null;
             txtAddress1.Text = null;
             txtAddress2.Text = null;
             txtCity.Text = null;
             txtDistrict.Text = null;
             txtTelephone.Text = null;
             txtMobile.Text = null;
             txtUserAssigned.Text = null;
             txtPwdAssigned.Text = null;

             // Clear location
             comboLoc.Items.Clear();
             comboLoc.Hide();
             txtLocation.Show();
             txtLocation.Text = null;
             txtGenNumber.Text = null;
             txtGenID.Text = null;
             txtGenType.Text = null;
             chkGenActive.Checked = false;

             btnSaveLocation.Enabled = false;
             btnSaveCustomer.Enabled = true;
             
         }

         

        private void btnSaveCustomer_Click(object sender, EventArgs e)
         {
             

             String SQLCustIns = String.Format(@"Insert into Customers(Owner,Organization,BuildingNo,Address1,Address2,City,District,Telephone,Mobile,LoginName,Password,Active) Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                 txtOwner.Text.Trim(),
                 txtOrganztn.Text.Trim(),        //
                 txtBuildingNo.Text.Trim(),
                 txtAddress1.Text.Trim(),
                 txtAddress2.Text.Trim(),
                 txtCity.Text.Trim(),
                 txtDistrict.Text.Trim(),
                 txtTelephone.Text.Trim(),
                 txtMobile.Text.Trim(),
                 txtUserAssigned.Text.Trim(),
                 txtPwdAssigned.Text.Trim(),
                  true                             //
                 ); try
             {                      
                 if (conn.State == ConnectionState.Closed)
                {                
                    conn.Open();
                } 
 
             
            
                using(SqlCommand cmd = new SqlCommand(SQLCustIns,conn))
                {
                    cmd.ExecuteNonQuery();
                }
                btnSaveCustomer.Enabled = false;
                btnSaveLocation.Enabled = true;
                 // Hide 
                txtOrganztn.Hide();
                comboOrg.Show();

                String SqlCusts = "SELECT Cust_Id,Organization FROM Customers";
                SqlCommand cmdc = new SqlCommand(SqlCusts, conn);
                using (SqlDataReader reader = cmdc.ExecuteReader())
                {
                    CustIdList.Clear();
                    OrgList.Clear();
                    while (reader.Read())
                    {
                        CustIdList.Add(Convert.ToInt32(reader[0]));
                        OrgList.Add(Convert.ToString(reader[1]));
                        comboOrg.Items.Add(Convert.ToString(reader[1]));
                    }
                    reader.Close();
                }
                txtLocation.Hide();
                comboLoc.Enabled = true;
                comboLoc.Show();
                btnSaveLocation.Enabled = false;
                comboOrg.SelectedIndex = comboOrg.Items.Count - 1;
             }
             catch(Exception ex)
             {
                 MessageBox.Show("Err,, Data Save: " + ex.Message);
             }
         }
         #endregion

         #region SaveGenerator Data
        private void btnSaveLocation_Click(object sender, EventArgs e)
         {
             Int32 last_cusid = (Int32)CustIdList[comboOrg.SelectedIndex];
             try
             {
                 if (conn.State == ConnectionState.Closed)
                 {
                     conn.Open();
                 } 
                 /* int lastGenId =0;
                  using (SqlCommand gencmd = new SqlCommand(SQLCurCust,conn)) //SQLCurGen, conn))
                 {
                    lastGenId = (int)gencmd.ExecuteScalar();
                     if(lastGenId == 0 || lastGenId  == null)
                         lastGenId = 1;
                 }
                   */
                 var chkd = (chkGenActive.Checked ? "1" : "0");
                   // bool chkd = Convert.ToBoolean(chkGenActive.Checked ? true: false); 
                // String SQLGenIns = String.Format(@"Insert Into Generators(Gen_Num,Cust_Id,Gen_Id,GenType,Location,Operational) Values({0},{1},{2},'{3}','{4}','{5}'"),
                 String SQLGenIns = String.Format(@"Insert Into Generators Values('{0}',{1},'{2}','{3}','{4}','{5}')",
                      txtGenNumber.Text.Trim(),
                      last_cusid,
                      txtGenID.Text.Trim(),
                      txtGenType.Text.Trim(),
                      txtLocation.Text.Trim(),
                      chkd  
                    );
             

                 using (SqlCommand cmd = new SqlCommand(SQLGenIns, conn))
                 {
                     cmd.ExecuteNonQuery();
                 }
                 txtLocation.Hide();
                 comboLoc.Enabled = true;
                 comboLoc.Show();
                 AttachLoc2Combo(last_cusid);
                 btnSaveLocation.Enabled = false;
                 // Add to PollGenerators
                 // TBD


             }
             catch (Exception ex)
             {
                 MessageBox.Show("Bit conversion error: " + ex.Message);
             }
         }
#endregion

        private void btnNewLoc_Click(object sender, EventArgs e)
        {
            comboLoc.Items.Clear();
            comboLoc.Hide();
            txtLocation.Show();
            txtGenNumber.Text = null;
            txtGenID.Text = null;
            txtGenType.Text = null;
             txtLocation.Text = null;
            chkGenActive.Checked = false;
            btnSaveLocation.Enabled = true;
        }

   /*      private void btnSaveData_Click(object sender, EventArgs e)
         {
            
         }
       */
         private void chkGenActive_CheckedChanged(object sender, EventArgs e)
         {
             Boolean chk = chkGenActive.Checked;
         }

   
         private void AdminFormClosing(object sender, FormClosingEventArgs e)
         {

         }


         private String AttachLoc2Combo(Int32 indx)
         {
             if (conn.State == ConnectionState.Closed)
             {
                 conn.Open();
             } 
                            
              String SqlLoc = String.Format(@"SELECT Location,Gen_id FROM Generators WHERE Cust_Id={0}", indx);
              try
              {
                  SqlCommand cmd = new SqlCommand(SqlLoc, conn);
                  using (SqlDataReader reader = cmd.ExecuteReader())
                  {
                      LocList.Clear();
                      GenList.Clear();
                      comboLoc.Items.Clear();
                      while (reader.Read())
                      {
                          LocList.Add(Convert.ToString(reader[0]));
                          GenList.Add(Convert.ToString(reader[1]));
                          comboLoc.Items.Add(Convert.ToString(reader[0]));
                      }
                      reader.Close();
                  }
                  if (LocList.Count > 0)
                  {
                      comboLoc.SelectedIndex = 0;
                      return GenList[0];
                  }
              }
              catch (ArgumentOutOfRangeException ex)
              {

              }
              return null;
         }


         private void comboOrg_SelectedIndexChanged(object sender, EventArgs e)
         {
             custindex =  CustIdList[comboOrg.SelectedIndex];
             Org = OrgList[comboOrg.SelectedIndex];
             DisplayCustInfo(custindex);
             genindex = AttachLoc2Combo(custindex);
             DisplayGenLocation(custindex,genindex);
             if (inst.sts)
             {
                 inst.org = Org;
                 inst.cust_id = custindex;
                 inst.gen_id = genindex;
             }
          }

         private void comboLoc_SelectedIndexChanged(object sender, EventArgs e)
         {
             genindex = GenList[comboLoc.SelectedIndex];
             locIndex = comboLoc.SelectedIndex;
             custindex = CustIdList[comboOrg.SelectedIndex];
             DisplayGenLocation(custindex, genindex);
             if (inst.sts)
             {
                 inst.gen_id = genindex;
             }
         }

         private void DisplayCustInfo(int indx)
         {
             SqlDataReader dr;
             String SqlNewOrg = String.Format(@"Select * FROM Customers WHERE Cust_id={0}", indx);
             if (conn.State == ConnectionState.Closed)
             {
                 conn.Open();
             } 
             
             using (SqlCommand cmd = new SqlCommand(SqlNewOrg, conn))
             {
                 cmd.CommandTimeout = 60;   // 60 ms time out
                 dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 if (conn.State != ConnectionState.Open)
                 {
                     conn.Open();
                 }
                 txtOrganztn.Hide();
                 comboOrg.Show();
                 while (dr.Read())
                 {
                     // hCustScrollBar.Value = dr.GetInt32(0);       //
                     txtOwner.Text = dr.GetString(1);
                     // txtOrganztn.Text = dr.GetString(2);
                     txtBuildingNo.Text = dr.GetString(3);
                     txtAddress1.Text = dr.GetString(4);
                     txtAddress2.Text = dr.GetString(5);
                     txtCity.Text = dr.GetString(6);
                     txtDistrict.Text = dr.GetString(7);
                     txtTelephone.Text = dr.GetString(8);
                     txtMobile.Text = dr.GetString(9);
                     txtUserAssigned.Text = dr.GetString(10);
                     txtPwdAssigned.Text = dr.GetString(11);

                 }

             }
             dr.Close();
           }

         private void DisplayGenLocation(Int32 cusid, String genid)
         {
          // Get CustIndex associated gen data
              if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                // update txt with last Generator records
                SqlDataReader drg = null;
                String SQLMaxGenData = String.Format(@"Select * From Generators Where Cust_Id = '{0}' and Gen_id ='{1}'", cusid, genid);
                try
                {
                    using (SqlCommand cmdg = new SqlCommand(SQLMaxGenData, conn))
                    {
                        cmdg.CommandTimeout = 60;   // 60 ms time out
                        drg = cmdg.ExecuteReader(CommandBehavior.CloseConnection);
                        if (drg.HasRows)
                        {
                            while (drg.Read())
                            {
                                //hScrollGenerator.Value = drg.GetInt32(0);       //
                                txtGenNumber.Text = drg.GetString(1);    // Mgr  Mob number
                                txtGenID.Text = drg.GetString(3);
                                txtGenType.Text = drg.GetString(4);
                                // txtLocation.Text = drg.GetString(5);
                                if (drg.GetBoolean(6))
                                {
                                    chkGenActive.Checked = true;
                                }
                                else
                                    chkGenActive.Checked = false;
                            }
                        }
                        else
                        {
                           txtGenNumber.Text = null;
                           txtGenID.Text = null;
                           txtGenType.Text = null;
                           chkGenActive.Checked = false;
                           comboLoc.Items.Clear();
                           comboLoc.SelectedText = null;

                       }
                    }
                    drg.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data error: " + ex.Message, "dB status", MessageBoxButtons.OK);
                }
           }

         private void panel1_Paint(object sender, PaintEventArgs e)
         {
            // panelUser.Controls.Add(tblPanel);
           // panelUser.Controls.Add(tblPanel);
         }

         private void btnPollGens_Click(object sender, EventArgs e)
         {
             DateTime date = monthCalendar2.SelectionRange.End;
             inst.dt = date.ToString();
             TabControl tc = inst.tc;
             inst.sts = true;
             tc.SelectedIndex = 4;
         }

         private void timerSummary_Tick(object sender, EventArgs e)
         {
             String alarm = "Normal";
             timerSummary.Interval = 5000;
             timerSummary.Enabled = false;
             tblPanel.Controls.Clear();
             List<String> gloc = new List<string>();
             List<String> glist = new List<String>();
             List<String> gmob = new List<string>();
             List<String> gop = new List<String>();
             int row = 0;
             int col = 0;

             String SqlSummary = String.Format(@"SELECT Gen_Id,Location,MgrMob,Operational From Generators WHERE Cust_Id ={0}", cust_id);
           
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SqlSummary, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                                      
                    while (reader.Read())
                    {
                       gloc.Add(Convert.ToString(reader[1]).Trim());  // location
                       glist.Add(Convert.ToString(reader[0]).Trim());  // Generator id 
                       gmob.Add(Convert.ToString(reader[2]).Trim());  // mgr mobile 
                       gop.Add(Convert.ToString(reader[3]).Trim());
                       
                       
                    }
                    reader.Close();
                   } // using

                col = 0;
                row = 0;

                for (int indx = 0;indx < glist.Count(); indx++)
                {
                    tblPanel.Controls.Add(new Label() { Text = gloc[indx] }, col++, row);
                    tblPanel.Controls.Add(new Label() { Text = glist[indx] }, col++, row);
                    tblPanel.Controls.Add(new Label() { Text = gmob[indx] }, col++, row);
                    col = 0;
                    ++row;
                }

                // Process Alarm condition for the generators
                col = 4;
                row = 0;
                string prev = "Normal";
                int gx = 0;
                 for (gx =0; gx < glist.Count(); gx++)
                {
                    if (gop[gx].Equals("True"))
                    {
                        alarm ="Normal";
                        String SqlAlarm = String.Format(@"SELECT TOP 1 * FROM Alarms a, generators g WHERE a.Gen_Id = g.Gen_Id AND g.Gen_Id ='{0}'", glist[gx].ToString());
                        using (SqlCommand cmdg = new SqlCommand(SqlAlarm, conn))
                        {
                            cmdg.CommandTimeout = 60;   // 60 ms time out
                            SqlDataReader drg = cmdg.ExecuteReader(); //CommandBehavior.CloseConnection);
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
                                RxList.Add(drg.GetDecimal(21));
                                RxList.Add(drg.GetDecimal(22));
                                RxList.Add(drg.GetDecimal(23));
                                RxList.Add(drg.GetDecimal(24));
                                RxList.Add(drg.GetDecimal(25));
                                RxList.Add(drg.GetDecimal(26));
                            }
                            drg.Close();
                          } // using

                            int i = 1;
                            if (RxList.Count > 0)
                            {

                                for (i = 1; i < RxList.Count; i++)
                                {

                                    if (RxList[i] == 1)    // No Alarm
                                    {
                                       if(!prev.Equals("Warning!")){
                                           alarm = "Normal";
                                        //alarmList[i].ImageLocation = @"..\..\images\led_grn12x12.jpg";
                                       }
                                    }
                                    else
                                        if (RxList[i] == 15)   // Alarm Not Implemented
                                        {
                                            if(!prev.Equals("Warning!")){
                                              alarm = "Normal";
                                            //alarmList[i].ImageLocation = @"..\..\images\led_grn12x12.jpg";
                                             }
                                            // alarmList[i].ImageLocation = @"..\..\images\blue.bmp";
                                        }
                                        else
                                        {   
                                            if (RxList[i] > 1)   // Alarm 2,3,4
                                            {
                                                alarm = "Warning!";
                                                prev=alarm;
                                                if (i == FUELTHEFT)
                                                {
                                                    alarm = "Red Alert";
                                                    //  alarmList[i].ImageLocation = @"..\..\images\red.bmp";
                                                    // alarm_detected = true;
                                                    break;
                                                }
                                            }
                                            else
                                                if (RxList[i] == 0)   // Disabled digital input
                                                {
                                                   if(!prev.Equals("Warning")){
                                                       alarm = "Normal";
                                                    //alarmList[i].ImageLocation = @"..\..\images\led_grn12x12.jpg";
                                                   }
                                                    //alarmList[i].ImageLocation = @"..\..\images\gray.bmp"; 
                                                }
                                        }
                                  } //for(rxlist.Count
                             }
                    }  //if(RxList
                    else    //RxCount = 0  i.e No alarm data avaialable
                    {
                        if(gop[gx].Equals("True"))  // operational
                        {  
                            alarm = "Normal";
                        }
                        else
                        {
                            alarm = "Non-Operational";
                        }

                    }
                    LinkLabel lblAlarm = new LinkLabel(){Text=alarm};
                    lblAlarm.Click += new EventHandler(Alarm_Click);
                    if(alarm.Contains("Red Alert"))
                    {
                        lblAlarm.LinkColor = System.Drawing.Color.Red;
                    }
                    else
                    if(alarm.Contains("Warning"))
                    {
                        lblAlarm.LinkColor = System.Drawing.Color.Orange;
                    }
                    else
                    {
                        lblAlarm.LinkColor = System.Drawing.Color.Green;
                    }
                    tblPanel.Controls.Add(lblAlarm, col, row++);
                } // for gx
            }
            catch(Exception ex)
            {
                string err = "Errorr : " + ex.Message;
            }
             timerSummary.Interval = 5000;
             timerSummary.Enabled = true;
         }

         private void Alarm_Click(object sender, EventArgs e)
         {
            Label lbl = sender as Label;
            locIndex = tblPanel.GetPositionFromControl(lbl).Row;
            string genno = tblPanel.GetControlFromPosition(1, locIndex).Text.Trim();
            //btnMonitor_Click_1(sender, e);
             //.....................
            if (inst.admin)
            {
                if (comboLoc.Items.Count == 0)
                {
                    MessageBox.Show("Please enter Generator data.");
                }
            }
            else
                if (!inst.admin)
                {
                    //locIndex = 0;
                    custindex = cust_id;

                }

            try
            {
                DateTime date = monthCalendar2.SelectionRange.End;
                inst.dt = date.ToString();
                //if (!inst.sts)
                //{
                inst.cust_id = custindex;
                inst.gen_id =genno;
                inst.org = Org;
                inst.indx = locIndex;
                // }
                TabControl tc = inst.tc;
                inst.sts = true;
                tc.SelectedIndex = 2;   // jump to status

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Monitor =>: " + ex.Message);
            }
             //.........................
         }
        // EOProg
     }  // class
}   // ns
