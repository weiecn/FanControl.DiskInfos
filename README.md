# FanControl.DiskInfos
��һЩʹ��RAID/HBA�����û����޷���ȡ��Ӳ�̵��¶���Ϣ������������ͨ����ȡWindows��wmi��Ϣ��ȡӲ�̵��¶���Ϣ��
��ʵ��ԭ��Ƚϼ�,ͨ��ִ��Get-Disk��ȡ��Ӳ����Ϣ�б�,Ȼ�������ȡ�¶�.
FanControl�᳢�Ե���Update����,Ϊ�˱���Ƶ�����õ����ڴ漰CPU��Դ���Ĺ���,���������10�볢�Ի�ȡһ���¶���Ϣ.(���޵�����Update�����Ż��ȡ,���û�е���Update����,��һֱ�����ȡ�¶���Ϣ)
 
Some users who use RAID/HBA cards may not be able to obtain the temperature information of their hard disks. This plugin can retrieve the temperature information of the hard disks by reading the WMI (Windows Management Instrumentation) information in Windows.
The implementation principle is relatively simple. It retrieves the list of hard disk information by executing the Get-Disk command and then obtains the temperature of each disk one by one.
FanControl will attempt to call the Update method. To avoid excessive consumption of memory and CPU resources caused by frequent calls, this plugin will attempt to retrieve the temperature information every 10 seconds. (The temperature information will only be retrieved if the Update method is called. If the Update method is not called, the temperature information will not be retrieved.)
 
[FanControl](https://github.com/Rem0o/FanControl.Releases)