using System.IO.Abstractions.TestingHelpers;
using System.Runtime.InteropServices;
using Xunit;

namespace HelloWorldLinux.Tests;

public class ProgramTests
{
    [Fact]
    public void Main_ReturnsZero()
    {
        // Arrange
        string[] args = Array.Empty<string>();

        // Act
        int result = Program.Main(args);

        // Assert
        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData(OSPlatform.Linux, true)]
    [InlineData(OSPlatform.Windows, false)]
    [InlineData(OSPlatform.OSX, false)]
    public void IsLinux_ReturnsExpectedValue(OSPlatform platform, bool expected)
    {
        Assert.Equal(
            expected,
            RuntimeInformation.IsOSPlatform(platform)
        );
    }

    [Fact]
    public void OSDescription_IsNotEmpty()
    {
        Assert.NotEmpty(RuntimeInformation.OSDescription);
    }

    [Fact]
    public void EnvironmentVariables_ReturnsExpectedValues()
    {
        // Arrange
        var testVars = new[] { "HOME", "USER", "SHELL", "PATH" };

        // Act & Assert
        foreach (var key in testVars)
        {
            var value = Environment.GetEnvironmentVariable(key);
            // On Linux, these should typically be set
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Assert.NotNull(value);
                Assert.NotEmpty(value);
            }
        }
    }

    [Fact]
    public void TempDirectory_CanList()
    {
        // Arrange
        var mockFileSystem = new MockFileSystem();
        var tempPath = "/tmp";
        var testFiles = new[]
        {
            "/tmp/test1.txt",
            "/tmp/test2.txt",
            "/tmp/testdir/"
        };

        foreach (var file in testFiles)
        {
            if (file.EndsWith("/"))
                mockFileSystem.Directory.CreateDirectory(file);
            else
                mockFileSystem.File.WriteAllText(file, "test content");
        }

        // Act
        var entries = mockFileSystem.Directory.EnumerateFileSystemEntries(tempPath).ToList();

        // Assert
        Assert.Equal(testFiles.Length, entries.Count);
        foreach (var testFile in testFiles)
        {
            Assert.Contains(
                entries,
                e => e.TrimEnd('/') == testFile.TrimEnd('/')
            );
        }
    }

    [Fact]
    public void OsRelease_CanRead()
    {
        // Arrange
        var mockFileSystem = new MockFileSystem();
        var osReleasePath = "/etc/os-release";
        var mockContent = @"NAME=""Ubuntu""
VERSION=""22.04.3 LTS (Jammy Jellyfish)""
ID=ubuntu
ID_LIKE=debian
PRETTY_NAME=""Ubuntu 22.04.3 LTS""
VERSION_ID=""22.04""
HOME_URL=""https://www.ubuntu.com/""
SUPPORT_URL=""https://help.ubuntu.com/""
BUG_REPORT_URL=""https://bugs.launchpad.net/ubuntu/""
PRIVACY_POLICY_URL=""https://www.ubuntu.com/legal/terms-and-policies/privacy-policy""
VERSION_CODENAME=jammy
UBUNTU_CODENAME=jammy";

        mockFileSystem.File.WriteAllText(osReleasePath, mockContent);

        // Act
        var exists = mockFileSystem.File.Exists(osReleasePath);
        var content = mockFileSystem.File.ReadAllText(osReleasePath);

        // Assert
        Assert.True(exists);
        Assert.Equal(mockContent, content);
        Assert.Contains("Ubuntu", content);
        Assert.Contains("22.04", content);
    }
}