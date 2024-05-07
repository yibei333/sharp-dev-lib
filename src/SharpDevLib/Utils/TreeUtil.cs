using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib;

/// <summary>
/// tree node interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITreeNode<T> where T : ITreeNode<T>
{
    /// <summary>
    /// primary key
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// parent key
    /// </summary>
    public Guid? ParentId { get; set; }
    /// <summary>
    /// show order
    /// </summary>
    public int Order { get; set; }
    /// <summary>
    /// children elements
    /// </summary>
    public List<T> Children { get; set; }
}

/// <summary>
/// tree util
/// </summary>
public static class TreeUtil
{
    /// <summary>
    /// build tree from a collection
    /// </summary>
    /// <typeparam name="T">tree element type</typeparam>
    /// <param name="source">collection</param>
    /// <returns>tree</returns>
    public static List<T> BuildTree<T>(this List<T>? source) where T : class, ITreeNode<T>
    {
        if (source.IsEmpty()) return new List<T>();
        var parents = source.Where(x => x.ParentId.IsEmpty()).OrderBy(x => x.Order).ToList();
        parents.ForEach(x => x.AddChildren(source!));
        return parents;
    }

    static void AddChildren<T>(this T current, List<T> source) where T : class, ITreeNode<T>
    {
        if (current.Children.IsNull()) current.Children = new List<T>();
        current.Children.Clear();
        var children = source.Where(x => x.ParentId == current.Id).OrderBy(x => x.Order).ToList();
        if (children.IsEmpty()) return;
        current.Children.AddRange(children);
        children.ForEach(child => child.AddChildren(source));
    }
}
