using Bam.Net;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class ConsoleMenuInput : IMenuInput
    {
        public const string SelectorPrefix = ":";

        public ConsoleMenuInput()
        {
            ExitCode = 0;
        }

        public StringBuilder Input { get; set; }

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

        public bool IsMenuItemNavigation
        {
            get
            {
                return IsMenuItemNavigationKey(ConsoleKey);
            }
        }

        public bool IsMenuNavigation
        {
            get
            {
                return IsMenuNavigationKey(ConsoleKey);
            }
        }

        public bool IsSelector
        {
            get
            {
                if (!string.IsNullOrEmpty(Value))
                {
                    return Value.StartsWith(SelectorPrefix);
                }
                return false;
            }
        }

        public string Value
        {
            get
            {
                return Input.ToString();
            }
        }

        public string Selector
        {
            get
            {
                if (IsSelector)
                {
                    return Value.Trim().TruncateFront(1);
                }
                return string.Empty;
            }
        }

        public ConsoleKey ConsoleKey
        {
            get
            {
                return ConsoleKeyInfo.Key;
            }
        }

        public ConsoleKeyInfo ConsoleKeyInfo
        {
            get;
            set;
        }

        public int ItemNumber
        {
            get
            {
                if (int.TryParse(Value, out int itemNumber))
                {
                    return itemNumber;
                }
                return -1;
            }
        }

        public bool NextItem
        {
            get;
            set;
        }

        public bool PreviousItem
        {
            get;
            set;
        }

        public bool NextMenu
        {
            get;
            set;
        }

        public bool PreviousMenu
        {
            get;
            set;
        }

        public virtual bool IsMenuItemNavigationKey(ConsoleKey key)
        {
            return key == ConsoleKey.UpArrow ||
                key == ConsoleKey.DownArrow;
        }

        public virtual bool IsMenuNavigationKey(ConsoleKey key)
        {
            return key == ConsoleKey.RightArrow ||
                key == ConsoleKey.LeftArrow;
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
