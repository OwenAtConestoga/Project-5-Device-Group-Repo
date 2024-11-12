using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Subscribing;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading.Tasks;

public abstract class MqttDevice
{
    private readonly IMqttClient _client;
    private readonly string _brokerAddress = "broker.hivemq.com";
    private readonly string _topic;

    public MqttDevice(string topic)
    {
        _topic = topic;
        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();

        // Connect to the MQTT broker asynchronously
        Task.Run(async () => await ConnectMqttBroker()).Wait();
    }

    private async Task ConnectMqttBroker()
    {
        var options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer(_brokerAddress, 1883) // Default port for MQTT
            .WithCleanSession()
            .Build();

        _client.ConnectedAsync += async e =>
        {
            Console.WriteLine($"Connected to MQTT broker at {_brokerAddress}");
        };

        _client.DisconnectedAsync += async e =>
        {
            Console.WriteLine("Disconnected from MQTT broker");
        };

        try
        {
            await _client.ConnectAsync(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to MQTT broker: {ex.Message}");
        }
    }

    // Publish a message to the specified topic
    public async Task PublishMessage(string payload)
    {
        if (!_client.IsConnected)
        {
            Console.WriteLine("MQTT client is not connected. Reconnecting...");
            await ConnectMqttBroker();
        }

        var message = new MqttApplicationMessageBuilder()
            .WithTopic(_topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .WithRetainFlag(false)
            .Build();

        try
        {
            MqttClientPublishResult result = await _client.PublishAsync(message);
            if (result.ReasonCode == MqttClientPublishReasonCode.Success)
            {
                Console.WriteLine($"Published to {_topic}: {payload}");
            }
            else
            {
                Console.WriteLine($"Failed to publish: {result.ReasonCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while publishing message: {ex.Message}");
        }
    }

    // Subscribe to receive messages for a specific topic
    public async Task SubscribeAsync(Func<string, Task> onMessageReceived)
    {
        if (!_client.IsConnected)
        {
            Console.WriteLine("MQTT client is not connected. Reconnecting...");
            await ConnectMqttBroker();
        }

        _client.ApplicationMessageReceivedAsync += e =>
        {
            string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine($"Received from {_topic}: {message}");
            return onMessageReceived(message);
        };

        var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter(f => f.WithTopic(_topic).WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce))
            .Build();

        try
        {
            await _client.SubscribeAsync(subscribeOptions);
            Console.WriteLine($"Subscribed to {_topic}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while subscribing to {_topic}: {ex.Message}");
        }
    }
}
