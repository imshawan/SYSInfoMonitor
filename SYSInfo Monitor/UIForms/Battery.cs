using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SYSInfoMonitorLib;

namespace SYSInfo_Monitor.UIForms
{
    public partial class Battery : Form
    {
        public Battery()
        {
            InitializeComponent();
        }
        WinAPI WinAPI = new WinAPI();
        private void UpdateBatteryElements()
        {
            PowerStatus pwr = SystemInformation.PowerStatus;

            String strBatteryChargingStatus = pwr.BatteryChargeStatus.ToString();
            if (strBatteryChargingStatus == "0" || strBatteryChargingStatus == null)
            {
                label4.Text = "Battery Discharging";
                pictureBox2.Visible = false;
            }
            else if (strBatteryChargingStatus.ToLower() == "high" || strBatteryChargingStatus.ToLower().Contains("critical"))
            {
                label4.Text = strBatteryChargingStatus + ", Not Charging";
                pictureBox2.Visible = false;
            }
            else
            {
                label4.Text = strBatteryChargingStatus;
            }

            if (strBatteryChargingStatus.ToLower().Contains("charging"))
            {
                pictureBox2.Visible = true;
            }
            if (strBatteryChargingStatus.ToLower().Contains("nosystembattery"))
            {
                progressBar1.Visible = false;
            }
            if (strBatteryChargingStatus.ToLower().Contains("charging") || strBatteryChargingStatus.ToLower().Contains("critical") || strBatteryChargingStatus.ToLower().Contains("nosystembattery"))
            {
                label7.Text = "AC Outlet";
            }
            else
            {
                label7.Text = "On Battery";
                pictureBox2.Visible = false;
            }

            var secs = pwr.BatteryLifeRemaining;
            if (secs == -1)
            {
                label5.Text = "Unknown";
            }
            else
            {
                TimeSpan t = TimeSpan.FromSeconds(secs);
                string timeRemaining = string.Format("{0:D2} hours, {1:D2} minutes and\n{2:D2} seconds remaining on battery",
                                t.Hours,
                                t.Minutes,
                                t.Seconds);
                label5.Text = timeRemaining;
            }


            var strBatterylife = pwr.BatteryLifePercent;
            strBatterylife = strBatterylife * 100;
            label6.Text = strBatterylife + "%";

            if (strBatteryChargingStatus.ToLower().Contains("nosystembattery"))
            {
                pictureBox2.Visible = false;
                label6.Text = "...?";
                label9.Text = "No Internal Battery";
            }
            else
            {
                progressBar1.Value = (int)strBatterylife;
                progressBar1.SetState(1);
                if (strBatterylife >= 95 && strBatterylife <= 100)
                {
                    label9.Text = "Healthy";
                }

                if (strBatterylife >= 75 && strBatterylife < 95)
                {
                    label9.Text = "Healthy";
                }
                if (strBatterylife >= 65 && strBatterylife < 75)
                {
                    label9.Text = "Healthy";
                }
                if (strBatterylife >= 50 && strBatterylife < 65)
                {
                    label9.Text = "Healthy";
                }
                if (strBatterylife >= 25 && strBatterylife < 50)
                {
                    label9.Text = "Good";
                }
                if (strBatterylife >= 15 && strBatterylife < 25)
                {
                    label9.Text = "Low Battery";
                    progressBar1.SetState(3);
                }
                if (strBatterylife >= 1 && strBatterylife < 15)
                {
                    progressBar1.SetState(2);
                    label9.Text = "Low Battery, Plug in Charger";
                }
            }
        }
        private void Battery_Load(object sender, EventArgs e)
        {
            WinAPI.AnimateWindow(this.Handle, 150, WinAPI.CENTER);
            this.CenterToParent();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateBatteryElements();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0)
            {
                timer1.Stop();
                timer2.Stop();
                this.Close();
            }
            else
            {
                this.Opacity -= 0.1;
            }
        }
    }
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
    
}
