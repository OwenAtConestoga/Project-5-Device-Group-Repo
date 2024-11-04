using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV
{
    public class LockHub : HomeSecurityHub
    {
        // not sure if i should ahve them set to false
        public LockHub(bool isActive = false) : base("Lock") { }
    }

    public class SensorHub : HomeSecurityHub
    {
        public SensorHub() : base("Sensor") { }
    }

    public class CameraHub : HomeSecurityHub
    {
        public CameraHub() : base("Camera") { }
    }

    public class AlarmHub : HomeSecurityHub
    {
        public AlarmHub() : base("Alarm") { }
    }

    public class TrackerHub : HomeSecurityHub
    {
        public TrackerHub() : base("Tracker") { }
    }
}
