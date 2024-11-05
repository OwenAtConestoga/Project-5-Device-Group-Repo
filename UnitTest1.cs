using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectV;

namespace ProjectV
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WashroomCameraDeviceStatusOn()
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera");
            washroomCamera.turnDeviceOn();

            bool expected = true;

            bool actual = washroomCamera.devicePowerStatus();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void WashroomCameraDeviceStatusNotOn()
        {
            Camera washroomCamera = new Camera(123, "WashroomCamera");

            bool expected = false;

            bool actual = washroomCamera.devicePowerStatus();

            Assert.AreEqual(expected, actual);
        }
    }
}