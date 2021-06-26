using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYSInfo_Monitor.UIForms
{
    public partial class Battery : Form
    {
        public Battery()
        {
            InitializeComponent();
        }
        private void UpdateBatteryElements()
        {
            PowerStatus pwr = SystemInformation.PowerStatus;

            String strBatteryChargingStatus = pwr.BatteryChargeStatus.ToString();
            if (strBatteryChargingStatus == "0" || strBatteryChargingStatus == null)
            {
                label4.Text = "Battery Discharging";
            }
            else if (strBatteryChargingStatus.ToLower() == "high" || strBatteryChargingStatus.ToLower().Contains("critical"))
            {
                label4.Text = strBatteryChargingStatus + ", Not Charging";
            }
            else
            {
                label4.Text = strBatteryChargingStatus;
            }
            if (strBatteryChargingStatus.ToLower().Contains("charging") || strBatteryChargingStatus.ToLower().Contains("critical"))
            {
                label7.Text = "AC Outlet";
            }
            else
            {
                label7.Text = "On Battery";
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

            if (strBatterylife >= 75 && strBatterylife < 100)
            {
                panel1.Size = new System.Drawing.Size(152, 70);

            }
            else if (strBatterylife >= 75 && strBatterylife < 65)
            {
                panel1.Size = new System.Drawing.Size(111, 70);

            }
            else if (strBatterylife >= 50 && strBatterylife > 25)
            {
                panel1.Size = new System.Drawing.Size(95, 70);

            }
            else if (strBatterylife >= 25 && strBatterylife < 15)
            {
                panel1.Size = new System.Drawing.Size(80, 70);

            }
            else if (strBatterylife >= 15 && strBatterylife > 0)
            {
                panel1.Size = new System.Drawing.Size(4, 70);

            }
        }
        private void Battery_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateBatteryElements();
        }
    }
}
