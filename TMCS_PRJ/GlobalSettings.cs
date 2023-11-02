using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class GlobalSettings
    {

        public static Logger Logger { get; } = new Logger();
        public enum ChannelType
        {
            INPUT,
            OUTPUT,
        }
    }

    public class Logger
    {
        private readonly string logFilePath;
        public Logger()
        {
            string logDirectory = Directory.GetCurrentDirectory() + "\\log";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            logFilePath = Path.Combine(logDirectory, "log" + $"({DateTime.Now:yyyy-MM-dd})" + ".txt");
        }

        public void LogError(string message)
        {
            Log("ERROR", message);
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        object ojb = new object();
        private void Log(string logLevel, string message)
        {
            lock (ojb)
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
        }
    }
}
