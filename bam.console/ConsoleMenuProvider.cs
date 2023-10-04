using Bam.CommandLine;
using Bam.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Sys.Console
{
    public class ConsoleMenuProvider : MenuProvider<ConsoleCommandAttribute>
    {
        public ConsoleMenuProvider(IMenuItemProvider menuItemProvider, IMenuItemSelector menuItemSelector, IMenuItemRunner menuItemRunner) : base(menuItemProvider, menuItemSelector, menuItemRunner)
        {
        }

        public override IMenu GetMenu(Type type)
        {
            return GetMenu<ConsoleCommandAttribute>(type);
        }
    }
}
