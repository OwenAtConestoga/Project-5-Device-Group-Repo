//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectV
//{
//    internal class sender
//    {
//        private HomeSecurityHub hub;
//        private LockHub lockHub;
//        //private SensorHub sensorHub;
//        //private CameraHub cameraHub;
//        //private AlarmHub alarmHub;
//        //private TrackerHub trackerHub;
//        //private SecurityHubLogger logger;

//        public List<string> SendStatesMessage()
//        {

//            //create a list to hold all the strings that will be sent
//            List<string> states = new List<string>();

//            //Check each of the devices 
//            foreach (var device in )
//            {
//                if (device.isOn ? true : false)
//                {
//                    states.Add($"1, {device.deviceID}, {device.deviceName}, {device.isOn}, {hub.IsActive}");
//                }
                
//            }

//            //Returns the list of active devices to the client send
//            return states;

//        }

//    }
//}
