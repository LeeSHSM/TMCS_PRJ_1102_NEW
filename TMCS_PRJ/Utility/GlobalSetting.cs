using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LshGlobalSetting
{
    public class GlobalSetting
    {
        public static Logger Logger { get; } = new Logger();
        public static readonly string DBConnectString = "Server=192.168.50.50;Database=TMCS;User Id=sa;password=tkdgus12#;";
        public static readonly IPAddress MATRIX_IP = IPAddress.Parse("192.168.50.8");
        public static readonly int MATRIX_PORT = 23;
        public enum ChannelType
        {
            INPUT,
            OUTPUT,
        }

        public enum DBMatrixChannelInfo
        {
            channelname,
            port,
            channeltype,
            routeno,
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
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff} [{logLevel}] {message}";
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
        }
    }
}
