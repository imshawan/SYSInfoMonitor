using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;
using SYSInfoMonitorLib;
using System.Reflection;

namespace SYSInfo_Monitor
{
    public partial class sysInfo : Form
    {
        GetSYSInfo SysInfo = new GetSYSInfo();

        DateTime now = DateTime.Now;

        public string ClipboardData = "";
        private List<KeyValuePair<string, string>> CurrentSelectedData = new List<KeyValuePair<string, string>>();

        private List<KeyValuePair<string, string>> GraphicsInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> StorageInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> OSInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> NetworkInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> AudioDevices = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> Printers = new List<KeyValuePair<string, string>>();

        public sysInfo()
        {
            InitializeComponent();
        }

        [DllImport("shell32.dll", EntryPoint = "#261",
               CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void GetUserTilePath(
        string username,
        UInt32 whatever, // 0x80000000
        StringBuilder picpath, int maxLength);

        public static string GetUserTilePath(string username)
        {   // username: use null for current user
            var sb = new StringBuilder(1000);
            GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
            return sb.ToString();
        }

        public static Image GetUserTile(string username)
        {
            return Image.FromFile(GetUserTilePath(username));
        }

        public bool GetperfomanceCounterStatus()
        {
            try
            {
                PerformanceCounter cpu = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total", true);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Couldn't start the Perfomance Counter, Trying to Fix...", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Process p = new Process();
                p.StartInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe";
                p.StartInfo.WorkingDirectory = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\SysWOW64";
                p.StartInfo.Arguments = "lodctr /r";
                p.Start();

                try
                {
                    PerformanceCounter cpu = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total", true);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        private void LoadInitialItems()
        {
            // Get System name
            label5.Text = Environment.MachineName;
            bunifuPictureBox1.Image = GetUserTile(Environment.UserName);

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = $"SysInfo Monitor v{version.ToString()}";

            // Get DATE, TIME and DAY
            const string year = "yyyy";
            const string month = "MMMM";
            const string day = "dddd";
            const string dayInNumber = "dd";

            string strYear = now.ToString(year);
            string strMonth = now.ToString(month);
            string strDay = now.ToString(day);
            string strDayInINT = now.ToString(dayInNumber);
            string date = "";
            float d = float.Parse(strDayInINT);

            if (d == 1)
            {
                date = strDayInINT + "st " + strMonth + ", " + strYear;
            }
            else if (d == 2)
            {
                date = strDayInINT + "nd " + strMonth + ", " + strYear;
            }
            else if (d == 3)
            {
                date = strDayInINT + "rd " + strMonth + ", " + strYear;
            }
            else
            {
                date = strDayInINT + "th " + strMonth + ", " + strYear;
            }
           
            bunifuLabel1.Text = strDay;
            bunifuLabel2.Text = date;

            if (!GetperfomanceCounterStatus())
            {
                MessageBox.Show("Cound not fix Perfomance Counter, some functions may fail...", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sysInfo_Load(object sender, EventArgs e)
        {
            LoadInitialItems();
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton5_Click_1(object sender, EventArgs e)
        {
            // Processor Information
            SYSInfo_Monitor.UIForms.Processor ProcessorIn = new SYSInfo_Monitor.UIForms.Processor();
            ProcessorIn.ShowDialog(this);
        }

        private void bunifuImageButton6_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Graphics Graphic = new SYSInfo_Monitor.UIForms.Graphics();
            Graphic.ShowDialog(this);
        }

        private void bunifuImageButton7_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Storage Storage = new SYSInfo_Monitor.UIForms.Storage();
            Storage.ShowDialog(this);
        }

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.OS OS = new SYSInfo_Monitor.UIForms.OS();
            OS.ShowDialog(this);
        }

        private void bunifuImageButton11_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.NICs Network = new SYSInfo_Monitor.UIForms.NICs();
            Network.ShowDialog(this);
        }

        private void bunifuImageButton12_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Audio Audio = new SYSInfo_Monitor.UIForms.Audio();
            Audio.ShowDialog(this);
        }

        private void bunifuImageButton8_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.MoreItems More = new SYSInfo_Monitor.UIForms.MoreItems();
            More.ShowDialog(this);
        }

        private void bunifuImageButton13_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.About About = new SYSInfo_Monitor.UIForms.About();
            About.ShowDialog(this);
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Battery Battery = new SYSInfo_Monitor.UIForms.Battery();
            Battery.ShowDialog(this);
        }

        private void bunifuImageButton3_Click_1(object sender, EventArgs e)
        {
            //GITHUB PAGE
            Process.Start("https://github.com/imshawan/SYSInfoMonitor");
        }

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
            //NETWORK INFO
            SYSInfo_Monitor.UIForms.Network Net = new SYSInfo_Monitor.UIForms.Network();
            Net.ShowDialog(this);
        }

        private void bunifuImageButton4_Click_1(object sender, EventArgs e)
        {
            SYSInfo_Monitor.UIForms.Diagnostics Diagnostic = new SYSInfo_Monitor.UIForms.Diagnostics();
            Diagnostic.ShowDialog(this);
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {

        }
    }
}

