using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using CommandData2;
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
        public async Task Test_1()
        {
            // Verify connection setup between Device and server in TCP
            await _device.StartDeviceAsync("127.0.0.1", 8080);
            Assert.IsTrue(_device.IsConnected, "Device should be connected to the server.");
        }

        [TestMethod]
        public async Task Test_2()
        {
            // Validate data format being sent
            await _device.StartDeviceAsync("127.0.0.1", 8080);
            string data = _device.GenerateData();
            StringAssert.Matches(data, new Regex(@"Data from device [a-f0-9\-]+ at [\d\-\s:]+"));
        }

        [TestMethod]
        public void Test_3()
        {
            // Verify the device responds to state change to "On"
            _device.UpdateState(Device.State.On);
            Assert.AreEqual(Device.State.On, _device.CurrentState);
        }

        [TestMethod]
        public void Test_4()
        {
            // Verify if changing the state to "Off" stops functionality
            _device.UpdateState(Device.State.Off);
            Assert.AreEqual(Device.State.Off, _device.CurrentState);
            Assert.IsFalse(_device.IsConnected, "Device functionality should stop when turned off.");
        }

        [TestMethod]
        public void Test_5()
        {
            // Check logging format for error events
            string logFilePath = "log.txt";
            if (File.Exists(logFilePath)) File.Delete(logFilePath);
            Logger.Log("Test error message", Logger.LogType.Error);
            string lastLog = File.ReadLines(logFilePath).Last();
            StringAssert.Contains(lastLog, "Error");
            StringAssert.Matches(lastLog, new Regex(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}, Error, Test error message"));
        }

        [TestMethod]
        public void Test_6()
        {
            // Verify device functionality in different states
            _device.UpdateState(Device.State.On);
            Assert.AreEqual(Device.State.On, _device.CurrentState);
            _device.UpdateState(Device.State.Charging);
            Assert.AreEqual(Device.State.Charging, _device.CurrentState);
            _device.UpdateState(Device.State.Off);
            Assert.AreEqual(Device.State.Off, _device.CurrentState);
        }

        [TestMethod]
        public void Test_7()
        {
            // Validate if state changes and functionality are logged correctly
            string logFilePath = "log.txt";
            if (File.Exists(logFilePath)) File.Delete(logFilePath);
            _device.UpdateState(Device.State.On);
            _device.PrintCurrentState();
            string lastLog = File.ReadLines(logFilePath).Last();
            StringAssert.Contains(lastLog, "Current state: On");
        }

        // Additional unit tests (8-15)
        [TestMethod]
        public void Test_8()
        {
            _device.UpdateState(Device.State.Charging);
            Assert.AreEqual(Device.State.Charging, _device.CurrentState);
        }

        [TestMethod]
        public void Test_9()
        {
            string data = _device.GenerateData();
            Assert.IsTrue(data.Contains("Data from device"));
        }

        [TestMethod]
        public void Test_10()
        {
            _device.UpdateState(Device.State.Off);
            Assert.AreEqual(Device.State.Off, _device.CurrentState);
            _device.UpdateState(Device.State.On);
            Assert.AreEqual(Device.State.On, _device.CurrentState);
        }

        [TestMethod]
        public void Test_11()
        {
            Assert.IsFalse(_device.IsConnected, "Device should not be connected initially.");
        }

        [TestMethod]
        public void Test_12()
        {
            Assert.AreEqual("Initial Data", _device.GenerateData());
        }

        [TestMethod]
        public void Test_13()
        {
            _device.UpdateState(Device.State.Charging);
            Assert.IsTrue(_device.IsCharging, "Device should be charging.");
        }

        [TestMethod]
        public void Test_14()
        {
            string logFilePath = "log.txt";
            Logger.Log("Test info message", Logger.LogType.Info);
            string lastLog = File.ReadLines(logFilePath).Last();
            StringAssert.Contains(lastLog, "Info");
        }

        [TestMethod]
        public void Test_15()
        {
            string logFilePath = "log.txt";
            Logger.Log("Test warning message", Logger.LogType.Warning);
            string lastLog = File.ReadLines(logFilePath).Last();
            StringAssert.Contains(lastLog, "Warning");
        }
    }

    [TestClass]
    public class SmartDehumidifierUnitTests
    {
        private SmartDehumidifier _smartDehumidifier;

        [TestInitialize]
        public void Setup()
        {
            _smartDehumidifier = new SmartDehumidifier();
        }

        [TestMethod]
        public void Test_16()
        {
            // Test Clamp with valid input
            int result = SmartDehumidifier.Clamp(50, 0, 100);
            Assert.AreEqual(50, result, "Clamp should return the input value if it's within range.");
        }

        [TestMethod]
        public void Test_17()
        {
            // Test Clamp with out-of-range input
            int resultLow = SmartDehumidifier.Clamp(-10, 0, 100);
            Assert.AreEqual(0, resultLow, "Clamp should return the minimum value if input is below range.");

            int resultHigh = SmartDehumidifier.Clamp(110, 0, 100);
            Assert.AreEqual(100, resultHigh, "Clamp should return the maximum value if input is above range.");
        }

        [TestMethod]
        public void Test_18()
        {
            // Test UI update with initial state
            Assert.IsFalse(_smartDehumidifier.IsPowerOn, "Initial power state should be OFF.");
            Assert.AreEqual("OFF", _smartDehumidifier.statusTextBox.Text, "UI should reflect the initial OFF state.");
        }

        [TestMethod]
        public void Test_19()
        {
            // Test that power button click toggles the power state
            _smartDehumidifier.powerButton.PerformClick();
            Assert.IsTrue(_smartDehumidifier.IsPowerOn, "Power state should be ON after clicking the power button.");
        }

        [TestMethod]
        public void Test_20()
        {
            // Test that UI updates when turning power ON
            _smartDehumidifier.powerButton.PerformClick();
            Assert.AreEqual("ON", _smartDehumidifier.statusTextBox.Text, "UI should show 'ON' when power is ON.");
        }

        [TestMethod]
        public void Test_21()
        {
            // Test that UI updates when turning power OFF
            _smartDehumidifier.powerButton.PerformClick();
            _smartDehumidifier.powerButton.PerformClick();
            Assert.AreEqual("OFF", _smartDehumidifier.statusTextBox.Text, "UI should show 'OFF' when power is OFF.");
        }

        [TestMethod]
        public void Test_22()
        {
            // Test that dehumidifier status text updates on state change
            _smartDehumidifier.UpdateState(SmartDehumidifier.State.On);
            Assert.AreEqual("Dehumidifier is ON", _smartDehumidifier.statusTextBox.Text, "UI should reflect 'Dehumidifier is ON'.");
        }

        [TestMethod]
        public void Test_23()
        {
            // Test state transition from ON to OFF
            _smartDehumidifier.UpdateState(SmartDehumidifier.State.Off);
            Assert.AreEqual(SmartDehumidifier.State.Off, _smartDehumidifier.CurrentState, "Dehumidifier state should be OFF.");
        }
    }

    [TestClass]
    public class SmartThermostatUnitTests
    {
        private SmartThermostat _smartThermostat;

        [TestInitialize]
        public void Setup()
        {
            _smartThermostat = new SmartThermostat();
        }

        [TestMethod]
        public void Test_24()
        {
            // Test initial power state
            Assert.IsFalse(_smartThermostat.isPowerOn, "Initial power state should be OFF.");
        }

        [TestMethod]
        public void Test_25()
        {
            // Test power toggle functionality
            _smartThermostat.powerButton.PerformClick();
            Assert.IsTrue(_smartThermostat.isPowerOn, "Power state should be ON after toggling.");
            _smartThermostat.powerButton.PerformClick();
            Assert.IsFalse(_smartThermostat.isPowerOn, "Power state should be OFF after toggling again.");
        }

        [TestMethod]
        public void Test_26()
        {
            // Test temperature increase
            _smartThermostat.powerButton.PerformClick(); // Turn ON
            int initialTemp = _smartThermostat.currentTemperature;
            _smartThermostat.tempUpButton.PerformClick();
            Assert.AreEqual(initialTemp + 1, _smartThermostat.currentTemperature, "Temperature should increase by 1.");
        }

        [TestMethod]
        public void Test_27()
        {
            // Test temperature decrease
            _smartThermostat.powerButton.PerformClick(); // Turn ON
            int initialTemp = _smartThermostat.currentTemperature;
            _smartThermostat.tempDownButton.PerformClick();
            Assert.AreEqual(initialTemp - 1, _smartThermostat.currentTemperature, "Temperature should decrease by 1.");
        }

        [TestMethod]
        public void Test_28()
        {
            // Test UI updates when temperature increases
            _smartThermostat.powerButton.PerformClick(); // Turn ON
            _smartThermostat.tempUpButton.PerformClick();
            Assert.AreEqual((_smartThermostat.currentTemperature + 1).ToString(), _smartThermostat.tempDisplayLabel.Text, "UI should update with the new temperature.");
        }

        [TestMethod]
        public void Test_29()
        {
            // Test UI updates when temperature decreases
            _smartThermostat.powerButton.PerformClick(); // Turn ON
            _smartThermostat.tempDownButton.PerformClick();
            Assert.AreEqual((_smartThermostat.currentTemperature - 1).ToString(), _smartThermostat.tempDisplayLabel.Text, "UI should update with the new temperature.");
        }

        [TestMethod]
        public void Test_30()
        {
            // Test maximum temperature limit
            _smartThermostat.UpdateTemperature(30); // Set to max
            _smartThermostat.tempUpButton.PerformClick(); // Try to increase
            Assert.AreEqual(30, _smartThermostat.currentTemperature, "Temperature should be capped at 30.");
        }

        [TestMethod]
        public void Test_31()
        {
            // Test minimum temperature limit
            _smartThermostat.UpdateTemperature(10); // Set to min
            _smartThermostat.tempDownButton.PerformClick(); // Try to decrease
            Assert.AreEqual(10, _smartThermostat.currentTemperature, "Temperature should be capped at 10.");
        }
    }

    [TestClass]
    public class IntegrationTests
    {
        private SmartThermostat _smartThermostat;
        private SmartDehumidifier _smartDehumidifier;

        [TestInitialize]
        public void Setup()
        {
            _smartThermostat = new SmartThermostat();
            _smartDehumidifier = new SmartDehumidifier();
        }

        [TestMethod]
        public void Test_32()
        {
            // Integration test for both devices being powered on
            _smartThermostat.powerButton.PerformClick(); // Turn ON
            _smartDehumidifier.UpdateState(SmartDehumidifier.State.On); // Turn ON
            Assert.IsTrue(_smartThermostat.isPowerOn && _smartDehumidifier.CurrentState == SmartDehumidifier.State.On, "Both devices should be ON together.");
        }

        [TestMethod]
        public void Test_33()
        {
            // Test UI updates when turning ON both devices (integration test)
            _smartThermostat.powerButton.PerformClick(); // Turn ON
            _smartDehumidifier.UpdateState(SmartDehumidifier.State.On); // Turn ON
            Assert.AreEqual("ON", _smartThermostat.statusTextBox.Text, "Thermostat UI should reflect ON state.");
            Assert.AreEqual("ON", _smartDehumidifier.statusTextBox.Text, "Dehumidifier UI should reflect ON state.");
        }

        // Performance tests
        [TestMethod]
        public void Test_34()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _device.StartDeviceAsync("127.0.0.1", 8080);
            stopwatch.Stop();
            Assert.IsTrue(_device.IsConnected, "Device should be connected.");
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= 2000, "Startup time should be less than or equal to 2 seconds.");
        }

        [TestMethod]
        public async Task Test_35()
        {
            string largeData = new string('A', 1024 * 1024); // 1 MB of data
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _device.SendDataAsync(largeData);
            stopwatch.Stop();
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= 3000, "Data transmission should be less than or equal to 3 seconds.");
        }

        [TestMethod]
        public async Task Test_36()
        {
            const int deviceCount = 100;
            var tasks = Enumerable.Range(0, deviceCount).Select(async _ =>
            {
                var device = new Device();
                await device.StartDeviceAsync("127.0.0.1", 8080);
                Assert.IsTrue(device.IsConnected, "Each device should connect successfully.");
            });
            await Task.WhenAll(tasks);
        }

        [TestMethod]
        public void Test_37()
        {
            _smartThermostat.powerButton.PerformClick();
            _smartDehumidifier.UpdateState(SmartDehumidifier.State.On);
            Assert.IsTrue(_smartThermostat.isPowerOn, "Thermostat should be ON.");
            Assert.AreEqual(SmartDehumidifier.State.On, _smartDehumidifier.CurrentState, "Dehumidifier should be ON.");
            Assert.AreEqual("ON", _smartThermostat.statusTextBox.Text);
            Assert.AreEqual("Dehumidifier is ON", _smartDehumidifier.statusTextBox.Text);
        }
        [TestMethod]
        public void Test_38()
        {
            _smartThermostat.powerButton.PerformClick();
            _smartDehumidifier.UpdateState(SmartDehumidifier.State.On);
            _smartDehumidifier.UpdateState(SmartDehumidifier.State.Off);
            Assert.IsTrue(_smartThermostat.isPowerOn, "Thermostat should remain ON.");
            Assert.AreEqual(SmartDehumidifier.State.Off, _smartDehumidifier.CurrentState, "Dehumidifier should transition to OFF state.");
        }
        
        [TestMethod]
        public async Task Test_39()
        {
            await _device.StartDeviceAsync("127.0.0.1", 8080);
            Assert.IsTrue(_device.IsConnected, "Device should be connected initially.");
            _device.SimulateDisconnection(); // Assume this method simulates disconnection
            Assert.IsFalse(_device.IsConnected, "Device should be disconnected.");
            await Task.Delay(5000); // Simulate reconnection delay
            Assert.IsTrue(_device.IsConnected, "Device should reconnect within 5 seconds.");
        }

        [TestMethod]
        public async Task Test_40()
        {
            const int deviceCount = 50;
            var thermostats = Enumerable.Range(0, deviceCount).Select(_ => new SmartThermostat()).ToList();
            var dehumidifiers = Enumerable.Range(0, deviceCount).Select(_ => new SmartDehumidifier()).ToList();
            var tasks = thermostats.Select(t => Task.Run(() => t.powerButton.PerformClick()))
                .Concat(dehumidifiers.Select(d => Task.Run(() => d.UpdateState(SmartDehumidifier.State.On))));
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= 2000, "State transitions should complete within 2 seconds.");
        }
    }
}
