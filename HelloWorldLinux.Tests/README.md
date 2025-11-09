# Test Project for HelloWorldLinux

This test project uses:
- xUnit as the test framework
- System.IO.Abstractions for file system mocking
- Moq for general mocking (when needed)

## Running the Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test(s)
dotnet test --filter "FullyQualifiedName~ProgramTests.Main_ReturnsZero"
```

## Test Categories

1. Platform Detection
   - `IsLinux_ReturnsExpectedValue` - Tests OS platform detection
   - `OSDescription_IsNotEmpty` - Validates OS description

2. Environment Variables
   - `EnvironmentVariables_ReturnsExpectedValues` - Checks critical env vars

3. File System Operations
   - `TempDirectory_CanList` - Tests /tmp directory listing
   - `OsRelease_CanRead` - Tests /etc/os-release reading

4. Program Flow
   - `Main_ReturnsZero` - Validates program exit code

## Notes

- File system operations use System.IO.Abstractions to avoid touching real files
- Environment-specific tests are guarded by platform checks
- Mock data (like os-release content) represents typical Linux values