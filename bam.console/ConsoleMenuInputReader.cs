using Bam.Shell;
using Bam.Shell.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleMenuInputReader : IMenuInputReader
    {
        public ConsoleMenuInputReader(IBamContext bamContext)
        {
            BamContext = bamContext;
            ExitKey = MenuManager.DefaultExitKey;
            Input = new StringBuilder();
        }

        public event EventHandler<MenuInputOutputLoopEventArgs> ReadingInput;

        protected IBamContext BamContext
        {
            get;
            private set;
        }

        protected StringBuilder Input
        {
            get;
            private set;
        }

        public ConsoleKey ExitKey
        {
            get;
            private set;
        }

        public IMenuInput ReadMenuInput()
        {
            ConsoleKeyInfo consoleKeyInfo = System.Console.ReadKey();
            if (!ConsoleMenuInput.IsNavigationKey(consoleKeyInfo.Key))
            {
                Input.Append(consoleKeyInfo.KeyChar);

                if (consoleKeyInfo.Key == ConsoleKey.Backspace)
                {
                    string currentInput = Input.ToString();
                    int newLength = currentInput.Length - 2;
                    if (newLength >= 0)
                    {
                        StringBuilder newInput = new StringBuilder(Input.ToString().Substring(0, newLength));
                        Input = newInput;
                    }
                }
                // clean up backspaces
                string input = Input.ToString().Trim();
                if (consoleKeyInfo.Key == ConsoleKey.Spacebar)
                {
                    input += " ";
                }
                Input.Clear();
                Input.Append(input);
            }

            ConsoleMenuInput menuInput = new ConsoleMenuInput
            {
                Input = Input,
                Exit = consoleKeyInfo.Key == ConsoleKey.Escape,
                ConsoleKeyInfo = consoleKeyInfo,
                NextItem = consoleKeyInfo.Key == ConsoleKey.DownArrow,
                PreviousItem = consoleKeyInfo.Key == ConsoleKey.UpArrow,
                NextMenu = consoleKeyInfo.Key == ConsoleKey.RightArrow,
                PreviousMenu = consoleKeyInfo.Key == ConsoleKey.LeftArrow,
                Enter = consoleKeyInfo.Key == ConsoleKey.Enter
            };
            ReadingInput?.Invoke(this, new MenuInputOutputLoopEventArgs
            {
                MenuInputReader = this,
                MenuInput = menuInput,
            });
            return menuInput;
        }
    }
}
