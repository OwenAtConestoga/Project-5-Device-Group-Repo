using Devices;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CommandData2
{
    public partial class SmartThermostat : Form
    {
        private bool isPowerOn = false;
        private int currentTemperature = 20; // Default temperature in Celsius
        private TcpClient _tcpClient;
        private Guid DeviceId;
        private State CurrentState;

        public SmartThermostat()
        {
            InitializeComponent();
            InitializeCustomComponents();
            _tcpClient = new TcpClient();
            DeviceId = Guid.NewGuid();
            CurrentState = State.Off; // Initialize to Off
        }

        private void InitializeCustomComponents()
        {
            // UI elements initialization (temperature display, buttons, etc.)
        }

        public enum State
        {
            On,
            Off
        }

        private void powerButton_Click(object sender, EventArgs e)
        {
            if (isPowerOn)
                StopDevice();
            else
                UpdateState(State.On);
        }

        private void tempUpButton_Click(object sender, EventArgs e)
        {
            if (isPowerOn && currentTemperature < 30)
                currentTemperature++;
        }

        private void tempDownButton_Click(object sender, EventArgs e)
        {
            if (isPowerOn && currentTemperature > 10)
                currentTemperature--;
        }

        public void UpdateState(State newState)
        {
            CurrentState = newState;
            isPowerOn = CurrentState == State.On;
        }

        private void UpdateThermostatLabels()
        {
            temperatureLabel.Text = currentTemperature + "°";
        }

        public async Task StartDeviceAsync(string serverIp, int port)
        {
            UpdateState(State.On);

            await ConnectTcpAsync(serverIp, port);

            Task.Run(() => RunDevice());
            Task.Run(() => ReceiveDataAsync());
        }

        public void StopDevice()
        {
            UpdateState(State.Off);
            _tcpClient.Close();
        }

        private async Task ConnectTcpAsync(string serverIp, int port)
        {
            try
            {
                await _tcpClient.ConnectAsync(serverIp, port);
                Logger.Log($"SmartThermostat connected", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                Logger.Log($"Connection error: {e.Message}", Logger.LogType.Error);
            }
        }

        private void RunDevice()
        {
            while (CurrentState == State.On)
            {
                var data = GenerateDeviceData();
                SendDataAsync(data).Wait();
                Task.Delay(1000).Wait();
            }
        }

        public string GenerateDeviceData()
        {
            int isOn = CurrentState == State.On ? 1 : 0;
            return $"0, 0, SmartThermostat, {isOn}, {currentTemperature}";
        }

        private async Task SendDataAsync(string data)
        {
            if (_tcpClient.Connected)
            {
                try
                {
                    var stream = _tcpClient.GetStream();
                    var encodedData = Encoding.UTF8.GetBytes(data);

                    await stream.WriteAsync(encodedData, 0, encodedData.Length);
                    Logger.Log($"Sent Data: {data}", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    Logger.Log($"Error sending data: {e.Message}", Logger.LogType.Error);
                }
            }
        }

        private async Task ReceiveDataAsync()
        {
            var buffer = new byte[1024];
            var stream = _tcpClient.GetStream();

            while (_tcpClient.Connected && CurrentState != State.Off)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Server disconnected.");
                        break;
                    }

                    var receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    HandleReceivedData(receivedData);
                }
                catch (Exception e)
                {
                    Logger.Log($"Error receiving data: {e.Message}", Logger.LogType.Error);
                }
            }
        }

        private void HandleReceivedData(string data)
        {
            try
            {
                // Split the received data into parts
                var segments = data.Split(',');

                // Validate the data format
                if (segments.Length < 5)
                {
                    Logger.Log("Invalid data received: insufficient parts", Logger.LogType.Error);
                    return;
                }

                // Extract the relevant fields (isOn, waterLvl)
                if (int.TryParse(segments[3].Trim(), out var isOn) &&
                    int.TryParse(segments[4].Trim(), out var temp))
                {
                    // Update the state of the device
                    var newState = isOn == 1 ? State.On : State.Off;
                    UpdateState(newState);

                    // Update the temperature
                    currentTemperature = temp;
                    UpdateThermostatLabels();

                    Logger.Log($"Updated Dehumidifier state: {newState}, Water Level: {temp}°, Humidity: {currentTemperature}°", Logger.LogType.Info);
                }
                else
                {
                    Logger.Log("Invalid data format for state or temperature values", Logger.LogType.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error processing received data: {ex.Message}", Logger.LogType.Error);
            }
        }

        public async Task SendCustomMessageAsync(string message)
        {
            if (_tcpClient.Connected)
            {
                try
                {
                    var stream = _tcpClient.GetStream();
                    var encodedData = Encoding.UTF8.GetBytes(message);

                    await stream.WriteAsync(encodedData, 0, encodedData.Length);
                    Logger.Log($"Custom message sent: {message}", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    Logger.Log($"Error sending custom message: {e.Message}", Logger.LogType.Error);
                }
            }
            else
            {
                Logger.Log("TCP client not connected. Unable to send message.", Logger.LogType.Error);
            }
        }
    }
}
