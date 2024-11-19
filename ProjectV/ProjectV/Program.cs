
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new SecurityHubLogger();

            Console.WriteLine($"Operations log location: {logger.GetOperationsLogPath()}");

            //Console.WriteLine("Hello, World!");

            //// testing alarms in log files
            //// Initialize AlarmHub
            //var alarmHub = new AlarmHub(logger);

            //// Create and add alarms to the hub
            //Alarm frontDoorAlarm = new Alarm(789, "FrontDoorAlarm", null);
            //Alarm backDoorAlarm = new Alarm(101, "BackDoorAlarm", null);

            //// Turn on the alarms
            //frontDoorAlarm.turnDeviceOn();
            //backDoorAlarm.turnDeviceOn();

            //var lockHub = new LockHub(logger);
            ////var sensorHub = new SensorHub();
            ////var cameraHub = new CameraHub();

            //Camera washroomCamera = new Camera(123, "WashroomCamera", null);
            //washroomCamera.turnDeviceOn();

            //Camera bedroomCamera = new Camera(456, "FrontDoorCamera", null);


            // Initialize all hubs
            var lockHub = new LockHub(logger);
            var sensorHub = new SensorHub(logger);
            var cameraHub = new CameraHub(logger);
            var alarmHub = new AlarmHub(logger);
            var trackerHub = new TrackerHub(logger);

            // Configure Lock Hub
            lockHub.Activate();
            lockHub.AddDevice(new Lock(1, "Front Door Lock", logger));
            lockHub.AddDevice(new Lock(2, "Back Door Lock", logger));
            lockHub.AddDevice(new Lock(3, "Garage Door Lock", logger));
            Console.WriteLine("\n=== Lock Hub Devices ===");
            lockHub.ListDevices();

            // Configure Sensor Hub
            sensorHub.Activate();
            sensorHub.AddDevice(new Sensor(4, "Living Room Motion Sensor", logger));
            sensorHub.AddDevice(new Sensor(5, "Kitchen Window Sensor", logger));
            sensorHub.AddDevice(new Sensor(6, "Basement Door Sensor", logger));
            sensorHub.AddDevice(new Sensor(7, "Garage Motion Sensor", logger));
            Console.WriteLine("\n=== Sensor Hub Devices ===");
            sensorHub.ListDevices();

            // Configure Camera Hub
            cameraHub.Activate();
            cameraHub.AddDevice(new Camera(8, "Front Door Camera", logger));
            cameraHub.AddDevice(new Camera(9, "Back Door Camera", logger));
            cameraHub.AddDevice(new Camera(10, "Driveway Camera", logger));
            cameraHub.AddDevice(new Camera(11, "Garage Camera", logger));
            Console.WriteLine("\n=== Camera Hub Devices ===");
            cameraHub.ListDevices();

            // Configure Alarm Hub
            alarmHub.Activate();
            alarmHub.AddDevice(new Alarm(12, "Main Siren", logger));
            alarmHub.AddDevice(new Alarm(13, "Backup Siren", logger));
            alarmHub.AddDevice(new Alarm(14, "Panic Button", logger));
            Console.WriteLine("\n=== Alarm Hub Devices ===");
            alarmHub.ListDevices();

            // Configure Tracker Hub
            trackerHub.Activate();
            trackerHub.AddDevice(new Tracker(15, "Vehicle GPS Tracker", logger));
            trackerHub.AddDevice(new Tracker(16, "Pet Collar Tracker", logger));
            trackerHub.AddDevice(new Tracker(17, "Asset Tracker", logger));
            Console.WriteLine("\n=== Tracker Hub Devices ===");
            trackerHub.ListDevices();

            Console.WriteLine("\n=== Deactivation Of Devices ===");

            // Instantiate Receiver
            var receiver = new Receiver(trackerHub, logger);

            receiver.TurnOffTracker(16);
            receiver.CheckTrackerState(16);  // This will print if the tracker is ON or OFF
            Console.WriteLine();


            // List all devices and their statuses
            trackerHub.ListDevices();

            Console.WriteLine(); 

            trackerHub.RemoveDevice("Pet Collar Tracker");
            Console.WriteLine();

            trackerHub.ListDevices();



            // this line ensures that the console stays open
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

    }
}
