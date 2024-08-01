using Bam.Shell;
using Bam.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
