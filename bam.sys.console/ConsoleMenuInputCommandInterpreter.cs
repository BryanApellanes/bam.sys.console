using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Sys.Console
{
    public class ConsoleMenuInputCommandInterpreter : IMenuInputCommandInterpreter
    {
        private Dictionary<string, Func<IMenuManager, IMenuInput, object>> _commands;

        public ConsoleMenuInputCommandInterpreter() { }

        public bool InterpretInput(IMenuManager menuManager, IMenuInput menuInput, out IMenuInputCommandInterpreterResult result)
        {
            // 
            throw new NotImplementedException();
        }
    }
}
