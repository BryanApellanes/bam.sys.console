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
    public class ConsoleMenuItemRunResultRenderer : IMenuItemRunResultRenderer
    {
        public void RenderMenuItemRunResult(IMenuItemRunResult menuItemRunResult)
        {
            if (menuItemRunResult != null)
            {
                if (menuItemRunResult.Success)
                {
                    if (menuItemRunResult.MenuItem != null && !string.IsNullOrEmpty(menuItemRunResult.MenuItem.DisplayName))
                    {
                        Message.PrintLine("{0} succeeded", ConsoleColor.Green, menuItemRunResult.MenuItem.DisplayName);
                    }
                    else
                    {
                        Message.PrintLine("Item succeeded", ConsoleColor.Green);
                    }
                }
                else
                {
                    if (menuItemRunResult.MenuItem != null && !string.IsNullOrEmpty(menuItemRunResult.MenuItem.DisplayName))
                    {
                        Message.PrintLine("{0} failed", ConsoleColor.Red, menuItemRunResult.MenuItem.DisplayName);
                    }

                    if (menuItemRunResult.Exception != null && !string.IsNullOrEmpty(menuItemRunResult.Exception.StackTrace))
                    {

                        Message.PrintLine(menuItemRunResult.Exception.GetMessageAndStackTrace(), ConsoleColor.Magenta);
                    }
                }
            }
        }
    }
}
