using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.CommandLine;
using Bam;
using Bam.Logging;
using Bam.Shell;

namespace Bam.Console.Tests.TestClasses
{
    [Menu<MenuItemAttribute>("Menu Items")]
    [Menu<ConsoleCommandAttribute>("Console Commands")]
    public class ConsoleTestMenuClass
    {
        public ConsoleTestMenuClass() { }

        [ConsoleCommand("Do some stuff")]
        public void ConsoleCommand(string input)
        {
            Message.PrintLine("This is a console command. You specified '{0}' as the input value", input);
        }

        [ConsoleCommand]
        public void NoName(string input)
        {
            Message.PrintLine("This is a console command with no attribute name. You specified '{0}' as the input value", input);
        }

        [ConsoleCommand]
        public void MethodWith2Inputs(string value, string value2)
        {
            Message.PrintLine("This is a console command with 2 inputs. You specified '{0}' for value and '{1}' for value2", value, value2);
        }
        
        [ConsoleCommand]
        public void UseConsole(string value, string value2)
        {
            System.Console.WriteLine("This is a console command with 2 inputs. You specified '{0}' for value and '{1}' for value2", value, value2);
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
            MenuSpec menuSpec1 = new MenuSpec(typeof(ConsoleTestMenuClass), typeof(MenuItem));
            MenuSpec menuSpec2 = new MenuSpec(typeof(ConsoleTestMenuClass), typeof(MenuItem));

            menuSpec1.Equals(menuSpec2).ShouldBeTrue();
        }

        [ConsoleCommand("log", "add an information entry")]
        public void LogInfo()
        {
            Message.PrintLine("log console command");
            Log.Info("Hello this is a log message");
        }

        [InputCommand("inputCommandTest")]
        public void InputCommandTest()
        {
            System.Console.WriteLine("Executed {0}", nameof(InputCommandTest));
        }
    }
}
