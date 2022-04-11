using System.Diagnostics;

Console.WriteLine("Nuget package folders:");
Process cmd = new Process();
cmd.StartInfo.FileName = "nuget.exe";
cmd.StartInfo.Arguments = "locals all -list";
cmd.StartInfo.RedirectStandardInput = true;
cmd.StartInfo.RedirectStandardOutput = true;
cmd.StartInfo.CreateNoWindow = true;
cmd.StartInfo.UseShellExecute = false;
cmd.Start();
Console.WriteLine(cmd.StandardOutput.ReadToEnd());

Console.WriteLine("Provide nuget package folder path");
string? packageLocation = Console.ReadLine();

if (string.IsNullOrEmpty(packageLocation))
{
    Console.WriteLine("No path provided.");
    return;
}
else if (!Directory.Exists(packageLocation))
{
    Console.WriteLine("Path does not exist.");
    return;
}

var packages = Directory.GetDirectories(packageLocation);
foreach (var package in packages)
{
    var versions = Directory.GetDirectories(package);

    if (versions.Length <= 1)
        continue;

    Console.WriteLine($"{package} has multiple versions.");
    Console.WriteLine($"Name: {package}, Count: {versions.Length}");

    for (var i = 0; i < versions.Length - 1; i++)
    {
        Console.WriteLine($"Deleting version: {versions[i]}");
        Directory.Delete(versions[i], true);
    }
}