using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ProjectV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //var lockHub = new LockHub();
            //var sensorHub = new SensorHub();
            //var cameraHub = new CameraHub();

            Camera washroomCamera = new Camera(123, "WashroomCamera");
            washroomCamera.turnDeviceOn();

            Camera bedroomCamera = new Camera();

            // this line ensures that the console stays open
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
