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
