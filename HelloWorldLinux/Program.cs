using System;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

class Program
{
    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
    static int Main(string[] args)
    {
        Console.WriteLine("Hello from HelloWorldLinux!");
        Console.WriteLine($"OS: {RuntimeInformation.OSDescription}");
        Console.WriteLine($"Is Linux: {RuntimeInformation.IsOSPlatform(OSPlatform.Linux)}");

        var osRelease = "/etc/os-release";
        if (File.Exists(osRelease))
        {
            Console.WriteLine($"\nContents of {osRelease}:");
            try
            {
                Console.WriteLine(File.ReadAllText(osRelease));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Could not read {osRelease}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"\n{osRelease} not found.");
        }

        var dir = "/tmp";
        Console.WriteLine($"\nFiles in {dir}:");
        try
        {
            foreach (var entry in Directory.EnumerateFileSystemEntries(dir))
            {
                Console.WriteLine("  " + Path.GetFileName(entry));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Could not list {dir}: {ex.Message}");
        }

        Console.WriteLine("\nSample environment variables:");
        foreach (var key in new[] { "HOME", "USER", "SHELL", "PATH" })
        {
            Console.WriteLine($"{key} = {Environment.GetEnvironmentVariable(key)}");
        }

        return 0;
    }
}
