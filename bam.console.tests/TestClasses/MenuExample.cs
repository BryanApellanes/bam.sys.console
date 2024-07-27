using Bam.Shell;
using System;

namespace Bam.Console.Tests.TestClasses;

[Menu<MenuItemAttribute>("A named Menu")]
public class MenuExample
{
    [MenuItem]
    public void ThisIsTheFirstMenuItem()
    {
        System.Console.WriteLine("Executed {0}", nameof(ThisIsTheFirstMenuItem));
    }
    
    [MenuItem]
    public void ThisIsTheSecondMenuItem()
    {
        System.Console.WriteLine("Executed {0}", nameof(ThisIsTheFirstMenuItem));
    }
    
    [MenuItem("Menu item with a name")]
    public void ThisMenuItemHasAName()
    {
        System.Console.WriteLine("Executed {0}", nameof(ThisIsTheFirstMenuItem));
    }
}