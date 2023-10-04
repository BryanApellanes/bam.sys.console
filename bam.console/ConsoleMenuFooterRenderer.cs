using Bam.Net.CommandLine;
using Bam.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleMenuFooterRenderer : IMenuFooterRenderer
    {
        public void RenderMenuFooter(IMenu menu, params IMenu[] otherMenus)
        {
            Message.PrintLine(menu.FooterText);
            List<IMenu> menus = new List<IMenu>
            {
                menu
            };

            if (otherMenus != null)
            {
                menus.AddRange(otherMenus);
            }
            menus.Sort((x, y) => x.Selector.CompareTo(y.Selector));

            foreach (IMenu m in menus)
            {
                ConsoleColorCombo colors = new ConsoleColorCombo(ConsoleColor.Cyan, ConsoleColor.Black);
                if (m == menu)
                {
                    colors = new ConsoleColorCombo(ConsoleColor.Black, ConsoleColor.White);
                }
                Message.Print("[{0}{1}] {2}\t", colors, ConsoleMenuInput.SelectorPrefix, m.Selector, m.Name);
            }
            Message.PrintLine();
        }
    }
}
