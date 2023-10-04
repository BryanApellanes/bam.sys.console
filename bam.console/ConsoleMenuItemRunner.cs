using Bam.Commandline.Menu;
using Bam.Services;
using Bam.Sys;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Sys.Console
{
    public class ConsoleMenuItemRunner : IMenuItemRunner
    {
        public ConsoleMenuItemRunner(IDependencyProvider dependencyProvider, IMenuInputParser menuInputParser)
        {
            DependencyProvider = dependencyProvider;
            InputParser = menuInputParser;
        }

        public ConsoleMenuItemRunner(IDependencyProvider dependencyProvider) : this(dependencyProvider, new MenuInputParaser(new StringArgumentProvider()))
        {
        }

        public IMenuInputParser InputParser { get; set; }

        /// <summary>
        /// Gets the componenet that provides instances for non static menu item methods.
        /// </summary>
        public IDependencyProvider DependencyProvider { get; private set; }

        public IMenuItemRunResult RunMenuItem(IMenuItem menuItem, IMenuInput menuInput)
        {
            try
            {
                if (!menuItem.MethodInfo.IsStatic && menuItem.Instance == null)
                {
                    if (menuItem.MethodInfo.DeclaringType != null)
                    {
                        menuItem.Instance = DependencyProvider.Get(menuItem.MethodInfo.DeclaringType);
                    }
                }

                object? result = menuItem.MethodInfo.Invoke(menuItem.Instance, InputParser.GetMethodParameters(menuItem, menuInput));

                return new MenuItemRunResult()
                {
                    MenuItem = menuItem,
                    Success = true,
                    MenuInput = menuInput,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new MenuItemRunResult
                {
                    MenuItem = menuItem,
                    Success = false,
                    MenuInput = menuInput,
                    Exception = ex,
                };
            }
        }
    }
}
