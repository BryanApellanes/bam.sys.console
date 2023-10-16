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
            if (inputCommandResult != null)
            {
                commandName = inputCommandResult.InputName ?? commandName;
                if (!inputCommandResult.Success)
                {
                    colors = new ConsoleColorCombo(ConsoleColor.Magenta, ConsoleColor.Yellow);
                    result = "failed";
                }
            }
            Message.Print("> {0} -- ", ConsoleColor.Cyan, commandName);
            Message.PrintLine(" {1}", colors, commandName, result);
        }
    }
}
