using Devices;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandData2
{
    public partial class SmartFridge : Form
    {
        private int fridgeTemperature = 4;
        private int freezerTemperature = -2;
        private TcpClient _tcpClient;
        private Guid DeviceId;
        private State CurrentState;

        public SmartFridge()
        {
            InitializeComponent();
            _tcpClient = new TcpClient();
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
            fridgeTempLabel.Text = fridgeTemperature + "°";
            freezerTempLabel.Text = freezerTemperature + "°";
            Logger.Log("Fridge UI Updated", Logger.LogType.Info);
        }

        // Methods for device state management
        public void UpdateState(State newState)
        {
            CurrentState = newState;
            PrintCurrentState();
        }

        public void PrintCurrentState()
        {
            Console.WriteLine($"Current state: {CurrentState}\nDevice ID: {DeviceId}");
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
                Logger.Log($"SmartFridge connected to home", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                Logger.Log($"Exception occurred: {e.Message}", Logger.LogType.Error);
            }
        }

        private void RunDevice()
        {
            while (CurrentState != State.Off)
            {
                var data = GenerateDeviceData();
                SendDataAsync(data).Wait();
                Task.Delay(1000).Wait();
            }
        }

        public string GenerateDeviceData()
        {
            int isOn = CurrentState == State.On ? 1 : 0;
            return $"0, 0, SmartFridge, {isOn}, {fridgeTemperature}, {freezerTemperature}";
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
                    Logger.Log($"Exception occurred: {e.Message}", Logger.LogType.Error);
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
                    Logger.Log($"Received data: {receivedData}", Logger.LogType.Info);
                    HandleReceivedData(receivedData);
                }
                catch (Exception e)
                {
                    Logger.Log($"Exception occurred: {e.Message}", Logger.LogType.Error);
                }
            }
        }

        private void HandleReceivedData(string data)
        {
            // Logic to parse and handle commands from the received data
            Logger.Log($"Processing received data: {data}", Logger.LogType.Info);
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