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
    public partial class MoreItems : Form
    {
        public MoreItems()
        {
            InitializeComponent();
        }

        bool status = false;
        SYSInfoMonitorLib.GetSYSInfo GetInfo = new SYSInfoMonitorLib.GetSYSInfo();
        private List<KeyValuePair<string, string>> PrintersInfo = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> BaseBoard = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> BIOS = new List<KeyValuePair<string, string>>();

        private void GetAllValues(List<KeyValuePair<string, string>> Values, string TypeOfValue)
        {
            foreach (var val in Values)
            {
                if (TypeOfValue == "printer")
                {
                    bunifuDataGridView1.Rows.Add(" " + val.Key + ":", val.Value);
                }
                if (TypeOfValue == "motherboard")
                {
                    bunifuDataGridView2.Rows.Add(" " + val.Key + ":", val.Value);
                }
                if (TypeOfValue == "bios")
                {
                    bunifuDataGridView3.Rows.Add(" " + val.Key + ":", val.Value);
                }
            }
        }

        private void MoreItems_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!status)
            {
                PrintersInfo = GetInfo.GetPrinters();
                BaseBoard = GetInfo.GetBaseBoard();
                BIOS = GetInfo.GetBIOS();

                GetAllValues(PrintersInfo, "printer");
                GetAllValues(BaseBoard, "motherboard");
                GetAllValues(BIOS, "bios");


                status = true;
            }
            timer1.Stop();

        }
    }
}
