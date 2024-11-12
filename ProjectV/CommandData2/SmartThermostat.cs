using Devices;
using System;
using System.Windows.Forms;

namespace CommandData2
{
    public partial class SmartThermostat : Form
    {
        private bool isPowerOn = false;
        private int currentTemperature = 20; // Default temperature in Celsius
        private Label temperatureDisplay;
        private Device thermostatDevice;

        public SmartThermostat()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Initialize temperature display label
            temperatureDisplay = new Label
            {
                Location = new System.Drawing.Point(100, 50),
                Size = new System.Drawing.Size(100, 30),
                Font = new System.Drawing.Font("Arial", 16),
                Text = $"{currentTemperature}°C"
            };
            this.Controls.Add(temperatureDisplay);

            // Update UI state
            UpdateUIState();
        }

        private void powerButton_Click(object sender, EventArgs e)
        {
            isPowerOn = !isPowerOn;
            UpdateUIState();
        }

        private void tempUpButton_Click(object sender, EventArgs e)
        {
            if (isPowerOn && currentTemperature < 30)
            {
                currentTemperature++;
                UpdateTemperatureDisplay();
            }
        }

        private void tempDownButton_Click(object sender, EventArgs e)
        {
            if (isPowerOn && currentTemperature > 10)
            {
                currentTemperature--;
                UpdateTemperatureDisplay();
            }
        }

        private void UpdateUIState()
        {
            temperatureDisplay.Enabled = isPowerOn;
            tempUpButton.Enabled = isPowerOn;
            tempDownButton.Enabled = isPowerOn;
            powerButton.Text = isPowerOn ? "Turn Off" : "Turn On";

            if (!isPowerOn)
            {
                temperatureDisplay.Text = "-- °C";
            }
            else
            {
                UpdateTemperatureDisplay();
            }
        }

        private void UpdateTemperatureDisplay()
        {
            temperatureDisplay.Text = $"{currentTemperature}°C";
        }

        public void StopDevice()
        {
            thermostatDevice.StopDevice();
            isPowerOn = false;
        }
    }
}