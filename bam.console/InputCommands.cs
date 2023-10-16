using Bam.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class InputCommands
    {
        public InputCommands(Type type)
        {
            this.Commands = new Dictionary<string, InputCommand>();
            this.ContainerType = type;
            this.LoadCommands();
        }

        protected void LoadCommands()
        {
            foreach(MethodInfo methodInfo in ContainerType.GetMethods())
            {
                if(methodInfo.HasCustomAttributeOfType(out InputCommandAttribute attribute))
                {
                    InputCommand option = new InputCommand(this.ContainerType, methodInfo, attribute);
                    if (this.Commands.ContainsKey(option.Name))
                    {
                        throw new InvalidOperationException($"Duplicate option name specified: {option.Name}");
                    }
                    Commands.Add(option.Name, option);
                }
            }
        }

        public Type ContainerType { get; private set; }

        public Dictionary<string, InputCommand> Commands{ get; private set; }
    }
}
