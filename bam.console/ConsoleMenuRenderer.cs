using bam.configuration;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleMenuRenderer : MenuRenderer
    {
        public ConsoleMenuRenderer(IMenuHeaderRenderer headerRenderer, IMenuFooterRenderer footerRenderer, IMenuInputReader inputReader) : base(headerRenderer, footerRenderer, inputReader)
        {
            Divider = "*".Times(30);
        }

        public string Divider
        {
            get;
            set;
        }

        protected override void RenderItems(IMenu menu)
        {
            int number = 0;
            foreach (IMenuItem item in menu.Items)
            {
                string pointer = item.Selected ? ">" : " ";
                string selector = !string.IsNullOrEmpty(item.Selector) ? $"[{ConsoleMenuInput.SelectorPrefix}{item.Selector}] " : "";
                Message.PrintLine($"{pointer} {++number}. {selector}{item.DisplayName}");
            }
        }

        public override void RerenderMenu(IMenu menu, IMenuInput menuInput, params IMenu[] otherMenus)
        {
            System.Console.Clear();
            RenderMenu(menu, otherMenus);
            Message.Print(menuInput.Value, ConsoleColor.Green);
            if (menuInput.Enter)
            {
                Message.PrintLine();
                Message.PrintLine();
                Message.PrintLine(Divider, ConsoleColor.Yellow);
                Message.PrintLine();
            }
        }

        public override void RenderMenu(IMenu menu, params IMenu[] otherMenus)
        {
            HeaderRenderer.RenderMenuHeader(menu);
            RenderItems(menu);
            FooterRenderer.RenderMenuFooter(menu, otherMenus);
        }
    }
}
