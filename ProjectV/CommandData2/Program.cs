using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandData2;

namespace Devices
{
    class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize Devices
            var smartFridge = new SmartFridge();
            var smartDehumidifier = new SmartDehumidifier();
            var smartThermostat = new SmartThermostat();

            // Define server connection parameters
            string serverIp = "10.144.111.200"; // change as needed
            int serverPort = 5000;

            // Connect devices to the server
            await ConnectDevicesAsync(serverIp, serverPort, smartFridge, smartDehumidifier, smartThermostat);
            Console.WriteLine("All devices connected");

            // Start device UI forms asynchronously
            Task.Run(() => Application.Run(smartFridge));
            Task.Run(() => Application.Run(smartDehumidifier));
            Task.Run(() => Application.Run(smartThermostat));
            Console.WriteLine("All UIs Running");
            Logger.Log("Devices initialized and connected to the server.", Logger.LogType.Info);

            // Start listening for instructions
            await MonitorAndControlDevicesAsync(smartFridge, smartDehumidifier, smartThermostat);

            // Graceful shutdown of devices on application exit
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                smartFridge.StopDevice();
                smartDehumidifier.StopDevice();
                smartThermostat.StopDevice();
                Logger.Log("Devices stopped.", Logger.LogType.Info);
            };
        }

        private static async Task ConnectDevicesAsync(string serverIp, int port, SmartFridge fridge, SmartDehumidifier dehumidifier, SmartThermostat thermostat)
        {
            await Task.WhenAll(
                fridge.StartDeviceAsync(serverIp, port),
                dehumidifier.StartDeviceAsync(serverIp, port),
                thermostat.StartDeviceAsync(serverIp, port)
            );
            Logger.Log("All devices connected to the server.", Logger.LogType.Info);
        }

        private static async Task MonitorAndControlDevicesAsync(SmartFridge fridge, SmartDehumidifier dehumidifier, SmartThermostat thermostat)
        {
            // Start periodic updates
            var updateTask = SendPeriodicUpdatesAsync(fridge, dehumidifier, thermostat);

            // Continuously monitor TCP connections for messages
            while (true)
            {
                    var buffer = new byte[1024];
                    var stream = fridge._tcpClient.GetStream(); // Assuming shared TCP stream for devices

                if (stream.DataAvailable)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        // Parse the message to determine the device type
                        string[] dataParts = receivedData.Split(',');
                        int hubID = int.Parse(dataParts[0].Trim());
                        int deviceID = int.Parse(dataParts[1].Trim());

                        switch (deviceID)
                        {
                            case 0:
                                fridge.HandleReceivedData(receivedData);
                                break;
                            case 1:
                                dehumidifier.HandleReceivedData(receivedData);
                                break;
                            case 2:
                                thermostat.HandleReceivedData(receivedData);
                                break;
                            default:
                                Logger.Log($"Unknown DeviceID: {deviceID}. Ignoring message.", Logger.LogType.Error);
                                break;
                        }
                    }
                }

                await Task.Delay(100); // Rate limiting
            } //End of while loop
        }



        private static async Task SendPeriodicUpdatesAsync(SmartFridge fridge, SmartDehumidifier dehumidifier, SmartThermostat thermostat)
        {
            while (true)
            {
                Console.WriteLine("Sending periodic data");
                await Task.WhenAll(

                    SendDeviceDataAsync(fridge),
                    SendDeviceDataAsync(dehumidifier),
                    SendDeviceDataAsync(thermostat)
                );
                Console.WriteLine("All device data sent (30s)");
                await Task.Delay(30000); // Wait for 30 seconds before the next update
            }
        }

        // Data sending functions for each device
        private static async Task SendDeviceDataAsync(SmartFridge fridge)
        {
            await fridge.SendDataAsync(fridge.GenerateDeviceData());
        }
        private static async Task SendDeviceDataAsync(SmartDehumidifier dehumidifier)
        {
            await dehumidifier.SendDataAsync(dehumidifier.GenerateDeviceData());
        }
        private static async Task SendDeviceDataAsync(SmartThermostat thermostat)
        {
            await thermostat.SendDataAsync(thermostat.GenerateDeviceData());
        }
    }
}
