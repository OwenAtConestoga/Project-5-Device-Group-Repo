
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

            Console.WriteLine("Hello, World!");

            // testing alarms in log files
            // Initialize AlarmHub
            var alarmHub = new AlarmHub(logger);

            // Create and add alarms to the hub
            Alarm frontDoorAlarm = new Alarm(789, "FrontDoorAlarm", null);
            Alarm backDoorAlarm = new Alarm(101, "BackDoorAlarm", null);

            // Turn on the alarms
            frontDoorAlarm.turnDeviceOn();
            backDoorAlarm.turnDeviceOn();

            var lockHub = new LockHub(logger);
            //var sensorHub = new SensorHub();
            //var cameraHub = new CameraHub();

            Camera washroomCamera = new Camera(123, "WashroomCamera", null);
            washroomCamera.turnDeviceOn();

            Camera bedroomCamera = new Camera(456, "FrontDoorCamera", null);

            // this line ensures that the console stays open
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }
}
