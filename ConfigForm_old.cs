using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace SmsMon
{
    public partial class ConfigForm : Form
    {

        private param inst;
        public ConfigForm(param p_inst)
        {
            InitializeComponent();
            inst = p_inst;

            foreach (String prt in SerialPort.GetPortNames())
            {
                comboBoxRxModem.Items.Add(prt);
                comboBoxPollModem.Items.Add(prt);
            }
            comboBoxRxModem.SelectedIndex = 0;
            comboBoxPollModem.SelectedIndex = 0;

            for(int t=1;t<10;t++)  
            {   int i =20;
                comboBoxPollTimer.Items.Add(i*t + " Secs");
            }
            comboBoxPollTimer.SelectedIndex = 0;

        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {

        }

        
        private void comboBoxRxModem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration conf =ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
             conf.AppSettings.Settings["RxMODEM"].Value = comboBoxRxModem.SelectedItem.ToString();
             conf.Save(ConfigurationSaveMode.Modified);
        }

        private void comboBoxPollModem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration conf = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            conf.AppSettings.Settings["PollMODEM"].Value = comboBoxPollModem.SelectedItem.ToString();
            conf.Save(ConfigurationSaveMode.Modified);
        }

        private void comboBoxPollTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration conf = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            conf.AppSettings.Settings["GenPollTime"].Value = comboBoxPollTimer.SelectedItem.ToString();
            conf.Save(ConfigurationSaveMode.Modified);
        }
   }
}
