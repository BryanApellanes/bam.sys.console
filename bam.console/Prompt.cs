using System.Text;

namespace Bam.Console;

public class Prompt
{
    /// <summary>
    /// Prompt for a selection from the specified list of values
    /// </summary>
    /// <param name="options"></param>
    /// <param name="prompt"></param>
    /// <param name="color"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T SelectFrom<T>(IEnumerable<T> options, string prompt = "Select an option from the list", ConsoleColor color = ConsoleColor.DarkCyan)
    {
        return SelectFrom(options, (t) => t.ToString(), prompt, color);
    }

    /// <summary>
    /// Prompt for a selection from the specified list of values, using the specified optionTextSelector to extract option text from the options.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="optionTextSelector"></param>
    /// <param name="prompt"></param>
    /// <param name="color"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T SelectFrom<T>(IEnumerable<T> options, Func<T, string> optionTextSelector, string prompt = "Select an option from the list", ConsoleColor color = ConsoleColor.DarkCyan)
    {
        T[] optionsArray = options.ToArray();
        string[] optionStrings = options.Select(optionTextSelector).ToArray();
        return optionsArray[SelectFrom(optionStrings, prompt, color)];
    }

    public static int SelectFrom(string[] options, string prompt = "Select an option from the list", ConsoleColor color = ConsoleColor.Cyan)
    {
        StringBuilder list = new StringBuilder();
        for (int i = 0; i < options.Length; i++)
        {
            list.AppendFormat("{0}. {1}\r\n", (i + 1).ToString(), options[i]);
        }
        list.AppendLine();
        list.Append(prompt);
        int value = ForNumber(list.ToString(), color) - 1;
        Args.ThrowIf(value < 0, "Invalid selection");
        Args.ThrowIf(value > options.Length - 1, "Invalid selection");
        return value;
    }
    
    /// <summary>
    /// Prompts the user for [y]es or [n]o returning true for [y] and false for [n].
    /// </summary>
    /// <param name="message">Optional message for the user.</param>
    /// <param name="allowQuit">If true provides an additional [q]uit option which if selected
    /// will call Environment.Exit(0).</param>
    /// <returns>boolean</returns>
    public static bool Confirm(string message, ConsoleColor color, bool allowQuit)
    {
        Message.Print(message, color);
        if (allowQuit)
        {
            Message.PrintLine(" [q] ");
        }
        else
        {
            Message.PrintLine();
        }

        string answer = System.Console.ReadLine().Trim().ToLower();
        if (answer.IsAffirmative())
        {
            return true;
        }

        if (answer.IsNegative())
        {
            return false;
        }

        if (allowQuit && answer.IsExitRequest())
        {
            Environment.Exit(0);
        }

        return false;
    }

    public static int ForNumber(string message, ConsoleColor color = ConsoleColor.Cyan)
    {
        return ForInt(message, color);
    }

    public static long ForLong(string message, ConsoleColor color = ConsoleColor.Cyan)
    {
        string value = Show(message, color);
        long result = -1;
        long.TryParse(value, out result);
        return result;
    }

    public static int ForInt(string message, ConsoleColor color = ConsoleColor.Cyan)
    {
        string value = Show(message, color);
        int result = -1;
        int.TryParse(value, out result);
        return result;
    }

        /// <summary>
    /// Prompts the user for input.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>string</returns>
    public static string Show(string message, ConsoleColor textColor = ConsoleColor.Cyan)
    {
        return Show(message, textColor, false);
    }

    public static string Show(string message, ConsoleColor textColor, bool allowQuit)
    {
        return Show(message, ">>", textColor, allowQuit);
    }

    public static string Show(string message, string promptTxt, ConsoleColor textColor)
    {
        return Show(message, promptTxt, textColor, false);
    }

    public static string Show(string message, string promptTxt, ConsoleColor textColor, bool allowQuit)
    {
        return Show(message, promptTxt, new ConsoleColorCombo(textColor), allowQuit);
    }

    public static string Show(string message, string promptTxt, ConsoleColor textColor, ConsoleColor backgroundColor)
    {
        return Show(message, promptTxt, new ConsoleColorCombo(textColor, backgroundColor), false);
    }

    public static string Show(string message, string promptTxt, ConsoleColor textColor, ConsoleColor backgroundColor, bool allowQuit)
    {
        return Show(message, promptTxt, new ConsoleColorCombo(textColor, backgroundColor), allowQuit);
    }

    public static string Show(string message, string promptTxt, ConsoleColorCombo colors, bool allowQuit)
    {
        return Provider(message, promptTxt, colors, allowQuit);
    }

    
    static Func<string, string, ConsoleColorCombo, bool, string> _provider;
    public static Func<string, string, ConsoleColorCombo, bool, string> Provider
    {
        get
        {
            return _provider ?? (_provider = (message, promptTxt, colors, allowQuit) =>
            {
                Message.Print($"{message} {promptTxt} ", colors);
                Thread.Sleep(200);
                string answer = System.Console.ReadLine();

                if (allowQuit && answer.ToLowerInvariant().Equals("q"))
                {
                    Environment.Exit(0);
                }

                return answer.Trim();
            });
        }
        set => _provider = value;
    }
}