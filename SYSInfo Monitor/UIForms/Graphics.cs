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
    public partial class Graphics : Form
    {
        public Graphics()
        {
            InitializeComponent();
        }

        bool status = false;
        GetSYSInfo GetInfo = new GetSYSInfo();
        private List<KeyValuePair<string, string>> GraphicsInfo = new List<KeyValuePair<string, string>>();
        bool findFirst = false;

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
                    foreach (var vals in GraphicsInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}, {vals.Value}\n");
                    }
                }
                else if (args == "txt")
                {
                    foreach (var vals in GraphicsInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}: {vals.Value}\n");
                    }
                }

                MessageBox.Show("File Saved Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetGraphicsData(List<KeyValuePair<string, string>> Values)
        {
            foreach (var val in Values)
            {
                if (val.Key.ToLower() == "name" && findFirst == false)
                {
                    label1.Text = "Preferred: " + val.Value;
                    findFirst = true;
                }
                else
                {
                    bunifuDataGridView1.Rows.Add(" " + val.Key + ":", val.Value);
                }
            }
        }

        private void Graphics_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void copyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetInfo.StringBuilderFunc(GraphicsInfo));
        }

        private void saveToTextFiletxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("txt");
        }

        private void saveDataToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("csv");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!status)
            {
                GraphicsInfo = GetInfo.GetGraphicsInfo();
                GetGraphicsData(GraphicsInfo);
                status = true;
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
