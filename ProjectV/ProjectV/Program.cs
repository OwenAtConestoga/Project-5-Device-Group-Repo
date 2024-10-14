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

            var lockHub = new LockHub();
            var sensorHub = new SensorHub();
            var cameraHub = new CameraHub();
        }
    }
}
