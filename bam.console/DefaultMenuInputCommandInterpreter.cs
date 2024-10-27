using Bam;
using Bam.Services;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class DefaultMenuInputCommandInterpreter : IMenuInputCommandInterpreter
    {
        private readonly Dictionary<Type, InputCommands> _inputOptions = new Dictionary<Type, InputCommands>();

        public DefaultMenuInputCommandInterpreter(IDependencyProvider dependencyProvider)
        {
            this.DependencyProvider = dependencyProvider;
        }

        public bool InterpretInput(IMenuManager menuManager, IMenuInput menuInput, out IInputCommandResults result)
        {
            InputCommandResults results = new InputCommandResults();

            result = results;

            if (menuManager == null)
            {
                return false;
            }

            if (menuManager.CurrentMenu == null)
            {
                return false;
            }

            string[] commands = menuInput.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (commands.Length == 0)
            {
                return false;
            }
            string optionName = commands[0];
            string[] arguments = new string[] { };
            if (commands.Length > 1)
            {
                List<string> argList = new List<string>();
                for (int i = 1; i < commands.Length; i++)
                {
                    argList.Add(commands[i]);
                }

                arguments = argList.ToArray();
            }

            MenuSpec menuSpec = menuManager.CurrentMenu.GetSpec();
            InputCommands inputOptions = GetInputOptions(menuSpec.ContainerType);
            if (inputOptions.Commands.TryGetValue(optionName, out var command))
            {
                InputCommandResult optionResult = InvokeOption(command, arguments);
                results.AddResult(optionResult);
                return true;
            }
            else if (inputOptions.Commands.TryGetValue(menuInput.Value, out var commandOption))
            {
                InputCommandResult optionResult = InvokeOption(commandOption, arguments);
                results.AddResult(optionResult);
                return true;
            }

            return false;
        }

        public IDependencyProvider DependencyProvider 
        {
            get; 
            private set; 
        }

        protected InputCommands GetInputOptions(Type containerType)
        {
            if (!_inputOptions.ContainsKey(containerType))
            {
                _inputOptions.Add(containerType, new InputCommands(containerType));
            }
            return _inputOptions[containerType];
        }

        protected virtual object?[] GetMethodArguments(MethodInfo methodInfo)
        {
            return new DependencyProviderMethodArgumentProvider(DependencyProvider).GetMethodArguments(methodInfo);
        }

        protected virtual InputCommandResult InvokeOption(InputCommand command, string[] inputStrings)
        {
            try
            {
                object? instance = null;
                if (!command.OptionMethod.IsStatic)
                {
                    instance = DependencyProvider.Get(command.ContainerType);
                }

                return new InputCommandResult
                {
                    InputName = command.Name,
                    InvocationResult = command.OptionMethod.Invoke(instance, GetMethodArguments(command.OptionMethod))
                };
            }
            catch (Exception ex)
            {
                return new InputCommandResult()
                {
                    InputName = command.Name,
                    Exception = ex.GetInnerException()
                };
            }
        }
    }
}
