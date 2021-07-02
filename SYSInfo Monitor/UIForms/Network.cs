using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SYSInfoMonitorLib;

namespace SYSInfo_Monitor.UIForms
{
    public partial class Network : Form
    {
        GetSYSInfo GetInfo = new GetSYSInfo();
        WinAPI WinAPI = new WinAPI();

        public Network()
        {
            InitializeComponent();
        }

        private void StatusUpdate()
        {
            string AllNETs = GetInfo.GetAllActiveNICs();
            if (AllNETs == string.Empty)
            {
                label6.Text = "Disconnected";
                label7.Text = "None";
                label4.Text = "";
                label8.Text = "None";
                label10.Text = "None";
            }
            else
            {
                label6.Text = "Connected";
                string[] Nets = AllNETs.Split(',');
                label7.Text = Nets[0];
                label4.Text = Nets[1];
                label8.Text = Nets[2];
                label10.Text = Nets[3];
            }
        }

        private void Network_Load(object sender, EventArgs e)
        {
            WinAPI.AnimateWindow(this.Handle, 100, WinAPI.CENTER);
            StatusUpdate();
        }

        void Network_Load(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Green, ButtonBorderStyle.Solid);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0)
            {
                timer1.Stop();
                this.Close();
            }
            else
            {
                this.Opacity -= 0.1;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
    }
}
