using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectV;
using System;
using System.IO;

namespace RecieverTests
{

    [TestClass]
    public class ReceiverTests
    {
        private TrackerHub trackerHub;
        private SecurityHubLogger logger;
        private Receiver receiver;
        private StringWriter consoleOutput;

        [TestInitialize]
        public void Setup()
        {
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            logger = new SecurityHubLogger();
            trackerHub = new TrackerHub(logger);
            receiver = new Receiver(trackerHub, logger);
        }

        [TestMethod]
        public void TurnOffTracker_ExistingTracker_DeactivatesTracker()
        {
            // Arrange
            var tracker = new Tracker(1, "TestTracker", logger);
            tracker.activateTracker();
            trackerHub.AddDevice(tracker);

            // Act
            receiver.TurnOffTracker(1);

            // Assert
            Assert.IsFalse(tracker.isActivated);
            StringAssert.Contains(consoleOutput.ToString(), "Deactivating Tracker ID 1: TestTracker");
        }

        [TestMethod]
        public void TurnOffTracker_NonExistentTracker_PrintsNotFoundMessage()
        {
            // Act
            receiver.TurnOffTracker(999);

            // Assert
            StringAssert.Contains(consoleOutput.ToString(), "Tracker with ID 999 not found.");
        }

        [TestMethod]
        public void TurnOffTracker_NonTrackerDevice_PrintsErrorMessage()
        {
            // Arrange
            var camera = new Camera(2, "TestCamera", logger);
            trackerHub.AddDevice(camera);

            // Act
            receiver.TurnOffTracker(2);

            // Assert
            StringAssert.Contains(consoleOutput.ToString(), "Device with ID 2 is not a tracker.");
        }

        [TestMethod]
        public void CheckTrackerState_ExistingTracker_PrintsStateCorrectly()
        {
            // Arrange
            var tracker = new Tracker(1, "TestTracker", logger);
            tracker.turnDeviceOn();
            trackerHub.AddDevice(tracker);

            // Act
            receiver.CheckTrackerState(1);

            // Assert
            StringAssert.Contains(consoleOutput.ToString(), "Tracker TestTracker is currently ON.");
        }

        [TestMethod]
        public void CheckTrackerState_NonExistentTracker_PrintsNotFoundMessage()
        {
            // Act
            receiver.CheckTrackerState(999);

            // Assert
            StringAssert.Contains(consoleOutput.ToString(), "Tracker with ID 999 not found.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            Console.SetOut(Console.Out);
        }
    }




}

