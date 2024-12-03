using System;

namespace ProjectV
{
    public class Receiver
    {
        private TrackerHub trackerHub;
        private SecurityHubLogger logger;

        // Constructor to accept the TrackerHub and logger
        public Receiver(TrackerHub trackerHub, SecurityHubLogger logger)
        {
            this.trackerHub = trackerHub;
            this.logger = logger;
        }

        // Method to find and deactivate the tracker with a given ID
        public void TurnOffTracker(int trackerId)
        {
            // Retrieve the tracker by its ID
            var device = trackerHub.GetDeviceById(trackerId); // Get device by ID
            if (device != null)
            {
                // Check if the device is actually a Tracker and cast it to Tracker
                if (device is Tracker tracker)
                {
                    Console.WriteLine($"Deactivating Tracker ID {tracker.deviceID}: {tracker.deviceName}");
                    tracker.deactivateTracker();  // Deactivate the tracker
                    //trackerHub.RemoveDevice(tracker.deviceName);
                }
                else
                {
                    Console.WriteLine($"Device with ID {trackerId} is not a tracker.");
                }
            }
            else
            {
                Console.WriteLine($"Tracker with ID {trackerId} not found.");
            }
        }

        // Method to find and activate the tracker with a given ID
        public void TurnOnTracker(int trackerId)
        {
            // Retrieve the tracker by its ID
            var device = trackerHub.GetDeviceById(trackerId); // Get device by ID
            if (device != null)
            {
                // Check if the device is actually a Tracker and cast it to Tracker
                if (device is Tracker tracker)
                {
                    Console.WriteLine($"Activating Tracker ID {tracker.deviceID}: {tracker.deviceName}");
                    tracker.activateTracker();  // activate the tracker
                    //trackerHub.RemoveDevice(tracker.deviceName);
                }
                else
                {
                    Console.WriteLine($"Device with ID {trackerId} is not a tracker.");
                }
            }
            else
            {
                Console.WriteLine($"Tracker with ID {trackerId} not found.");
            }
        }


        public void CheckTrackerState(int trackerId)
        {
            var tracker = trackerHub.GetDeviceById(trackerId);
            if (tracker != null)
            {
                Console.WriteLine($"Tracker {tracker.deviceName} is currently {(tracker.isOn ? "ON" : "OFF")}.");
            }
            else
            {
                Console.WriteLine($"Tracker with ID {trackerId} not found.");
            }
        }

        // parse the incoming message that is to be received from home 
        // will turn on or off specified devices 
        public void ParseIncomingMessage(string message)
        {
            try
            {
                // split the incoming message into components
                var parts = message.Split(','); 

                if (parts.Length != 4) // ensure the message has 4 components
                {
                    Console.WriteLine("Invalid message format. Expected format: HubID, DeviceID, DeviceName, IsOn");
                    return;
                }

                // parse the message components
                int hubId = int.Parse(parts[0].Trim());
                int deviceId = int.Parse(parts[1].Trim());
                string deviceName = parts[2].Trim();
                bool isOn = parts[3].Trim() == "1";

                // retrieve the device by its ID or name
                var device = trackerHub.GetDeviceById(deviceId);

                // if the device exists and is a tracker 
                if (device != null && device is Tracker tracker)
                {
                    if (isOn && !tracker.isOn) // turn on the tracker if its off  
                    {
                        Console.WriteLine($"Turning ON Tracker {tracker.deviceID}: {tracker.deviceName}");
                        tracker.activateTracker();
                    }
                    else if (!isOn && tracker.isOn) // turn off the tracker if it's on 
                    {
                        Console.WriteLine($"Turning OFF Tracker {tracker.deviceID}: {tracker.deviceName}");
                        tracker.deactivateTracker();
                    }
                    else // print out that it's already on / off 
                    {
                        Console.WriteLine($"Tracker {tracker.deviceID}: {tracker.deviceName} is already {(isOn ? "ON" : "OFF")}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Device with ID {deviceId} and Name {deviceName} not found or is not a tracker.");
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
