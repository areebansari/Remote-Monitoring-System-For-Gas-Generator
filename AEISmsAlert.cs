using System;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Management;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System.Windows.Forms;
using System.IO.Ports;


namespace SmsMon
{
    public  class AEISmsAlert
    {
        int port = 0;
        static bool STS = false;
        public GsmCommMain comm = null;
        static string Modem = null;
        static int BaudRate;
       public static AEISmsAlert Inst;

        private AEISmsAlert(){
        }

        
        public static AEISmsAlert getInstance(string modem,int bauds) //string modem,int baudrate)
        {
            Modem = modem;
            BaudRate = bauds;
              if (Inst == null)
              { 
                  Inst = new AEISmsAlert();
                  STS = Inst.Open();
              }
              return Inst;
           
        }

         public bool OK
        {
            get { return STS; }
        }

        private bool Open()
        {
            try
            {
              //  ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort");
              //  ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem ");
                ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity ");

                /*         foreach (ManagementObject queryObj in searcher.Get())
                         {

                             MessageBox.Show(queryObj["Name"].ToString());

                             if (queryObj["Name"].ToString().Contains(Modem))   // i.e "ST-Ericsson ... "
                             {

                                 string xx = queryObj["Description"].ToString();
                                 //  string com = queryObj["DeviceID"].ToString();
                                 // int index = com.LastIndexOf('M');
                                  if(index > 0 && index <= 5)                     // Check for valid COMx port
                                 // port = Convert.ToInt32(com.Substring(index + 1, com.Length - index - 1));
                                //
                             }
                         } 
                  
                foreach (ManagementObject queryObj2 in searcher2.Get())
                {

                   // MessageBox.Show(queryObj2["Name"].ToString());

                    if (queryObj2["Name"].ToString().Contains(Modem))   // i.e "ST-Ericsson ... "
                    {

                        string com = System.Convert.ToString(queryObj2["Description"]);

                        if (com.Contains("(COM"))
                        {
                            int index = com.LastIndexOf('M');
                            if (index > 0 && index <= 5)                     // Check for valid COMx port
                                port = Convert.ToInt32(com.Substring(index + 1, com.Length - index - 1));
                        }
                        //  string com = queryObj["DeviceID"].ToString();
                        // int index = com.LastIndexOf('M');
                        // if(index > 0 && index <= 5)                     // Check for valid COMx port
                        // port = Convert.ToInt32(com.Substring(index + 1, com.Length - index - 1));
                        //
                    }
                }  
*/
                foreach (ManagementObject queryObj3 in searcher3.Get())
                {
                    
                    //MessageBox.Show(queryObj3["Name"].ToString());
                   
                    if (queryObj3["Name"].ToString().Contains(Modem))   // i.e "ST-Ericsson ... "
                    {
                        if ((string)queryObj3["Status"] == "OK")
                        {
                            string com = System.Convert.ToString(queryObj3["Name"]);

                            if (com.Contains("(COM"))
                            {
                                int index = com.LastIndexOf('M');
                                int index2 = com.LastIndexOf(')');
                                string substr = com.Substring(index + 1, com.Length - index2 + 1);
                                port = Convert.ToInt32(substr);
                                // Get Modem Port
                                if (port > 3)
                                {
                                    port -= 2;  // i.e if 18 then port=16
                                }
                                break;
                            }
                        }
                    } 
                }   
                
                if (port != 0)
                {
                    comm = new GsmCommMain(port, BaudRate, 1000);
                    // Register with network

                    comm.PhoneConnected += new EventHandler(comm_PhoneConnected);
                    comm.PhoneConnected += new EventHandler(comm_PhoneDisconnected);

                    try
                    {
                        if(!comm.IsOpen())
                           comm.Open();
                        if (this.comm.IsOpen())
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Modem unable to open: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Port not found: " + ex);
               // EventLog.WriteEntry("AeiGenAlert: Comm  is Closed");
            }
            return false;
        }

        private void comm_PhoneDisconnected(object sender, EventArgs e)
        {
            MessageBox.Show("Phione disconnected");
        }

        private void comm_PhoneConnected(object sender, EventArgs e)
        {
            MessageBox.Show("Phione connected");
           // throw new NotImplementedException();
        }// Open Modem

        public String ReadSms()
        {
            DecodedShortMessage[] decsms = null;
            if (comm.IsOpen())
            {
                do
                {
                    try
                    {
                          Cursor.Current = Cursors.WaitCursor;
                          decsms = comm.ReadMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);
                          Cursor.Current = Cursors.Default;
                    
                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(1);
                } while (decsms == null);
            }
            int len = decsms.Length;
            return "OK";
        }

       
        public bool Alert(string msg, string number)
        {
            try
            {
                if (comm.IsOpen())
                {
                    byte dcs = DataCodingScheme.Class0_7Bit;
                    SmsSubmitPdu pdu = new SmsSubmitPdu(msg, number, "", dcs);
                    comm.SendMessage(pdu);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alert unable to send error: " + ex.Message);
               // EventLog.WriteEntry("AeiGenAlert: No SMS Alert");
            }
            return false;
        }  //Alert

        public bool SendMessage(string msg, string number)
        {
           try
           {
               if(comm.IsOpen())
                {
                    SmsSubmitPdu pdu = new SmsSubmitPdu(msg, number);
                    comm.SendMessage(pdu);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SendMessage PDU: " + ex.Message);
                //EventLog.WriteEntry("AeiGenAlert: No SMS Message");
            }
            return false;
        }  // Message
    
        public void Close()
         {
            try
            {
                 if (comm.IsOpen())
                 {
                     comm.Close();
                 }
            }
            catch (Exception ex)
            {

            }
         }

        // Get Operator
        public string GetOPerator()
        {
            if (comm.IsOpen())
            {

                OperatorInfo info = comm.GetCurrentOperator();
                return info.TheOperator;
            }
            return "None";
        }

    }
}
