using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.CommandLine;
using Bam.Net.CommandLine;
using Bam.Sys;

namespace Bam.Sys.Console.Tests.TestClasses
{
    [Menu("Sys Console Menu")]
    public class SysConsoleTestMenuClass
    {
        public SysConsoleTestMenuClass() { }

        [ConsoleCommand("Do some stuff")]
        public void ConsoleAction(string input) 
        {
            Message.PrintLine("this is a console action. you typed {0}", input);
        }

        [MenuItem("Menu item one")]
        public void MenuMethod()
        {
            Message.PrintLine("This is item one.");
        }

        [MenuItem]
        public void ThrowAnException()
        {
            throw new Exception("This method throws an exception for testing purposes");
        }

        [MenuItem]
        public void AcceptInput(string theInput)
        {
            Message.PrintLine("You typed: {0}", ConsoleColor.Cyan, theInput);
        }

        [MenuItem(DisplayName = "Item with a selector", Selector = "choose me")]
        public void SelectorItem()
        {
            Message.PrintLine("This item is selectable by the selector 'choose me'");
        }
    }
}
