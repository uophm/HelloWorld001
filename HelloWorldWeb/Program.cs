using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

// Configure Kestrel for Linux
if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        // Listen on any IP address
        options.ListenAnyIP(5000);
        
        // Optionally listen on Unix socket
        options.ListenUnixSocket("/tmp/helloworldweb.sock");
    });
}

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");

// Basic routes
app.MapGet("/", () => Results.Ok(new { 
    message = "Hello from ASP.NET Core on Linux!",
    os = RuntimeInformation.OSDescription,
    framework = RuntimeInformation.FrameworkDescription,
    processorArchitecture = RuntimeInformation.ProcessArchitecture.ToString()
}));

app.MapGet("/env", () => Results.Ok(new {
    hostName = Environment.MachineName,
    currentDirectory = Environment.CurrentDirectory,
    osVersion = Environment.OSVersion,
    // Some properties are Windows-specific; guard them for cross-platform runs
    userDomainName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Environment.UserDomainName : null,
    userName = Environment.UserName,
    systemDirectory = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Environment.SystemDirectory : (Environment.GetEnvironmentVariable("HOME") ?? Environment.CurrentDirectory)
}));

// File system info (with error handling)
app.MapGet("/fs", () =>
{
    try
    {
        var tempPath = Path.GetTempPath();
        var drives = DriveInfo.GetDrives()
            .Select(d => new
            {
                name = d.Name,
                type = d.DriveType.ToString(),
                format = d.DriveFormat,
                isReady = d.IsReady,
                // DriveInfo.AvailableFreeSpace is the correct property for available space
                availableSpace = d.IsReady ? d.AvailableFreeSpace : 0L,
                totalSpace = d.IsReady ? d.TotalSize : 0L
            });

        return Results.Ok(new
        {
            tempPath,
            drives = drives.ToList()
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            title: "Error getting filesystem info",
            detail: ex.Message
        );
    }
});

app.Run();