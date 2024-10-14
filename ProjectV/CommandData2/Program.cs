using System.Threading;
using System.Threading.Tasks;

namespace Devices
{
    class Program
    {
        static void Main(string[] args)
        {
            var device = new Device();
            device.PrintCurrentState();
            Task.Run(() => device.StartDeviceAsync("127.0.0.1", 8080));

            Thread.Sleep(100000);
            device.StopDevice();
        }
    }
}
