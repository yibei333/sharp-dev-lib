using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib;

/// <summary>
/// zip util
/// </summary>
public static class ZipUtil
{
    /// <summary>
    /// compress files
    /// </summary>
    /// <param name="directory">directory to compress</param>
    /// <param name="targetFile">target file path</param>
    /// <param name="password">compress password</param>
    /// <param name="progress">compress progress</param>
    /// <exception cref="ArgumentNullException">if directory is nulll or empty</exception>
    public static void Compress(this string directory,string targetFile,string? password=null,Action<int>? progress=null)
    {
        directory.EnsureDirectoryExist();
        new FileInfo(targetFile).Directory.FullName.EnsureDirectoryExist();
        using var zip = new Ionic.Zip.ZipFile();
        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
        if (password.NotEmpty()) zip.Password = password;
        zip.AddDirectory(directory);
        var lastProgress = 0;
        zip.SaveProgress += (sender, args) =>
        {
            if (args.EntriesTotal <= 0) return;
            var totalProgress = args.EntriesSaved * 100 / args.EntriesTotal;
            if (totalProgress <= lastProgress) return;
            lastProgress = totalProgress;
            progress?.Invoke(lastProgress);
        };
        zip.Save(targetFile);
    }

    /// <summary>
    /// compress files
    /// </summary>
    /// <param name="files">files to compress</param>
    /// <param name="targetFile">target file path</param>
    /// <param name="password">compress password</param>
    /// <param name="progress">compress progress</param>
    /// <exception cref="ArgumentNullException">if file path is null or empty</exception>
    /// <exception cref="FileNotFoundException">if file not found</exception>
    public static void Compress(this IEnumerable<string> files, string targetFile, string? password = null, Action<int>? progress = null)
    {
        foreach (var file in files)
        {
            file.EnsureFileExist();
        }
        using var zip = new Ionic.Zip.ZipFile();
        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
        if (password.NotEmpty()) zip.Password = password;
        zip.AddFiles(files, false, "");
        var lastProgress = 0;
        zip.SaveProgress += (sender, args) =>
        {
            if (args.EntriesTotal <= 0) return;
            var totalProgress = args.EntriesSaved * 100 / args.EntriesTotal;
            if (totalProgress <= lastProgress) return;
            lastProgress = totalProgress;
            progress?.Invoke(lastProgress);
        };
        zip.Save(targetFile);
    }

    /// <summary>
    /// extract normal zip file
    /// </summary>
    /// <param name="sourceFile">zip source file path</param>
    /// <param name="targetPath">extract target path</param>
    /// <param name="password">zip file password</param>
    /// <param name="progress">progress notify</param>
    /// <exception cref="ArgumentNullException">if file path is null or empty</exception>
    /// <exception cref="FileNotFoundException">if file not found</exception>
    public static void Extract(this string sourceFile, string targetPath, string? password = null, Action<int>? progress = null)
    {
        sourceFile.EnsureFileExist();
        targetPath.EnsureDirectoryExist();
        using var stream = new FileStream(sourceFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        var totalEntryCount = new System.IO.Compression.ZipArchive(stream).Entries.Count;
        var entryIndex = 0;
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = ReaderFactory.Open(stream, new ReaderOptions { Password = password });

        var lastProgress = 0;
        reader.EntryExtractionProgress += (sender, e) =>
        {
            var totalProgress = entryIndex * 100 / totalEntryCount;
            if (totalProgress <= lastProgress) return;
            lastProgress = totalProgress;
            progress?.Invoke(lastProgress);
        };

        while (reader.MoveToNextEntry())
        {
            entryIndex++;
            if (!reader.Entry.IsDirectory)
            {
                reader.WriteEntryToDirectory(targetPath, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
        }
    }
}
