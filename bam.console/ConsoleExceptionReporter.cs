namespace Bam.Console
{
    public class ConsoleExceptionReporter : IExceptionReporter
    {
        public void ReportException(string message, Exception exception)
        {
            Message.PrintLine(message, ConsoleColor.DarkRed);
            ReportException(exception);
        }

        public void ReportException(Exception exception)
        {
            Message.PrintLine(exception.Message, ConsoleColor.DarkRed);
            Message.PrintLine(exception.GetStackTrace(), ConsoleColor.Magenta);
        }
    }
}
