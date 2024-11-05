using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectV;

namespace DeviceTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        // this test checks if camera status is set to on
        public void Test001_CameraDeviceStatusOn()
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera", null, null);
            washroomCamera.turnDeviceOn(); // turn on the camera 

            bool expected = true;

            bool actual = washroomCamera.devicePowerStatus();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        // this test will check if the power status is off after creating a camerea 
        public void Test002_CameraDeviceStatusNotOn()
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera", null, null);

            // do not turn on the camera 

            bool expected = false;

            bool actual = washroomCamera.devicePowerStatus();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test will check if the ID is set correctly 
        public void Test003_CameraIDCheck()
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera", null, null);

            int expected = 123;

            int actual = washroomCamera.deviceID;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test will check if the device name is set correctly 
        public void Test004_CameraDeviceNameCheck()
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera", null, null);

            string expected = "WashroomCamera";

            string actual = washroomCamera.deviceName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test will call the motion detected function will set the boolean value
        // of motionDetected to be true 
        public void Test005_CameraMotionDetection()
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera", null, null);

            washroomCamera.turnDeviceOn();

            washroomCamera.DetectMotion();

            bool expected = true;

            bool actual = washroomCamera.motionDetected;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test will power on the alarm and check device power status 
        public void Test006_AlarmPowersOn()
        {
            Alarm frontDoorAlarm = new Alarm(789, "FrontDoorAlarm", null, null);

            frontDoorAlarm.turnDeviceOn();

            bool expected = true;

            bool actual = frontDoorAlarm.devicePowerStatus();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test will activate the alarm and check if it is activated 
        public void Test007_AlarmIsActivated()
        {
            Alarm frontDoorAlarm = new Alarm(789, "FrontDoorAlarm", null, null);

            frontDoorAlarm.turnDeviceOn();

            frontDoorAlarm.activateAlarm(); 

            bool expected = true;

            bool actual = frontDoorAlarm.isActivated;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test will activate and then deactivate alarm, ensuring that deactivate works 
        public void Test008_AlarmCanBeDeactivated()
        {
            Alarm frontDoorAlarm = new Alarm(789, "FrontDoorAlarm", null, null);

            frontDoorAlarm.turnDeviceOn();

            frontDoorAlarm.activateAlarm();

            frontDoorAlarm.deactivateAlarm(); 

            bool expected = false;

            bool actual = frontDoorAlarm.isActivated;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test will trigger a sensor and check the isTriggered status
        public void Test009_SensorIsTriggered()
        {
            Sensor windowSensor = new Sensor(007, "WindowSensor1", null, null);

            windowSensor.turnDeviceOn();

            windowSensor.triggerSensor(); 

            bool expected = true;

            bool actual = windowSensor.isTriggered;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // this test checks to see if the logger file exists 
        public void Test010_LoggerFileAndDirectoryExist()
        {
            // Arrange
            var logger = new SecurityHubLogger();

            // Get the log file path
            string logFilePath = logger.GetOperationsLogPath();

            // Act
            bool fileExists = File.Exists(logFilePath);

            // Assert
            Assert.IsTrue(fileExists, $"The log file should exist at the specified path: {logFilePath}");
        }


        


    }
}

