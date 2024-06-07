using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace SharpDevLib.Transport;

internal static class TransportInternalExtensions
{
    internal static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    internal static bool NotNullOrWhiteSpace([NotNullWhen(true)] this string? str) => !string.IsNullOrWhiteSpace(str);

    internal static string GetFileExtension(this string? filePath, bool includePoint = true)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(filePath));
        var extension = new FileInfo(filePath).Extension;
        return includePoint ? extension : extension.TrimStart('.');
    }

    internal static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? source) => source is null || source.Count() <= 0;

    internal static bool NotNullOrEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? source) => source is not null && source.Count() > 0;

    internal static void RemoveFileIfExist(this string path)
    {
        if (path.IsNullOrWhiteSpace()) return;
        if (File.Exists(path)) File.Delete(path);
    }

    internal static string TrimStart(this string source, string target)
    {
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source.Trim();
        source = source.Trim();
        target = target.Trim();
        if (source.StartsWith(target)) return source.Substring(source.IndexOf(target) + target.Length);
        return source;
    }

    internal static string TrimEnd(this string source, string target)
    {
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source.Trim();
        source = source.Trim();
        target = target.Trim();
        if (source.EndsWith(target)) return source.Substring(0, source.IndexOf(target));
        return source;
    }

    internal static void EnsureDirectoryExist(this string directory)
    {
        if (directory.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(directory));
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }

    internal static FileStream OpenOrCreate(this FileInfo fileInfo)
    {
        fileInfo.Directory.FullName.EnsureDirectoryExist();
        var stream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    internal static async Task CopyToAsync(this Stream source, Stream target, CancellationToken cancellationToken, Action<long>? transfered = null)
    {
        await Task.Yield();
        var buffer = new byte[2048];
        int length;
        if (source.CanSeek) source.Seek(0, SeekOrigin.Begin);

        while ((length = source.Read(buffer, 0, buffer.Length)) > 0)
        {
            if (cancellationToken.IsCancellationRequested) break;
            target.Write(buffer, 0, length);

            transfered?.Invoke(length);
        }
    }

    internal static string FormatPath(this string path) => path.Trim().Replace("\\", "/");

    internal static string GetFileName(this string filePath, bool includeExtension = true)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(filePath));
        var fileInfo = new FileInfo(filePath);
        return includeExtension ? fileInfo.Name : fileInfo.Name.TrimEnd(fileInfo.Extension);
    }

    internal static string CombinePath(this string leftPath, string rightPath) => Path.Combine(leftPath.Trim(), rightPath.Trim().TrimStart('/').TrimStart('\\')).FormatPath();

    internal static void EnsureFileExist(this string filePath)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(filePath));
        if (!File.Exists(filePath)) throw new FileNotFoundException($"file not found", filePath);
    }

    static readonly double _kbUnit = 1024;
    static readonly double _mbUnit = 1024 * _kbUnit;
    static readonly double _gbUnit = 1024 * _mbUnit;
    static readonly double _tbUnit = 1024 * _gbUnit;
    internal static string ToFileSizeString(this long size)
    {
        if (size < 0) throw new ArgumentException("file size should greater than equal 0", nameof(size));
        if (size > _tbUnit) return $"{(Math.Round(size / _tbUnit, 2))}TB";
        else if (size > _gbUnit) return $"{(Math.Round(size / _gbUnit, 2))}GB";
        else if (size > _mbUnit) return $"{(Math.Round(size / _mbUnit, 2))}MB";
        else if (size > _kbUnit) return $"{(Math.Round(size / _kbUnit, 2))}KB";
        else return $"{size}Byte";
    }

    internal static DateTime UtcStartTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    internal static bool TrySerialize(this object obj, out string json)
    {
        try
        {
            json = JsonSerializer.Serialize(obj);
            return true;
        }
        catch
        {
            json = string.Empty;
            return false;
        }
    }

    internal static string Serialize(this object obj) => JsonSerializer.Serialize(obj);
    internal static object? DeSerialize(this string json, Type type) => JsonSerializer.Deserialize(json, type);

    internal static string GetTypeName(this Type type, bool isFullName = false)
    {
        if (!type.IsGenericType) return isFullName ? type.FullName ?? type.Name : type.Name;

        var names = new List<string>();
        foreach (var item in type.GetGenericArguments())
        {
            names.Add(GetTypeName(item, isFullName));
        };
        var typeName = isFullName ? type.FullName ?? type.Name : type.Name;
        return $"{typeName.Split('`')[0]}<{string.Join(",", names)}>";
    }
}
