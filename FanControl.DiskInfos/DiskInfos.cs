using FanControl.DiskInfos;
using FanControl.Plugins;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace FanControl.DiskInfosPlugin
{
    public class DiskInfosPlugin : IPlugin2, IDisposable
    {
        private bool _initialized;

        public string Name => "DiskInfos";
        public void Close()
        {

        }

        public void Dispose()
        {

        }
        //Get-Disk  -Number 1 | Get-StorageReliabilityCounter | Select-Object -Property "Temperature"
        public void Initialize()
        {


                using (var powerShell = PowerShell.Create())
                {
                    powerShell.AddScript("Get-Disk");

                    var results = powerShell.Invoke();

                    foreach (var result in results)
                    {
                        _sensors.Add(new DiskTemperatureSensor($"{result.Properties["Number"].Value}", $"{result.Properties["FriendlyName"].Value}", $"{result.Properties["SerialNumber"].Value}"));
                    }
                    powerShell.Dispose();
                }
            _initialized = true;
        }

        public List<DiskTemperatureSensor> _sensors = new List<DiskTemperatureSensor>();

        public void Load(IPluginSensorsContainer _container)
        {
            if (!_initialized)
            {
                return;
            }

            _container.TempSensors.AddRange(_sensors);
        }

        public void Update()
        {
            if (!_initialized) return;

        }
    }
}
