using System;
using System.IO;

namespace Devices
{
    internal static class Logger
    {
        const string logFilePath = "log.txt";

        public enum LogType
        {
            Info,
            Alert,
            Error
        }

        public static void Log(string message, LogType type)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var logEntry = $"{timestamp}, {type}, {message}";

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }
    }
}