using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYSInfo_Monitor.UIForms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/imshawan");
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Process.Start("https://imshawan.netlify.app/");
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.instagram.com/shawan_sm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/imshawan/SYSInfoMonitor/blob/main/docs/EULA.md");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/imshawan/SYSInfoMonitor/blob/main/docs/gpl-3.0-LICENSE.md");
        }

        private void About_Load(object sender, EventArgs e)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            label2.Text = $"Version {version.ToString()}";
        }
    }
}
