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

        public string ClipboardData = "";
        private List<KeyValuePair<string, string>> CurrentSelectedData = new List<KeyValuePair<string, string>>();

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

        private void LoadInitialItems()
        {
            // Get System name
            label5.Text = Environment.MachineName;

            // Get DATE, TIME and DAY
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

        private void sysInfo_Load(object sender, EventArgs e)
        {
            
            timer1.Start();
            LoadInitialItems();
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

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        PerformanceCounter cpu = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total", true);

        private void timer1_Tick(object sender, EventArgs e)
        {
            
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
            //GITHUB PAGE
            Process.Start("https://github.com/imshawan/SYSInfoMonitor");
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

        }
        private void test()
        {
            SelectQuery myQuery = new SelectQuery("SELECT * from Win32_Processor");

            ManagementObjectSearcher mySearcher = new ManagementObjectSearcher(myQuery);


            foreach (ManagementBaseObject obj in mySearcher.Get())
            {
                MessageBox.Show("Result: "+ Convert.ToInt16(obj["LoadPercentage"]));
            }
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Diagnostics Diagnostic = new SYSInfo_Monitor.UIForms.Diagnostics();
            Diagnostic.ShowDialog(this);
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Battery Battery = new SYSInfo_Monitor.UIForms.Battery();
            Battery.ShowDialog(this);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            //NETWORK INFO
            SYSInfo_Monitor.UIForms.Network Net = new SYSInfo_Monitor.UIForms.Network();
            Net.ShowDialog(this);
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            // Processor Information
            SYSInfo_Monitor.UIForms.Processor ProcessorIn = new SYSInfo_Monitor.UIForms.Processor();
            ProcessorIn.ShowDialog(this);
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Graphics Graphic = new SYSInfo_Monitor.UIForms.Graphics();
            Graphic.ShowDialog(this);
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Storage Storage = new SYSInfo_Monitor.UIForms.Storage();
            Storage.ShowDialog(this);
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.OS OS = new SYSInfo_Monitor.UIForms.OS();
            OS.ShowDialog(this);
        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.NICs Network = new SYSInfo_Monitor.UIForms.NICs();
            Network.ShowDialog(this);
        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Audio Audio = new SYSInfo_Monitor.UIForms.Audio();
            Audio.ShowDialog(this);
        }
    }
}

