using System;
using System.IO;
using Newtonsoft.Json;
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
            // Ensure paths are absolute and logs directory exists
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logsDirectory = Path.Combine(baseDirectory, "logs");
            
            // Create full paths
            _operationsLogPath = Path.Combine(logsDirectory, operationsLogPath);
            _transmissionLogPath = Path.Combine(logsDirectory, transmissionLogPath);
            
            // Ensure directory exists
            EnsureLogDirectoryExists();
        }

        private void EnsureLogDirectoryExists()
        {
            string logDirectory = Path.GetDirectoryName(_operationsLogPath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                LogOperation("Logger", $"Created log directory: {logDirectory}");
            }
        }

        public string GetOperationsLogPath() => _operationsLogPath;
        public string GetTransmissionLogPath() => _transmissionLogPath;

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
}