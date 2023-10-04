using Bam.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleCommandAttribute : MenuItemAttribute
    {
        public ConsoleCommandAttribute() { }
        public ConsoleCommandAttribute(string name) : base(name)
        { }

        public ConsoleCommandAttribute(string name, string description) : base(name, description)
        {
        }

        public string? ValueExample
        {
            get;
            private set;
        }

        public string? Switch
        {
            get;
            private set;
        }
    }
}
