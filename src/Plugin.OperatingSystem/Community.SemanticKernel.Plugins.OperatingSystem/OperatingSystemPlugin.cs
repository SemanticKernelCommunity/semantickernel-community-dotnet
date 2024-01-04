using System.Collections;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.SemanticKernel;

namespace Community.SemanticKernel.Plugins.OperatingSystem;

public sealed class OperatingSystemPlugin
{

    [KernelFunction, Description("Appends given values to environment variable name.")]
    public Task<string> AppendToEnvironmentVariableAsync(
            [Description("Environment variable name")] string name,
            [Description("Values to append")] string[] values,
            CancellationToken cancellationToken = default)
    {
        var value = Environment.GetEnvironmentVariable(name) ?? string.Empty;
        value = string.Join(Path.PathSeparator, value, values);
        Environment.SetEnvironmentVariable(name, value);

        return Task.FromResult(value);
    }

    [KernelFunction, Description("Returns the value of an environment variable with the given name.")]
    public Task<string> GetEnvironmentVariableAsync(
            [Description("Environment variable name")] string name,
            [Description("Default value")] string defaultValue = "",
            CancellationToken cancellationToken = default)
    {
        var value = Environment.GetEnvironmentVariable(name) ?? defaultValue;

        return Task.FromResult(value);
    }

    [KernelFunction, Description("Returns currently available environment variables")]
    public Task<IDictionary> GetEnvironmentVariablesAsync(
            CancellationToken cancellationToken = default)
    {
        var values = Environment.GetEnvironmentVariables();

        return Task.FromResult(values);
    }

    [KernelFunction, Description("Deletes the specified environment variable.")]
    public Task RemoveEnvironmentVariableAsync(
               [Description("Environment variable names")] string[] names,
               CancellationToken cancellationToken = default)
    {
        foreach (var name in names)
        {
            Environment.SetEnvironmentVariable(name, string.Empty);
        }

        return Task.CompletedTask;
    }

    [KernelFunction, Description("Copies the source directory into the destination.")]

    public async Task CopyDirectoryAsync(
            [Description("Source directory")] string sourceDirectory,
            [Description("Destination directory")] string destinationDirectory,
            [Description("Recursively copies subdirectories")] bool recursive = false,
            CancellationToken cancellationToken = default)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDirectory);

        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDirectory);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDirectory, file.Name);
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDirectory, subDir.Name);
                await CopyDirectoryAsync(subDir.FullName, newDestinationDir, true, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    [KernelFunction, Description("Copies the source file into the destination.")]
    public Task CopyFileAsync(
        [Description("Source file")] string sourcePath = "",
        [Description("Destination file")] string destinationPath = "",
        CancellationToken cancellationToken = default)
    {
        var file = new FileInfo(sourcePath);

        if (!file.Exists)
        {
            throw new FileNotFoundException($"Source file not found: {file.FullName}");
        }

        File.Copy(sourcePath, destinationPath);

        return Task.CompletedTask;
    }

    [KernelFunction, Description("Creates a binary file with the given content.")]
    public async Task CreateBinaryFileAsync(
         [Description("Destination file")] string path,
         [Description("File binary content")] byte[] content,
        CancellationToken cancellationToken = default)
    {
        if (File.Exists(path) && File.GetAttributes(path).HasFlag(FileAttributes.ReadOnly))
        {
            // Most environments will throw this with OpenWrite, but running inside docker on Linux will not.
            throw new UnauthorizedAccessException($"File is read-only: {path}");
        }

        using var writer = File.OpenWrite(path);
        await writer.WriteAsync(content, 0, content.Length).ConfigureAwait(false);
    }

    [KernelFunction, Description("Read a binary file.")]
    public async Task<Byte[]> GetBinaryFileAsync(
        [Description("Source file")] string path,
        CancellationToken cancellationToken = default)
    {
        if (File.Exists(path) && File.GetAttributes(path).HasFlag(FileAttributes.ReadOnly))
        {
            // Most environments will throw this with OpenWrite, but running inside docker on Linux will not.
            throw new UnauthorizedAccessException($"File is read-only: {path}");
        }

        using var reader = File.OpenRead(path);
        var buffer = new Byte[reader.Length];
        await reader.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
        return buffer;

    }

    [KernelFunction, Description("Returns items in a directory")]
    public Task<string[]> ListDirectoryAsync(
        [Description("Search directory")] string searchDirectory,
        [Description("Items filter")] string? pattern = "*",
        CancellationToken cancellationToken = default)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(searchDirectory);

        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Search directory not found: {dir.FullName}");

        Matcher matcher = new();
        if (pattern != null)
        {
            matcher.AddInclude(pattern);
        }

        IEnumerable<string> matchingFiles = matcher.GetResultsInFullPath(searchDirectory);

        return Task.FromResult(matchingFiles.ToArray());
    }
}
