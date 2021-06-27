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

namespace SYSInfo_Monitor.UIForms
{
    public partial class Graphics : Form
    {
        public Graphics()
        {
            InitializeComponent();
        }

        SYSInfoMonitorLib.GetSYSInfo GetInfo = new SYSInfoMonitorLib.GetSYSInfo();
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

        public void GetGraphicsData()
        {
            GraphicsInfo = GetInfo.GetGraphicsInfo();
            foreach (var val in GraphicsInfo)
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
            GetGraphicsData();
        }
    }
}
