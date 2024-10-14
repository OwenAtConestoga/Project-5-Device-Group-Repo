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
    internal class SecurityDevice
    {
        public int deviceID { get; set; }
        public string deviceName { get; set; }
        public bool isOn { get; set; }

        public virtual void turnDeviceOn()
        {
            isOn = true;
            Console.WriteLine(this.deviceName + this.deviceID + " is now ON");
        }

        public virtual void turnDeviceOff()
        {
            isOn = false;
            Console.WriteLine(this.deviceName + this.deviceID + " is now OFF");
        }

        public bool devicePowerStatus()
        {
            return this.isOn; 
        }

        public SecurityDevice() // non param constructor 
        {
            this.deviceID = 0;
            this.deviceName = "Unknown";
            this.isOn = false;
        }

        public SecurityDevice(int deviceID, string deviceName) // param constructor
        {
            this.deviceID = deviceID;
            this.deviceName = deviceName;
            isOn = false;

            Console.WriteLine("New security deviced created!");
        }


    }

    // subclass for Camera
    internal class Camera : SecurityDevice
    {
        public bool motionDetected { get; set; }

        // constructor for the Camera class
        public Camera(int deviceID, string deviceName) : base(deviceID, deviceName)
        {

        }

        public Camera() // non param constructor
        {
            this.deviceID = 0;
            this.deviceName = "Unknown";
            this.isOn = false;
        }

    }

    internal class Lock : SecurityDevice
    {
        public bool isLocked { get; set;}

        public void lockLock()
        {
            this.isLocked = true;
        }

        public void unlockLock()
        {
            this.isLocked = false;
        }

        public bool checkLockStatus()
        {
            return this.isLocked;
        }

    }

    internal class Sensor : SecurityDevice
    {
        public bool isTriggered { get; set;}

        public void triggerSensor() 
        { 
            this.isTriggered = true;
        }

        public void resetSensor()
        {
            this.isTriggered = false;
        }

        public bool getSensorStatus()
        {
            return this.isTriggered;
        }

        // implement send alarm function somehow 

    }

    internal class Alarm : SecurityDevice 
    { 
        public bool isActivated { get; set;}

        public void activateAlarm()
        {
            this.isActivated = true;
        }

        public void deactivateAlarm()
        {
            this.isActivated = false;
        }
    }

    internal class Tracker : SecurityDevice
    {
        public bool isActivated { get; set;}
        public double location { get; set;}

        public bool getTrackerStatus()
        {
                return this.isActivated;
        }

    }

    class MainProgram
    {
        static void Main(string[] args)
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera");
            washroomCamera.turnDeviceOn();

            Camera bedroomCamera = new Camera();

            // this line ensures that the console stays open
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
