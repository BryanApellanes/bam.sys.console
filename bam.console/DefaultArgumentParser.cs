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
