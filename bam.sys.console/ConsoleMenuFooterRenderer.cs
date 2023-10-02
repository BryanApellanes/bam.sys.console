using Bam.Net.CommandLine;
using Bam.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Sys.Console
{
    public class ConsoleMenuFooterRenderer : IMenuFooterRenderer
    {
        public void RenderMenuFooter(IMenu menu, params IMenu[] otherMenus)
        {
            Message.PrintLine(menu.FooterText);
            if(otherMenus != null)
            {
                foreach(IMenu otherMenu in otherMenus)
                {
                    Message.Print("[{0}{1}] {2}\t", ConsoleMenuInput.SelectorPrefix, otherMenu.Selector, otherMenu.Name);
                }
            }
            Message.PrintLine();
        }
    }
}
