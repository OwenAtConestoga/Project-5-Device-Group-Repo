using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectV
{
    public abstract class HomeSecurityHub
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public List<SecurityDevice> ConnectedDevices { get; set; }
        protected readonly SecurityHubLogger Logger;

        public HomeSecurityHub(string name, SecurityHubLogger logger)
        {
            Name = name;
            IsActive = false;
            ConnectedDevices = new List<SecurityDevice>();
            Logger = logger;
        }

        public virtual Task Activate()
        {
            IsActive = true;
            Logger.LogOperation(Name, "Hub activated");
            return Task.CompletedTask;
        }

        public virtual Task Deactivate()
        {
            IsActive = false;
            Logger.LogOperation(Name, "Hub deactivated");
            return Task.CompletedTask;
        }

        public virtual Task AddDevice(SecurityDevice device)
        {
            ConnectedDevices.Add(device);
            Logger.LogOperation(Name, $"Device added: {device.deviceName}");
            return Task.CompletedTask;
        }

        public virtual Task RemoveDevice(string deviceName)
        {
            SecurityDevice deviceToRemove = ConnectedDevices.FirstOrDefault(d => d.deviceName == deviceName);
            if (deviceToRemove != null)
            {
                ConnectedDevices.Remove(deviceToRemove);
                Logger.LogOperation(Name, $"Device removed: {deviceName}");
            }
            return Task.CompletedTask;
        }

        // This method doesn't need to be async since it's just writing to console
        public virtual void ListDevices()
        {
            Logger.LogOperation(Name, "Listing all devices");
            Console.WriteLine($"Devices connected to {Name} hub:");
            foreach (var device in ConnectedDevices)
            {
                Console.WriteLine($"- {device.deviceName}");
            }
        }
    }
}
