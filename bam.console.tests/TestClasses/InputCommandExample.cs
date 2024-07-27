using Bam.Shell;

namespace Bam.Console.Tests.TestClasses;

[Menu<MenuItemAttribute>("Input Command Menu")]
public class InputCommandExample
{
    [MenuItem]
    public void MenuItem()
    {
        System.Console.WriteLine("Execute {0}", nameof(MenuItem));
    }
    
    [InputCommand("input command one", "Execute the input command")]
    public void InputCommandOne()
    {
        System.Console.WriteLine("Executed {0}", nameof(InputCommandOne));
        System.Console.WriteLine("Calling MenuItem method from input command...");
        MenuItem();
    }
}