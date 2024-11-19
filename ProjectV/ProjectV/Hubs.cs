using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV
{
    public class LockHub : HomeSecurityHub
    {
        public LockHub(SecurityHubLogger logger, bool isActive = false)
            : base("Lock", logger)
        {
            IsActive = isActive;
            Logger?.LogOperation("LockHub", $"LockHub initialized with IsActive set to {IsActive}");
        }
    }

    public class SensorHub : HomeSecurityHub
    {
        public SensorHub(SecurityHubLogger logger)
            : base("Sensor", logger)
        {
            Logger?.LogOperation("SensorHub", "SensorHub initialized");
        }
    }

    public class CameraHub : HomeSecurityHub
    {
        public CameraHub(SecurityHubLogger logger)
            : base("Camera", logger)
        {
            Logger?.LogOperation("CameraHub", "CameraHub initialized");
        }
    }

    public class AlarmHub : HomeSecurityHub
    {
        public AlarmHub(SecurityHubLogger logger)
            : base("Alarm", logger)
        {
            Logger?.LogOperation("AlarmHub", "AlarmHub initialized");
        }
    }

    public class TrackerHub : HomeSecurityHub
    {
        public TrackerHub(SecurityHubLogger logger)
            : base("Tracker", logger)
        {
            Logger?.LogOperation("TrackerHub", "TrackerHub initialized");
        }


    }


}