using MQTTnet;
using MQTTnet.Client;
using System;
using System.Text;
using System.Threading.Tasks;

class LockReceiver
{
    private static bool isLocked = false;

    public static async Task Main(string[] args)
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        // Configure connection options with TLS
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithClientId("LockReceiver")
            .WithTcpServer("741443276d504742a780ebb38fa36465.s1.eu.hivemq.cloud", 8883)
            .WithCredentials("tester", "projectvtester")
            .WithTls() // Use TLS directly without further options
            .Build();

        // Handle received lock/unlock messages
        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment.ToArray());
            if (payload.ToLower() == "true")
            {
                isLocked = true;
                Console.WriteLine("System is now locked.");
            }
            else if (payload.ToLower() == "false")
            {
                isLocked = false;
                Console.WriteLine("System is now unlocked.");
            }
            return Task.CompletedTask;
        };

        // Connect to the broker and subscribe
        await mqttClient.ConnectAsync(mqttClientOptions);
        await mqttClient.SubscribeAsync("sensor/lock");

        Console.WriteLine("Connected to HiveMQ broker and listening for lock/unlock commands...");
        Console.WriteLine("Press Ctrl+C to exit.");

        // Keep running to receive messages
        await Task.Delay(System.Threading.Timeout.InfiniteTimeSpan);
    }
}
