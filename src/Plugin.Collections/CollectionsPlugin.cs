using System.Collections;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.SemanticKernel;

namespace Community.SemanticKernel.Plugins.Collections;

public sealed class CollectionPlugin
{

    /// <summary>
    /// Append a string item into a collection of strings.
    /// </summary>
    /// <param name="collection">"String items collection"</param>
    /// <param name="item">"String item to append"</param>
    /// <returns>A new collection that ends with the item.</returns>
    [KernelFunction, Description("Append a string item into a collection of strings.")]
    public string[] AppendStringItem(
        [Description("String items collection")] string[] collection,
        [Description("String item to append")] string item)
    {
        return collection.Append(item).ToArray();
    }

    /// <summary>
    /// Returns first string item in a collection of strings.
    /// </summary>
    /// <param name="collection">String items collection</param>
    /// <returns>default if source is empty, otherwise the first item in the collection.</returns>
    [KernelFunction, Description("Returns first string item in a collection of strings.")]
    public string? FirstStringItem(
        [Description("String items collection")] string[] collection)
    {
        return collection.FirstOrDefault();
    }

    /// <summary>
    /// Returns last string item in a collection of strings.
    /// </summary>
    /// <param name="collection">String items collection</param>
    /// <returns>default if source is empty, otherwise the last item in the collection.</returns>
    [KernelFunction, Description("Returns last string item in a collection of strings.")]
    public string? LastStringItem(
       [Description("String items collection")] string[] collection)
    {
        return collection.LastOrDefault();
    }

    /// <summary>
    /// Sorts a collection of strings in ascending order.
    /// </summary>
    /// <param name="collection">"String items collection"</param>
    /// <returns>Collection sorted in ascending order.</returns>
    [KernelFunction, Description("Sorts a collection of strings in ascending order.")]
    public string[] Order(
           [Description("String items collection")] string[] collection)
    {
        return collection.Order().ToArray();
    }

    /// <summary>
    /// Sorts a collection of strings in descending order.
    /// </summary>
    /// <param name="collection">"String items collection"</param>
    /// <returns>Collection sorted in descending order.</returns>
    [KernelFunction, Description("Sorts a collection of strings in descending order.")]
    public string[] OrderDescending(
           [Description("String items collection")] string[] collection)
    {
        return collection.OrderDescending().ToArray();
    }

    /// <summary>
    /// Concats two collections of strings.
    /// </summary>
    /// <param name="firstCollection">"First string items collection"</param>
    /// <param name="secondCollection">"Second string items collection"</param>
    /// <returns>A new collection containing the items of the two input collections.</returns>
    [KernelFunction, Description("Concats two collections of strings.")]
    public string[] Concat(
             [Description("First string items collection")] string[] firstCollection,
             [Description("Second string items collection")] string[] secondCollection)
    {
        return firstCollection.Concat(secondCollection).ToArray();
    }

    /// <summary>
    /// Returns a specified number of continuous item from the start of a collection of strings.
    /// </summary>
    /// <param name="collection">String items collection</param>
    /// <param name="count">The number of items to return</param>
    /// <returns>A new collection that contains the specified number of elements from the start of the input sequence.</returns>
    [KernelFunction, Description("Returns a specified number of continuous item from the start of a collection of strings.")]
    public string[] Take(
        [Description("String items collection")] string[] collection,
        [Description("The number of items to return")] int count)
    {
        return collection.Take(count).ToArray();
    }

    /// <summary>
    /// Bypasses a specified number of elements in a collection of strings.
    /// </summary>
    /// <param name="collection">String items collection</param>
    /// <param name="count">The number of items to skip</param>
    /// <returns>A new collection that contains the items that occur after the specified index in the input collection.</returns>
    [KernelFunction, Description("Bypasses a specified number of elements in a collection of strings.")]
    public string[] Skip(
        [Description("String items collection")] string[] collection,
        [Description("The number of items to skip")] int count)
    {
        return collection.Skip(count).ToArray();
    }

    /// <summary>
    /// Gets the value at the specified position in a collection of strings.
    /// </summary>
    /// <param name="collection">String items collection</param>
    /// <param name="index">The position of the collection item to get</param>
    /// <returns>The value at the specified index</returns>
    [KernelFunction, Description("Gets the value at the specified position in a collection of strings.")]
    public string? GetValueAsync(
            [Description("String items collection")] string[] collection,
            [Description("The position of the collection item to get")] int index)
    {
        return collection.GetValue(index)?.ToString();
    }

    /// <summary>
    /// Inverts the order of the items in a collection of strings.
    /// </summary>
    /// <param name="collection">String items collection</param>
    /// <returns>A new collection with the input collection in a reverse order</returns>
    [KernelFunction, Description("Inverts the order of the items in a collection of strings.")]
    public string[] Reverse(
               [Description("String items collection")] string[] collection)
    {
        return collection.Reverse().ToArray();
    }

    /// <summary>
    /// Returns the number of items in a collection of strings.
    /// </summary>
    /// <param name="collection">String items collection</param>
    /// <returns>The number of items in the input collection</returns>
    [KernelFunction, Description("Returns the number of items in a collection of strings.")]
    public int Count(
            [Description("String items collection")] string[] collection)
    {
        return collection.Count();
    }
}
