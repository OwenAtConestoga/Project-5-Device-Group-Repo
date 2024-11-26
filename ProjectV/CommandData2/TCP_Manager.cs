using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Devices
{
    public class SharedTcpManager
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        // Constructor to initialize TcpClient and NetworkStream
        public SharedTcpManager(string serverAddress, int port)
        {
            try
            {
                _tcpClient = new TcpClient(serverAddress, port);
                _networkStream = _tcpClient.GetStream();
                Console.WriteLine($"Connected to server at {serverAddress}:{port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
                throw;
            }
        }

        public async Task SendAsync(string data)
        {
            if (_tcpClient.Connected && _networkStream != null)
            {
                try
                {
                    byte[] dataToSend = Encoding.ASCII.GetBytes(data); // Convert the data to a byte array
                    Console.WriteLine("Sending data...");
                    _networkStream.Write(dataToSend, 0, dataToSend.Length); // Synchronously write the data to the stream
                    Console.WriteLine($"Data sent: {data}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending data: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("TCP Client is not connected.");
            }
        }

        // Receive data asynchronously
        public async Task<string> ReceiveAsync()
        {
            if (_tcpClient.Connected && _networkStream != null)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Data received: {receivedData}");
                        return receivedData;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error receiving data: {ex.Message}");
                }
            }
            return null;
        }

        // Close the connection safely
        public void CloseConnection()
        {
            try
            {
                _networkStream?.Close();
                _tcpClient?.Close();
                Console.WriteLine("TCP connection closed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
            }
        }

        // Method to start receiving and sending data asynchronously in separate loops
        public async Task MonitorAndUpdateDevicesAsync(string dataToSend)
        {
            // Start receiving data in a loop
            var receiveTask = Task.Run(async () =>
            {
                while (_tcpClient.Connected)
                {
                    var receivedData = await ReceiveAsync();
                    if (receivedData != null)
                    {
                        // Handle the received data
                        Console.WriteLine($"Handling received data: {receivedData}");
                    }
                    await Task.Delay(1000); // Wait 1 second before checking for new data
                }
            });

            // Periodically send data every 30 seconds
            var sendTask = Task.Run(async () =>
            {
                while (_tcpClient.Connected)
                {
                    await SendAsync(dataToSend);
                    await Task.Delay(30000); // Wait 30 seconds before sending again
                }
            });

            // Wait for both tasks to finish
            await Task.WhenAll(receiveTask, sendTask);
        }
    }
}
