using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV
{
    public class LockHub : HomeSecurityHub {
        public LockHub(SecurityHubLogger logger, IStatusReporter statusReporter, bool isActive = false)
            : base("Lock", logger, statusReporter) {
            IsActive = isActive;
            Logger?.LogOperation("LockHub", $"LockHub initialized with IsActive set to {IsActive}");
        }
    }

    public class SensorHub : HomeSecurityHub {
        public SensorHub(SecurityHubLogger logger, IStatusReporter statusReporter)
            : base("Sensor", logger, statusReporter) { 
            Logger?.LogOperation("SensorHub", "SensorHub initialized");
        }
    }

    public class CameraHub : HomeSecurityHub {
        public CameraHub(SecurityHubLogger logger, IStatusReporter statusReporter)
            : base("Camera", logger, statusReporter) {
            Logger?.LogOperation("CameraHub", "CameraHub initialized");
        }
    }

    public class AlarmHub : HomeSecurityHub {
        public AlarmHub(SecurityHubLogger logger, IStatusReporter statusReporter)
            : base("Alarm", logger, statusReporter) {
            Logger?.LogOperation("AlarmHub", "AlarmHub initialized");
        }
    }

    public class TrackerHub : HomeSecurityHub {
        public TrackerHub(SecurityHubLogger logger, IStatusReporter statusReporter)
            : base("Tracker", logger, statusReporter) {
            Logger?.LogOperation("TrackerHub", "TrackerHub initialized");
        }
    }
}
