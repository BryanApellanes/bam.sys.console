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
