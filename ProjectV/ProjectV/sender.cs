using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectV
{
    internal class Sender
    {
        //private HomeSecurityHub hub;
        private LockHub lockHub;
        private SensorHub sensorHub;
        private CameraHub cameraHub;
        private AlarmHub alarmHub;
        private TrackerHub trackerHub;
        private SecurityHubLogger logger;

        public Sender(AlarmHub alarmHub, TrackerHub trackerHub, LockHub lockhub, SensorHub sensorHub, CameraHub cameraHub, SecurityHubLogger logger)
        {
            this.trackerHub = trackerHub;
            this.alarmHub = alarmHub;
            this.lockHub = lockhub;
            this.sensorHub = sensorHub;
            this.cameraHub = cameraHub;
            this.logger = logger;
        }

        //public Sender(LockHub lockhub) { 

        //    this.lockHub = lockhub;
        //    //this.logger = logger;
        //}

        public List<string> SendStatesMessage()
        {

            //create a list to hold all the strings that will be sent
            List<string> states = new List<string>();

            //Console.WriteLine("1");

            foreach (var device in alarmHub.ConnectedDevices)
            {

                //Console.WriteLine("2");

                if (!device.isOn)
                {
                    //Console.WriteLine("3");
                    int power = device.isOn ? 1 : 0;
                    int stat = device.state ? 1 : 0;
                    states.Add($"1, {device.deviceID}, {device.deviceName}, {power}, {stat}");
                }



            }

            foreach (var device in trackerHub.ConnectedDevices)
            {

                //Console.WriteLine("2");

                if (!device.isOn)
                {
                    //Console.WriteLine("3");
                    int power = device.isOn ? 1 : 0;
                    int stat = device.state ? 1 : 0;
                    states.Add($"1, {device.deviceID}, {device.deviceName}, {power}, {stat}");
                }



            }

            //Check each of the devices 
            foreach (var device in lockHub.ConnectedDevices)
            {

                //Console.WriteLine("2");

                if (!device.isOn)
                {
                    //Console.WriteLine("3");
                    int power = device.isOn ? 1 : 0;
                    int stat = device.state ? 1 : 0;
                    states.Add($"1, {device.deviceID}, {device.deviceName}, {power}, {stat}");
                }

                

            }

            foreach (var device in sensorHub.ConnectedDevices)
            {

                //Console.WriteLine("2");

                if (!device.isOn)
                {
                    //Console.WriteLine("3");
                    int power = device.isOn ? 1 : 0;
                    int stat = device.state ? 1 : 0;
                    states.Add($"1, {device.deviceID}, {device.deviceName}, {power}, {stat}");
                }



            }

            foreach (var device in cameraHub.ConnectedDevices)
            {

                //Console.WriteLine("2");

                if (!device.isOn)
                {
                    //Console.WriteLine("3");
                    int power = device.isOn ? 1 : 0;
                    int stat = device.state ? 1 : 0;
                    states.Add($"1, {device.deviceID}, {device.deviceName}, {power}, {stat}");
                }



            }

            //Returns the list of active devices to the client send
            return states;

        }



    }
}
