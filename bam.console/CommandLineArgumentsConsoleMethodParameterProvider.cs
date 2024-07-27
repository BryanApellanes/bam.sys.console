using System.Reflection;

namespace Bam.Console;

public class CommandLineArgumentsConsoleMethodParameterProvider : IConsoleMethodParameterProvider
{
    
    public object[]? GetMethodParameters(MethodInfo methodInfo)
    {
        List<object> results = new List<object>();
        foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
        {
            if (parameterInfo.Name != null && BamConsoleContext.Current.Arguments.Contains(parameterInfo.Name))
            {
                results.Add(BamConsoleContext.Current.Arguments[parameterInfo.Name]);
            }
        }

        return results.ToArray();
    }
}