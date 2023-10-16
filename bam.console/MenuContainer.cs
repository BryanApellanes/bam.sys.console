using Bam.Shell;
using Bam.Console;
using Bam.Services;
using System.Reflection;

namespace Bam.Testing
{
    public abstract class MenuContainer
    {
        public MenuContainer() { }

        public MenuContainer(IDependencyProvider dependencyProvider)
        {
            this.DependencyProvider = dependencyProvider;
            this.MethodArgumentProvider = new DependencyProviderMethodArgumentProvider(dependencyProvider);
        }

        protected IDependencyProvider DependencyProvider
        {
            get;
            private set;
        }

        protected IMethodArgumentProvider MethodArgumentProvider
        {
            get;
            set;
        }

        protected IExceptionReporter ExceptionReporter
        {
            get
            {
                return DependencyProvider.Get<IExceptionReporter>();
            }
        }

        protected ISuccessReporter SuccessReporter
        {
            get
            {
                return DependencyProvider.Get<ISuccessReporter>();
            }
        }

        [InputCommand("all")]
        public void RunAllItems(IMenuManager menuManager)
        {
            if (menuManager == null)
            {
                throw new InvalidOperationException($"{nameof(menuManager)} not specified.");
            }

            if (menuManager.CurrentMenu == null)
            {
                throw new InvalidOperationException($"Current menu is not set.");
            }

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
                    TryInvoke(item.DisplayName, item.MethodInfo, instance);
                    SuccessReporter.ReportSuccess($"{item.DisplayName} completed successfully.");
                }
            }
        }
        private void TryInvoke(string itemDisplayName, MethodInfo method, object? instance)
        {
            try
            {
                method.Invoke(instance, MethodArgumentProvider.GetMethodArguments(method));
            }
            catch (Exception ex)
            {
                this.ExceptionReporter.ReportException(ex);
            }
        }        
    }
}
