// Uncomment if needed later
// using System.Collections.Generic;

namespace ProjectV
{
    public abstract class HomeSecurityHub
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }

        // Remove or comment these lines if they're unused
        // protected readonly SecurityHubLogger Logger;
        // protected readonly IStatusReporter StatusReporter;

        public HomeSecurityHub(string name)
        {
            Name = name;
            IsActive = false;
        }
    }
}
