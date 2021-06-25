using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using SYSInfoMonitorLib;

namespace SYSInfo_Monitor
{
    public partial class sysInfo : Form
    {
        SYSInfoMonitorLib.GetSYSInfo SysInfo = new SYSInfoMonitorLib.GetSYSInfo();
        DateTime now = DateTime.Now;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        public string ClipboardData = "";
        private List<KeyValuePair<string, string>> CurrentSelectedData = new List<KeyValuePair<string, string>>();

        private List<KeyValuePair<string, string>> ProcessorInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> GraphicsInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> StorageInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> OSInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> NetworkInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> AudioDevices = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> Printers = new List<KeyValuePair<string, string>>();

        ///
        /// Handling the window messages
        ///


        public sysInfo()
        {
            InitializeComponent();

        }

        private void sysInfo_Load(object sender, EventArgs e)
        {
            // Get System name
            label5.Text = SysInfo.Battery("systemname")[0];
            timer1.Start();
            const string year = "yyyy";
            const string month = "MMMM";
            const string day = "dddd";
            const string dayInNumber = "dd";

            string strYear = now.ToString(year);
            string strMonth = now.ToString(month);
            string strDay = now.ToString(day);
            string strDayInINT = now.ToString(dayInNumber);
            string date = strDayInINT + "th " + strMonth + ", " + strYear;
            bunifuLabel1.Text = strDay;
            bunifuLabel2.Text = date;
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
        private string StringBuilderFunc(List<KeyValuePair<string, string>> data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var values in data)
            {
                sb.AppendFormat("{0}:    {1}", values.Key, values.Value);
                sb.AppendLine();
            }
            return sb.ToString();
        }
        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            var selected = metroComboBox1.SelectedItem;

            ProcessorInfo = SysInfo.GetProcessorInfo();
            GraphicsInfo = SysInfo.GetGraphicsInfo();
            StorageInfo = SysInfo.GetStorageInfo();
            OSInfo = SysInfo.GetOSInfo();
            NetworkInfo = SysInfo.GetNetworkInformation();
            AudioDevices = SysInfo.GetAudioDevices();
            Printers = SysInfo.GetPrinters();

            if (selected.ToString() == "Processor (CPU)")
            {
                CurrentSelectedData = ProcessorInfo;
                //bunifuDataGridView1.Rows.Clear();
                foreach (var val in ProcessorInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Graphics Card (GPU)")
            {
                CurrentSelectedData = GraphicsInfo;
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in GraphicsInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Storage Devices")
            {
                CurrentSelectedData = StorageInfo;
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in StorageInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Operating System")
            {
                CurrentSelectedData = OSInfo;
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in OSInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Network Interface Card (NIC)")
            {
                CurrentSelectedData = NetworkInfo;
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in NetworkInfo)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Audio Devices")
            {
                CurrentSelectedData = AudioDevices;
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in AudioDevices)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            else if (selected.ToString() == "Printers")
            {
                CurrentSelectedData = Printers;
                bunifuDataGridView1.Rows.Clear();
                foreach (var val in Printers)
                {
                    bunifuDataGridView1.Rows.Add(val.Key, val.Value);
                }
            }
            */
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float totalMemory = 0;
            float freeMemory = 0;

            double totalFreeSpace = 0;
            double totalSpace = 0;
            double usedSpace = 0;

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                totalMemory = (float.Parse(result["TotalVisibleMemorySize"].ToString()) / 1024);
                freeMemory = (float.Parse(result["FreePhysicalMemory"].ToString()) / 1024);
            }

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    totalFreeSpace += d.AvailableFreeSpace;
                    totalSpace += d.TotalSize;
                }
            }

            var RamVal = 100 - ((freeMemory / totalMemory) * 100);
           // bunifuCircleProgressbar1.Value = (int)RamVal;

          //  label9.Text = ((int)totalMemory).ToString() + " MB";
            //label10.Text = ((int)totalMemory - (int)freeMemory).ToString() + " MB";
            //label11.Text = ((int)freeMemory).ToString() + " MB";

            usedSpace = 100 - (totalFreeSpace / totalSpace) * 100;
            //bunifuCircleProgressbar2.Value = (int)usedSpace;

            
        }
        private void SaveToFile(string args)
        {
            string filePath = "";

            SaveFileDialog dialog = new SaveFileDialog();
            if (args == "csv")
            {
                dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                dialog.FileName = "SYSInfo.csv";
            }
            else if (args == "txt")
            {
                dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.FileName = "SYSInfo.txt";
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            if (filePath == "")
            {
                return;
            }
            
            else
            {
                if (args == "csv")
                {
                    foreach (var vals in CurrentSelectedData)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}, {vals.Value}\n");
                    }
                }  
                else if (args == "txt") 
                {
                    foreach (var vals in CurrentSelectedData)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}: {vals.Value}\n");
                    }
                }
                
                MessageBox.Show("File Saved Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void saveDataToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("csv");
        }

        private void saveToTextFiletxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("txt");
        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void copyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(StringBuilderFunc(CurrentSelectedData));
        }

        private void X_Click(object sender, EventArgs e)
        {
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var array = SysInfo.Battery("battery");
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {

        }
    }
}

