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
    public partial class Storage : Form
    {
        public Storage()
        {
            InitializeComponent();
        }
        GetSYSInfo GetInfo = new GetSYSInfo();
        private List<KeyValuePair<string, string>> StorageInfo = new List<KeyValuePair<string, string>>();

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
                    foreach (var vals in StorageInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}, {vals.Value}\n");
                    }
                }
                else if (args == "txt")
                {
                    foreach (var vals in StorageInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}: {vals.Value}\n");
                    }
                }

                MessageBox.Show("File Saved Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetStorageData()
        {
            string sysdrive = " ";
            string usage = " ";
            StorageInfo = GetInfo.GetStorageInfo();
            foreach (var val in StorageInfo)
            {
                if (val.Key.ToLower() == "drivename" && val.Value.ToLower().Contains("c"))
                {
                    sysdrive = "System Drive> " + val.Value + " - ";
                }
                else if (val.Key == "sysdrive")
                {
                    usage = val.Value;
                }
                else
                {
                    bunifuDataGridView1.Rows.Add(" " + val.Key + ":", val.Value);
                }
            }
            label1.Text = sysdrive + usage;
        }

        private void Storage_Load(object sender, EventArgs e)
        {
            GetStorageData();
        }

        private void copyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetInfo.StringBuilderFunc(StorageInfo));
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
    }
}
