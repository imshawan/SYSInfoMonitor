using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Globalization;

namespace SYSInfoMonitorLib
{
    public class GetSYSInfo
    {
        // Load all suffixes in an array  
        static readonly string[] suffixes =
        { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        public string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }

        public string StringBuilderFunc(List<KeyValuePair<string, string>> data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var values in data)
            {
                sb.AppendFormat("{0}: {1}", values.Key, values.Value);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public string[] GetProcessorInfo()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            string[] processorInfo = new string[13];
            int i = 0;
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                processorInfo[i] = obj["Name"].ToString(); i++;
                processorInfo[i] = obj["DeviceID"].ToString(); i++;
                processorInfo[i] = obj["Manufacturer"].ToString(); i++;
                processorInfo[i] = (float.Parse(obj["CurrentClockSpeed"].ToString()) / 1000).ToString() + "Ghz"; i++;
                processorInfo[i] = obj["Caption"].ToString(); i++;
                processorInfo[i] = obj["NumberOfCores"].ToString(); i++;
                processorInfo[i] = obj["NumberOfLogicalProcessors"].ToString(); i++;
                processorInfo[i] = obj["VirtualizationFirmwareEnabled"].ToString(); i++;
                processorInfo[i] = obj["Architecture"].ToString(); i++;
                processorInfo[i] = obj["ProcessorType"].ToString(); i++;
                try
                {
                    processorInfo[i] = obj["Characteristics"].ToString(); i++;
                }
                catch (Exception)
                {
                    processorInfo[i] = "Not Found"; i++;
                }
                processorInfo[i] = obj["AddressWidth"].ToString() + "Bit"; i++;
            }
            return processorInfo;
        }
        public List<KeyValuePair<string, string>>  GetProcessorInfoInKeyValuePair()
        {
            var processorInfo = new List<KeyValuePair<string, string>>();
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                processorInfo.Add(new KeyValuePair<string, string>("Name", $"{obj["Name"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("DeviceID", $"{obj["DeviceID"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("Manufacturer", $"{obj["Manufacturer"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("CurrentClockSpeed", $"{float.Parse(obj["CurrentClockSpeed"].ToString()) / 1000}Ghz"));
                processorInfo.Add(new KeyValuePair<string, string>("Caption", $"{obj["Caption"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("NumberOfCores", $"{obj["NumberOfCores"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("NumberOfLogicalProcessors", $"{obj["NumberOfLogicalProcessors"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("VirtualizationFirmwareEnabled", $"{obj["VirtualizationFirmwareEnabled"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("Architecture", $"{obj["Architecture"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("Family", $"{obj["Family"]}"));
                processorInfo.Add(new KeyValuePair<string, string>("ProcessorType", $"{obj["ProcessorType"]}"));
                try
                {
                    processorInfo.Add(new KeyValuePair<string, string>("Characteristics", $"{obj["Characteristics"]}"));
                }
                catch (Exception)
                {
                    processorInfo.Add(new KeyValuePair<string, string>("Characteristics", "Not Found"));
                }
                processorInfo.Add(new KeyValuePair<string, string>("AddressWidth", $"{obj["AddressWidth"]}bit"));
            }
            return processorInfo;
        }
        public List<KeyValuePair<string, string>> GetGraphicsInfo()
        {
            var GraphicsInfo = new List<KeyValuePair<string, string>>();
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject obj in myVideoObject.Get())
            {
                GraphicsInfo.Add(new KeyValuePair<string, string>("Name", $"{obj["Name"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("Status", $"{obj["Status"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("Caption", $"{obj["Caption"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("DeviceID", $"{obj["DeviceID"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("AdapterRAM", $"{FormatSize(Convert.ToInt64(obj["AdapterRAM"]))}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("AdapterDACType", $"{obj["AdapterDACType"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("Monochrome", $"{obj["Monochrome"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("InstalledDisplayDrivers", $"{obj["InstalledDisplayDrivers"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("DriverVersion", $"{obj["DriverVersion"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("VideoProcessor", $"{obj["VideoProcessor"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("VideoArchitecture", $"{obj["VideoArchitecture"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>("VideoMemoryType", $"{obj["VideoMemoryType"]}"));
                GraphicsInfo.Add(new KeyValuePair<string, string>(" ", " "));
            }
            return GraphicsInfo;
        }

        public List<KeyValuePair<string, string>> GetStorageInfo()
        {
            var StorageInfo = new List<KeyValuePair<string, string>>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                // Console.WriteLine("Drive {0}", d.Name);

                if (d.IsReady == true)
                {
                    StorageInfo.Add(new KeyValuePair<string, string>("DriveName", $"{d.Name}"));
                    if (d.Name.ToLower().Contains("c"))
                    {
                        StorageInfo.Add(new KeyValuePair<string, string>("sysdrive", $"{FormatSize(Convert.ToInt64(d.AvailableFreeSpace))} of {FormatSize(Convert.ToInt64(d.TotalSize))} Available"));
                    }
                    StorageInfo.Add(new KeyValuePair<string, string>("DriveInfo", $"{d.DriveType}"));
                    StorageInfo.Add(new KeyValuePair<string, string>("VolumeLabel", $"{d.VolumeLabel}"));
                    StorageInfo.Add(new KeyValuePair<string, string>("DriveFormat", $"{d.DriveFormat}"));
                    StorageInfo.Add(new KeyValuePair<string, string>("AvailableFreeSpace", $"{FormatSize(Convert.ToInt64(d.AvailableFreeSpace))}"));
                    StorageInfo.Add(new KeyValuePair<string, string>("TotalSize", $"{FormatSize(Convert.ToInt64(d.TotalSize))}"));
                    StorageInfo.Add(new KeyValuePair<string, string>("RootDirectory", $"{d.RootDirectory}"));
                    StorageInfo.Add(new KeyValuePair<string, string>(" ", " "));
                }
            }
            return StorageInfo;
        }
        public List<KeyValuePair<string, string>> GetOSInfo()
        {
            var OSInfo = new List<KeyValuePair<string, string>>();

            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                OSInfo.Add(new KeyValuePair<string, string>("Name", $"{obj["Caption"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("OSType", $"{obj["OSType"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("Version", $"{obj["Version"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("WindowsDirectory", $"{obj["WindowsDirectory"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("ProductType", $"{obj["ProductType"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("SerialNumber", $"{obj["SerialNumber"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("SystemDirectory", $"{obj["SystemDirectory"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("CountryCode", $"{obj["CountryCode"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("CurrentTimeZone", $"{obj["CurrentTimeZone"]}"));
                OSInfo.Add(new KeyValuePair<string, string>("EncryptionLevel", $"{obj["EncryptionLevel"]}-bit Encryption"));
            }
            return OSInfo;
        }

        public List<KeyValuePair<string, string>> GetNetworkInformation()
        {
            var NetworkInformation = new List<KeyValuePair<string, string>>();

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics == null || nics.Length < 1)
            {
                NetworkInformation.Add(new KeyValuePair<string, string>("Error", "No Network Interfaces Found!"));
            }
            else
            {
                foreach (NetworkInterface adapter in nics)
                {
                    string versions = "";
                    if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                    {
                        versions = "IPv4";
                    }
                    if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                    {
                        if (versions.Length > 0)
                        {
                            versions += ", ";
                        }
                        versions += "IPv6";
                    }
                    IPInterfaceProperties properties = adapter.GetIPProperties();

                    NetworkInformation.Add(new KeyValuePair<string, string>("Description", $"{adapter.Description}"));
                    NetworkInformation.Add(new KeyValuePair<string, string>("NetworkInterfaceType", $"{adapter.NetworkInterfaceType}"));
                    NetworkInformation.Add(new KeyValuePair<string, string>("PhysicalAddress", $"{adapter.GetPhysicalAddress().ToString()}"));
                    NetworkInformation.Add(new KeyValuePair<string, string>("IPVersion", $"{versions}"));
                    NetworkInformation.Add(new KeyValuePair<string, string>("OperationalStatus", $"{adapter.OperationalStatus}"));
                    NetworkInformation.Add(new KeyValuePair<string, string>(" ", $" "));
                }
            }
            return NetworkInformation;
        }
        public List<KeyValuePair<string, string>> GetAudioDevices()
        {
            var AudioDevices = new List<KeyValuePair<string, string>>();

            ManagementObjectSearcher myAudioObject = new ManagementObjectSearcher("select * from Win32_SoundDevice");

            foreach (ManagementObject obj in myAudioObject.Get())
            {
                AudioDevices.Add(new KeyValuePair<string, string>("Name", $"{obj["Name"]}"));
                AudioDevices.Add(new KeyValuePair<string, string>("ProductName", $"{obj["ProductName"]}"));
                AudioDevices.Add(new KeyValuePair<string, string>("Availability", $"{obj["Availability"]}"));
                AudioDevices.Add(new KeyValuePair<string, string>("DeviceID", $"{obj["DeviceID"]}"));
                AudioDevices.Add(new KeyValuePair<string, string>("PowerManagementSupported", $"{obj["PowerManagementSupported"]}"));
                AudioDevices.Add(new KeyValuePair<string, string>("Status", $"{obj["Status"]}"));
                AudioDevices.Add(new KeyValuePair<string, string>("StatusInfo", $"{obj["StatusInfo"]}"));
                AudioDevices.Add(new KeyValuePair<string, string>(" ", $" "));

            }
            return AudioDevices;
        }
        
        public List<KeyValuePair<string, string>> GetPrinters()
        {
            var Printers = new List<KeyValuePair<string, string>>();

            ManagementObjectSearcher myPrinterObject = new ManagementObjectSearcher("select * from Win32_Printer");

            foreach (ManagementObject obj in myPrinterObject.Get())
            {
                Printers.Add(new KeyValuePair<string, string>("Name", $"{obj["Name"]}"));
                Printers.Add(new KeyValuePair<string, string>("Network", $"{obj["Network"]}"));
                Printers.Add(new KeyValuePair<string, string>("Availability", $"{obj["Availability"]}"));
                Printers.Add(new KeyValuePair<string, string>("Default", $"{obj["Default"]}"));
                Printers.Add(new KeyValuePair<string, string>("DeviceID", $"{obj["DeviceID"]}"));
                Printers.Add(new KeyValuePair<string, string>("Status", $"{obj["Status"]}"));
                Printers.Add(new KeyValuePair<string, string>(" ", $" "));

            }
            return Printers;
        }

        public List<KeyValuePair<string, string>> GetBaseBoard()
        {
            var BaseBoard = new List<KeyValuePair<string, string>>();

            ManagementObjectSearcher myPrinterObject = new ManagementObjectSearcher("select * from Win32_BaseBoard");

            foreach (ManagementObject obj in myPrinterObject.Get())
            {
                BaseBoard.Add(new KeyValuePair<string, string>("Name", $"{obj["Name"]}"));
                BaseBoard.Add(new KeyValuePair<string, string>("Description", $"{obj["Description"]}"));
                BaseBoard.Add(new KeyValuePair<string, string>("Manufacturer", $"{obj["Manufacturer"]}"));
                BaseBoard.Add(new KeyValuePair<string, string>("Model", $"{obj["Model"]}"));
                BaseBoard.Add(new KeyValuePair<string, string>("Removable", $"{obj["Removable"]}"));
                BaseBoard.Add(new KeyValuePair<string, string>("SerialNumber", $"{obj["SerialNumber"]}"));
                BaseBoard.Add(new KeyValuePair<string, string>("Status", $"{obj["Status"]}"));
                BaseBoard.Add(new KeyValuePair<string, string>("Version", $"{obj["Version"]}"));

            }
            return BaseBoard;
        }

        public string DateTimeToString(String DateTime)
        {
            DateTime dt = ManagementDateTimeConverter.ToDateTime(DateTime);
            return dt.ToString("dd-MMM-yyyy");//date format
        }

        public List<KeyValuePair<string, string>> GetBIOS()
        {
            var BIOS = new List<KeyValuePair<string, string>>();

            ManagementObjectSearcher myPrinterObject = new ManagementObjectSearcher("select * from Win32_BIOS");

            foreach (ManagementObject obj in myPrinterObject.Get())
            {
                BIOS.Add(new KeyValuePair<string, string>("Name", $"{obj["Name"]}"));
                BIOS.Add(new KeyValuePair<string, string>("Manufacturer", $"{obj["Manufacturer"]}"));
                BIOS.Add(new KeyValuePair<string, string>("ReleaseDate", $"{DateTimeToString(obj["ReleaseDate"].ToString())}"));
                BIOS.Add(new KeyValuePair<string, string>("Description", $"{obj["Description"]}"));
                BIOS.Add(new KeyValuePair<string, string>("SerialNumber", $"{obj["SerialNumber"]}"));
                BIOS.Add(new KeyValuePair<string, string>("SMBIOSBIOSVersion", $"{obj["SMBIOSBIOSVersion"]}"));
                BIOS.Add(new KeyValuePair<string, string>("Status", $"{obj["Status"]}"));
                BIOS.Add(new KeyValuePair<string, string>("Version", $"{obj["Version"]}"));

            }
            return BIOS;
        }

        public string GetAllActiveNICs()
        {
            string NICs = string.Empty;
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    var addr = ni.GetIPProperties().GatewayAddresses.FirstOrDefault();
                    if (addr != null)
                    {
                        if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        {
                            NICs = ni.Name + "," + ni.Description;
                            foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                            {
                                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                {
                                     NICs += "," + ip.Address + "," + ip.IPv4Mask;
                                }
                            }
                        }
                    }
                }
            }
            return NICs;
        }
    }
    public class WinAPI
    {
        // WinAPI Windows Load Animate
        public const int HOR_POSITIVE = 0X1;
        public const int HOR_NEGATIVE = 0X2;

        public const int VER_POSITIVE = 0X4;
        public const int VER_NEGATIVE = 0X8;

        public const int CENTER = 0X10;

        public const int BLEND = 0X80000;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwflag);
    }
}
