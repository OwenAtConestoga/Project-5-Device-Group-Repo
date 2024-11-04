using System.Threading.Tasks;

namespace ProjectV
{
    public interface IStatusReporter
    {
        Task SendStatusUpdateAsync(string hubName, string status);
        string GetCurrentStatus();
    }
}