using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using sysInfoClass;

namespace SYSInfo_Monitor
{
    public partial class sysInfo : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        ///
        /// Handling the window messages
        ///
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        sysInfoClass.GetSYSInfo SysInfo = new sysInfoClass.GetSYSInfo();

        public sysInfo()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sysInfo_Load(object sender, EventArgs e)
        {
            metroComboBox1.SelectedIndex = 0;

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = metroComboBox1.SelectedItem;
            var ProcessorInfo = SysInfo.GetProcessorInfo();
            var GraphicsInfo = SysInfo.GetGraphicsInfo();
            var StorageInfo = SysInfo.GetStorageInfo();
            var OSInfo = SysInfo.GetOSInfo();
            var NetworkInfo = SysInfo.GetNetworkInformation();
            var AudioDevices = SysInfo.GetAudioDevices();
            var Printers = SysInfo.GetPrinters();

            if (selected.ToString() == "Processor (CPU)")
            {
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in ProcessorInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Graphics Card (GPU)")
            {
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in GraphicsInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Storage Devices")
            {
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in StorageInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Operating System")
            {
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in OSInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Network Interface Card (NIC)")
            {
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in NetworkInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Audio Devices")
            {
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in AudioDevices)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Printers")
            {
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in Printers)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
