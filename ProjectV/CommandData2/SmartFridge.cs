using Devices;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandData2
{
    public partial class SmartFridge : Form
    {
        private int fridgeTemperature = 4;
        private int freezerTemperature = -2;
        private readonly SharedTcpManager _tcpManager;
        private Guid DeviceId;
        public State CurrentState;

        public SmartFridge(SharedTcpManager tcpManager)
        {
            InitializeComponent();
            _tcpManager = tcpManager;
            DeviceId = Guid.NewGuid();
            CurrentState = State.Off; // Initialize to Off
            UpdateTemperatureLabels();
        }

        // Enum to represent device states
        public enum State
        {
            On,
            Off
        }

        public void UpdateState(State newState)
        {
            CurrentState = newState;
        }

        // UI Button Handlers
        private void fridgeTempUpButton_Click(object sender, EventArgs e)
        {
            fridgeTemperature++;
            UpdateTemperatureLabels();
        }

        private void fridgeTempDownButton_Click(object sender, EventArgs e)
        {
            fridgeTemperature--;
            UpdateTemperatureLabels();
        }

        private void freezerTempUpButton_Click(object sender, EventArgs e)
        {
            freezerTemperature++;
            UpdateTemperatureLabels();
        }

        private void freezerTempDownButton_Click(object sender, EventArgs e)
        {
            freezerTemperature--;
            UpdateTemperatureLabels();
        }

        private void UpdateTemperatureLabels()
        {
            // Adjust numeric labels in the WPF UI
            fridgeTempLabel.Text = fridgeTemperature + "°";
            freezerTempLabel.Text = freezerTemperature + "°";
            Logger.Log("Fridge Device Updated", Logger.LogType.Info);
        }

        public async Task SendDeviceDataAsync()
        {
            string data = $"0, 0, SmartFridge, 1, {fridgeTemperature}, {freezerTemperature}";
            await _tcpManager.SendAsync(data);
        }

        public async Task HandleReceivedDataAsync(string data)
        {
            var segments = data.Split(',');
            if (segments.Length == 6)
            {
                // Try to parse the fridge and freezer temperatures from the segments
                if (int.TryParse(segments[3].Trim(), out var isOn) &&
                    int.TryParse(segments[4].Trim(), out int parsedFridgeTemp) &&
                    int.TryParse(segments[5].Trim(), out int parsedFreezerTemp))
                {
                    // Update fridge and freezer temperatures
                    fridgeTemperature = parsedFridgeTemp;
                    freezerTemperature = parsedFreezerTemp;

                    // Update the UI with the new temperature values
                    UpdateTemperatureLabels();
                    Console.WriteLine($"Fridge updated: Fridge Temp {fridgeTemperature}°, Freezer Temp {freezerTemperature}°");
                }
                else
                {
                    Console.WriteLine("Invalid temperature data received for fridge.");
                }
            }
            else
            {
                Console.WriteLine("Invalid data format for SmartFridge.");
            }
        }


    }
}