using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Sys.Console
{
    public class ConsoleCommandAttribute : MenuAttribute
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
