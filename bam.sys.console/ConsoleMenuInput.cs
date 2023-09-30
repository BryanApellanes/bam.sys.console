using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Sys.Console
{
    public class ConsoleMenuInput : IMenuInput
    {
        public ConsoleMenuInput()
        {
            Index = -1;
            ExitCode = 0;
        }

        public bool Exit
        {
            get;
            set;
        }

        public int ExitCode
        {
            get;
            set;
        }

        public bool Enter
        {
            get;
            set;
        }

        public bool IsNavigation
        {
            get
            {
                return IsNavigationKey(ConsoleKey);
            }
        }

        public string Value
        {
            get;
            set;
        }

        public ConsoleKey ConsoleKey
        {
            get;
            set;
        }

        public ConsoleKeyInfo ConsoleKeyInfo
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public bool Next
        {
            get;
            set;
        }

        public bool Previous
        {
            get;
            set;
        }

        public static bool IsNavigationKey(ConsoleKey key)
        {
            return key == ConsoleKey.UpArrow ||
                    key == ConsoleKey.DownArrow ||
                    key == ConsoleKey.LeftArrow ||
                    key == ConsoleKey.RightArrow;
        }
    }
}
