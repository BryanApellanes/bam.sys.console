using Bam.Shell;

namespace Bam.Console
{
    public class ConsoleMenuProvider : MenuProvider<ConsoleCommandAttribute>
    {
        public ConsoleMenuProvider(IMenuItemProvider menuItemProvider, IMenuItemSelector menuItemSelector, IMenuItemRunner menuItemRunner) : base(menuItemProvider, menuItemSelector, menuItemRunner)
        {
        }

        public override IMenu GetMenu(Type type)
        {
            return GetMenu<ConsoleCommandAttribute>(type);
        }
    }
}
