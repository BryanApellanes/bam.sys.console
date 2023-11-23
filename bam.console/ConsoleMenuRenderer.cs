using bam.configuration;
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
    public class ConsoleMenuRenderer : MenuRenderer
    {
        public ConsoleMenuRenderer(IMenuHeaderRenderer headerRenderer, IMenuFooterRenderer footerRenderer, IMenuInputReader inputReader, IMenuInputCommandRenderer inputCommandRenderer) : base(headerRenderer, footerRenderer, inputReader, inputCommandRenderer)
        {
            Divider = "*".Times(30);
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
                RenderDivider();
            }
        }

        public override void RenderMenu(IMenu selectedMenu, params IMenu[] menus)
        {
            HeaderRenderer.RenderMenuHeader(selectedMenu);
            RenderItems(selectedMenu);
            FooterRenderer.RenderMenuFooter(selectedMenu, menus);
            RenderInputCommands(selectedMenu);
            RenderPrompt(selectedMenu);
        }

        public override void RenderDivider()
        {
            Message.PrintLine();
            Message.PrintLine(Divider, ConsoleColor.DarkYellow);
            Message.PrintLine();
        }

        public override void RenderInputCommands(IMenu menu)
        {
            InputCommandRenderer.RenderMenuInputCommands(menu);
        }

        public virtual void RenderPrompt(IMenu menu)
        {
            Message.Print($" {menu.Selector} > ");
        }
    }
}
