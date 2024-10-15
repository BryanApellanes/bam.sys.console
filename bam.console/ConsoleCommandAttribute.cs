using Bam;
using Bam.Shell;

namespace Bam.Console
{
    /// <summary>
    /// Used to adorn methods that may be executed by a command line switch.
    /// </summary>
    public class ConsoleCommandAttribute : Attribute//: MenuItemAttribute
    {
        public ConsoleCommandAttribute() { }
        public ConsoleCommandAttribute(string name) //: base(name)
        {
            this.OptionName = name.CamelCase(true, new string[] { " " });
            this.OptionShortName = this.OptionName.CaseAcronym(true);
        }

        public ConsoleCommandAttribute(string name, string description) //: base(name, description)
        {
            this.OptionName = name.CamelCase(true, new string[] { " " });
            this.OptionShortName = this.OptionName.CaseAcronym(true);
        }

        public string? ValueExample
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the command line option to specify in order to execute the method.
        /// </summary>
        public string? OptionName
        {
            get;
            private set;
        }

        public string? OptionShortName
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }
    }
}
