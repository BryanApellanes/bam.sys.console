using Bam.CoreServices;
using Bam.Services;
using Bam.Shell;

namespace Bam.Console;

public class ConsoleMenuOptions : MenuOptions
{
    public ConsoleMenuOptions(IMenuRenderer menuRenderer, IMenuHeaderRenderer menuHeaderRenderer, IMenuFooterRenderer menuFooterRenderer, IMenuProvider menuProvider, IMenuInputReader menuInputReader, IMenuInputCommandInterpreter menuInputCommandInterpreter, IMenuItemRunner menuItemRunner, IMenuItemRunResultRenderer menuItemRunResultRenderer, IInputCommandResultRenderer inputCommandResultRenderer) : base(menuRenderer, menuHeaderRenderer, menuFooterRenderer, menuProvider, menuInputReader, menuInputCommandInterpreter, menuItemRunner, menuItemRunResultRenderer, inputCommandResultRenderer)
    {
    }

    public static ServiceRegistry GetServiceRegistry<TypedArgumentProviderType>(IMenuItemRunResultRenderer? menuItemRunResultRenderer = null) where TypedArgumentProviderType: ITypedArgumentProvider
    {
        ServiceRegistry registry = new ServiceRegistry();
        registry
            .For<IDependencyProvider>().UseSingleton(registry)
            .For<ServiceRegistry>().UseSingleton(registry)
            .For<IMenuRenderer>().Use<ConsoleMenuRenderer>()
            .For<IMenuHeaderRenderer>().UseSingleton<ConsoleMenuHeaderRenderer>()
            .For<IMenuFooterRenderer>().UseSingleton<ConsoleMenuFooterRenderer>()
            .For<IMenuInputCommandRenderer>().Use<ConsoleMenuInputCommandRenderer>()
            .For<IMenuItemProvider>().Use<MenuItemProvider>()
            .For<IMenuProvider>().Use<ConsoleMenuProvider>()
            .For<IMenuInputReader>().UseSingleton<ConsoleMenuInputReader>()
            .For<IMenuInputCommandInterpreter>().Use<DefaultMenuInputCommandInterpreter>()
            .For<IMenuInputMethodArgumentProvider>().Use<MenuInputMethodArgumentProvider>()
            .For<ITypedArgumentProvider>().Use<TypedArgumentProviderType>()
            .For<IMenuItemRunResultRenderer>().UseSingleton(menuItemRunResultRenderer ?? new ConsoleMenuItemRunResultRenderer())
            .For<IInputCommandResultRenderer>().Use<ConsoleInputCommandResultRenderer>()
            .For<IMenuItemSelector>().Use<MenuItemSelector>()
            .For<IMenuItemRunner>().Use<ConsoleMenuItemRunner>()
            .For<ISuccessReporter>().Use<ConsoleSuccessReporter>()
            .For<IConsoleMethodParameterProvider>().Use<CommandLineArgumentsConsoleMethodParameterProvider>()
            .For<IExceptionReporter>().Use<ConsoleExceptionReporter>();
        return registry;
    } 
}