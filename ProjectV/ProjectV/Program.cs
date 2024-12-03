
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

            //var client = new client();

            //client.testingClient();


            // Initialize all hubs
            var lockHub = new LockHub(logger);
            var sensorHub = new SensorHub(logger);
            var cameraHub = new CameraHub(logger);
            var alarmHub = new AlarmHub(logger);
            var trackerHub = new TrackerHub(logger);

            //Configure Lock Hub
            lockHub.Activate();
            lockHub.AddDevice(new Lock(1, "Front Door Lock", logger));
            //    lockHub.AddDevice(new Lock(2, "Back Door Lock", logger));
            //    lockHub.AddDevice(new Lock(3, "Garage Door Lock", logger));


            // Configure Sensor Hub
            sensorHub.Activate();
            sensorHub.AddDevice(new Sensor(4, "Living Room Motion Sensor", logger));
            //    sensorHub.AddDevice(new Sensor(5, "Kitchen Window Sensor", logger));
            //    sensorHub.AddDevice(new Sensor(6, "Basement Door Sensor", logger));


            // Configure Camera Hub
            cameraHub.Activate();
            cameraHub.AddDevice(new Camera(8, "Front Door Camera", logger));
            //    cameraHub.AddDevice(new Camera(9, "Back Door Camera", logger));
            //    cameraHub.AddDevice(new Camera(10, "Driveway Camera", logger));


            // Configure Alarm Hub
            alarmHub.Activate();
            alarmHub.AddDevice(new Alarm(12, "Main Siren", logger));
            //    alarmHub.AddDevice(new Alarm(13, "Backup Siren", logger));
            //    alarmHub.AddDevice(new Alarm(14, "Panic Button", logger));


            // Configure Tracker Hub
            trackerHub.Activate();
            trackerHub.AddDevice(new Tracker(15, "Vehicle GPS Tracker", logger));
            //    trackerHub.AddDevice(new Tracker(16, "Pet Collar Tracker", logger));
            //    trackerHub.AddDevice(new Tracker(17, "Asset Tracker", logger));
            //    Console.WriteLine("\n==========================================================================");


            // Instantiate Receiver
            var receiver = new Receiver(alarmHub, trackerHub, lockHub, sensorHub, cameraHub, logger);

            //    // show example of how we can receive a message from the home and parse it to turn on / off devices 
            //    // using their device ID and name 
            //    // HUBID, DEVICEID, DEVICENAME, TURN ON/OFF, STATE 

            Console.WriteLine("---------------------------------------------------------------------------------");

            // tracker example 
            trackerHub.ListDevices();
            string incomingMessage = "5, 15, Vehicle GPS Tracker, 1, 1";
            receiver.ParseIncomingMessage(incomingMessage);
            incomingMessage = "5, 15, Vehicle GPS Tracker, 0, 0";
            receiver.ParseIncomingMessage(incomingMessage);

            Console.WriteLine("---------------------------------------------------------------------------------");

            // alarm example 
            alarmHub.ListDevices();
            incomingMessage = "4, 12, Main Siren, 1, 1";
            receiver.ParseIncomingMessage(incomingMessage);
            incomingMessage = "4, 12, Main Siren, 0, 0";
            receiver.ParseIncomingMessage(incomingMessage);

            Console.WriteLine("---------------------------------------------------------------------------------");

            // camera 
            cameraHub.ListDevices();
            incomingMessage = "3, 8, Front Door Camera, 1, 1";
            receiver.ParseIncomingMessage(incomingMessage);
            incomingMessage = "3, 8, Front Door Camera, 0, 0";
            receiver.ParseIncomingMessage(incomingMessage);

            Console.WriteLine("---------------------------------------------------------------------------------");

            // sensor 
            sensorHub.ListDevices();
            incomingMessage = "2, 4, Living Room Motion Sensor, 1, 1";
            receiver.ParseIncomingMessage(incomingMessage);
            incomingMessage = "2, 4, Living Room Motion Sensor, 0, 0";
            receiver.ParseIncomingMessage(incomingMessage);

            Console.WriteLine("---------------------------------------------------------------------------------");

            // lock example 
            // turn on the lock and lock it 
            lockHub.ListDevices();
            incomingMessage = "1, 1, Front Door Lock, 1, 1";
            receiver.ParseIncomingMessage(incomingMessage);
            incomingMessage = "1, 1, Front Door Lock, 0, 0";
            receiver.ParseIncomingMessage(incomingMessage);

            Console.WriteLine("---------------------------------------------------------------------------------");

            // this line ensures that the console stays open
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

    }
}
