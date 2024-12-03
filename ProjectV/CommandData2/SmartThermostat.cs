using Devices;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandData2
{
    public partial class SmartThermostat : Form
    {
        private int currentTemperature = 20; // Default temperature in Celsius
        private readonly SharedTcpManager _tcpManager;
        private Guid DeviceId;
        private State CurrentState;

        public SmartThermostat(SharedTcpManager tcpManager)
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

        public void UpdateState(State newState)
        {
            CurrentState = newState;
        }

        private void powerButton_Click(object sender, EventArgs e)
        {
            if (CurrentState == State.On)
                UpdateState(State.Off);
            else
                UpdateState(State.On);
        }

        private void tempUpButton_Click(object sender, EventArgs e)
        {
            currentTemperature++;
            UpdateThermostatLabels();
        }

        private void tempDownButton_Click(object sender, EventArgs e)
        {
            currentTemperature--;
            UpdateThermostatLabels();
        }

        private void UpdateThermostatLabels()
        {
            temperatureLabel.Text = currentTemperature + "°";
        }

        public async Task SendDeviceDataAsync()
        {
            int isOn = CurrentState == State.On ? 1 : 0;
            string data = $"0, 0, SmartThermostat, {isOn}, {currentTemperature}";
            await _tcpManager.SendAsync(data);
        }

        public async Task HandleReceivedDataAsync(string data)
        {
                // Split the received data into parts
                var segments = data.Split(',');

            // Validate the data format
            if (segments.Length == 5) // Expecting 5 parts for Thermostat
            {
                // Extract the relevant fields (isOn, temperature)
                if (int.TryParse(segments[3].Trim(), out var isOn) &&
                    int.TryParse(segments[4].Trim(), out var temperature))
                {
                    // Update the state of the device
                    var newState = isOn == 1 ? State.On : State.Off;
                    UpdateState(newState);

                    // Update the temperature
                    currentTemperature = temperature;
                    UpdateThermostatLabels();

                    Logger.Log($"Updated Thermostat state: {newState}, Temperature: {temperature}°", Logger.LogType.Info);
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
