using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SYSInfoMonitorLib;

namespace SYSInfo_Monitor.UIForms
{
    public partial class NICs : Form
    {
        public NICs()
        {
            InitializeComponent();
        }

        bool status = false;
        GetSYSInfo GetInfo = new GetSYSInfo();
        private List<KeyValuePair<string, string>> NICInfo = new List<KeyValuePair<string, string>>();

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
                    foreach (var vals in NICInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}, {vals.Value}\n");
                    }
                }
                else if (args == "txt")
                {
                    foreach (var vals in NICInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}: {vals.Value}\n");
                    }
                }

                MessageBox.Show("File Saved Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetNICData(List<KeyValuePair<string, string>> Values)
        {
            foreach (var val in Values)
            {
                bunifuDataGridView1.Rows.Add(" " + val.Key + ":", val.Value);
            }
        }

        private void NICs_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetInfo.StringBuilderFunc(NICInfo));
        }

        private void saveToTextFiletxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("txt");
        }

        private void saveDataToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("csv");
        }

        private void StatusUpdate()
        {
            string AllNETs = GetInfo.GetAllActiveNICs();
            if (AllNETs == string.Empty)
            {
                label1.Text = "Network State: Disconnected";
            }
            else
            {
                string[] Nets = AllNETs.Split(',');
                label1.Text = "Connected to Internet using " + Nets[0];
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!status)
            {
                NICInfo = GetInfo.GetNetworkInformation();
                GetNICData(NICInfo);
                StatusUpdate();
                status = true;
            }
            timer1.Stop();
        }
    }
}
