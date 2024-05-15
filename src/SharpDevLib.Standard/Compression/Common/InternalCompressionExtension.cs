using SharpCompress.Archives.GZip;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Tar;
using SharpCompress.Archives.Zip;
using SharpCompress.Compressors.BZip2;
using SharpCompress.Compressors.Xz;
using SharpDevLib.Standard.Compression.Compress;
using SharpDevLib.Standard.Compression.DeCompress;

namespace SharpDevLib.Standard;

internal static class InternalCompressionExtension
{
    internal static readonly Dictionary<string, CompressionFormat> SupportedFormats = new()
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

    internal static readonly List<string> SupportedExtensions = SupportedFormats.Select(x => x.Key).ToList();

    internal static readonly Dictionary<CompressionFormat, Type> CompressHandlers = new()
    {
        { CompressionFormat.Zip,typeof(ZipCompressHandler) },
        { CompressionFormat.Rar,typeof(RarCompressHandler) },
        { CompressionFormat.SevenZip,typeof(SevenZipCompressHandler) },
        { CompressionFormat.Tar,typeof(TarCompressHandler) },
        { CompressionFormat.Gz,typeof(GzCompressHandler) },
        { CompressionFormat.Xz,typeof(XzCompressHandler) },
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

    internal static CompressionFormat GetFormatByName(this string path)
    {
        if (path.IsNullOrWhiteSpace()) return CompressionFormat.UnKnown;
        var extension = path.GetFileExtension();
        return SupportedFormats.TryGetValue(extension, out var format) ? format : throw new CompressionFormatNotSupportedException(extension);
    }

    internal static CompressionFormat GetFormatByFileInfo(this FileInfo fileInfo, string? password)
    {
        if (!fileInfo.Exists) throw new FileNotFoundException("file not found", fileInfo.FullName);
        using var stream = fileInfo.OpenRead();

        if (ZipArchive.IsZipFile(stream)) return CompressionFormat.Zip;
        if (RarArchive.IsRarFile(stream)) return CompressionFormat.Rar;
        if (SevenZipArchive.IsSevenZipFile(stream)) return CompressionFormat.SevenZip;
        if (TarArchive.IsTarFile(stream)) return CompressionFormat.Tar;
        if (GZipArchive.IsGZipFile(stream)) return CompressionFormat.Gz;
        if (XZStream.IsXZStream(stream)) return CompressionFormat.Xz;
        if (BZip2Stream.IsBZip2(stream)) return CompressionFormat.Bz2;
        throw new CompressionFormatNotSupportedException(fileInfo.Extension);
    }

    internal static async Task InternalCompressAsync(this CompressOption option)
    {
        var format = option.Format;
        var type = CompressHandlers.TryGetValue(format, out var handlerType) ? handlerType : throw new Exception($"unable to find handler of format '{format}'");
        var instance = Activator.CreateInstance(type, option) as CompressHandler ?? throw new NullReferenceException();
        await instance.HandleAsync();
    }

    internal static async Task InternalDeCompressAsync(this DeCompressOption option)
    {
        var format = option.Format;
        var type = DeCompressHandlers.TryGetValue(format, out var handlerType) ? handlerType : throw new Exception($"unable to find handler of format '{format}'");
        var instance = Activator.CreateInstance(type, option) as DeCompressHandler ?? throw new NullReferenceException();
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
}