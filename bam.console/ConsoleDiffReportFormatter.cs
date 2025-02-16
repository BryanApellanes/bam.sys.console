/*
	Copyright © Bryan Apellanes 2015  
*/

using System.Text;
using Bam.Analytics;

namespace Bam.Console
{
    public class ConsoleDiffReportFormatter : DiffReportFormatter
    {
        public ConsoleDiffReportFormatter() { }
        public ConsoleDiffReportFormatter(DiffReport report) : base(report) { }
        public override void WriteLine(int lineNumber, string text, StringBuilder output)
        {
            Message.PrintLine("{0}{1}", ConsoleColor.Cyan, NumberLines ? "{0} ".Format(lineNumber) : "", text);
        }

        public override void WriteDeletedLine(int lineNumber, string text, StringBuilder output)
        {
            Message.PrintLine("-{0}{1}", ConsoleColor.Red, NumberLines ? "{0} ".Format(lineNumber) : "", text);
        }

        public override void WriteInsertedLine(int lineNumber, string text, StringBuilder output)
        {
            Message.PrintLine("+{0}{1}", ConsoleColor.Green, NumberLines ? "{0} ".Format(lineNumber) : "", text);
        }
    }
}
