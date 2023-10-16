using Bam.Net;
using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleExceptionReporter : IExceptionReporter
    {
        public void ReportException(Exception exception)
        {
            Message.PrintLine(exception.Message, ConsoleColor.Magenta);
            Message.PrintLine(exception.GetStackTrace(), ConsoleColor.Magenta);
        }
    }
}
