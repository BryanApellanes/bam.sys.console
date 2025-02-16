namespace Bam.Console
{
    /// <summary>
    /// An interface defining a method for parsing command line arguments.
    /// </summary>
    public interface IArgumentParser
    {
        IParsedArguments ParseArguments(string[] arguments); 
    }
}
