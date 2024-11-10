using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Devices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Devices.Tests
{
    [TestClass]
    public class DeviceTests
    {
        private Device _device;

        [TestInitialize]
        public void Setup()
        {
            _device = new Device();
        }

        [TestMethod]
        public async Task Test_ConnectionSetup_Success()
        {
            // Test Case 1: Verify connection setup between SmartFridge and Home module in TCP
            await _device.StartDeviceAsync("127.0.0.1", 8080);
            Assert.IsTrue(_device._tcpClient.Connected, "Device should be connected to the server.");
        }

        [TestMethod]
        public async Task Test_DataFormat_Success()
        {
            // Test Case 2: Validate data format being sent
            await _device.StartDeviceAsync("127.0.0.1", 8080);
            string data = _device.GenerateData();
            StringAssert.Matches(data, new System.Text.RegularExpressions.Regex(@"Data from device [a-f0-9\-]+ at [\d\-\s:]+"));
        }

        [TestMethod]
        public void Test_StateChange_On_Success()
        {
            // Test Case 3: Verify the SmartFridge responds to state change to "On"
            _device.UpdateState(Device.State.On);
            Assert.AreEqual(Device.State.On, _device.CurrentState);
        }

        [TestMethod]
        public void Test_StateChange_Off_FunctionalityStopped()
        {
            // Test Case 4: Verify if changing the state to "Off" stops functionality
            _device.UpdateState(Device.State.Off);
            Assert.AreEqual(Device.State.Off, _device.CurrentState);

            // Verify the device stops functionality (e.g., TCP disconnection)
            Assert.IsFalse(_device._tcpClient.Connected, "Device functionality should stop when turned off.");
        }

        [TestMethod]
        public void Test_Logging_ErrorFormat_Failure()
        {
            // Test Case 5: Check logging format for error events
            string logFilePath = "log.txt";
            if (File.Exists(logFilePath)) File.Delete(logFilePath);

            Logger.Log("Test error message", Logger.LogType.Error);
            string lastLog = File.ReadLines(logFilePath).Last();
            StringAssert.Contains(lastLog, "Error");
            StringAssert.Matches(lastLog, new System.Text.RegularExpressions.Regex(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}, Error, Test error message"));
        }

        [TestMethod]
        public void Test_Functionality_InDifferentStates()
        {
            // Test Case 6: Verify SmartFridge functionality in different states
            _device.UpdateState(Device.State.On);
            Assert.AreEqual(Device.State.On, _device.CurrentState);

            _device.UpdateState(Device.State.Charging);
            Assert.AreEqual(Device.State.Charging, _device.CurrentState);

            _device.UpdateState(Device.State.Off);
            Assert.AreEqual(Device.State.Off, _device.CurrentState);
        }

        [TestMethod]
        public void Test_StateChange_AndLogging()
        {
            // Test Case 7: Validate if state changes and functionality are logged correctly
            string logFilePath = "log.txt";
            if (File.Exists(logFilePath)) File.Delete(logFilePath);

            _device.UpdateState(Device.State.On);
            _device.PrintCurrentState();

            string lastLog = File.ReadLines(logFilePath).Last();
            StringAssert.Contains(lastLog, "Current state: On");
        }
    }
}
