using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleSuccessReporter : ISuccessReporter
    {
        public void ReportSuccess(string message)
        {
            Message.PrintLine(message, ConsoleColor.DarkGreen);
        }
    }
}
