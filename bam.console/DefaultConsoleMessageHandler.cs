using System;
using Bam.Console;

namespace Bam
{
    public class DefaultConsoleMessageHandler : IConsoleMessageHandler
    {
        public void Log(params Console.ConsoleMessage[] consoleMessages)
        {
            ConsoleMessage.Log(consoleMessages);
        }

        public void Print(params ConsoleMessage[] messages)
        {
            if (messages != null)
            {
                foreach (ConsoleMessage message in messages)
                {
                    PrintMessage(message);
                }
            }
        }
        
        public static void PrintMessage(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            System.Console.ForegroundColor = foregroundColor;
            System.Console.BackgroundColor = backgroundColor;
            System.Console.Write(message);
            System.Console.ResetColor();
        }
        
        public static void PrintMessage(ConsoleMessage message)
        {
            System.Console.ForegroundColor = message.Colors.ForegroundColor;
            System.Console.BackgroundColor = message.Colors.BackgroundColor;
            System.Console.Write(message.Text);
            System.Console.ResetColor();
        }
    }
}