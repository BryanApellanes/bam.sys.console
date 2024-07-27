using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ArgumentParsingException : Exception
    {
        public ArgumentParsingException(ParsedArguments parsedArguments) : base(parsedArguments.Message)
        { 
            this.ParsedArguments = parsedArguments;
        }

        public ParsedArguments ParsedArguments { get; private set; }
    }
}
