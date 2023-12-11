using Bam.Shell;
using Bam.Services;
using System.Reflection;
using static Bam.Net.Analytics.Diff;
using Bam.Net;

namespace Bam.Console
{
    public abstract class MenuContainer
    {
        public MenuContainer() { }

        /// <summary>
        /// Create a new MenuContainer using the specified dependency provider.
        /// </summary>
        /// <param name="dependencyProvider"></param>
        public MenuContainer(IDependencyProvider dependencyProvider)
        // Note that this uses service locator specifically to empower a test writer
        // to manipulate the state of the test container.
        {
            this.SetDependencyProvider(dependencyProvider);
        }

        protected void SetDependencyProvider(IDependencyProvider dependencyProvider)
        {
            DependencyProvider = dependencyProvider;
            MethodArgumentProvider = new DependencyProviderMethodArgumentProvider(dependencyProvider);
        }

        protected IDependencyProvider? DependencyProvider
        {
            get;
            private set;
        }

        protected IMethodArgumentProvider? MethodArgumentProvider
        {
            get;
            private set;
        }

        protected IExceptionReporter? ExceptionReporter
        {
            get
            {
                return DependencyProvider?.Get<IExceptionReporter>();
            }
        }

        protected ISuccessReporter? SuccessReporter
        {
            get
            {
                return DependencyProvider?.Get<ISuccessReporter>();
            }
        }

        /// <summary>
        /// Get a configured instance of the specified generic type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return DependencyProvider.Get<T>();
        }

        [InputCommand("all", "run all listed commands")]
        public InputCommandResults RunAllItems(IMenuManager menuManager)
        {
            if (menuManager == null)
            {
                throw new InvalidOperationException($"{nameof(menuManager)} not specified.");
            }

            if (menuManager.CurrentMenu == null)
            {
                throw new InvalidOperationException($"Current menu is not set.");
            }
            InputCommandResults results = new InputCommandResults();
            foreach (IMenuItem item in menuManager.CurrentMenu.Items)
            {
                if (item != null && item.MethodInfo != null)
                {
                    object? instance = null;
                    if (!item.MethodInfo.IsStatic && item.MethodInfo.DeclaringType != null)
                    {
                        instance = DependencyProvider.Get(item.MethodInfo.DeclaringType);
                    }
                    if (instance == null && item.Instance != null)
                    {
                        instance = item.Instance;
                    }

                    results.AddResult(TryInvoke(item.DisplayName, item.MethodInfo, instance));
                }
            }
            return results;
        }
        private InputCommandResult TryInvoke(string itemDisplayName, MethodInfo method, object? instance)
        {
            try
            {
                object? result = method.Invoke(instance, MethodArgumentProvider.GetMethodArguments(method));
                SuccessReporter.ReportSuccess($"{itemDisplayName} completed successfully.");
                return new InputCommandResult()
                {
                    InputName = itemDisplayName,
                    InvocationResult = result
                };
            }
            catch (Exception ex)
            {
                Exception e = ex.GetInnerException();
                ExceptionReporter.ReportException($"{itemDisplayName} failed.", e);
                return new InputCommandResult()
                { 
                    InputName = itemDisplayName, 
                    Exception = e 
                };
            }
        }
    }
}
