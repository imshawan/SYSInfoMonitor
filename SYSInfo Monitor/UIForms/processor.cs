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
using OpenHardwareMonitor.Hardware;
using System.Diagnostics;

namespace SYSInfo_Monitor.UIForms
{
    public partial class Processor : Form
    {
        GetSYSInfo GetInfo = new GetSYSInfo();
        private string[] ProcessorInfo = new string[13];
        List<KeyValuePair<string, string>> KeyValuePairsToStr = new List<KeyValuePair<string, string>>();
        bool status = false;

        public Processor()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                // Closed by calling Close()
                timer1.Stop();
                timer2.Stop();
            }

            else
            {
                // Closed by X or Alt+F4"
                timer1.Stop();
                timer2.Stop();
            }
        }
        UpdateVisitor updateVisitor = new UpdateVisitor();
        Computer computer = new Computer();
        public List<KeyValuePair<string, string>> GetThermalsInfo()
        {
            List<KeyValuePair<string, string>> ThermalData = new List<KeyValuePair<string, string>>();

            
            computer.Open();
            computer.CPUEnabled = true;
            
            computer.Accept(updateVisitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            ThermalData.Add(new KeyValuePair<string, string>(computer.Hardware[i].Sensors[j].Name, computer.Hardware[i].Sensors[j].Value.ToString()));
                    }
                }
            }
            //computer.Close();
            return ThermalData;
        }

        public void GetProcessorData(string[] Values)
        {
            int i = 0;
            label1.Text = Values[i]; i++;
            label11.Text = Values[i]; i++;
            label13.Text = Values[i]; i++;
            label17.Text = Values[i]; i++;
            label12.Text = Values[i]; i++;
            label15.Text = Values[i]; i++;
            label18.Text = Values[i]; i++;
            label14.Text = Values[i]; i++;
            label16.Text = Values[i]; i++;
            label19.Text = Values[i]; i++;
            label21.Text = Values[i]; i++;
            label23.Text = Values[i]; i++;
        }
        private void processor_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }

        private void SaveToFile(string args, string content)
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
                    foreach (var vals in KeyValuePairsToStr)
                    {
                        File.AppendAllText(filePath, $"{vals.Key}, {vals.Value}\n");
                    }
                    File.AppendAllText(filePath, "\nThermal Informtion:\n");
                    foreach (var vals in GetThermalsInfo())
                    {
                        File.AppendAllText(filePath, $"{vals.Key}, {vals.Value}\n");
                    }
                }
                else if (args == "txt")
                {
                    File.AppendAllText(filePath, content);
                }

                MessageBox.Show("File Saved Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void copyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = "\nThermal Information: \n" + GetInfo.StringBuilderFunc(GetThermalsInfo());
            Clipboard.SetText(GetInfo.StringBuilderFunc(KeyValuePairsToStr) + temp);
        }

        private void saveToTextFiletxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = GetInfo.StringBuilderFunc(KeyValuePairsToStr) +
                "\nThermal Information: \n" + GetInfo.StringBuilderFunc(GetThermalsInfo());
            SaveToFile("txt", temp);
        }

        private void saveDataToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile("csv", string.Empty);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!status)
            {
                ProcessorInfo = GetInfo.GetProcessorInfo();
                GetProcessorData(ProcessorInfo);
                KeyValuePairsToStr = GetInfo.GetProcessorInfoInKeyValuePair();
                status = true;
            }
            timer1.Stop();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var temp = GetThermalsInfo();
            bunifuDataGridView1.Rows.Clear();
            foreach (var vals in temp)
            {
                if (vals.Key.ToLower().Contains("package"))
                {
                    label25.Text = vals.Value + "°C";
                }
                else
                {
                    bunifuDataGridView1.Rows.Add(vals.Key + ":", vals.Value + "°C");
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            
        }
    }
    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
