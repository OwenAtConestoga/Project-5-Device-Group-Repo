using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandData2;

namespace Devices
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize Devices
            var smartFridge = new SmartFridge();
            var smartDehumidifier = new SmartDehumidifier();
            var smartThermostat = new SmartThermostat();

            // Start Devices
            Logger.Log("SmartFridge running", Logger.LogType.Info);
            Task.Run(() => Application.Run(smartFridge));
            Logger.Log("SmartDehumidifier running", Logger.LogType.Info);
            Task.Run(() => Application.Run(smartDehumidifier));
            Logger.Log("SmartThermostat running", Logger.LogType.Info);
            Task.Run(() => Application.Run(smartThermostat));

            // Periodically send status updates every 30 seconds
            Task.Run(async () => await SendPeriodicUpdatesAsync(smartFridge, smartDehumidifier, smartThermostat));

            // Close devices before ending program
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                smartFridge.StopDevice();
                smartDehumidifier.StopDevice();
                smartThermostat.StopDevice();
                Logger.Log("Devices stopped", Logger.LogType.Info);
            };

            // Keep the main thread alive to monitor the UI applications
            Application.Run();
        }

        // Periodically send status updates for all devices every 30 seconds
        private static async Task SendPeriodicUpdatesAsync(SmartFridge fridge, SmartDehumidifier dehumidifier, SmartThermostat thermostat)
        {
            while (true)
            {
                await Task.WhenAll(
                    SendDeviceDataAsync(fridge),
                    SendDeviceDataAsync(dehumidifier),
                    SendDeviceDataAsync(thermostat)
                );
                await Task.Delay(30000); // Wait for 30 seconds before sending the next update
            }
        }

        // Send data for the fridge
        private static async Task SendDeviceDataAsync(SmartFridge fridge)
        {
            string deviceData = fridge.GenerateDeviceData();
            await fridge.SendCustomMessageAsync(deviceData);
        }

        // Send data for the dehumidifier
        private static async Task SendDeviceDataAsync(SmartDehumidifier dehumidifier)
        {
            string deviceData = dehumidifier.GenerateDeviceData();
            await dehumidifier.SendCustomMessageAsync(deviceData);
        }

        // Send data for the thermostat
        private static async Task SendDeviceDataAsync(SmartThermostat thermostat)
        {
            string deviceData = thermostat.GenerateDeviceData();
            await thermostat.SendCustomMessageAsync(deviceData);
        }
    }
}
