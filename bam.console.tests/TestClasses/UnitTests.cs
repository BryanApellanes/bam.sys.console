using Bam.Shell;
using Bam.Test;

namespace Bam.Console.Tests.TestClasses
{
    [Menu<TestAttribute>("Tests")]
    public class UnitTests
    {
        [Test(TestType.Unit)]
        public void FakeTestForTesting()
        {
            Message.PrintLine("it worked", ConsoleColor.Green);
        }
    }
}
