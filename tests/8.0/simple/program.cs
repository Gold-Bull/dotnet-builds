using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

try
{
    string? framework = Assembly
        .GetEntryAssembly()?
        .GetCustomAttribute<TargetFrameworkAttribute>()?
        .FrameworkName;

    Console.WriteLine($"OS Description\t\t\t:\t{RuntimeInformation.OSDescription}");
    Console.WriteLine($"OS Architecture\t\t\t:\t{RuntimeInformation.OSArchitecture}");
    Console.WriteLine($"Process Architecture\t\t:\t{RuntimeInformation.ProcessArchitecture}");
    Console.WriteLine($".NET Framework Name\t\t:\t{framework}");
    Console.WriteLine($".NET Version\t\t\t:\t{RuntimeInformation.FrameworkDescription}");
    Console.WriteLine($".NET Runtime Identifier\t\t:\t{RuntimeInformation.RuntimeIdentifier}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex}");
}
