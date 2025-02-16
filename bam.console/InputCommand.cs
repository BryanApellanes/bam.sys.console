using System.Reflection;

namespace Bam.Console
{
    public class InputCommand
    {
        public InputCommand(Type containerType, MethodInfo optionMethod, InputCommandAttribute attribute) 
        {
            this.ContainerType = containerType;
            this.OptionMethod = optionMethod;
            this.Attribute = attribute;
            if (Attribute == null)
            {
                throw new InvalidOperationException("Option name not specified");
            }
        }

        protected InputCommandAttribute Attribute { get; set; }

        public string Name
        {
            get
            {
                return Attribute.Name;
            }
        }

        public string Description
        {
            get
            {
                return Attribute.Description;
            }
        }


        public Type ContainerType { get; set; }
        
        public MethodInfo OptionMethod { get; set; }
    }
}
