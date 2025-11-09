# Linux-friendly ASP.NET Core Web API

A minimal web API that runs well on Linux, demonstrating:
- Linux-specific Kestrel configuration (IP and Unix socket endpoints)
- System information endpoints
- Health check endpoint
- Swagger/OpenAPI documentation
- Error handling
- Configuration for both development and production

## Requirements
- .NET 8.0 SDK
- Linux environment (works on Windows too, but Unix socket feature only on Linux)

## Quick Start

```bash
cd HelloWorldWeb
dotnet restore
dotnet run
```

The API will be available at:
- http://localhost:5000 (all interfaces)
- Unix socket: /tmp/helloworldweb.sock (Linux only)

## API Endpoints

- `GET /` - Basic hello message with runtime info
- `GET /health` - Health check endpoint
- `GET /env` - Environment information
- `GET /fs` - File system information
- `/swagger` - Interactive API documentation (development only)

## Running as a Service

To run as a systemd service on Linux:

1. Create a service file:
```bash
sudo nano /etc/systemd/system/helloworldweb.service
```

2. Add this content (adjust paths):
```ini
[Unit]
Description=HelloWorld Web API
After=network.target

[Service]
WorkingDirectory=/path/to/HelloWorldWeb
ExecStart=/usr/bin/dotnet run --no-launch-profile
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=helloworldweb
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

3. Enable and start:
```bash
sudo systemctl enable helloworldweb
sudo systemctl start helloworldweb
sudo systemctl status helloworldweb
```

## Development

VS Code launch configuration is provided for debugging. Press F5 to start with debugger.

Configuration files:
- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development overrides (more verbose logging)

## Docker Support

Build and run with Docker:

```bash
docker build -t helloworldweb .
docker run -p 5000:5000 helloworldweb
```