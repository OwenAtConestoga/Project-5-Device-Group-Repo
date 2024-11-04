﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectV
{
    public abstract class HomeSecurityHub
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public List<Device> ConnectedDevices { get; set; }

        // Add these new fields
        protected readonly SecurityHubLogger Logger;
        protected readonly IStatusReporter StatusReporter;

        // Update the constructor
        public HomeSecurityHub(string name, SecurityHubLogger logger, IStatusReporter statusReporter)
        {
            Name = name;
            IsActive = false;
            ConnectedDevices = new List<Device>();
            Logger = logger;
            StatusReporter = statusReporter;
        }

        // Update methods to be async and include logging
        public virtual async Task Activate()
        {
            IsActive = true;
            Logger.LogOperation(Name, "Hub activated");
            await StatusReporter.SendStatusUpdateAsync(Name, "Active");
        }

        public virtual async Task Deactivate()
        {
            IsActive = false;
            Logger.LogOperation(Name, "Hub deactivated");
            await StatusReporter.SendStatusUpdateAsync(Name, "Inactive");
        }

        public virtual async Task AddDevice(Device device)
        {
            ConnectedDevices.Add(device);
            Logger.LogOperation(Name, $"Device added: {device.Name}");
            await StatusReporter.SendStatusUpdateAsync(Name, $"Device added: {device.Name}");
        }

        public virtual async Task RemoveDevice(string deviceName)
        {
            Device deviceToRemove = ConnectedDevices.FirstOrDefault(d => d.Name == deviceName);
            if (deviceToRemove != null)
            {
                ConnectedDevices.Remove(deviceToRemove);
                Logger.LogOperation(Name, $"Device removed: {deviceName}");
                await StatusReporter.SendStatusUpdateAsync(Name, $"Device removed: {deviceName}");
            }
        }

        public virtual void ListDevices()
        {
            Logger.LogOperation(Name, "Listing all devices");
            Console.WriteLine($"Devices connected to {Name} hub:");
            foreach (var device in ConnectedDevices)
            {
                Console.WriteLine($"- {device.Name}");
            }
        }
    }
}