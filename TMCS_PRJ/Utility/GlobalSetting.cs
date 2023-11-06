using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class GlobalSetting
    {
        public static Logger Logger { get; } = new Logger();
        public static readonly string MATRIX_DB = "Server=192.168.50.50;Database=TMCS;User Id=sa;password=tkdgus12#;";
        public static readonly IPAddress MATRIX_IP = IPAddress.Parse("192.168.50.8");
        public static readonly int MATRIX_PORT = 23;
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
