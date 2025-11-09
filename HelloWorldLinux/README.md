HelloWorldLinux
================

A tiny .NET 8 console application example intended to run on Linux. It demonstrates:

- Detecting the OS via RuntimeInformation
- Reading `/etc/os-release` when present
- Listing files in `/tmp`
- Printing a few environment variables

Build & run (requires .NET 8 SDK):

```bash
cd HelloWorldLinux
dotnet restore
dotnet build
dotnet run
```

Publish a self-contained Linux binary (example for x64):

```bash
dotnet publish -c Release -r linux-x64 --self-contained true -o publish
# then run: ./publish/HelloWorldLinux
```

Notes:
- This project targets `net8.0` and uses modern SDK-style project format.
- If you don't have the .NET SDK installed on this machine, run the above commands on a Linux machine with the SDK.
