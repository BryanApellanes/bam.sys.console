using Bam.Net;
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
            Args.ThrowIfNull(inputCommandResult, nameof(inputCommandResult));

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
            Message.PrintLine();
            Message.Print(" > command ");
            Message.Print("'{0}'", new ConsoleColorCombo(ConsoleColor.White, ConsoleColor.DarkYellow), commandName);
            Message.Print(" --> ");
            Message.Print(" {0}", colors, result);
            Message.PrintLine();
            
            if(inputCommandResult?.Message != null)            
            {
                Message.PrintLine(inputCommandResult.Message, colors);
            }

            if (extended != string.Empty)
            {
                Message.PrintLine(extended, ConsoleColor.DarkMagenta);
            }
        }
    }
}
