using System;
using System.Windows.Forms;
using MQTTnet;
using MQTTnet.Client;
using System.Threading.Tasks;

namespace ProjectV
{
    public partial class Form1 : Form
    {
        private IMqttClient _mqttClient;
        private string _selectedDoor;
        private bool _isLocked;

        public Form1()
        {
            InitializeComponent();

            // Initialize MQTT client
            _mqttClient = new MqttFactory().CreateMqttClient();

            // Set up event handlers for connection status
            _mqttClient.ConnectedAsync += async e =>
            {
                Console.WriteLine("Connected to MQTT broker.");
                lblConnectionStatus.Text = "Connected";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Green;
                await Task.CompletedTask;
            };

            _mqttClient.DisconnectedAsync += async e =>
            {
                Console.WriteLine("Disconnected from MQTT broker.");
                lblConnectionStatus.Text = "Disconnected";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
                await Task.CompletedTask;
            };
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Automatically connect to the MQTT broker on form load
            var options = new MqttClientOptionsBuilder()
                .WithClientId("LockController")
                .WithTcpServer("741443276d504742a780ebb38fa36465.s1.eu.hivemq.cloud", 8883)  // HiveMQ Cloud broker URL
                .WithCredentials("tester", "projectvtester") // Your credentials
                .WithTls() // Enable TLS for secure connection
                .WithCleanSession()
                .Build();

            try
            {
                await _mqttClient.ConnectAsync(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to the MQTT broker: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblConnectionStatus.Text = "Connection Failed";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            }

            // Set the default door selection to the first option
            comboBoxDoors.SelectedIndex = 0;
        }

        private void comboBoxDoors_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the selected door based on user choice
            _selectedDoor = comboBoxDoors.SelectedItem.ToString();
            UpdateDoorStatus();
        }

        private void UpdateDoorStatus()
        {
            lblDoorStatus.Text = $"Status: {(_isLocked ? "Locked" : "Unlocked")}";
        }

        private async void btnLock_Click(object sender, EventArgs e)
        {
            // Check if the MQTT client is connected
            if (!_mqttClient.IsConnected)
            {
                MessageBox.Show("The MQTT client is not connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure a door is selected
            if (string.IsNullOrEmpty(_selectedDoor))
            {
                MessageBox.Show("Please select a door first.");
                return;
            }

            // Construct topic based on the selected door
            var topic = $"{_selectedDoor.Replace(" ", "").ToLower()}/control";
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload("LOCK")
                .Build();

            // Publish the lock command
            await _mqttClient.PublishAsync(message);
            _isLocked = true;
            UpdateDoorStatus();
            Console.WriteLine($"Sent LOCK command to {_selectedDoor}");
        }

        private async void btnUnlock_Click(object sender, EventArgs e)
        {
            // Check if the MQTT client is connected
            if (!_mqttClient.IsConnected)
            {
                MessageBox.Show("The MQTT client is not connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure a door is selected
            if (string.IsNullOrEmpty(_selectedDoor))
            {
                MessageBox.Show("Please select a door first.");
                return;
            }

            // Construct topic based on the selected door
            var topic = $"{_selectedDoor.Replace(" ", "").ToLower()}/control";
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload("UNLOCK")
                .Build();

            // Publish the unlock command
            await _mqttClient.PublishAsync(message);
            _isLocked = false;
            UpdateDoorStatus();
            Console.WriteLine($"Sent UNLOCK command to {_selectedDoor}");
        }
    }
}
