using FanControl.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FanControl.DiskInfos
{
    public class DiskTemperatureSensor : IPluginSensor
    {
        public DiskTemperatureSensor(string number, string friendlyName, string serialNumber)
        {
            this.Id = number;
            this.Name = $"{friendlyName.Trim(' ')} {serialNumber.Trim(' ')}";
            this.SerialNumber = serialNumber;
            this.FriendlyName = friendlyName;
            powerShell = PowerShell.Create();
            Update();
        }
        PowerShell powerShell;
        public string Id { get; private set; }

        public string Name { get; private set; }
        public string SerialNumber { get; private set; }
        public string FriendlyName { get; private set; }
        public float? Value { get; private set; }
        private Task currentTask = Task.CompletedTask;

        private bool isGet = false;
        private DateTime lastUpdateDatetime { get; set; } = DateTime.Now.AddSeconds(-100);

        public void Update()
        {
            if (currentTask.IsCompleted)
            {
                currentTask = Task.Run(async () =>
                {
                    if ((DateTime.Now - lastUpdateDatetime).TotalSeconds < 10) 
                    {
                        Console.WriteLine($"{Id} read Task is cool down");
                        return;
                    }
                    lastUpdateDatetime = DateTime.Now;
                    try
                    {
                        powerShell.Commands.Clear();
                        powerShell.AddScript($"Get-Disk -Number \"{Id}\" | Get-StorageReliabilityCounter | Select-Object -Property \"Temperature\"");
                        var results = await Task.FromResult(powerShell.Invoke()).ConfigureAwait(false);
                        if (results.Count > 0)
                        {
                            this.Value = float.Parse(results[0].Properties["Temperature"].Value.ToString());
                            Console.WriteLine($"{Id} {Name}->{this.Value}");
                        }
                        else
                        {
                            this.Value = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating temperature for {Id}: {ex.Message}");
                        this.Value = null;
                    }
                });
            }
            else
            {
                Console.WriteLine($"{Id} read Task is running");
            }
        }
       
    }
}
