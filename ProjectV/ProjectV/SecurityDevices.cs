using ProjectV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    private AlarmHub alarmHub;

    public bool isActivated { get; set; }

    public Alarm(int deviceID, string deviceName, SecurityHubLogger logger)
        : base(deviceID, deviceName, logger)
    {
        Logger?.LogOperation(deviceName, "Alarm device created");
    }

    public void activateAlarm()
    {
        isActivated = true;
        isOn = true;
        Logger?.LogOperation(deviceName, "Alarm activated");
    }

    public void deactivateAlarm()
    {
        isActivated = false;
        isOn = false;
        Logger?.LogOperation(deviceName, "Alarm deactivated");
    }

    // Method to find and deactivate the tracker with a given ID
    public void TurnOffAlarm(int alarmID)
    {
        // Retrieve the tracker by its ID
        var device = alarmHub.GetDeviceById(alarmID); // Get device by ID
        if (device != null)
        {
            // Check if the device is actually a Tracker and cast it to Tracker
            if (device is Alarm alarm)
            {
                Console.WriteLine($"Deactivating Alarm ID {alarm.deviceID}: {alarm.deviceName}");
                alarm.deactivateAlarm();  // Deactivate the alarm
                                              //trackerHub.RemoveDevice(tracker.deviceName);
            }
            else
            {
                Console.WriteLine($"Device with ID {alarmID} is not a Alarm.");
            }
        }
        else
        {
            Console.WriteLine($"Alarm with ID {alarmID} not found.");
        }
    }

    // Method to find and activate the alarm with a given ID
    public void TurnOnAlarm(int alarmID)
    {
        // Retrieve the tracker by its ID
        var device = alarmHub.GetDeviceById(alarmID); // Get device by ID
        if (device != null)
        {
            // Check if the device is actually a Tracker and cast it to Tracker
            if (device is Alarm alarm)
            {
                Console.WriteLine($"Activating Tracker ID {alarm.deviceID}: {alarm.deviceName}");
                alarm.activateAlarm();  // activate the tracker
                                            //trackerHub.RemoveDevice(tracker.deviceName);
            }
            else
            {
                Console.WriteLine($"Device with ID {alarmID} is not a Alarm.");
            }
        }
        else
        {
            Console.WriteLine($"Alarm with ID {alarmID} not found.");
        }
    }

}

internal class Tracker : SecurityDevice
{
    private TrackerHub trackerHub;

    public bool isActivated { get; set; }
    // public double location { get; set; }

    public Tracker(int deviceID, string deviceName, SecurityHubLogger logger)
        : base(deviceID, deviceName, logger)
    {
        Logger?.LogOperation(deviceName, "Tracker device created");
    }

    public void activateTracker()
    {
        isActivated = true;
        isOn = true; 
        Logger?.LogOperation(deviceName, "Tracker activated");
    }

    public void deactivateTracker()
    {
        isActivated = false;
        isOn = false;
        Logger?.LogOperation(deviceName, "Tracker deactivated");
    }


    // Method to find and deactivate the tracker with a given ID
    public void TurnOffTracker(int trackerId)
    {
        // Retrieve the tracker by its ID
        var device = trackerHub.GetDeviceById(trackerId); // Get device by ID
        if (device != null)
        {
            // Check if the device is actually a Tracker and cast it to Tracker
            if (device is Tracker tracker)
            {
                Console.WriteLine($"Deactivating Tracker ID {tracker.deviceID}: {tracker.deviceName}");
                tracker.deactivateTracker();  // Deactivate the tracker
                                              //trackerHub.RemoveDevice(tracker.deviceName);
            }
            else
            {
                Console.WriteLine($"Device with ID {trackerId} is not a tracker.");
            }
        }
        else
        {
            Console.WriteLine($"Tracker with ID {trackerId} not found.");
        }
    }

    // Method to find and activate the tracker with a given ID
    public void TurnOnTracker(int trackerId)
    {
        // Retrieve the tracker by its ID
        var device = trackerHub.GetDeviceById(trackerId); // Get device by ID
        if (device != null)
        {
            // Check if the device is actually a Tracker and cast it to Tracker
            if (device is Tracker tracker)
            {
                Console.WriteLine($"Activating Tracker ID {tracker.deviceID}: {tracker.deviceName}");
                tracker.activateTracker();  // activate the tracker
                                            //trackerHub.RemoveDevice(tracker.deviceName);
            }
            else
            {
                Console.WriteLine($"Device with ID {trackerId} is not a tracker.");
            }
        }
        else
        {
            Console.WriteLine($"Tracker with ID {trackerId} not found.");
        }
    }


}

