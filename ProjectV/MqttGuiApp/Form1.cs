using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MqttGuiApp
{
    public partial class Form1 : Form
    {
        private IMqttClient _mqttClient;

        public Form1()
        {
            InitializeComponent();
            txtTopic.Text = ConfigurationManager.AppSettings["MqttTopic"]; // Default topic
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_mqttClient == null || !_mqttClient.IsConnected)
            {
                await ConnectToMqttBroker();
            }
        }

        private async Task ConnectToMqttBroker()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            var brokerUrl = ConfigurationManager.AppSettings["MqttBrokerUrl"];
            var port = int.Parse(ConfigurationManager.AppSettings["MqttPort"]);
            var username = ConfigurationManager.AppSettings["MqttUsername"];
            var password = ConfigurationManager.AppSettings["MqttPassword"];

            var mqttOptions = new MqttClientOptionsBuilder()
                .WithClientId("LockReceiverGui")
                .WithTcpServer(brokerUrl, port)
                .WithCredentials(username, password)
                .WithTlsOptions(options => { options.UseTls = true; })
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment.ToArray());
                string message = $"Received on {e.ApplicationMessage.Topic}: {payload}";

                // Update the ListBox on the UI thread
                Invoke(new Action(() =>
                {
                    lstMessages.Items.Add(message);
                    lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
                }));

                return Task.CompletedTask;
            };

            _mqttClient.ConnectedAsync += async e =>
            {
                Invoke(new Action(() =>
                {
                    lstMessages.Items.Add("Connected to MQTT broker!");
                }));
                await _mqttClient.SubscribeAsync(txtTopic.Text);
            };

            _mqttClient.DisconnectedAsync += e =>
            {
                Invoke(new Action(() =>
                {
                    lstMessages.Items.Add("Disconnected from MQTT broker.");
                }));
                return Task.CompletedTask;
            };

            await _mqttClient.ConnectAsync(mqttOptions);
        }
    }
}
