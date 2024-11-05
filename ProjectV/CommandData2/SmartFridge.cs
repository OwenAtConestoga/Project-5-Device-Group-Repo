using Devices;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandData2
{
    public partial class SmartFridge : Form
    {
        //Initial values
        private int fridgeTemperature = 35;
        private int freezerTemperature = 0;
        private Device fridgeDevice;

        public SmartFridge()
        {
            InitializeComponent();
            fridgeDevice = new Device();
            UpdateTemperatureLabels();
        }
        private void UpdateTemperatureLabels()
        {
            //Update WPF with internal variables
            fridgeTempLabel.Text = fridgeTemperature + "°";
            freezerTempLabel.Text = freezerTemperature + "°";
        }

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

        public async Task StartFridgeDeviceAsync(string serverIp, int port)
        {
            await fridgeDevice.StartDeviceAsync(serverIp, port);
            Logger.Log("SmartFridge started", Logger.LogType.Info);
        }

        public void StopFridgeDevice()
        {
            fridgeDevice.StopDevice();
            Logger.Log("SmartFridge stopped", Logger.LogType.Info);
        }

        public void UpdateFridgeDeviceState(Device.State newState)
        {
            fridgeDevice.UpdateState(newState);
            Logger.Log($"Fridge device state updated to {newState}", Logger.LogType.Info);
        }
    }
}
