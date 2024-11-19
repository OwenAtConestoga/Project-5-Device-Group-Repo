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


        }
}
