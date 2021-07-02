using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SYSInfoMonitorLib;

namespace SYSInfo_Monitor.UIForms
{
    public partial class Diagnostics : Form
    {
        bool update = false;
        public Diagnostics()
        {
            InitializeComponent();
        }

        WinAPI WinAPI = new WinAPI();

        private void Diagnostics_Load(object sender, EventArgs e)
        {
            WinAPI.AnimateWindow(this.Handle, 150, WinAPI.CENTER);

            this.StartPosition = FormStartPosition.CenterParent;
            timer1.Start();
            //UpdateDiskAndCPUInfo();


        }
        PerformanceCounter cpu = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total", true);
        ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");

        private void UpdateUiElements()
        {
            //label1.Text = ((int)Math.Round(cpu.NextValue(), 2)).ToString() + " %";

            //CPU
            bunifuCircleProgressbar1.Value = (int)Math.Round(cpu.NextValue(), 2);

            // --------------------------------------------------------------------------------------------------------
            //RAM
            float totalMemory = 0;
            float freeMemory = 0;

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                totalMemory = (float.Parse(result["TotalVisibleMemorySize"].ToString()) / 1024);
                freeMemory = (float.Parse(result["FreePhysicalMemory"].ToString()) / 1024);
            }

            var RamVal = 100 - ((freeMemory / totalMemory) * 100);
            bunifuCircleProgressbar2.Value = (int)RamVal;

            label7.Text = ((int)totalMemory).ToString() + " Megabytes";
            label8.Text = ((int)totalMemory - (int)freeMemory).ToString() + " Megabytes";
            label9.Text = ((int)freeMemory).ToString() + " Megabytes";
            
            //label1.Text = ((int)Math.Round(cpu.NextValue(), 2)).ToString() + " %";
            if (!update)
            {
                UpdateDiskAndCPUInfo();
                update = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUiElements();
        }
        private void UpdateDiskAndCPUInfo()
        {
            //CPU Elements
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                label12.Text = float.Parse(obj["CurrentClockSpeed"].ToString()) / 1000 + " GHz";
                label32.Text = obj["NumberOfCores"].ToString();
                label34.Text = obj["NumberOfLogicalProcessors"].ToString();
            }

            // --------------------------------------------------------------------------------------------------------
            //DISK SPACE
            double totalFreeSpace = 0;
            double totalSpace = 0;
            double usedSpace = 0;

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    totalFreeSpace += d.AvailableFreeSpace;
                    totalSpace += d.TotalSize;
                }
            }

            usedSpace = 100 - (totalFreeSpace / totalSpace) * 100;
            var u_s = totalSpace - totalFreeSpace;
            bunifuCircleProgressbar3.Value = (int)usedSpace;
            label18.Text = ((int)Math.Round((totalSpace / 1024d / 1024d / 1024d), 2)).ToString() + " Gigabytes";
            label20.Text = ((int)Math.Round((u_s / 1024d / 1024d / 1024d), 2)).ToString() + " Gigabytes";
            label21.Text = ((int)Math.Round((totalFreeSpace / 1024d / 1024d / 1024d), 2)).ToString() + " Gigabytes";
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0)
            {
                timer1.Stop();
                timer2.Stop();
                this.Close();
            }
            else
            {
                this.Opacity -= 0.1;
            }
        }
    }
}
