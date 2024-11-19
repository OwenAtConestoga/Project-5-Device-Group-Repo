using ProjectV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("TestCases")]


// Group 7 Main 
// Security Device 

namespace ProjectV
{
    // create parent SecurityDevice class 
    public class SecurityDevice
    {
        public int deviceID { get; set; }
        public string deviceName { get; set; }
        public bool isOn { get; set; }
        protected readonly SecurityHubLogger Logger;

        public virtual void turnDeviceOn()
        {
            isOn = true;
            Logger?.LogOperation(deviceName, $"Device {deviceID} turned ON");
            Console.WriteLine(this.deviceName + this.deviceID + " is now ON");
        }

        public virtual void turnDeviceOff()
        {
            isOn = false;
            Logger?.LogOperation(deviceName, $"Device {deviceID} turned OFF");
            Console.WriteLine(this.deviceName + this.deviceID + " is now OFF");
        }

        public bool devicePowerStatus()
        {
            Logger?.LogOperation(deviceName, $"Device {deviceID} power status checked: {isOn}");
            return this.isOn; 
        }

        public SecurityDevice() // non param constructor 
        {
            this.deviceID = 0;
            this.deviceName = "Unknown";
            this.isOn = false;
        }

        public SecurityDevice(int deviceID, string deviceName, SecurityHubLogger logger)
        {
            this.deviceID = deviceID;
            this.deviceName = deviceName;
            isOn = false;
            this.Logger = logger;

            Logger?.LogOperation(deviceName, $"New security device created: ID {deviceID}");
            Console.WriteLine("New security device created!");
        }


    }

    // subclass for Camera
    internal class Camera : SecurityDevice
    {
        public bool motionDetected { get; set; }

        public Camera(int deviceID, string deviceName, SecurityHubLogger logger)
            : base(deviceID, deviceName, logger)
        {
            Logger?.LogOperation(deviceName, "Camera device created");
        }

        public void DetectMotion()
        {
            motionDetected = true;
            Logger?.LogOperation(deviceName, "Motion detected by camera");
        }
    }


    internal class Lock : SecurityDevice
    {
        public bool isLocked { get; set; }

        public Lock(int deviceID, string deviceName, SecurityHubLogger logger)
            : base(deviceID, deviceName, logger)
        {
            Logger?.LogOperation(deviceName, "Lock device created");
        }

        public void lockLock()
        {
            isLocked = true;
            Logger?.LogOperation(deviceName, "Lock is now locked");
        }

        public void unlockLock()
        {
            isLocked = false;
            Logger?.LogOperation(deviceName, "Lock is now unlocked");
        }
    }


    internal class Sensor : SecurityDevice
    {
        public bool isTriggered { get; set; }

        public Sensor(int deviceID, string deviceName, SecurityHubLogger logger)
            : base(deviceID, deviceName, logger)
        {
            Logger?.LogOperation(deviceName, "Sensor device created");
        }

        public void triggerSensor()
        {
            isTriggered = true;
            Logger?.LogOperation(deviceName, "Sensor triggered");
        }

        public void resetSensor()
        {
            isTriggered = false;
            Logger?.LogOperation(deviceName, "Sensor reset");
        }
    }
}

internal class Alarm : SecurityDevice
{
    public bool isActivated { get; set; }

    public Alarm(int deviceID, string deviceName, SecurityHubLogger logger)
        : base(deviceID, deviceName, logger)
    {
        Logger?.LogOperation(deviceName, "Alarm device created");
    }

    public void activateAlarm()
    {
        isActivated = true;
        Logger?.LogOperation(deviceName, "Alarm activated");
    }

    public void deactivateAlarm()
    {
        isActivated = false;
        Logger?.LogOperation(deviceName, "Alarm deactivated");
    }
}

internal class Tracker : SecurityDevice
{
    public bool isActivated { get; set; }
    public double location { get; set; }

    public Tracker(int deviceID, string deviceName, SecurityHubLogger logger)
        : base(deviceID, deviceName, logger)
    {
        Logger?.LogOperation(deviceName, "Tracker device created");
    }

    public void activateTracker()
    {
        isActivated = true;
        Logger?.LogOperation(deviceName, "Tracker activated");
    }

    public void deactivateTracker()
    {
        isActivated = false;
        Logger?.LogOperation(deviceName, "Tracker deactivated");
    }
}

