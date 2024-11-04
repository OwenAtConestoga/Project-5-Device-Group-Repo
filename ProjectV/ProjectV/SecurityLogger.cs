using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectV
{
    public class SecurityHubLogger
    {
        private readonly string _operationsLogPath;
        private readonly string _transmissionLogPath;
        private static readonly object _lockObject = new object();

        public SecurityHubLogger(string operationsLogPath = "operations.log", string transmissionLogPath = "transmission.log")
        {
            _operationsLogPath = operationsLogPath;
            _transmissionLogPath = transmissionLogPath;
        }

        public void LogOperation(string hubName, string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {hubName} | {message}";
            WriteToLog(_operationsLogPath, logEntry);
        }

        public void LogTransmission(string hubName, string data)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {hubName} | Transmitted: {data}";
            WriteToLog(_transmissionLogPath, logEntry);
        }

        private void WriteToLog(string logPath, string message)
        {
            lock (_lockObject)
            {
                try
                {
                    File.AppendAllText(logPath, message + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to log: {ex.Message}");
                }
            }
        }
    }

    public class SecurityHubStatusReporter : IStatusReporter
    {
        private readonly string _apiEndpoint;
        private readonly SecurityHubLogger _logger;
        private string _currentStatus;

        public SecurityHubStatusReporter(string apiEndpoint, SecurityHubLogger logger)
        {
            _apiEndpoint = apiEndpoint;
            _logger = logger;
            _currentStatus = "Initializing";
        }

        public async Task SendStatusUpdateAsync(string hubName, string status)
        {
            _currentStatus = status;
            var statusUpdate = new
            {
                HubName = hubName,
                Status = status,
                Timestamp = DateTime.Now
            };

            string jsonStatus = JsonSerializer.Serialize(statusUpdate);
            _logger.LogTransmission(hubName, jsonStatus);

            // TODO: Replace with actual API call
            await Task.Delay(100); 
        }

        public string GetCurrentStatus() => _currentStatus;
    }
}