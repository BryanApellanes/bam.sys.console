using Bam.Commandline.Menu;
using Bam;
using Bam.Configuration;
using Bam.CoreServices;
using Bam.Logging;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class BamConsoleContext : BamContext, IBamConsoleContext
    {
        static BamConsoleContext()
        {
            Current = new BamConsoleContext();
        }

        public BamConsoleContext()
        {
            ValidArgumentInfo = new List<ArgumentInfo>();
            ServiceRegistry = GetDefaultContextServiceRegistry();
        }

        public BamConsoleContext(ServiceRegistry serviceRegistry) : this()
        {
            ServiceRegistry = serviceRegistry;
        }

        public static void Main(string[] args)
        {
            Main(args, MenuSpecs.LoadList.ToArray());
        }

        public static void Main(string[] args, params Assembly[] assemblies)
        {
            Main(args, MenuSpecs.Scan(assemblies).ToArray());
        }

        public static void Main(string[] args, params MenuSpecs[] menuSpecs)
        {
            Current.AddSwitches();
            Current.AddConfigurationSwitches();
            MenuSpecs.LoadList = menuSpecs;
            Main(args, () => { });
        }

        public static async void Main(string[] args, Action preInit, ConsoleArgsParsedDelegate? parseErrorHandler = null)
        {
            if (parseErrorHandler == null)
            {
                parseErrorHandler = (a) => throw new ArgumentException(a.Message);
            }

            Initialize(args, preInit, parseErrorHandler);

            Current.MenuManager.StartInputOutputLoop();
            System.Console.ReadLine();
        }

        protected static void Initialize(string[] args, Action preInit, ConsoleArgsParsedDelegate? parseErrorHandler)
        {
            Current.ArgsParsedError += parseErrorHandler;

            preInit();

            Current.AddValidArgument("?", true, description: "Show usage");
            Current.AddValidArgument("v", true, description: "Show version information");
            Current.AddValidArgument("i", true, description: "Run interactively");

            // TODO: extract/encapsulate a IConsoleActionProvider discovery mechanism 
            Current.AddValidArgument("ut", true, description: "Run all unit tests");
            Current.AddValidArgument("it", true, description: "Run all integration tests");
            Current.AddValidArgument("spec", true, description: "Run all specification tests");
            // -/ TODO: extract/encapsulate a IConsoleActionProvider discovery mechanism 

            Current.ParseArgs(args);

            if (Current.Arguments.Contains("?"))
            {
                Current.Usage(Assembly.GetExecutingAssembly());
                Exit();
            }
            else if (Current.Arguments.Contains("v"))
            {
                Version(Assembly.GetEntryAssembly());
                Exit();
            }

            if (Current.Arguments.Length > 0 && !Current.Arguments.Contains("i"))
            {
                if (ExecuteSwitches(Current.Logger, Current.Arguments))
                {
                    Exit(0);
                }
            }
        }

        public static void Exit(int code = 0)
        {
            System.Console.ResetColor();
            Exiting?.Invoke(code);
            Thread.Sleep(1000);
            Environment.Exit(code);
            Exited?.Invoke(code);
        }

        public static BamConsoleContext Current
        {
            get;
            private set;
        }

        public event ConsoleArgsParsedDelegate ArgsParsed;
        public event ConsoleArgsParsedDelegate ArgsParsedError;

        public static event ExitDelegate Exiting;
        public static event ExitDelegate Exited;

        public override ServiceRegistry ServiceRegistry
        {
            get;
            protected set;
        }

        public IArgumentParser ArgumentParser
        {
            get
            {
                return ServiceRegistry.Get<IArgumentParser>();
            }
        }

        public IConfigurationProvider ConfigurationProvider
        {
            get
            {
                return ServiceRegistry.Get<IConfigurationProvider>();
            }
        }

        public IApplicationNameProvider ApplicationNameProvider
        {
            get
            {
                return ServiceRegistry.Get<IApplicationNameProvider>();
            }
        }

        public ILogger Logger
        {
            get
            {
                return ServiceRegistry.Get<ILogger>();
            }
        }

        public IMenuManager MenuManager
        {
            get
            {
                return ServiceRegistry.Get<IMenuManager>();
            }
        }

        public void AddValidArgument(string name, string? description = null)
        {
            AddValidArgument(name, false, description: description);
        }

        public void AddValidArgument(string name, bool allowNull, bool addAcronym = false, string? description = null, string? valueExample = null)
        {
            ValidArgumentInfo.Add(new ArgumentInfo(name, allowNull, description, valueExample));
            if (addAcronym)
            {
                ValidArgumentInfo.Add(new ArgumentInfo(name.CaseAcronym().ToLowerInvariant(), allowNull, $"{description}; same as {name}", valueExample));
            }
        }

        protected IParsedArguments Arguments
        {
            get;
            set;
        }

        protected List<ArgumentInfo> ValidArgumentInfo
        {
            get;
            set;
        }

        protected void Usage(Assembly assembly)
        {
            string assemblyVersion = assembly.GetName().Version.ToString();
            string fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
            string usageFormat = @"Assembly Version: {0}
File Version: {1}

{2} [arguments]";
            FileInfo info = new FileInfo(assembly.Location);
            Message.PrintLine(usageFormat, assemblyVersion, fileVersion, info.Name);
            Thread.Sleep(3);
            foreach (ArgumentInfo argInfo in ValidArgumentInfo)
            {
                string valueExample = string.IsNullOrEmpty(argInfo.ValueExample) ? string.Empty : string.Format(":{0}\r\n", argInfo.ValueExample);
                Message.PrintLine("/{0}{1}\r\n    {2}", argInfo.Name, valueExample, argInfo.Description);
            }
            Thread.Sleep(30);
        }

        public static void Version(Assembly assembly)
        {
            FileVersionInfo fv = FileVersionInfo.GetVersionInfo(assembly.Location);
            AssemblyCommitAttribute commitAttribute = assembly.GetCustomAttribute<AssemblyCommitAttribute>();
            StringBuilder versionInfo = new StringBuilder();
            versionInfo.AppendFormat("AssemblyVersion: {0}\r\n", assembly.GetName().Version.ToString());
            versionInfo.AppendFormat("AssemblyFileVersion: {0}\r\n", fv.FileVersion.ToString());
            if (commitAttribute != null)
            {
                versionInfo.AppendFormat("Commit: {0}\r\n", commitAttribute.Commit);
            }
            else
            {
                versionInfo.AppendFormat("Commit: AssemblyCommitAttribute not found on specified assembly: {0}\r\n",
                    assembly.Location);
            }

            Message.PrintLine(versionInfo.ToString(), ConsoleColor.Cyan);
        }

        public override ServiceRegistry GetDefaultContextServiceRegistry()
        {
            ServiceRegistry serviceRegistry = new ServiceRegistry()
                .For<IBamContext>().Use(this)
                .For<IArgumentParser>().Use<DefaultArgumentParser>()
                .For<IConfigurationProvider>().Use(new DefaultConfigurationProvider())
                .For<IApplicationNameProvider>().Use(new ProcessApplicationNameProvider())
                .For<ILogger>().Use(new ConsoleLogger())
                .For<IMenuHeaderRenderer>().Use<ConsoleMenuHeaderRenderer>()
                .For<IMenuFooterRenderer>().Use<ConsoleMenuFooterRenderer>()
                .For<IMenuInputCommandRenderer>().Use<ConsoleMenuInputCommandRenderer>()
                .For<IMenuProvider>().Use<ConsoleMenuProvider>()
                .For<IMenuInputReader>().Use<ConsoleMenuInputReader>()
                .For<IMenuItemProvider>().Use<MenuItemProvider>()
                .For<IMenuRenderer>().Use<ConsoleMenuRenderer>()
                .For<IMenuInputMethodArgumentProvider>().Use<MenuInputMethodArgumentProvider>()
                .For<ITypedArgumentProvider>().Use<StringArgumentProvider>()
                .For<IMenuItemRunResultRenderer>().Use<ConsoleMenuItemRunResultRenderer>()
                .For<IInputCommandResultRenderer>().Use<ConsoleInputCommandResultRenderer>()
                .For<IMenuItemSelector>().Use<MenuItemSelector>()
                .For<IMenuItemRunner>().Use<ConsoleMenuItemRunner>()
                .For<ISuccessReporter>().Use<ConsoleSuccessReporter>()
                .For<IExceptionReporter>().Use<ConsoleExceptionReporter>();            
           
            serviceRegistry.For<ConsoleMenuHeaderRenderer>().Use(serviceRegistry.Get<IMenuHeaderRenderer>())
                .For<ConsoleMenuFooterRenderer>().Use(serviceRegistry.Get<IMenuFooterRenderer>())
                .For<ConsoleMenuInputReader>().Use(serviceRegistry.Get<IMenuInputReader>());

            serviceRegistry
                .For<Services.IDependencyProvider>().Use(serviceRegistry)
                .For<ServiceRegistry>().Use(serviceRegistry)
                .For<IMenuInputCommandInterpreter>().Use<DefaultMenuInputCommandInterpreter>()
                .For<IMenuManager>().UseSingleton<MenuManager>();


            serviceRegistry
                .For<IExceptionReporter>().Use<ConsoleExceptionReporter>();

            return serviceRegistry;
        }

        protected void AddSwitches()
        {
            Assembly? entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                foreach (Type type in entryAssembly.GetTypes())
                {
                    AddSwitches(type);
                }
            }
        }

        /// <summary>
        /// Reads the methods of the specified type and adds those addorned with <see cref="ConsoleCommandAttribute" /> as valid command line switches.
        /// </summary>
        /// <param name="type"></param>
        protected void AddSwitches(Type type)
        {
            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.HasCustomAttributeOfType(out ConsoleCommandAttribute attribute))
                {
                    if (!string.IsNullOrEmpty(attribute.OptionName))
                    {
                        AddValidArgument(attribute.OptionName, true, true, attribute.Description, attribute.ValueExample);
                    }

                    AddValidArgument(method.Name.CamelCase(), true, true, attribute.Description, attribute.ValueExample);
                }
            }
        }

        /// <summary>
        /// Reads the configuration settings and adds them as valid command line switches.
        /// </summary>
        protected void AddConfigurationSwitches()
        {
            Dictionary<string, string> configuration = ConfigurationProvider.GetApplicationConfiguration(ApplicationNameProvider.GetApplicationName());

            configuration.Keys.Each(key => AddValidArgument(key, $"Override value from config: {configuration[key]}"));
        }

        protected void ParseArgs(string[] args)
        {
            Arguments = this.ArgumentParser.ParseArguments(args);
            if (Arguments.Status == ArgumentParseStatus.Error || Arguments.Status == ArgumentParseStatus.Invalid)
            {
                ArgsParsedError?.Invoke(Arguments);
            }
            else if (Arguments.Status == ArgumentParseStatus.Success)
            {
                ArgsParsed?.Invoke(Arguments);
            }
        }

        protected static bool ExecuteSwitches(ILogger logger, IParsedArguments arguments)
        {
            Assembly? entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null)
            {
                logger.Info("Entry assembly is null for ({0})", typeof(BamConsoleContext).Name);
                return false;
            }
            bool executed = false;
            foreach (Type type in entryAssembly.GetTypes())
            {
                foreach (string key in arguments.Keys)
                {
                    ConsoleMethod? methodToInvoke = GetConsoleMethod(arguments, type, key);
                    if (methodToInvoke != null)
                    {
                        CheckDebug(arguments);
                        methodToInvoke.TryInvoke((ex) => logger.Error("Exception executing switch ({0})", ex, key));
                        logger.Info("Executed {0}: {1}", key, methodToInvoke.Information);
                        executed = true;
                    }
                }
            }
            return executed;
        }

        private static ConsoleMethod? GetConsoleMethod(IParsedArguments arguments, Type type, string key, object instance = null)
        {
            string commandLineSwitch = key;
            string switchValue = arguments[key];
            MethodInfo[] methods = type.GetMethods();
            List<ConsoleMethod> toExecute = new List<ConsoleMethod>();
            foreach (MethodInfo method in methods)
            {
                if (method.HasCustomAttributeOfType(out ConsoleCommandAttribute consoleAction))
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
                    if (
                        consoleAction.OptionName.Or("").Equals(commandLineSwitch) ||
                        consoleAction.OptionName.CaseAcronym().ToLowerInvariant().Or("").Equals(commandLineSwitch) ||
                        method.Name.CamelCase().Equals(commandLineSwitch) ||
                        method.Name.CamelCase().CaseAcronym().ToLowerInvariant().Equals(commandLineSwitch)
                        )
                    {
                        toExecute.Add(new ConsoleMethod(method, consoleAction, instance, switchValue));
                    }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }

    (toExecute.Count > 1).IsFalse("Multiple ConsoleActions found with the specified command line switch: {0}".Format(commandLineSwitch));

            if (toExecute.Count == 0)
            {
                return null;
            }

            return toExecute[0];
        }

        private static void CheckDebug(IParsedArguments arguments)
        {
            if (arguments.Contains("debug"))
            {
                System.Console.WriteLine($"Attach Debugger: ProcessId={Process.GetCurrentProcess().Id}");
                System.Console.ReadLine();
            }
        }
    }
}
