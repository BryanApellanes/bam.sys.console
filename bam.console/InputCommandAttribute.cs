using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    /// <summary>
    /// Used to addorn methods that may be executed by an name at a bam menu prompt.
    /// </summary>
    public class InputCommandAttribute : Attribute
    {
        public InputCommandAttribute() { }
        public InputCommandAttribute(string name, string description = null) 
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
