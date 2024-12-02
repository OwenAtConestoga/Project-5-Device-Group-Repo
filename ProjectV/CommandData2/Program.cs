﻿using CommandData2;
using System;
using System.Diagnostics;
using System.Net.Sockets;
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
            string serverIP = "127.0.0.1";
            int serverPort = 5000;
            var tcpManager = new SharedTcpManager(serverIP, serverPort);

            // Initialize devices
            var smartFridge = new SmartFridge(tcpManager);
            var smartDehumidifier = new SmartDehumidifier(tcpManager);
            var smartThermostat = new SmartThermostat(tcpManager);

            // Start device UI
            Console.WriteLine("Running UIs");
            Task.Run(() => Application.Run(smartFridge));
            Task.Run(() => Application.Run(smartDehumidifier));
            Task.Run(() => Application.Run(smartThermostat));

            // Monitor and send periodic updates
            var monitorTask = MonitorDevicesAsync(serverIP, serverPort, smartFridge, smartDehumidifier, smartThermostat);
            var updateTask = SendDeviceUpdatesAsync(smartFridge, smartDehumidifier, smartThermostat);
            await Task.WhenAll(monitorTask, updateTask);

            // Cleanup
            AppDomain.CurrentDomain.ProcessExit += (s, e) => tcpManager.CloseConnection();
        }

        private static async Task SendDeviceUpdatesAsync(SmartFridge smartFridge, SmartDehumidifier smartDehumidifier, SmartThermostat smartThermostat)
        {
            while (true)
            {
                // Periodically send device updates
                Console.WriteLine("Sending update...");
                await smartFridge.SendDeviceDataAsync();
                await smartDehumidifier.SendDeviceDataAsync();
                await smartThermostat.SendDeviceDataAsync();
                Thread.Sleep(30000); //Loop every 30s
            }
        }

        private static async Task MonitorDevicesAsync(string serverIp, int serverPort, SmartFridge fridge, SmartDehumidifier dehumidifier, SmartThermostat thermostat)
        {
            // Initialize TCP manager with given IP and port.
            var tcpManager = new SharedTcpManager(serverIp, serverPort);

            while (true)
            {
                // Receive data
                Console.WriteLine("Waiting for incoming data...");
                string data = await tcpManager.ReceiveAsync();
                Console.WriteLine("8");
                switch (ValidateReceivedData(data))
                {
                    case 0: // SmartFridge
                        await fridge.HandleReceivedDataAsync(data);
                        Console.WriteLine("Data successfully passed to the SmartFridge.");
                        break;
                    case 1: // Dehumidifier
                        await dehumidifier.HandleReceivedDataAsync(data);
                        Console.WriteLine("Data successfully passed to the Dehumidifier.");
                        break;
                    case 2: // Thermostat
                        await thermostat.HandleReceivedDataAsync(data);
                        Console.WriteLine("Data successfully passed to the Thermostat.");
                        break;
                    default:
                        Console.WriteLine("Unexpected DeviceID. Skipping...");
                        break;
                }
                Thread.Sleep(100); // Rate Limiting
            }
        }
        private static int ValidateReceivedData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                Console.WriteLine("Validation failed: Data is null or empty.");
                return -1;
            }

            // Split the data into segments
            var segments = data.Split(',');
            if (segments.Length != 5 && segments.Length != 6)
            {
                Console.WriteLine("Validation failed: Incorrect number of fields.");
                return -1;
            }

            // Validate common fields
            if (!int.TryParse(segments[0].Trim(), out int hubId) || hubId != 0 ||
                !int.TryParse(segments[1].Trim(), out int deviceId) || deviceId < 0 || deviceId > 2 ||
                string.IsNullOrWhiteSpace(segments[2].Trim()) ||
                !int.TryParse(segments[3].Trim(), out int isOn) || (isOn != 0 && isOn != 1))
            {
                Console.WriteLine("Validation failed: Invalid HubID, DeviceID, Name, or IsOn field.");
                return -1;
            }

            return deviceId;
        }
    }
}
