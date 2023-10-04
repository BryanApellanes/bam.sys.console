using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.CommandLine;
using Bam.Console;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Logging;
using Bam.Sys;

namespace Bam.Console.Tests.TestClasses
{
    [Menu<MenuItemAttribute>("Sys Console Menu")]
    [Menu<ConsoleCommandAttribute>("Console Commands")]
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

        [ConsoleCommand]
        public void MenuSpecComparison()
        {
            MenuSpec menuSpec1 = new MenuSpec(typeof(SysConsoleTestMenuClass), typeof(MenuItem));
            MenuSpec menuSpec2 = new MenuSpec(typeof(SysConsoleTestMenuClass), typeof(MenuItem));

            menuSpec1.Equals(menuSpec2).ShouldBeTrue();
        }

        [ConsoleCommand("log", "add an information entry")]
        public void LogInfo()
        {
            Log.Info("Hello this is a log message");
        }
    }
}
