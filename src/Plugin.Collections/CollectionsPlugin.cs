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

    [KernelFunction, Description("Returns a specified number of continuous item from the start of a collection of strings")]
    public Task<string[]> TakeAsync(
        [Description("String items collection")] string[] collection,
        [Description("The number of items to return")] int count,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.Take(count).ToArray());
    }

    [KernelFunction, Description("Bypasses a specified number of elements in a collection of strings")]
    public Task<string[]> SkipAsync(
        [Description("String items collection")] string[] collection,
        [Description("The number of items to skip")] int count,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.Skip(count).ToArray());
    }

    [KernelFunction, Description("Gets the value at the specified position in a collection of strings")]
    public Task<string?> GetValueAsync(
            [Description("String items collection")] string[] collection,
            [Description("the position of the collection item to get.")] int index,
            CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.GetValue(index)?.ToString());
    }

    [KernelFunction, Description("Returns the number of elements in a strings")]
    public Task<int> CountAsync(
            [Description("String items collection")] string[] collection,
            CancellationToken cancellationToken = default)
    {
        return Task.FromResult(collection.Count());
    }
}
