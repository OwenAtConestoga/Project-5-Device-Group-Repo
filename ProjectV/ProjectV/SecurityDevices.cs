using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol; // Ensure this is included for MqttQualityOfServiceLevel
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectV
{
    internal class Lock
    {
        public bool IsLocked { get; private set; }
        private readonly IMqttClient _mqttClient;

        public Lock(int deviceId, string deviceName, IMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
            IsLocked = false;
        }

        public async Task LockDevice()
        {
            IsLocked = true;
            await PublishLockStatus();
        }

        public async Task UnlockDevice()
        {
            IsLocked = false;
            await PublishLockStatus();
        }

        private async Task PublishLockStatus()
        {
            if (_mqttClient.IsConnected)
            {
                string topic = $"home/locks/lock";
                string payload = IsLocked ? "true" : "false";

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce) // Use QoS level
                    .Build();

                await _mqttClient.PublishAsync(message);
                Console.WriteLine($"Published to MQTT: {topic} - {payload}");
            }
            else
            {
                Console.WriteLine("MQTT client not connected.");
            }
        }
    }
}
