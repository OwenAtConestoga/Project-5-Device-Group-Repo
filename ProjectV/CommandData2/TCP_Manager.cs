using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Devices
{
    public class SharedTcpManager
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        // Constructor to initialize TcpClient and NetworkStream
        public SharedTcpManager(string serverAddress, int port)
        {
            try
            {
                tcpClient = new TcpClient(serverAddress, port);
                networkStream = tcpClient.GetStream();
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
            if (tcpClient.Connected && networkStream != null)
            {
                try
                {
                    byte[] dataToSend = Encoding.ASCII.GetBytes(data); // Convert the data to a byte array
                    networkStream.Write(dataToSend, 0, dataToSend.Length); // Synchronously write the data to the stream
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
        public async Task<string> ReceiveAsync()
        {
            if (tcpClient.Connected && networkStream != null)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    StringBuilder receivedData = new StringBuilder();
                    while (true)
                    {
                        // Check if data is available before attempting to read
                        if (networkStream.DataAvailable)
                        {
                            int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
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
                    Logger.Log("Data received from server", Logger.LogType.Info);
                    return completeData;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error receiving data: {ex.Message}");
                    Logger.Log("Error receiving data from server", Logger.LogType.Error);
                }
            }
            return null;
        }

        // Close the connection safely
        public void CloseConnection()
        {
            try
            {
                networkStream?.Close();
                tcpClient?.Close();
                Console.WriteLine("TCP connection closed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
                Logger.Log("Error closing TCP connection", Logger.LogType.Error);
            }
        }
    }
}
