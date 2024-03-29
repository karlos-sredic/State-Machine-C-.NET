using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger
{
    public class PrintLogger : ILogger
    {
        private readonly TextBox mTextBox;

        public PrintLogger(TextBox textBox)
        {
            mTextBox = textBox;
        }

        public void Log(string message)
        {
            mTextBox.AppendText(message+Environment.NewLine);
        }
    }
}
