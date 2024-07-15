# FanControl.DiskInfos
有一些使用RAID/HBA卡的用户会无法获取到硬盘的温度信息，这个插件可以通过读取Windows的wmi信息获取硬盘的温度信息。
其实现原理比较简单,通过执行Get-Disk获取到硬盘信息列表,然后逐个获取温度.
FanControl会尝试调用Update方法,为了避免频繁调用导致内存及CPU资源消耗过大,本插件会间隔10秒尝试获取一次温度信息.(仅限调用了Update方法才会获取,如果没有调用Update方法,则一直不会获取温度信息)
 
Some users who use RAID/HBA cards may not be able to obtain the temperature information of their hard disks. This plugin can retrieve the temperature information of the hard disks by reading the WMI (Windows Management Instrumentation) information in Windows.
The implementation principle is relatively simple. It retrieves the list of hard disk information by executing the Get-Disk command and then obtains the temperature of each disk one by one.
FanControl will attempt to call the Update method. To avoid excessive consumption of memory and CPU resources caused by frequent calls, this plugin will attempt to retrieve the temperature information every 10 seconds. (The temperature information will only be retrieved if the Update method is called. If the Update method is not called, the temperature information will not be retrieved.)
 
[FanControl](https://github.com/Rem0o/FanControl.Releases)