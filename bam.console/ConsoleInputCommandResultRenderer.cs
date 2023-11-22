using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleInputCommandResultRenderer : IInputCommandResultRenderer
    {
        public void RenderInputCommandResult(IInputCommandResult inputCommandResult)
        {
            ConsoleColorCombo colors = new ConsoleColorCombo(ConsoleColor.Green, ConsoleColor.Black);
            string result = "succeeded";
            string commandName = "command";
            string extended = string.Empty;
            if (inputCommandResult != null)
            {
                commandName = inputCommandResult.InputName ?? commandName;
                if (!inputCommandResult.Success)
                {
                    colors = new ConsoleColorCombo(ConsoleColor.DarkRed, ConsoleColor.DarkYellow);
                    result = "failed";
                    extended = inputCommandResult.Exception.GetMessageAndStackTrace();
                }
            }
            Message.Print("> {0} -- ", ConsoleColor.Cyan, commandName);
            Message.Print(" {1}", colors, commandName, result);
            Message.PrintLine();
            if (extended != string.Empty)
            {
                Message.PrintLine(extended, ConsoleColor.DarkRed);
            }
        }
    }
}
