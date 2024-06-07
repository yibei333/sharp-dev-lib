using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib.Compression;

internal static class InternalCompressionExtension
{
    internal static readonly Dictionary<string, CompressionFormat> SupportedCompressFormats = new()
    {
        {".zip",CompressionFormat.Zip},
        {".tar",CompressionFormat.Tar},
        {".tgz",CompressionFormat.Gz},
        {".gz",CompressionFormat.Gz},
        {".bz2",CompressionFormat.Bz2},
    };

    internal static readonly List<string> SupportedCompressExtensions = SupportedCompressFormats.Select(x => x.Key).ToList();

    internal static readonly Dictionary<string, CompressionFormat> SupportedDeCompressFormats = new()
    {
        {".zip",CompressionFormat.Zip},
        {".rar",CompressionFormat.Rar},
        {".7z",CompressionFormat.SevenZip},
        {".tar",CompressionFormat.Tar},
        {".tgz",CompressionFormat.Gz},
        {".gz",CompressionFormat.Gz},
        {".xz",CompressionFormat.Xz},
        {".bz2",CompressionFormat.Bz2},
    };

    internal static readonly List<string> SupportedDeCompressExtensions = SupportedDeCompressFormats.Select(x => x.Key).ToList();


    internal static readonly Dictionary<CompressionFormat, Type> CompressHandlers = new()
    {
        { CompressionFormat.Zip,typeof(ZipCompressHandler) },
        { CompressionFormat.Tar,typeof(TarCompressHandler) },
        { CompressionFormat.Gz,typeof(GzCompressHandler) },
        { CompressionFormat.Bz2,typeof(Bz2CompressHandler) },
    };

    internal static readonly Dictionary<CompressionFormat, Type> DeCompressHandlers = new()
    {
        { CompressionFormat.Zip,typeof(ZipDeCompressHandler) },
        { CompressionFormat.Rar,typeof(RarDeCompressHandler) },
        { CompressionFormat.SevenZip,typeof(SevenZipDeCompressHandler) },
        { CompressionFormat.Tar,typeof(TarDeCompressHandler) },
        { CompressionFormat.Gz,typeof(GzDeCompressHandler) },
        { CompressionFormat.Xz,typeof(XzDeCompressHandler) },
        { CompressionFormat.Bz2,typeof(Bz2DeCompressHandler) },
    };

    internal static CompressionFormat GetComopressFormat(this string path)
    {
        if (path.IsNullOrWhiteSpace()) return CompressionFormat.UnKnown;
        var extension = path.GetFileExtension();
        return SupportedCompressFormats.TryGetValue(extension, out var format) ? format : throw new CompressFormatNotSupportedException(extension);
    }

    internal static CompressionFormat GetDecompressFormat(this string path)
    {
        if (path.IsNullOrWhiteSpace()) return CompressionFormat.UnKnown;
        var extension = path.GetFileExtension();
        return SupportedDeCompressFormats.TryGetValue(extension, out var format) ? format : throw new DeCompressFormatNotSupportedException(extension);
    }

    internal static async Task InternalCompressAsync(this CompressOption option, CancellationToken? cancellationToken = null)
    {
        await Task.Yield();
        var format = option.Format;
        var type = CompressHandlers.TryGetValue(format, out var handlerType) ? handlerType : throw new Exception($"unable to find handler of format '{format}'");
        var instance = Activator.CreateInstance(type, option, cancellationToken) as CompressHandler ?? throw new NullReferenceException();
        await instance.HandleAsync();
    }

    internal static async Task InternalDeCompressAsync(this DeCompressOption option, CancellationToken? cancellationToken = null)
    {
        await Task.Yield();
        var format = option.Format;
        var type = DeCompressHandlers.TryGetValue(format, out var handlerType) ? handlerType : throw new Exception($"unable to find handler of format '{format}'");
        var instance = Activator.CreateInstance(type, option, cancellationToken) as DeCompressHandler ?? throw new NullReferenceException();
        await instance.HandleAsync();
    }

    internal static int ConvertToSharpZipLibLevel(this CompressionLevel level)
    {
        // 0 - store only to 9 - means best compression
        return level switch
        {
            CompressionLevel.Fastest => 1,
            CompressionLevel.MinimumSize => 9,
            _ => 5,
        };
    }

    #region internal simple extesnions
    internal static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    internal static bool NotNullOrWhiteSpace([NotNullWhen(true)] this string? str) => !string.IsNullOrWhiteSpace(str);

    internal static string GetFileExtension(this string? filePath, bool includePoint = true)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(filePath));
        var extension = new FileInfo(filePath).Extension;
        return includePoint ? extension : extension.TrimStart('.');
    }

    internal static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? source) => source is null || source.Count() <= 0;

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
    #endregion
}