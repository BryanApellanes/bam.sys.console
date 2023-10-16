using Bam.Net;
using Bam.Shell;

namespace Bam.Console
{
    public class ConsoleCommandAttribute : MenuItemAttribute
    {
        public ConsoleCommandAttribute() { }
        public ConsoleCommandAttribute(string name) : base(name)
        { }

        public ConsoleCommandAttribute(string name, string description) : base(name, description)
        {
            this.OptionName = name.PascalCase(true, new string[] { " " });
            this.OptionShortName = this.OptionName.CaseAcronym(true);
        }

        public string? ValueExample
        {
            get;
            private set;
        }

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
    }
}
