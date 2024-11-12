using Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandData2
{
    public partial class SmartDehumidifier : Form
    {
        private Device dehumidifierDevice;
        private int humidityLevel = 40; // Sample initial humidity level
        private int waterLevel = 40; // Sample initial water level percentage
        private bool isPowerOn = false;

        public SmartDehumidifier()
        {
            InitializeComponent();
            dehumidifierDevice = new Device(); // Initialize the Device instance
            UpdateDehumidifierUI();
        }

        //UI Button Handlers
        private void UpdateDehumidifierUI()
        {
            statusTextBox.Text = isPowerOn ? "ON" : "OFF";
            humidityLabel.Text = humidityLevel + "%";
            progressBar1.Value = waterLevel;
            Logger.Log("Dehumidifier UI Updated", Logger.LogType.Info);
        }

        private void powerButton_Click(object sender, EventArgs e)
        {
            isPowerOn = !isPowerOn;
            UpdateDeviceState(isPowerOn ? Device.State.On : Device.State.Off);
            UpdateDehumidifierUI();
        }

        private void UpdateDeviceState(Device.State newState)
        {
            dehumidifierDevice.UpdateState(newState);
            Logger.Log($"Dehumidifier state updated to {newState}", Logger.LogType.Info);
        }

        public async Task StartDehumidifierDeviceAsync(string serverIp, int port)
        {
            await dehumidifierDevice.StartDeviceAsync(serverIp, port);
            isPowerOn = true;
            UpdateDehumidifierUI();
        }

        public void StopDevice()
        {
            dehumidifierDevice.StopDevice();
            isPowerOn = false;
        }

        public async Task SendCustomMessageAsync(string message)
        {
            if (dehumidifierDevice != null)
            {
                await dehumidifierDevice.SendCustomDataAsync(message);
            }
        }

        public void SetHumidityLevel(int level)
        {
            humidityLevel = Clamp(level, 0, 100); // Ensure humidity is within 0-100%
            UpdateDehumidifierUI();
        }

        public void SetWaterLevel(int level)
        {
            waterLevel = Clamp(level, 0, 100); // Ensure water level is within 0-100%
            progressBar1.Value = waterLevel;
            UpdateDehumidifierUI();
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
