using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

try
{
    string? framework = Assembly
        .GetEntryAssembly()?
        .GetCustomAttribute<TargetFrameworkAttribute>()?
        .FrameworkName;

    Console.WriteLine($"OS Description          :\t{RuntimeInformation.OSDescription}");
    Console.WriteLine($"OS Architecture         :\t{RuntimeInformation.OSArchitecture}");
    Console.WriteLine($"Process Architecture    :\t{RuntimeInformation.ProcessArchitecture}");
    Console.WriteLine($".NET Framework Name     :\t{framework}");
    Console.WriteLine($".NET Version            :\t{RuntimeInformation.FrameworkDescription}");
    Console.WriteLine($".NET Runtime Identifier :\t{RuntimeInformation.RuntimeIdentifier}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex}");
}
