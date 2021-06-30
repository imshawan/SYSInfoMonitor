Some Known Errors
=================

### 1. Problems related to PerformanceCounter

An error may occur while you click the System Diagnostics button present
below the **System Name**, along side the **Battery and Network
buttons**. This is due to the PerformanceCounter and can occur if the
PerformanceCounter is not built in your PC. Therefore you need to
rebuild performance counter setting from system backup store.

Here's how to do it...

-   Go to Start, type”cmd”.
-   Right-click on cmd.exe and choose **‘Run as administrator’**
-   then you instead need to run \
     C:\\windows\\SysWOW64\> lodctr /r
-   After which you should get

