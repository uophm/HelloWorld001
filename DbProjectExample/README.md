# DbProjectExample

DbProjectExample is a .NET 8 Web API project that demonstrates integration with SQL Server using Entity Framework Core. It provides a RESTful API for managing products and includes automated integration tests to ensure reliability.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Local instance)
- [Docker](https://www.docker.com/products/docker-desktop) (Optional, for containerized execution)

## Database Configuration

The project is configured to use a SQL Server database. The connection string is located in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GroceryDb;User Id=SA;Password=Burlington123!;TrustServerCertificate=True;"
}
```

The application automatically creates the database and seeds initial data on startup using `db.Database.EnsureCreated()`.

## Commands to Run

To build and run the project, use the following commands from the root directory:

### Build the project
```bash
dotnet build
```

### Run the API
```bash
dotnet run --project DbProjectExample
```

## Running with Docker

You can run the entire environment (API and SQL Server) using Docker Compose from the root directory.

### Start the application
```bash
docker-compose up --build
```

The API will be available at [http://localhost:5062/swagger](http://localhost:5062/swagger).

## Accessing Swagger

When the application is running in the `Development` environment, you can access the Swagger UI to explore and test the API endpoints.

- **HTTPS**: [https://localhost:7085/swagger](https://localhost:7085/swagger)
- **HTTP**: [http://localhost:5062/swagger](http://localhost:5062/swagger)

## Executing Tests

The solution includes a test project `DbProjectExample.Tests` using xUnit and `Microsoft.AspNetCore.Mvc.Testing` for integration testing.

To run the tests:

```bash
dotnet test
```
