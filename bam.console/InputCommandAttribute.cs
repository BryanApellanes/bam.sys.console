using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class InputCommandAttribute : Attribute
    {
        public InputCommandAttribute() { }
        public InputCommandAttribute(string name) 
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}
