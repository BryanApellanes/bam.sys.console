# Bam.Console

Simple menuing system for dotnet console apps.

## Entry Point
To begin using the `Bam.Console` menuing system define your main entry point like the following.

```csharp
   class Program
    {
        static void Main(string[] args)
        {
            BamConsoleContext.StaticMain(args);
        }
    }
```

## Menu (Attribute)
Adorn a class with the `[Menu]` attribute and specify the attribute type that adorns the methods you wish to show in that menu.  In the following example the methods adorned with the `MenuItem` attribute are rendered items in the menu.

```csharp
[Menu<MenuItemAttribute>]
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
```

The preceding class definition renders the following menu:

```
MenuExample

Select an option below:
> 1. [:titfmi] ThisIsTheFirstMenuItem
  2. [:titsmi] ThisIsTheSecondMenuItem
  3. [:tmihan] Menu item with a name
-----------------------------------
[:me] MenuExample
-----------------------------------
 me > 
```

You may use the up and down arrow keys to navigate the menu items.  You may also type one of the selectors shown in square brackets prefixed by a colon `:` to jump directly to the item of your choice.  Press `Enter` to execute the currently selected item.

> Note: The selectors are a camel cased acronym of the method name, adjust your method names accordingly to get the selector you desire.

## Named Menu
You may specify an optional name for your menu by providing a name to the `Menu` attribute.

```csharp
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
```

The preceding class definition renders the following menu.

```
A named Menu

Select an option below:
> 1. [:titfmi] ThisIsTheFirstMenuItem
  2. [:titsmi] ThisIsTheSecondMenuItem
  3. [:tmihan] Menu item with a name
-----------------------------------
[:anm] A named Menu
-----------------------------------
 anm >
```

## InputCommand (Attribute)

Adorn a method with the `InputCommand` attribute and specify the name of the command you'd like to use to execute the command from the menu prompt.

```csharp
[Menu<MenuItemAttribute>("Input Command Menu")]
public class InputCommandExample
{
    [MenuItem]
    public void MenuItem()
    {
        System.Console.WriteLine("Execute {0}", nameof(MenuItem));
    }
    
    [InputCommand("input command one")]
    public void InputCommandOne()
    {
        System.Console.WriteLine("Executed {0}", nameof(InputCommandOne));
        System.Console.WriteLine("Calling MenuItem method from input command...");
        MenuItem();
    }
}
```

The preceding example defines the `InputCommandOne` method which may be executed from the menu prompt by entering `input command one`.

> Note: Methods adorned with the `InputCommand` attribute are not displayed in the menu unless also adorned with the attribute specified in the `Menu` attribute which adorns the class.

> Note: For a method adorned with the `InputCommand` attribute to be accessable, there must be at least one valid menu item in the menu, whether it is the `InputCommand` itself or another method addorned with a `MenuItem` attribute.

The preceding class definition renders the following menu.

```
Input Command Menu

Select an option below:
> 1. [:mi] MenuItem
-----------------------------------
[:icm] Input Command Menu
-----------------------------------
"input command one" -- Execute the input command
-----------------------------------
 icm > 
```

Typing `input command one` at the menu prompt results in the following output.

```
Input Command Menu

Select an option below:
> 1. [:mi] MenuItem
-----------------------------------
[:icm] Input Command Menu
-----------------------------------
"input command one" -- Execute the input command
-----------------------------------
 icm > input command one

******************************

Executed InputCommandOne
Calling MenuItem method from input command...
Execute MenuItem

 > command 'input command one' -->  succeeded

******************************
```

## ConsoleCommand (Attribute)
Adorn methods with the `ConsoleCommand` attribute that you want to be able to execute from the command line.


```csharp
[ConsoleCommand]
public void MethodName(string value, string value2)
{
    Console.WriteLine("You entered {0} for value and {1} for value 2", value, value2);
}
```

You can execute the preceding method from the command line using the following command.

```bash
{exeName} --methodName --value=value1 "--value2=another value"
```

The preceding command results in the following output.

```
You entered value1 for value and another value for value 2
[Time(7/27/2024 4:31:38 PM ms 410)]Thread=#1(1)~~App=bam.console.tests~~PID=131464~~Utc=7/27/2024::9:31 PM~~Executed methodName: Method Name
```