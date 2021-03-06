﻿

#region old_poll
/*
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions
 */
/*
namespace SmsMon

{
    public class usbPoll
    {
        //public SerialPort port;
        public event SmsMessageIndication smsEvent;
        public SerialPort port = null; // new SerialPort();
        public AutoResetEvent receiveNow;
        private String buf = String.Empty;
        private bool ok = false;
       
        //Configure Port
        public SerialPort OpenPort(string strPortName, int strBaudRate,int dataBits,int readTimeOut,int writeTimeOut)
        {
                 receiveNow = new AutoResetEvent(false);
                port = new SerialPort();

                port.PortName = strPortName.Trim();
                port.BaudRate = strBaudRate;               //updated by Anila (9600)
                port.DataBits = dataBits;
                port.StopBits = StopBits.One;
                port.Parity = Parity.None;
                port.ReadTimeout = readTimeOut;
                port.WriteTimeout = writeTimeOut;
                port.Encoding = Encoding.GetEncoding("iso-8859-1");
               // port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;

                return port;
        }

     

       // public bool isOpen { get { return ok; } set { ok = value; } }    // check modem port open
        //Close Port
        public void ClosePort()
        {
            port.Close();
            port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
            port = null;
        }

        //Execute AT Command
        public string ExecCommand(SerialPort port,string command, int responseTimeout, string errorMessage)
        {
            try
            {
                // receiveNow = new AutoResetEvent();
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                //Thread.Sleep(3000); //3 seconds
                string input = ReadResponse(port, responseTimeout);
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("No success message was received.");
                return input;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
        }

//public Boolean ModemStatus { get { return ModemSts; } set { ModemSts = value; } }

        public String GetCurrentOperator(SerialPort port)
        {
            String MobOperator = "No Operator found";
            try
            {

                ExecCommand(port, "AT", 300, "No phone connected");
                ExecCommand(port, "AT+COPS=3,0", 300, "No Operator found");
                String str = ExecCommand(port, "AT+COPS?", 300, "No Operator found");
                MobOperator = str.Substring(str.LastIndexOf("0") + 3, 5);
            }
            catch (Exception ex)
            {

            }
            if (MobOperator.Equals("No Operator found"))
            {
               // ModemStatus = true;
                return MobOperator;
            }
            return MobOperator;
        }




        //Receive data from port
        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                String bif = ReadResponse(port, 300);
                smsEvent(bif);
                if (e.EventType == SerialData.Chars)
                {
                    receiveNow.Set();

                }
            }
            catch (NullReferenceException ex)
            {
            }
        }

        public String ReadResponse(SerialPort port,int timeout)
        {
            string buffer = string.Empty;
             //receiveNow.Set();
            do
            {
                if (receiveNow.WaitOne(timeout, false))
                {
                    string t = port.ReadExisting();
                    buffer += t;
                }
                else
                {
                    
                   if (buffer.Length > 0)
                        throw new ApplicationException("Response received is incomplete.");
                    else
                        throw new ApplicationException("No data received from phone.");
                }
            }
            while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            return buffer;
        }
     

           


        #region Send SMS
       
        static AutoResetEvent readNow = new AutoResetEvent(false);

        public bool sendMsg(SerialPort port,string PhoneNo, string Message)
        {
            bool isSend = false;
            try
            {
                
                //this.port = OpenPort(strPortName,strBaudRate);
                string recievedData = ExecCommand(port,"AT", 300, "No phone connected at " + port + ".");
                recievedData = ExecCommand(port,"AT+CMGF=1", 300, "Failed to set message format.");
                String command = "AT+CMGS=\"" + PhoneNo + "\"";
                recievedData = ExecCommand(port,command, 300, "Failed to accept phoneNo");         
                command = Message + char.ConvertFromUtf32(26) + "\r";
                recievedData = ExecCommand(port,command, 3000, "Failed to send message"); //3 seconds
                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    recievedData = "Message sent successfully";
                    isSend = true;
                }
                else if (recievedData.Contains("ERROR"))
                {
                    string recievedError = recievedData;
                    recievedError = recievedError.Trim();
                    recievedData = "Following error occured while sending the message" + recievedError;
                    isSend = false;
                }
                return isSend;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (port != null)
                {
                    port.Close();
                    port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                    port = null;
                }
            }
        }     
        static void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
                readNow.Set();
        }

        #endregion

        #region Delete SMS
    /*    public void DeleteMsg(SerialPort port,string strPortName, string strBaudRate)
        {
            try
            {
                #region Open Port
                //this.port = OpenPort(strPortName,strBaudRate);
                #endregion

                #region Execute Command
                string recievedData = ExecCommand(port,"AT", 300, "No phone connected at " + strPortName + ".");
                recievedData = ExecCommand(port,"AT+CMGF=1", 300, "Failed to set message format.");              
                String command = "AT+CMGD=1,3";
                recievedData = ExecCommand(port,command, 300, "Failed to delete message");
                #endregion

                if (recievedData.EndsWith("\r\nOK\r\n"))
                    recievedData = "Message delete successfully";
                if (recievedData.Contains("ERROR"))
                {
                    string recievedError = recievedData;
                    recievedError = recievedError.Trim();
                    recievedData = "Following error occured while sending the message" + recievedError;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (port != null)
                {
                    #region Close Port
                    port.Close();
                    port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                    port = null;
                    #endregion
                }
            }
        }  
        

        public String MsgRxdEnableInterrupt(SerialPort port)
        {
            // Call once during initilization
            String str = ExecCommand(port, "AT", 300, "No phone connected");
            str = ExecCommand(port, "AT+CNMI=1", 300, "Event not enabled");   // CHECK PARAM?
            //str = ExecCommand(port, "AT+CMGF=1", 300, "Failed to set message format.");
            str = ExecCommand(port, "AT&W", 300, "Error..Message Event not enabled");   // write commnd
            return str;
        }

    }
}
*/
#endregion


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
    public class ModemSmsp : EventArgs
    {
        public String _msgx;
        public ModemSmsp(String msg) { _msgx = msg; }
    }

    class usbPoll
    {
        // public SmsMessageIndication smsEvent;
        private Boolean ModemStsp = false;
        private AutoResetEvent receiveNowp;
        private SerialPort port = new SerialPort();
        private String bufp = String.Empty;
        private ModemSms smsEvent;


        public SerialPort OpenPort(String comport, int baudrate, int bits, int rd_timeout, int wr_timeout)
        {
           receiveNowp = new AutoResetEvent(false);
            SerialPort port = new SerialPort();

            try
            {
                port.PortName = comport;
                port.BaudRate = baudrate;
                port.DataBits = bits;
                port.ReadTimeout = rd_timeout;
                port.WriteTimeout = wr_timeout;
                port.Encoding = Encoding.GetEncoding("iso-8859-1");
                port.StopBits = StopBits.One;
                port.Parity = Parity.None;
                port.Handshake = Handshake.None;
                // port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return port;
        }  //serialport


        public String SmsDataReceived()
        {
            int timeout = 300;  // 300ms
            bufp = String.Empty;
            try
            {
                do
                {
                    receiveNowp.Set();    // allow thread to read
                    if (receiveNowp.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        bufp += t;
                    }
                    else
                    {
                        bufp = null;
                        if (bufp.Length > 0)
                            throw new ApplicationException("Response received is incomplete.");
                        else
                            throw new ApplicationException("No data received from phone.");

                    }
                }
                while (!bufp.EndsWith("\r\nOK\r\n") && !bufp.EndsWith("\r\n> ") && !bufp.EndsWith("\r\nERROR\r\n"));
                return bufp;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return bufp;

        }

        public String DataReceived_data(object sender, SerialDataReceivedEventArgs e)
        {
            //Boolean ok = false;
            int timeout = 300;  // 300ms
            bufp = String.Empty;
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    do
                    {
                        //    Read chars rx'd
                        //receiveNow.Set();    // allow thread to read
                        if (receiveNowp.WaitOne(timeout, false))
                        {
                            string t = port.ReadExisting();
                            bufp += t;
                        }
                        else
                        {
                            bufp = null;
                            if (bufp.Length > 0)
                                throw new ApplicationException("Response received is incomplete.");
                            else
                                throw new ApplicationException("No data received from phone.");

                        }
                    }
                    while (!bufp.EndsWith("\r\nOK\r\n") && !bufp.EndsWith("\r\n> ") && !bufp.EndsWith("\r\nERROR\r\n"));
                    return bufp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bufp;
        }

        //Close Port
        public void ClosePort(SerialPort port)
        {
            try
            {
                port.Close();
                port.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedp);
                port = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DataReceivedp(object sender, SerialDataReceivedEventArgs e)
        {
            /*  if (e.EventType == SerialData.Chars)
              {
                  receiveNow.Set();  // allow read
                  //Thread.Sleep(150);
                  // read chars on every event
                    int dataLength = port.BytesToRead;
                    byte[] data = new byte[dataLength];
                    int nbrDataRead = port.Read(data, 0, dataLength);
                    if (nbrDataRead == 0)
                        return;
                     smsEvent = new ModemSms(data.ToString());  // generate SMS event
                 
                  //byte[] buf2 = data;
                  // create thread to processs data
             // }

              // Send data to whom ever interested
             if (NewSerialDataRecieved != null)
                  NewSerialDataRecieved(this, new SerialDataEventArgs(data));
               */
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    receiveNowp.Set();    // allow thread to read
                    if ((this.bufp = DataReceived_data(sender, e)) != null)
                    {
                        // Save SMS
                        String sms = this.bufp;
                        this.bufp = String.Empty;  // clear buffer
                        //    smsEvent(sms);  // generate SMS event
                    }
                }

            }
            catch (ApplicationException ex)
            {

            }
        }   // data Read

        public void DiscardRxtBuffer()
        {
            port.DiscardInBuffer();

        }

        public String ConfigModem()
        {
            String command = "ATZ";
            String sts = ExecCommand2(port, command, 300, "No Response");
            command = "AT+IPR=19200";   // default baud rate
            sts = ExecCommand2(port, command, 300, "No Response");
            command = "AT+CMGF=1";     // text mode
            sts = ExecCommand2(port, command, 300, "No Response");
            command = "AT+CNMI=2,2,0,0,0";   //  not saved on SIM
            sts = ExecCommand2(port, command, 300, "No Response");
            command = "AT+CMGD=\"DEL ALL\"";   // delete all sim data
            return sts = ExecCommand2(port, command, 300, "No Response");

        }

        //Execute AT Command
        public string ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            try
            {

                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNowp.Reset();
                port.Write(command + "\r");
                Thread.Sleep(100);
                receiveNowp.Set();            // allow read receive
                string input = ReadResponse(port, responseTimeout);
                if ((input.Length == 0 && !input.EndsWith("\r\n> ") && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("No success message was received.");
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ExecCommand2(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            try
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNowp.Reset();
                port.Write(command + "\r");
                Thread.Sleep(100);
                // read response from port
                int dataLength = port.BytesToRead;
                byte[] data = new byte[dataLength];
                int nbrDataRead = port.Read(data, 0, dataLength);
                if (nbrDataRead == 0)
                    return "";
                String ok = Encoding.UTF8.GetString(data);
                return ok;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

        public string ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    //readNow.Set();  // enable read
                    if (receiveNowp.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            throw new ApplicationException("Response received is incomplete.");
                        else
                            throw new ApplicationException("No data received from phone.");
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));


            }
            catch (ApplicationException ex)
            {
                throw ex;
                // buffer = String.Empty;
            }
            return buffer;
        }

        public Boolean ModemStatus { get { return ModemStsp; } set { ModemStsp = value; } }

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
                ModemStatus = true;
                return MobOperator;
            }
            return MobOperator;
        }

        public String MsgRxdEnableInterrupt(SerialPort port)
        {
            // Call once during initilization
            String str = ExecCommand2(port, "AT", 300, "Modem not responding");
            str = ExecCommand2(port, "AT+CNMI=2,2,0,0,0", 300, "Event not enabled");   // SMS sent directly to TE.
            str = ExecCommand2(port, "AT+CMGF=1", 300, "Failed to set message format.");   //
            // str = ExecCommand(port, "AT&W", 300, "Error..Message Event not enabled");   // write commnd
            return str;
        }

        public ShortMessageCollection ReadSMS(SerialPort port, string Command)
        {

            // Set up the phone and read the messages
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
            else
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
            //String smsReport = "AT + CMGS= "" + " + PhoneNo +"" ;   \\"\"" +"\r" + Message + "\r" + Char.
            String command = "AT+CMGS=\"" + PhoneNo + "\"" + " \r";
            port.Write(command);
            port.DiscardOutBuffer();
            //recievedData = ExecCommand(port, command, 300, "Failed to accept phoneNo");
            Thread.Sleep(100);
            String smsReport = Message + char.ConvertFromUtf32(26);
            port.Write(smsReport);
            port.DiscardOutBuffer(); //
            return true;
        }

    }  //class
}
