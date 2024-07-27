using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class DefaultArgumentParser : IArgumentParser
    {
        public IParsedArguments ParseArguments(string[] arguments)
        {
            return new ParsedArguments(arguments);
        }
    }
}
