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

        // Add these new fields
        protected readonly SecurityHubLogger Logger;

        // Update the constructor
        public HomeSecurityHub(string name, SecurityHubLogger logger)
        {
            Name = name;
            IsActive = false;
            ConnectedDevices = new List<SecurityDevice>();
            Logger = logger;
        }

        // Update methods to be async and include logging
        public virtual async Task Activate()
        {
            IsActive = true;
            Logger.LogOperation(Name, "Hub activated");
        }

        public virtual async Task Deactivate()
        {
            IsActive = false;
            Logger.LogOperation(Name, "Hub deactivated");
        }

        public virtual async Task AddDevice(SecurityDevice device)
        {
            ConnectedDevices.Add(device);
            Logger.LogOperation(Name, $"Device added: {device.deviceName}");
        }

        public virtual async Task RemoveDevice(string deviceName)
        {
            SecurityDevice deviceToRemove = ConnectedDevices.FirstOrDefault(d => d.deviceName == deviceName);
            if (deviceToRemove != null)
            {
                ConnectedDevices.Remove(deviceToRemove);
                Logger.LogOperation(Name, $"Device removed: {deviceName}");
            }
        }

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