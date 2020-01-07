using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;


namespace SmsMon
{

    // public delegate void SmsMessageIndication(String msg);
    public sealed class ModemPollSms : EventArgs
    {
        public String _msgpoll;
        public ModemPollSms(String msg) { _msgpoll = msg; }
    }

    public class UsbPollModem
    {
        // public SmsMessageIndication smsEvent;
        private Boolean ModemSts = false;
        public AutoResetEvent receiveNow;
        public SerialPort usbPollport = new SerialPort();
        private String buf = String.Empty;
        public ModemPollSms smsEvent;
        private static UsbPollModem PollModem = null;
        private Thread ReadThread;
        public static bool _Continue = false;
        public static bool _ContSMS = false;
        private bool _Wait = false;

         private UsbPollModem()
        {
             
        }
         
        public static UsbPollModem ModemInstance(){

            if (PollModem == null)
            {
                PollModem = new UsbPollModem();
            }
            return PollModem;
        }

     
        
        /* private UsbPillModem()
         {
             smsEvent= new SmsMessageIndication(gprsData);
         }
         
        */
       /*  public void gprsData(String msg)
         {
             String buf = msg;                      

         }  */

        #region Open and Close Ports
        //Open Port
        public SerialPort OpenPort(string p_strPortName, int p_uBaudRate, int p_uDataBits, int p_uReadTimeout, int p_uWriteTimeout)
        {
            receiveNow = new AutoResetEvent(false);
            SerialPort port = new SerialPort();

            try
            {
                port.PortName = p_strPortName;                 //COM1
                port.BaudRate = p_uBaudRate;                   //9600
                port.DataBits = p_uDataBits;                   //8
                port.StopBits = StopBits.One;                  //1
                port.Parity = Parity.None;                     //None
                port.ReadTimeout = p_uReadTimeout;             //300
                port.WriteTimeout = p_uWriteTimeout;           //300
                port.Encoding = Encoding.GetEncoding("iso-8859-1");
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return port;
        }

        //Close Port
        public void ClosePort()
        {
            try
            {
              //  port.Close();
             //   port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
             //   port = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public bool IsOpen()
        {
            return ModemSts;
        }

        private void ReadPort()
        {
            string SerialIn = null;
            byte[] RXBuffer = new byte[usbPollport.ReadBufferSize + 1];
            string SMSMessage = null;
            int Strpos = 0;
            string TmpStr = null;
            while (usbPollport.IsOpen == true)
            {
                if ((usbPollport.BytesToRead != 0) & (usbPollport.IsOpen == true))
                {
                    while (usbPollport.BytesToRead != 0)
                    {
                        usbPollport.Read(RXBuffer, 0, usbPollport.ReadBufferSize);
                        SerialIn =
                            SerialIn + System.Text.Encoding.ASCII.GetString(
                            RXBuffer);
                        if (SerialIn.Contains(">") == true)
                        {
                            _ContSMS = true;
                        }
                        /*  if (SerialIn.Contains("+CMGS:") == true)
                          {
                              _Continue = true;
                              if (Sending != null)
                                  Sending(true);
                              _Wait = false;
                              SerialIn = string.Empty;
                              RXBuffer = new byte[usbPollport.ReadBufferSize + 1];
                          }*/
                    }
                 /*   if (DataReceived != null)
                        DataReceived(SerialIn);
                    SerialIn = string.Empty;
                    RXBuffer = new byte[usbPollport.ReadBufferSize + 1];
                  */
                }
            }
        }    

        public String DataReceived_data(object sender, SerialDataReceivedEventArgs e)
        {
            //Boolean ok = false;
            int timeout = 300;  // 300ms
            buf = String.Empty;
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    do
                    {
                        //    Read chars rx'd
                        receiveNow.Set();    // allow thread to read
                        if (receiveNow.WaitOne(timeout, false))
                        {
                            string t = usbPollport.ReadExisting();
                            buf += t;
                        }
                        else
                        {
                            buf = null;
                             if (buf.Length > 0)
                                 throw new ApplicationException("Response received is incomplete.");
                             else
                                 throw new ApplicationException("No data received from phone.");
                             
                        }
                    }
                    while (!buf.EndsWith("\r\nOK\r\n") && !buf.EndsWith("\r\n> ") && !buf.EndsWith("\r\nERROR\r\n"));
                    //ok = true; // buf.EndsWith("r\nOK\r\n") || buf.EndsWith("\r\n> ") ? true : false;
                    return buf;
                }
            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return buf;
        }

        //Close Port
        public void ClosePort(SerialPort port)
        {
            try
            {
                port.Close();
                port.DataReceived -= new SerialDataReceivedEventHandler(DataReceived);
                ReadThread.Abort();
                port = null;
                ModemSts = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!isOpen) return;
            if (e.EventType == SerialData.Chars)
            {      //receiveNow.Set();

                // read chars on every event
                int dataLength = usbPollport.BytesToRead;
                byte[] data = new byte[dataLength];
                int nbrDataRead = usbPollport.Read(data, 0, dataLength);
                if (nbrDataRead == 0)
                    return;
                smsEvent = new ModemPollSms(data.ToString());  // generate SMS event
                //byte[] buf2 = data;
                // create thread to processs data
            }

            // Send data to whom ever interested
            /* if (NewSerialDataRecieved != null)
                 NewSerialDataRecieved(this, new SerialDataEventArgs(data));
            
             try
             {
                 if (e.EventType == SerialData.Chars)
                 {
                    receiveNow.Set();    // allow thread to read
                    if((this.buf = DataReceived_data(sender, e)) != null)
                    {
                         // Save SMS
                         String sms = this.buf;
                         this.buf = String.Empty;  // clear buffer
                         smsEvent(sms);  // generate SMS event
                    }   
                 }
                
             }
             catch (ApplicationException ex)
             {
                
             }  */
        }   // data Read

        public void DiscardRxtBuffer()
        {
            usbPollport.DiscardInBuffer();

        }

        public string ExecCommand2(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            String ok = "";
            try
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");
                //Thread.Sleep(100);
                // read response from port
                int dataLength = port.BytesToRead;
                byte[] data = new byte[dataLength];
                int nbrDataRead = port.Read(data, 0, dataLength);
                if (nbrDataRead == 0)
                    return "";
                ok = Encoding.UTF8.GetString(data);
                return ok;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   

        //Execute AT Command
  /*      public string ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            port.DiscardOutBuffer();
            port.DiscardInBuffer();
            receiveNow.Reset();
            port.Write(command +  "\r");
           // Thread.Sleep(100);
            return "\r\nOK\r\n";

            try
             {

                 port.DiscardOutBuffer();
                 port.DiscardInBuffer();
                 receiveNow.Reset();
                 port.Write(command + "\r");

                 string input = ReadResponse(port, responseTimeout);
                 if ((input.Length > 0) && (input.Contains('>'))){
                     //((!input.EndsWith("\r\n> "))(!input.EndsWith("\r\nOK\r\n"))))
                     throw new ApplicationException("No success mes&&sage was received.");
                 return input;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }  
        }
*/
        public string ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        /* if (buffer.Length > 0)
                             throw new ApplicationException("Response received is incomplete.");
                         else
                             throw new ApplicationException("No data received from phone.");
                         */
                        buffer = String.Empty;
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") || !buffer.EndsWith("\r\n> ") || !buffer.EndsWith("\r\nERROR\r\n"));


            }
            catch (ApplicationException ex)
            {
                // throw ex;
                buffer = String.Empty;
            }
            return buffer;
        }

        public Boolean isOpen { get { return ModemSts; } set { ModemSts = value; } }

        public String GetCurrentOperator(SerialPort port)
        {
            String MobOperator = "No Operator found";
            try
            {

                string ok = ExecCommand2(port, "AT", 300, "No phone connected");
                if (ok.Contains("OK"))
                {
                    ok = ExecCommand2(port, "AT+COPS=3,0", 300, "No Operator found");
                    String str = ExecCommand2(port, "AT+COPS?", 300, "No Operator found");
                    MobOperator = str.Substring(str.LastIndexOf("0") + 3, 5);
                }
            }
            catch (Exception ex)
            {

            }
            if (MobOperator.Equals("No Operator found"))
            {
                ModemSts = true;
                return MobOperator;
            }
            return MobOperator;
        }

        public String MsgRxdEnableInterrupt(SerialPort port)
        {
            // Call once during initilization
            String str = ExecCommand2(port, "AT", 300, "No phone connected");
            if (str.Contains("OK"))
            {
                str = ExecCommand2(port, "AT+CNMI=2,2,0,0,0", 300, "Event not enabled");   // SMS sent directly to TE.
                //str = ExecCommand2(port, "AT+CMGF=1", 300, "Failed to set message format.");
                str = ExecCommand2(port, "AT&W", 300, "Error..Message Event not enabled");   // write commnd
            }
            return str;
        }

        public ShortMessageCollection ReadSMS(SerialPort port, string Command)
        {

     /*       // Set up the phone and read the messages
            ShortMessageCollection messages = null;
            try
            {

                #region Execute Command
                // Check connection
                string input = ExecCommand(port, "AT", 300, "No phone connected");
                // Use message format "Text mode"
                input = ExecCommand(port, "AT+CMGF=1", 1000, "Failed: To Set Txt format.");
                // Use character set "PCCP437"
                //ExecCommand(port,"AT+CSCS=\"PCCP437\"", 300, "Failed to set character set.");  // stops incomplete msg rxd
                // Select SIM storage
                input = ExecCommand(port, "AT+CPMS=\"SM\"", 300, "Failed to select message storage.");
                // Read the messages
                input = ExecCommand(port, Command, 1000, "Failed to read the messages.");
                #endregion

                #region Parse messages
                messages = ParseMessages(input);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (messages != null)
                return messages;
            else   */
                return null;

        }

        public ShortMessageCollection ParseMessages(string input)
        {
            ShortMessageCollection messages = new ShortMessageCollection();
            try
            {
                Regex r = new Regex(@"\+CMT: \+(\d+),""(.+)"",""(.+)""\r\n(.+)");
                Match m = r.Match(input);
                while (m.Success)
                {
                    ShortMessage msg = new ShortMessage();
                    //msg.Index = int.Parse(m.Groups[1].Value);
                    msg.Index = m.Groups[1].Value;
                    msg.Status = m.Groups[2].Value;
                    msg.Sender = m.Groups[3].Value;
                    // msg.Alphabet = m.Groups[4].Value;
                    // msg.Sent = m.Groups[5].Value;
                    msg.Message = m.Groups[4].Value;
                    messages.Add(msg);

                    m = m.NextMatch();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return messages;
        }

        static AutoResetEvent readNow = new AutoResetEvent(false);
        public bool sendSMSMsg(SerialPort port, string PhoneNo, string Message)
        {
            int tries=0;
            receiveNow.Reset();
            

            port.DiscardOutBuffer();
            port.DiscardInBuffer();
            do
            {
                bool isSend = false;

                try
                {
                    string recievedData = ExecCommand2(port, "AT", 300, "No phone connected");
                    recievedData = ExecCommand2(port, "AT+CMGF=1", 300,
                        "Failed to set message format.");
                    String command = "AT+CMGS=\"" + PhoneNo + ";\r";
                    recievedData = ExecCommand2(port, command, 300,
                        "Failed to accept phoneNo");
                    command = Message + char.ConvertFromUtf32(26) + "\r";
                    recievedData = ExecCommand2(port, command, 3000,
                        "Failed to send message"); //3 seconds
                    if (recievedData.EndsWith("\r\nOK\r\n"))
                    {
                        isSend = true;
                    }
                    else if (recievedData.Contains("ERROR"))
                    {
                        isSend = false;
                    }
                    return isSend;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            } while (++tries < 10);
          return false;
        }

        //Execute AT Command
        public string ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            try
            {

                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                string input = ReadResponse(port, responseTimeout);
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("No success message was received.");
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Receive data from port
        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    receiveNow.Set();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

    }  //class
}
