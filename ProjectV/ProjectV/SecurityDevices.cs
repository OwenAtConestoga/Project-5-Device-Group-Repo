using ProjectV;
using System;
using System.Threading.Tasks;
using System.Text;
using MQTTnet;
using MQTTnet.Client;

namespace ProjectV
{
    // Base class for security devices
    public class SecurityDevice
    {
        public int deviceID { get; set; }
        public string deviceName { get; set; }
        public bool isOn { get; set; }
        protected readonly SecurityHubLogger Logger;
        protected readonly IStatusReporter StatusReporter;

        public virtual void turnDeviceOn()
        {
            isOn = true;
            Logger?.LogOperation(deviceName, $"Device {deviceID} turned ON");
            Console.WriteLine(this.deviceName + " " + this.deviceID + " is now ON");
        }

        public virtual void turnDeviceOff()
        {
            isOn = false;
            Logger?.LogOperation(deviceName, $"Device {deviceID} turned OFF");
            Console.WriteLine(this.deviceName + " " + this.deviceID + " is now OFF");
        }

        public bool devicePowerStatus()
        {
            Logger?.LogOperation(deviceName, $"Device {deviceID} power status checked: {isOn}");
            return this.isOn;
        }

        public SecurityDevice() // non-param constructor 
        {
            this.deviceID = 0;
            this.deviceName = "Unknown";
            this.isOn = false;
        }

        public SecurityDevice(int deviceID, string deviceName, SecurityHubLogger logger, IStatusReporter statusReporter)
        {
            this.deviceID = deviceID;
            this.deviceName = deviceName;
            isOn = false;
            this.Logger = logger;
            this.StatusReporter = statusReporter;

            Logger?.LogOperation(deviceName, $"New security device created: ID {deviceID}");
            Console.WriteLine("New security device created!");
        }
    }

    // Locker class with MQTT integration
    public class Locker : SecurityDevice
    {
        public bool IsLocked { get; private set; }
        private IMqttClient _mqttClient;
        private readonly int _retryInterval = 5000; // Retry every 5 seconds if connection fails

        public Locker(int deviceID, string deviceName, SecurityHubLogger logger, IStatusReporter statusReporter)
            : base(deviceID, deviceName, logger, statusReporter)
        {
            InitializeMqttClient();
            Logger?.LogOperation(deviceName, "Locker device created");
        }

        private async void InitializeMqttClient()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("LockerClient_" + deviceID)
                .WithTcpServer("741443276d504742a780ebb38fa36465.s1.eu.hivemq.cloud", 8883) // Replace with actual values
                .WithCredentials("tester", "projectvtester")
                .WithTls() // Enable TLS for secure connection
                .WithCleanSession()
                .Build();

            // Setup connection and disconnection handlers
            _mqttClient.ConnectedAsync += async e =>
            {
                Logger?.LogOperation(deviceName, "Connected to MQTT broker.");
                await _mqttClient.SubscribeAsync("locker/control");
                Console.WriteLine("Subscribed to MQTT topic: locker/control");
                await Task.CompletedTask;
            };

            _mqttClient.DisconnectedAsync += async e =>
            {
                Logger?.LogOperation(deviceName, $"Disconnected from MQTT broker: {e.Exception?.Message}");
                Console.WriteLine("Disconnected from MQTT broker. Retrying...");
                await Task.Delay(_retryInterval);
                await ReconnectAsync(options);
            };

            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                try
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    if (payload == "LOCK")
                    {
                        Lock();
                    }
                    else if (payload == "UNLOCK")
                    {
                        Unlock();
                    }
                }
                catch (Exception ex)
                {
                    Logger?.LogOperation(deviceName, $"Error processing MQTT message: {ex.Message}");
                    Console.WriteLine("Error processing message: " + ex.Message);
                }
                return Task.CompletedTask;
            };

            await ReconnectAsync(options);
        }

        private async Task ReconnectAsync(MqttClientOptions options)
        {
            while (!_mqttClient.IsConnected)
            {
                try
                {
                    await _mqttClient.ConnectAsync(options);
                }
                catch (Exception ex)
                {
                    Logger?.LogOperation(deviceName, $"Error reconnecting to MQTT broker: {ex.Message}");
                    Console.WriteLine("Retrying connection in 5 seconds...");
                    await Task.Delay(_retryInterval);
                }
            }
        }

        public void Lock()
        {
            IsLocked = true;
            Logger?.LogOperation(deviceName, "Locker is now locked");
            StatusReporter?.SendStatusUpdateAsync(deviceName, "Locked");
            Console.WriteLine($"{deviceName} is now locked.");
        }

        public void Unlock()
        {
            IsLocked = false;
            Logger?.LogOperation(deviceName, "Locker is now unlocked");
            StatusReporter?.SendStatusUpdateAsync(deviceName, "Unlocked");
            Console.WriteLine($"{deviceName} is now unlocked.");
        }
    }

    // Other device classes (Camera, Sensor, Alarm, etc.) remain the same
    internal class Camera : SecurityDevice
    {
        public bool motionDetected { get; set; }

        public Camera(int deviceID, string deviceName, SecurityHubLogger logger, IStatusReporter statusReporter)
            : base(deviceID, deviceName, logger, statusReporter)
        {
            Logger?.LogOperation(deviceName, "Camera device created");
        }

        public void DetectMotion()
        {
            motionDetected = true;
            Logger?.LogOperation(deviceName, "Motion detected by camera");
            StatusReporter?.SendStatusUpdateAsync(deviceName, "Motion detected");
        }
    }

    internal class Sensor : SecurityDevice
    {
        public bool isTriggered { get; set; }

        public Sensor(int deviceID, string deviceName, SecurityHubLogger logger, IStatusReporter statusReporter)
            : base(deviceID, deviceName, logger, statusReporter)
        {
            Logger?.LogOperation(deviceName, "Sensor device created");
        }

        public void triggerSensor()
        {
            isTriggered = true;
            Logger?.LogOperation(deviceName, "Sensor triggered");
        }

        public void resetSensor()
        {
            isTriggered = false;
            Logger?.LogOperation(deviceName, "Sensor reset");
        }
    }

    internal class Alarm : SecurityDevice
    {
        public bool isActivated { get; set; }

        public Alarm(int deviceID, string deviceName, SecurityHubLogger logger, IStatusReporter statusReporter)
            : base(deviceID, deviceName, logger, statusReporter)
        {
            Logger?.LogOperation(deviceName, "Alarm device created");
        }

        public void activateAlarm()
        {
            isActivated = true;
            Logger?.LogOperation(deviceName, "Alarm activated");
        }

        public void deactivateAlarm()
        {
            isActivated = false;
            Logger?.LogOperation(deviceName, "Alarm deactivated");
        }
    }

    internal class Tracker : SecurityDevice
    {
        public bool isActivated { get; set; }
        public double location { get; set; }

        public Tracker(int deviceID, string deviceName, SecurityHubLogger logger, IStatusReporter statusReporter)
            : base(deviceID, deviceName, logger, statusReporter)
        {
            Logger?.LogOperation(deviceName, "Tracker device created");
        }

        public void activateTracker()
        {
            isActivated = true;
            Logger?.LogOperation(deviceName, "Tracker activated");
        }

        public void deactivateTracker()
        {
            isActivated = false;
            Logger?.LogOperation(deviceName, "Tracker deactivated");
        }
    }
}
