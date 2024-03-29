using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class FileLogger : ILogger
    {
        private string mPathname;

        public FileLogger(string pathname)
        {
            mPathname = pathname;
        }

        public void Log(string message)
        {
            EnsureFileExists();
            File.AppendAllText(mPathname, message + Environment.NewLine);
        }

        private void EnsureFileExists()
        {
            if(!File.Exists(mPathname))
            {
                File.CreateText(mPathname);
            }
        }
    }
}
