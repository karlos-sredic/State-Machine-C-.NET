using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class FullLogger : ILogger
    {
        private readonly PrintLogger mPrintLogger;
        private readonly FileLogger mFileLogger;

        public FullLogger(string pathname) {
            mPrintLogger = new PrintLogger();
            mFileLogger = new FileLogger(pathname);
        }

        public void Log(string message)
        {
            string messageWithTimestamp = $"${GetTimestamp()} ${message}";

            mPrintLogger.Log(messageWithTimestamp);
            mFileLogger.Log(messageWithTimestamp);
        }

        private static string GetTimestamp()
        {
            return DateTime.Now.ToString("[HH:mm:ss.fff]");
        }
    }
}
