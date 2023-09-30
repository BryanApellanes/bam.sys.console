using Bam.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Sys.Console
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

        public void Reset()
        {
            Input.Clear();
        }

        public IMenuInput ReadMenuInput()
        {
            ReadingInput?.Invoke(this, new MenuInputOutputLoopEventArgs
            {

            });
            ConsoleKeyInfo consoleKeyInfo = System.Console.ReadKey();
            if (!ConsoleMenuInput.IsNavigationKey(consoleKeyInfo.Key))
            {
                Input.Append(consoleKeyInfo.KeyChar);
            }
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
            ConsoleMenuInput menuInput = new ConsoleMenuInput
            {
                Value = Input.ToString(),
                Exit = consoleKeyInfo.Key == ConsoleKey.Escape,
                ConsoleKey = consoleKeyInfo.Key,
                ConsoleKeyInfo = consoleKeyInfo,
                Next = consoleKeyInfo.Key == ConsoleKey.DownArrow,
                Previous = consoleKeyInfo.Key == ConsoleKey.UpArrow,
                Enter = consoleKeyInfo.Key == ConsoleKey.Enter
            };

            if (int.TryParse(menuInput.Value, out int stringIndex))
            {
                menuInput.Index = stringIndex - 1;
            }

            return menuInput;
        }
    }
}
