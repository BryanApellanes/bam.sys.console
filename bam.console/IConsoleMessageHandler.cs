namespace Bam.Console
{
    public interface IConsoleMessageHandler
    {
        void Log(params ConsoleMessage[] consoleMessages);
        void Print(params ConsoleMessage[] messages);
    }
}