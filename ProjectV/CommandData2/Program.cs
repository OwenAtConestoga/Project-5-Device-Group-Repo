using CommandData2;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devices
{
    class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            string serverIp = "127.0.0.1";
            int serverPort = 5000;
            var tcpManager = new SharedTcpManager(serverIp, serverPort);

            // Initialize devices
            var smartFridge = new SmartFridge(tcpManager);

            // Start device UI
            Console.WriteLine("Running UIs");
            Task.Run(() => Application.Run(smartFridge));

            // Monitor and send periodic updates
            var monitorTask = MonitorDevicesAsync(smartFridge);
            var updateTask = UpdateDevicesAsync(smartFridge);
            await Task.WhenAll(monitorTask, updateTask);

            // Cleanup
            AppDomain.CurrentDomain.ProcessExit += (s, e) => tcpManager.CloseConnection();
        }

        private static async Task MonitorDevicesAsync(SmartFridge smartFridge)
        {
            while (true)
            {
                Console.WriteLine("Waiting for incoming data...");
                await smartFridge.HandleReceivedDataAsync(); // Does nothing until it receives data
                Console.WriteLine("Data successfully handled");
                await Task.Delay(100); // Rate Limiting
            }
        }

        private static async Task UpdateDevicesAsync(SmartFridge smartFridge)
        {
            while (true)
            {
                // Periodically send device updates
                Console.WriteLine("Sending update...");
                await smartFridge.SendDeviceDataAsync();
                Console.WriteLine("Update sent");
                Thread.Sleep(30000); //Loop every 30s
            }
        }
    }
}
