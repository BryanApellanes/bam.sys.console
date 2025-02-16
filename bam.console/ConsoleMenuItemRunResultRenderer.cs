using Bam.Shell;

namespace Bam.Console
{
    public class ConsoleMenuItemRunResultRenderer : IMenuItemRunResultRenderer
    {
        public Action<IMenuItemRunResult> ItemRunSucceeded { get; set; } = (menuItemRunResult) => Message.PrintLine("{0} succeeded", ConsoleColor.Green, menuItemRunResult?.MenuItem?.DisplayName ?? "run");

        public Action<IMenuItemRunResult> ItemRunFailed { get; set; } = (menuItemRunResult) => Message.PrintLine("{0} failed", ConsoleColor.Magenta, menuItemRunResult?.MenuItem?.DisplayName ?? "run");

        public Action<IMenuItemRunResult> ItemRunException { get; set; } = (menuItemRunResult) => Message.PrintLine(menuItemRunResult?.Exception?.GetMessageAndStackTrace() ?? "Stacktrace unavailable", ConsoleColor.DarkMagenta);

        public void RenderMenuItemRunResult(IMenuItemRunResult menuItemRunResult)
        {
            if (menuItemRunResult != null)
            {
                if (menuItemRunResult.Success)
                {
                    ItemRunSucceeded(menuItemRunResult);
                }
                else
                {
                    if (menuItemRunResult.MenuItem != null && !string.IsNullOrEmpty(menuItemRunResult.MenuItem.DisplayName))
                    {
                        ItemRunFailed(menuItemRunResult);
                    }

                    if (menuItemRunResult.Exception != null && !string.IsNullOrEmpty(menuItemRunResult.Exception.StackTrace))
                    {
                        ItemRunException(menuItemRunResult);
                    }
                }
            }
        }
    }
}
