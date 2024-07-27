using System.Reflection;

namespace Bam.Console;

public interface IConsoleMethodParameterProvider
{
    object[]? GetMethodParameters(MethodInfo methodInfo);
}