using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV
{

    public abstract class HomeSecurityHub
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public List<Device> ConnectedDevices { get; set; }

        public HomeSecurityHub(string name)
        {
            Name = name;
            IsActive = false;
            ConnectedDevices = new List<Device>();
        }

        public virtual void Activate()
        {
            IsActive = true;
            Console.WriteLine($"{Name} hub activated");
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            Console.WriteLine($"{Name} hub deactivated");
        }

        public void AddDevice(string device)
        {
            // ie lockHub.AddDevice("Front Door Lock");
            ConnectedDevices.Add(device);
            Console.WriteLine($"{device} connected to {Name} hub.");
        }

        public virtual void RemoveDevice(string deviceName)
        {
            Device deviceToRemove = ConnectedDevices.FirstOrDefault(d => d.Name == deviceName);
            if (deviceToRemove != null)
            {
                ConnectedDevices.Remove(deviceToRemove);
                Console.WriteLine($"{deviceName} removed from {Name} hub.");
            }
            else
            {
                Console.WriteLine($"{deviceName} not found in {Name} hub.");
            }
        }

        public virtual void ListDevices()
        {
            Console.WriteLine($"Devices connected to {Name} hub:");
            foreach (var device in ConnectedDevices)
            {
                Console.WriteLine($"- {device.Name}");
            }
        }
    }
    }
