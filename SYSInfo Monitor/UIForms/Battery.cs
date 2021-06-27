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

            if (strBatteryChargingStatus.ToLower() == "charging")
            {
                pictureBox2.Visible = true;
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

            var height = panel1.Size.Height;

            if (strBatteryChargingStatus.ToLower().Contains("nosystembattery"))
            {
                panel1.Visible = false;
                pictureBox2.Visible = false;
                label6.Text = "...?";
                label9.Text = "No Internal Battery";
            }
            else
            {
                if (strBatterylife >= 95 && strBatterylife <= 100)
                {
                    panel1.Size = new System.Drawing.Size(155, height);
                    panel1.BackColor = Color.YellowGreen;
                    label9.Text = "Healthy";
                }

                if (strBatterylife >= 75 && strBatterylife < 95)
                {
                    panel1.Size = new System.Drawing.Size(132, height);
                    panel1.BackColor = Color.YellowGreen;
                    label9.Text = "Healthy";
                }
                if (strBatterylife >= 65 && strBatterylife < 75)
                {
                    panel1.Size = new System.Drawing.Size(111, height);
                    panel1.BackColor = Color.YellowGreen;
                    label9.Text = "Healthy";
                }
                if (strBatterylife >= 50 && strBatterylife < 65)
                {
                    panel1.Size = new System.Drawing.Size(80, height);
                    panel1.BackColor = Color.YellowGreen;
                    label9.Text = "Healthy";
                }
                if (strBatterylife >= 25 && strBatterylife < 50)
                {
                    panel1.Size = new System.Drawing.Size(65, height);
                    label9.Text = "Good";
                    panel1.BackColor = Color.YellowGreen;
                }
                if (strBatterylife >= 15 && strBatterylife < 25)
                {
                    panel1.Size = new System.Drawing.Size(30, height);
                    panel1.BackColor = Color.Yellow;
                    label9.Text = "Low Battery";

                }
                if (strBatterylife >= 1 && strBatterylife < 15)
                {
                    panel1.Size = new System.Drawing.Size(10, height);
                    panel1.BackColor = Color.Red;
                    label9.Text = "Low Battery, Plug in Charger";

                }
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
            timer1.Stop();
            this.Close();
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
    }
}
