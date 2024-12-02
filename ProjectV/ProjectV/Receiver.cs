using System;
using System.Security.Claims;

namespace ProjectV
{
    public class Receiver
    {
        private AlarmHub alarmHub; 
        private TrackerHub trackerHub;
        private LockHub lockHub;
        private SensorHub sensorHub;
        private CameraHub cameraHub;
        private SecurityHubLogger logger;

        // Constructor to accept the TrackerHub and logger
        public Receiver(AlarmHub alarmHub, TrackerHub trackerHub, LockHub lockhub, SensorHub sensorHub, CameraHub cameraHub, SecurityHubLogger logger)
        {
            this.trackerHub = trackerHub;
            this.alarmHub = alarmHub;
            this.lockHub = lockhub;     
            this.sensorHub = sensorHub;
            this.cameraHub = cameraHub;
            this.logger = logger;
        }

        // parse the incoming message that is to be received from home 
        // will turn on or off specified devices 
        public void ParseIncomingMessage(string message)
        {
            try
            {
                // split the incoming message into components
                var parts = message.Split(','); 

                if (parts.Length != 5) // ensure the message has 5 components
                {
                    Console.WriteLine("Invalid message format. Expected format: HubID, DeviceID, DeviceName, IsOn");
                    return;
                }

                // parse the message components
                int hubId = int.Parse(parts[0].Trim());
                int deviceId = int.Parse(parts[1].Trim());
                string deviceName = parts[2].Trim();
                bool isOn = parts[3].Trim() == "1";
                bool currentState = parts[4].Trim() == "1";

                //
                //
                //    noHub = 0,   
                //    Lock = 1,     
                //    Sensor = 2,    
                //    Camera = 3,     
                //    Alarm = 4,
                //    Tracker = 5
                //

                // if the device is a lock 
                if (hubId ==1)
                {
                    // retrieve the device by its ID or name
                    var device = lockHub.GetDeviceById(deviceId);
                    if (device != null && device is Lock lock1)
                    {
                        if (isOn && !lock1.isOn) // turn the device on if its off  
                        {
                            Console.WriteLine($"Turning ON Lock {lock1.deviceID}: {lock1.deviceName}");
                            lock1.turnDeviceOn(); 
                        }
                        else if (!isOn && lock1.isOn) // turn off the device if it's on 
                        {
                            Console.WriteLine($"Turning OFF Lock {lock1.deviceID}: {lock1.deviceName}");
                            lock1.turnDeviceOff(); 
                        }
                        else // print out that it's already on / off 
                        {
                            Console.WriteLine($"Lock {lock1.deviceID}: {lock1.deviceName} is already {(isOn ? "ON" : "OFF")}.");
                        }

                        // handle current state variable 
                        if (currentState && lock1.isOn && !lock1.isLocked) 
                        {
                            Console.WriteLine($"Locking Lock {lock1.deviceID}: {lock1.deviceName}"); // lock the lock 
                            lock1.lockLock();
                        }
                        
                        else if (!currentState && lock1.isOn && lock1.isLocked) 
                        {
                            Console.WriteLine($"Unlocking Lock {lock1.deviceID}: {lock1.deviceName}"); // unlock the lock 
                            lock1.unlockLock();
                        }

                        else // print out that it's locked / unlocked 
                        {
                            Console.WriteLine($"Lock {lock1.deviceID}: {lock1.deviceName} is already {(currentState ? "Locked" : "Unlocked")}.");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Device with ID {deviceId} and Name {deviceName} not found or is not a lock.");
                    }
                }

                // -----------------------------------------------------------------------------------

                // if the device exists and is a SENSOR 
                else if (hubId == 2)
                {
                    // retrieve the device by its ID or name
                    var device = sensorHub.GetDeviceById(deviceId);
                    if (device != null && device is Sensor sensor)
                    {
                        if (isOn && !sensor.isOn) // turn on the device if its off  
                        {
                            Console.WriteLine($"Turning ON Sensor {sensor.deviceID}: {sensor.deviceName}");
                            sensor.turnDeviceOn();
                        }
                        else if (!isOn && sensor.isOn) // turn off the device if it's on 
                        {
                            Console.WriteLine($"Turning OFF Sensor {sensor.deviceID}: {sensor.deviceName}");
                            sensor.turnDeviceOff();
                        }
                        else // print out that it's already on / off 
                        {
                            Console.WriteLine($"Sensor {sensor.deviceID}: {sensor.deviceName} is already {(isOn ? "ON" : "OFF")}.");
                        }

                        // handle current state variable 
                        if (currentState && sensor.isOn && !sensor.isTriggered)
                        {
                            Console.WriteLine($"Activating Sensor {sensor.deviceID}: {sensor.deviceName}");
                            sensor.triggerSensor(); // trigger the sensor 
                        }

                        else if (!currentState && sensor.isOn && sensor.isTriggered)
                        {
                            Console.WriteLine($"Deactivating {sensor.deviceID}: {sensor.deviceName}");
                            sensor.resetSensor(); // reset the sensor 
                        }

                        else // print out that it's locked / unlocked 
                        {
                            Console.WriteLine($"Sensor {sensor.deviceID}: {sensor.deviceName} is already {(currentState ? "Activated" : "Dectivated")}.");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Device with ID {deviceId} and Name {deviceName} not found or is not a Sensor.");
                    }
                }

                // -----------------------------------------------------------------------------------

                // if the device exists and is a CAMERA 
                else if (hubId == 3)
                {
                    // retrieve the device by its ID or name
                    var device = cameraHub.GetDeviceById(deviceId);
                    if (device != null && device is Camera camera)
                    {
                        if (isOn && !camera.isOn) // turn on the device if its off  
                        {
                            Console.WriteLine($"Turning ON Camera {camera.deviceID}: {camera.deviceName}");
                            camera.turnDeviceOn();
                        }
                        else if (!isOn && camera.isOn) // turn off the device if it's on 
                        {
                            Console.WriteLine($"Turning OFF Camera {camera.deviceID}: {camera.deviceName}");
                            camera.turnDeviceOff();
                        }
                        else // print out that it's already on / off 
                        {
                            Console.WriteLine($"Camera {camera.deviceID}: {camera.deviceName} is already {(isOn ? "ON" : "OFF")}.");
                        }

                        // handle current state variable 
                        if (currentState && camera.isOn)
                        {
                            Console.WriteLine($"Motion Detected By Camera {camera.deviceID}: {camera.deviceName}");
                            camera.DetectMotion();
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Device with ID {deviceId} and Name {deviceName} not found or is not a camera.");
                    }
                }

                // -----------------------------------------------------------------------------------

                // if the device exists and is a alarm 
                else if (hubId == 4)
                {
                    // retrieve the device by its ID or name
                    var device = alarmHub.GetDeviceById(deviceId);
                    if (device != null && device is Alarm alarm)
                    {
                        if (isOn && !alarm.isOn) // turn on the device if its off  
                        {
                            Console.WriteLine($"Turning ON Alarm {alarm.deviceID}: {alarm.deviceName}");
                            alarm.turnDeviceOn();
                        }
                        else if (!isOn && alarm.isOn) // turn off the device if it's on 
                        {
                            Console.WriteLine($"Turning OFF Alarm {alarm.deviceID}: {alarm.deviceName}");
                            alarm.turnDeviceOff();
                        }
                        else // print out that it's already on / off 
                        {
                            Console.WriteLine($"Alarm {alarm.deviceID}: {alarm.deviceName} is already {(isOn ? "ON" : "OFF")}.");
                        }

                        // handle current state variable 
                        if (currentState && alarm.isOn && !alarm.isActivated)
                        {
                            Console.WriteLine($"Activating Alarm {alarm.deviceID}: {alarm.deviceName}");
                            alarm.activateAlarm();
                        }

                        else if (!currentState && alarm.isOn && alarm.isActivated)
                        {
                            Console.WriteLine($"Deactivating {alarm.deviceID}: {alarm.deviceName}");
                            alarm.deactivateAlarm();
                        }

                        else // print out that it's locked / unlocked 
                        {
                            Console.WriteLine($"Alarm {alarm.deviceID}: {alarm.deviceName} is already {(currentState ? "Activated" : "Dectivated")}.");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Device with ID {deviceId} and Name {deviceName} not found or is not a Alarm.");
                    }
                }

                // -----------------------------------------------------------------------------------

                // if the device exists and is a tracker 
                else if (hubId == 5)
                 {
                    // retrieve the device by its ID or name
                    var device = trackerHub.GetDeviceById(deviceId);
                    if (device != null && device is Tracker tracker)
                    {
                        if (isOn && !tracker.isOn) // turn on the tracker if its off  
                        {
                            Console.WriteLine($"Turning ON Tracker {tracker.deviceID}: {tracker.deviceName}");
                            tracker.turnDeviceOn();
                        }
                        else if (!isOn && tracker.isOn) // turn off the tracker if it's on 
                        {
                            Console.WriteLine($"Turning OFF Tracker {tracker.deviceID}: {tracker.deviceName}");
                            tracker.turnDeviceOff();
                        }
                        else // print out that it's already on / off 
                        {
                            Console.WriteLine($"Tracker {tracker.deviceID}: {tracker.deviceName} is already {(isOn ? "ON" : "OFF")}.");
                        }

                        // handle current state variable 
                        if (currentState && tracker.isOn && !tracker.isActivated)
                        {
                            Console.WriteLine($"Activating Tracker {tracker.deviceID}: {tracker.deviceName}");
                            tracker.activateTracker();
                        }

                        else if (!currentState && tracker.isOn && tracker.isActivated)
                        {
                            Console.WriteLine($"Deactivating {tracker.deviceID}: {tracker.deviceName}");
                            tracker.deactivateTracker();
                        }

                        else // print out that it's locked / unlocked 
                        {
                            Console.WriteLine($"Tracker {tracker.deviceID}: {tracker.deviceName} is already {(currentState ? "Activated" : "Dectivated")}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Device with ID {deviceId} and Name {deviceName} not found or is not a tracker.");
                    }
                }

                else
                {
                    Console.WriteLine("Invalid HUBID please try again");
                }
                

            }
            catch (FormatException) // handle if a user enters a string for the ids instead of int 
            {
                Console.WriteLine("Invalid message format. Ensure IDs and IsOn are valid integers.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while parsing the message: {ex.Message}");
            }
        }


        // add functions above 
    }
}
