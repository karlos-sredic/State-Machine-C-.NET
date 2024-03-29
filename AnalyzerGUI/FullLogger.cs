using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger
{
    public class FullLogger : ILogger
    {
        private readonly PrintLogger mPrintLogger;
        private readonly FileLogger mFileLogger;

        public FullLogger(TextBox textBox, string pathname) {
            mPrintLogger = new PrintLogger(textBox);
            mFileLogger = new FileLogger(pathname);
        }

        public void Log(string message)
        {
            string messageWithTimestamp = $"{GetTimestamp()} {message}";

            mPrintLogger.Log(messageWithTimestamp);
            mFileLogger.Log(messageWithTimestamp);
        }

        private static string GetTimestamp()
        {
            return DateTime.Now.ToString("[HH:mm:ss.fff]");
        }
    }
}
