using Bam.Shell;
using Bam.Test.Menu;

namespace Bam.Console
{
    public class ConsoleMenuInputCommandRenderer : IMenuInputCommandRenderer
    {
        public void RenderMenuInputCommands(IMenu menu)
        {            
            InputCommands commands = new InputCommands(menu);
            if(commands.Commands.Count > 0)
            {
                Message.PrintLine(Menu.DefaultFooterText);
                foreach (string name in commands.Names)
                {
                    Message.PrintLine($"\"{name}\" -- {commands.Commands[name].Description}");
                }
            }
            Message.PrintLine(Menu.DefaultFooterText);
        }
    }
}
