using System.IO;

namespace SharpDevLib.Standard;

/// <summary>
/// the url utils
/// </summary>
public static class UrlExtension
{
    /// <summary>
    /// combine the url
    /// </summary>
    /// <param name="sourcePath">the source path</param>
    /// <param name="targetPath">the target path</param>
    /// <returns>combined path with the same splitor</returns>
    public static string CombinePath(this string? sourcePath, string? targetPath)
    {
        if (sourcePath is null) return targetPath ?? string.Empty;
        if (targetPath is null) return sourcePath ?? string.Empty;
        return Path.Combine(sourcePath.FormatPath().TrimEnd(new[] { '/' }), targetPath.FormatPath().TrimStart(new[] { '/' })).FormatPath();
    }

    /// <summary>
    /// replace charactor '\' to '/'
    /// </summary>
    /// <param name="path">the path</param>
    /// <returns>formated path</returns>
    public static string FormatPath(this string? path)
    {
        if (path is null) return string.Empty;
        return path.Trim().Replace("\\", "/");
    }
}
