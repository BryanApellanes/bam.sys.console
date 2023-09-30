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
        }
    }
}
