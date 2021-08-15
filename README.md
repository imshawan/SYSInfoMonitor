[![SYSInfoMonitor: Passing](https://img.shields.io/badge/SYSInfoMonitor-Passing-green)](https://github.com/imshawan/SYSInfoMonitor)
[![GitHub license](https://img.shields.io/github/license/imshawan/SYSInfoMonitor)](https://github.com/imshawan/SYSInfoMonitor/blob/main/LICENSE)
![Release: 1.3.3](https://img.shields.io/badge/Release-1.3.3-informational)

# SYSInfoMonitor

**SYSInfoMonitor** is an open-source System Information Monitor with modern user interface that can be used to monitor hardware and software information of a PC. This software can monitor the temperature sensors, clock speeds, load, graphics, memory, printers, baseboard and more of your computer.

<p>
	<img src="https://github.com/imshawan/SYSInfoMonitor/blob/main/src/sysinfo.jpg" width="500px">
</p>

## Getting System Information out using the software?

The SYSInfoMonitor can read information from devices such as:

-   Motherboards
-   Processor Informations
-   All graphics cards available
-   Audio devices
-   Printers available
-   BIOS information
-   HDD, SSD and NVMe or any storage drives
-   Network Interface cards

> **Note:** You can also save the displayed information to a text or csv file.

## Core Contents of the software

| Name | .NET Description                    |
| ------------- | ------------------------------ |
| **SYSInfoMonitor:** The main Windows Forms based application that presents all data in a graphical interface     | **.NET Framework 4.5**     |
| **SYSInfoMonitorLib:**  The Library which contains all features required in the main application   | **.NET Framework 4.5**     |

### External Frameworks and Library used for designing and extracting information:
| Framework | Uses                    |
| ------------- | ------------------------------ |
| **Bunifu Winforms 1.4**     | For making the Windows Forms based application more attractive.     |
| **OpenHardwareMonitorLib**    | For extracting CPU and it's Core thermal data     |

## Using the software
<ol>
<li><b>Download the ready to use latest builds</b></li>
If you're signed in to GitHub, you can also download all the latest builds and binaries <a href="https://github.com/imshawan/SYSInfoMonitor/releases">here</a>.
<li><b>Compiling the application locally on your computer</b>
<ul>
<li>Clone the git repository of SYSInfoMonitor<br>
	 `git clone https://github.com/imshawan/SYSInfoMonitor.git`
 </li>
<li>Add the C# reference the libraries in your local computer</li>
<li>Now you're ready to compile.</li>
</ul>
</li>
</ol>

>  **Note** You need to have Bunifu Framework libraries and OpenHardwareMonitorLib.dll present in your computer to compile the app locally.
  
## System Requirements
.NET Framework 4.5

## Important links

-   [EULA](https://github.com/imshawan/SYSInfoMonitor/blob/main/docs/EULA.md)
-   [LICENSE.md](https://github.com/imshawan/SYSInfoMonitor/blob/main/docs/gpl-3.0-LICENSE.md)
-   [Known Errors](https://github.com/imshawan/SYSInfoMonitor/blob/main/docs/ERRORS.md)
