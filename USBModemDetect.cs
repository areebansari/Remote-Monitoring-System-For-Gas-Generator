using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System.Threading;

namespace SmsMon
{
    class USBModemDetect
    {


        public USBModemDetect(String port, int bauds, int timeout)
        {
            Cursor.Current = Cursors.WaitCursor;
            CommSetting.comm = new GsmCommMain(port, baudRate, timeout);
            Cursor.Current = Cursors.Default;
            CommSetting.comm.PhoneConnected += new EventHandler(comm_PhoneConnected);
            CommSetting.comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
        }

        public Boolean ConnectModem()
        {
            
            return true;
        }



    }
}
