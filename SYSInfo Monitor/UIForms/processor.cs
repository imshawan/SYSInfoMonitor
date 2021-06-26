﻿using System;
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
    public partial class Processor : Form
    {
        SYSInfoMonitorLib.GetSYSInfo GetInfo = new SYSInfoMonitorLib.GetSYSInfo();
        private List<KeyValuePair<string, string>> ProcessorInfo = new List<KeyValuePair<string, string>>();

        public Processor()
        {
            InitializeComponent();
        }
        public void GetProcessorData()
        {
            ProcessorInfo = GetInfo.GetProcessorInfo();
            foreach (var val in ProcessorInfo)
            {
                if (val.Key.ToLower() == "name")
                {
                    label1.Text = val.Value;
                }
                else
                {
                    bunifuDataGridView1.Rows.Add(" " + val.Key + ":", val.Value);
                }
            }
        }
        private void processor_Load(object sender, EventArgs e)
        {
            GetProcessorData();
        }

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
                    foreach (var vals in ProcessorInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}, {vals.Value}\n");
                    }
                }
                else if (args == "txt")
                {
                    foreach (var vals in ProcessorInfo)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}: {vals.Value}\n");
                    }
                }

                MessageBox.Show("File Saved Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void copyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetInfo.StringBuilderFunc(ProcessorInfo));
        }

        private void saveToTextFiletxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("txt");
        }

        private void saveDataToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("csv");
        }
    }
}
