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

            //Initialize Devices
            var smartFridge = new SmartFridge();
            var smartDehumidifier = new SmartDehumidifier();

            //Start Devices
            Logger.Log("SmartFridge running", Logger.LogType.Info);
            smartFridge.Show();
            Logger.Log("SmartDehumidifier running", Logger.LogType.Info);
            smartDehumidifier.Show();

            // Run the main message loop
            Application.Run();

            // Close devices before ending program
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                smartFridge.StopFridgeDevice();
                smartDehumidifier.StopDehumidifierDevice();
                Logger.Log("Devices stopped", Logger.LogType.Info);
            };
        }
    }
}