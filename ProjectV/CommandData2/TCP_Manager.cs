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
                    StringBuilder receivedData = new StringBuilder();
                    while (true)
                    {
                        // Check if data is available before attempting to read
                        if (_networkStream.DataAvailable)
                        {
                            Console.WriteLine("1");
                            int bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length);
                            Console.WriteLine("2");
                            // If no data is read, break the loop (end of data or no more data)
                            if (bytesRead == 0)
                            {
                                Console.WriteLine("break");
                                break;
                            }

                            // Append received data
                            receivedData.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                            // Check if the message ends with a newline character
                            if (receivedData.ToString().EndsWith("\n"))
                            {
                                Console.WriteLine("break");
                                break;
                            }
                        }
                        else
                        {
                            Thread.Sleep(100);
                        }
                    }
                    string completeData = receivedData.ToString();
                    Console.WriteLine($"Data received: {receivedData}");
                    return completeData;
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
    }
}
