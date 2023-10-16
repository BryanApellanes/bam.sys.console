using Bam.Net;
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
    public class InputCommandInterpreter : IMenuInputCommandInterpreter
    {
        private Dictionary<Type, InputCommands> _inputOptions = new Dictionary<Type, InputCommands>();

        public InputCommandInterpreter(IDependencyProvider dependencyProvider)
        {
            this.DependencyProvider = dependencyProvider;
        }

        public bool InterpretInput(IMenuManager menuManager, IMenuInput menuInput, out IInputCommandResults result)
        {
            InputCommandResults returnValue = new InputCommandResults();

            result = returnValue;

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
                arguments = new string[commands.Length - 1];
                commands.CopyTo(arguments, 1);
            }

            MenuSpec menuSpec = menuManager.CurrentMenu.GetSpec();
            InputCommands inputOptions = GetInputOptions(menuSpec.ContainerType);
            if (inputOptions.Commands.ContainsKey(optionName))
            {
                InputCommandResult optionResult = InvokeOption(inputOptions.Commands[optionName], arguments);
                returnValue.AddResult(optionResult);
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
                    Exception = ex
                };
            }
        }
    }
}
