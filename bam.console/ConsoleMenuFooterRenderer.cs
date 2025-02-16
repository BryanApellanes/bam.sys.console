using Bam.Shell;

namespace Bam.Console
{
    public class ConsoleMenuFooterRenderer : IMenuFooterRenderer
    {
        public void RenderMenuFooter(IMenu selectedMenu, params IMenu[] allMenus)
        {
            Message.PrintLine(selectedMenu.FooterText);

            foreach (IMenu m in allMenus)
            {
                ConsoleColorCombo colors = new ConsoleColorCombo(ConsoleColor.Cyan, ConsoleColor.Black);
                if (m == selectedMenu)
                {
                    colors = new ConsoleColorCombo(ConsoleColor.Black, ConsoleColor.White);
                }
                Message.Print("[{0}{1}] {2}\t", colors, ConsoleMenuInput.SelectorPrefix, m.Selector, m.Name);
            }
            Message.PrintLine();
        }
    }
}
