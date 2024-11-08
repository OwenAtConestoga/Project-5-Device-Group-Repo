using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandData2;
using Devices;

namespace Devices
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var smartFridge = new SmartFridge();

            // Demonstrate device control via the SmartFridge interface
            Task.Run(async () =>
            {
                await smartFridge.StartFridgeDeviceAsync("127.0.0.1", 8080);

                // Wait a moment and then send custom data
                //Thread.Sleep(1000);
                //await smartFridge.SendCustomMessageAsync("Testing TCP data communication");

                //Testing state change
                Thread.Sleep(1000);
                await smartFridge.StartFridgeDeviceAsync("127.0.0.1", 8080);
                Logger.Log("SmartFridge Started", Logger.LogType.Info);
            });

            // Run the GUI in the main thread
            Application.Run(smartFridge);
            Logger.Log("GUI application running", Logger.LogType.Info);

            // Ensure the device is stopped when the form is closed
            smartFridge.StopFridgeDevice();
        }
    }
}