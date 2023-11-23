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
        public InputCommandAttribute(string name, string description = null) 
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
