using Devices;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandData2
{
    public partial class SmartDehumidifier : Form
    {
        private int humidityLevel = 40;
        private int waterLevel = 40;
        private readonly SharedTcpManager _tcpManager;
        private Guid DeviceId;
        private State CurrentState;

        public SmartDehumidifier(SharedTcpManager tcpManager)
        {
            InitializeComponent();
            _tcpManager = tcpManager;
            DeviceId = Guid.NewGuid();
            CurrentState = State.Off; // Initialize to Off
        }

        public enum State
        {
            On,
            Off
        }

        private void powerButton_Click(object sender, EventArgs e)
        {
            if (CurrentState == State.On)
                UpdateState(State.Off);
            else
                UpdateState(State.On);
            UpdateDehumidifierLabels();
        }

        public void UpdateState(State newState)
        {
            CurrentState = newState;
        }

        private void UpdateDehumidifierLabels()
        {
            humidityLabel.Text = humidityLevel + "°";
            int clampedWaterLevel = Math.Max(progressBar1.Minimum, Math.Min(progressBar1.Maximum, waterLevel));
            progressBar1.Value = clampedWaterLevel;

            if (CurrentState == State.On)
            {
                statusTextBox.Text =  "ON";
                statusTextBox.BackColor = Color.Green;
            }
            else
            {
                statusTextBox.Text =  "OFF";
                statusTextBox.BackColor = Color.Maroon;
            }
        }

        public async Task SendDeviceDataAsync()
        {
            int isOn = CurrentState == State.On ? 1 : 0;
            string data = $"0, 0, SmartDehumidifier, {isOn}, {humidityLevel}, {waterLevel}";
            await _tcpManager.SendAsync(data);
        }

        public async Task HandleReceivedDataAsync(string data)
        {
            // Split the received data into parts
            var segments = data.Split(',');

            // Validate the data format
            if (segments.Length == 6) // Expecting 6 parts for Dehumidifier
            {
                // Extract the relevant fields (isOn, waterLvl, humidityLvl)
                if (int.TryParse(segments[3].Trim(), out var isOn) &&
                    int.TryParse(segments[4].Trim(), out var waterLvl) &&
                    int.TryParse(segments[5].Trim(), out var humidityLvl))
                {
                    // Update the state of the device
                    var newState = isOn == 1 ? State.On : State.Off;
                    UpdateState(newState);

                    // Update the fields
                    waterLevel = waterLvl;
                    humidityLevel = humidityLvl;
                    UpdateDehumidifierLabels();

                    Logger.Log($"Updated Dehumidifier state: {newState}, Water Level: {waterLvl}, Humidity: {humidityLvl}", Logger.LogType.Info);
                }
                else
                {
                    Logger.Log("Invalid data format for state or temperature values", Logger.LogType.Error);
                }
            }
            else
            {
                Logger.Log("Invalid data received: insufficient parts", Logger.LogType.Error);
            }
        }
    }
}
