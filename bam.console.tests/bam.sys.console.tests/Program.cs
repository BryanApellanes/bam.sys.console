using Bam.CommandLine;
using Bam.Sys.Console;

namespace Bam.Net.Application
{
    [Serializable]
    class Program : CommandLineTool
    {
        static void Main(string[] args)
        {
            BamContext.Main(args);
        }
    }
}
