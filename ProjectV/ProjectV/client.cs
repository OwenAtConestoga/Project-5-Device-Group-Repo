using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ProjectV
{
    internal class client
    {

        private LockHub lockHub;

        private Sender sender;

        private Receiver receiver;


        public void testingClient(AlarmHub alarmHub, TrackerHub trackerHub, LockHub lockhub, SensorHub sensorHub, CameraHub cameraHub, SecurityHubLogger logger)
        {

            //IP of the server will change everytime we test
            // 127.0. 0.1.
            //"10.144.111.200"
            string serverAddress = "127.0.0.1";
            int port = 27000;

            //this.lockHub = lockhub;
            //this.trackerHub = trackerHub;
            //this.alarmHub = alarmHub;
            //this.lockHub = lockhub;
            //this.sensorHub = sensorHub;
            //this.cameraHub = cameraHub;
            //this.logger = logger;

            try
            {
                // Create the TcpClient
                TcpClient tcpClient = new TcpClient(serverAddress, port);
                NetworkStream networkStream = tcpClient.GetStream();

                sender = new Sender(alarmHub, trackerHub, lockhub, sensorHub, cameraHub, logger);
                //receiver = new Receiver();

                // Message to send
                //string message = "1, 789, FrontDoorLock, 0, 0";
                //byte[] dataToSend = Encoding.ASCII.GetBytes(message);

                //networkStream.Write(dataToSend, 0, dataToSend.Length);
                

                //// Receive response
                byte[] buffer = new byte[1024];
                //int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                //string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //Console.WriteLine("Received: " + response);

                List<string> temp = new List<string>();
                string message;

                //This will be the main sending loop just commented out for testing
                while (true){
                        //run sender funciton to check the state of all the devices
                        temp = sender.SendStatesMessage();

                        //loop through all the elements in the List
                        foreach (string tempmessage in temp)
                        {
                            message = tempmessage;

                            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
                            networkStream.Write(dataToSend, 0, dataToSend.Length);
                            Console.WriteLine("Sent: " + message);
                        }

                   


                    int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    receiver.ParseIncomingMessage(response);

                    Thread.Sleep(30000);

                }



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
