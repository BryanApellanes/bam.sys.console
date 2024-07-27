using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleMenu : MenuAttribute<ConsoleCommandAttribute>
    {
        public ConsoleMenu() 
        {
            this.Selector = "cm";
        }

        public ConsoleMenu(string name): base(name)
        {
            this.Selector = "cm";
        }
    }
}
