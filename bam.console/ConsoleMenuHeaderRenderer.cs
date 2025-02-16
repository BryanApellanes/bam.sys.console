using Bam.Shell;

namespace Bam.Console
{
    public class ConsoleMenuHeaderRenderer : IMenuHeaderRenderer
    {
        public void RenderMenuHeader(IMenu menu)
        {
            Message.PrintLine(menu.DisplayName);
            Message.PrintLine();
            Message.PrintLine(menu.HeaderText);
        }
    }
}
