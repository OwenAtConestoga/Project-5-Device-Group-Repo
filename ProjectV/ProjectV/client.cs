using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ProjectV
{
    internal class client
    {

        //private sender Sender;

        public void testingClient()
        {

            //IP of the server will change everytime we test
            string serverAddress = "10.144.111.200";
            int port = 5000;
            

            try
            {
                // Create the TcpClient
                TcpClient tcpClient = new TcpClient(serverAddress, port);
                NetworkStream networkStream = tcpClient.GetStream();

                // Message to send
                string message = "1, 789, FrontDoorLock, 0, 0";
                byte[] dataToSend = Encoding.ASCII.GetBytes(message);

                networkStream.Write(dataToSend, 0, dataToSend.Length);
                Console.WriteLine("Sent: " + message);

                //List<string> temp = new List<string>();
                //string message;

                //This will be the main sending loop just commented out for testing
                //while (true)
                //{
                //    //run sender funciton to check the state of all the devices
                //    temp = Sender.SendStatesMessage();

                //    //loop through all the elements in the List
                //    foreach (string tempmessage in temp)
                //    {
                //        message = tempmessage;

                //        byte[] dataToSend = Encoding.ASCII.GetBytes(message);
                //        networkStream.Write(dataToSend, 0, dataToSend.Length);
                //    }

                //    Thread.Sleep(30000);

                //}



                //// Receive response
                //byte[] buffer = new byte[1024];
                //int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                //string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //Console.WriteLine("Received: " + response);

                //// Close the connection

                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
