using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Devices
{
    public class Device
    {
        public State CurrentState { get; private set; }
        public Guid DeviceId { get; private set; }
        private TcpClient _tcpClient;

        public Device()
        {
            CurrentState = State.Off;
            DeviceId = Guid.NewGuid();
            _tcpClient = new TcpClient();
        }

        public enum State
        {
            Charging,
            On,
            Off
        }

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

        private void RunDevice()
        {
            while (CurrentState != State.Off)
            {
                var data = GenerateData();
                SendDataAsync(data).Wait();
                Thread.Sleep(1000);
            }
        }

        private string GenerateData()
        {
            return $"Data from device {DeviceId} at {DateTime.Now}";
        }

        private async Task ConnectTcpAsync(string serverIp, int port)
        {
            try
            {
                await _tcpClient.ConnectAsync(serverIp, port);
                Logger.Log($"Device connected to home", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                Logger.Log($"Exception occurred: {e.Message}", Logger.LogType.Error);
            }
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
                }
                catch (Exception e)
                {
                    Logger.Log($"Exception occurred: {e.Message}", Logger.LogType.Error);
                }
            }
        }

        public async Task SendCustomDataAsync(string data)
        {
            if (_tcpClient.Connected)
            {
                try
                {
                    var stream = _tcpClient.GetStream();
                    var encodedData = Encoding.UTF8.GetBytes(data);

                    await stream.WriteAsync(encodedData, 0, encodedData.Length);
                    Logger.Log($"Custom data sent: {data}", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    Logger.Log($"Error sending custom data: {e.Message}", Logger.LogType.Error);
                }
            }
            else
            {
                Logger.Log("TCP client not connected. Unable to send data.", Logger.LogType.Error);
            }
        }
    }
}