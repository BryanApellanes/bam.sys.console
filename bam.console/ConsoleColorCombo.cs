/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Console
{
    public class ConsoleColorCombo
    {
        public ConsoleColorCombo(ConsoleColor foreground)
        {
            ForegroundColor = foreground;
        }

        public ConsoleColorCombo(ConsoleColor foreground, ConsoleColor background) : this(foreground)
        {
            BackgroundColor = background;
        }

        public ConsoleColor ForegroundColor { get; private set; }
        public ConsoleColor BackgroundColor { get; private set; }
    }
}
