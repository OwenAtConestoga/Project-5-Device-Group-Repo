using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Devices;

namespace CommandData2
{
    public partial class SmartFridge : Form
    {
        private int fridgeTemperature = 35;
        private int freezerTemperature = 0;
        private Device fridgeDevice;

        public SmartFridge()
        {
            InitializeComponent();
            fridgeDevice = new Device(); // Initialize the Device instance
            UpdateTemperatureLabels();
        }

        //UI Button Handlers
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
            fridgeTempLabel.Text = fridgeTemperature + "°";
            freezerTempLabel.Text = freezerTemperature + "°";
            Logger.Log("Fridge UI Updated", Logger.LogType.Info);
        }

        //External Controls
        public void UpdateFridgeDeviceState(Device.State newState)
        {
            fridgeDevice.UpdateState(newState);
            Console.WriteLine($"Fridge device state updated to {newState}");
            Logger.Log("Device State Updated", Logger.LogType.Info);
        }

        public async Task StartFridgeDeviceAsync(string serverIp, int port)
        {
            await fridgeDevice.StartDeviceAsync(serverIp, port);
        }

        public void StopDevice()
        {
            fridgeDevice.StopDevice();
        }

        public async Task SendCustomMessageAsync(string message)
        {
            if (fridgeDevice != null)
            {
                await fridgeDevice.SendCustomDataAsync(message);
            }
        }
    }
}
