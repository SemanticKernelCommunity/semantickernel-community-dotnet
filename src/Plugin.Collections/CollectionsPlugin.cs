using System.Collections;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.SemanticKernel;

namespace Community.SemanticKernel.Plugins.Collections;

public sealed class CollectionPlugin
{

    [KernelFunction, Description("Append a string item into a collection of strings")]
    public Task<string[]> AppendStringItemAsync(
        [Description("String items collection")] string[] collection,
        [Description("String item to append")] string item,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.Append(item).ToArray());
    }

    [KernelFunction, Description("Returns first string item in a collection of strings")]
    public Task<string?> FirstStringItemAsync(
        [Description("String items collection")] string[] collection,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.FirstOrDefault());
    }

    [KernelFunction, Description("Returns last string item in a collection of strings")]
    public Task<string?> LastStringItemAsync(
       [Description("String items collection")] string[] collection,
       CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.LastOrDefault());
    }

    [KernelFunction, Description("Sorts a collection of strings in ascending order")]
    public Task<string[]> OrderAsync(
           [Description("String items collection")] string[] collection,
           CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.Order().ToArray());
    }

    [KernelFunction, Description("Sorts a collection of strings in descending order")]
    public Task<string[]> OrderDescAsync(
           [Description("String items collection")] string[] collection,
           CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.OrderDescending().ToArray());
    }

    [KernelFunction, Description("Concats two collections of strings")]
    public Task<string[]> ConcatAsync(
             [Description("First string items collection")] string[] collectionFirst,
             [Description("Second string items collection")] string[] collectionSecond,
             CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collectionFirst.Concat(collectionSecond).ToArray());
    }

     [KernelFunction, Description("Returns the number of elements in a strings")]
    public Task<int> CountAsync(
             [Description("String items collection")] string[] collection,
             CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.Count());
    }
}
