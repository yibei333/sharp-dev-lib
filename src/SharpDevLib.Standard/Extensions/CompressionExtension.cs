//using SharpCompress.Archives;
//using SharpCompress.Archives.Zip;
//using SharpCompress.Common;
//using SharpCompress.Readers;
//using SharpCompress.Writers;
//using System;
//using System.Collections.Generic;

namespace SharpDevLib.Standard;

/// <summary>
/// 压缩/解压扩展
/// </summary>
public static class CompressionExtension
{
    ///// <summary>
    ///// compress files
    ///// </summary>
    ///// <param name="directory">directory to compress</param>
    ///// <param name="targetFile">target file path</param>
    ///// <param name="password">compress password</param>
    ///// <param name="progress">compress progress</param>
    ///// <exception cref="ArgumentNullException">if directory is nulll or empty</exception>
    //public static void Compress(this string directory, string targetFile, string? password = null, Action<int>? progress = null)
    //{
    //    directory.EnsureDirectoryExist();
    //    new FileInfo(targetFile).Directory.FullName.EnsureDirectoryExist();
    //    using var zip = new Ionic.Zip.ZipFile();
    //    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
    //    if (password.NotNullOrEmpty()) zip.Password = password;
    //    zip.AddDirectory(directory);
    //    var lastProgress = 0;
    //    zip.SaveProgress += (sender, args) =>
    //    {
    //        if (args.EntriesTotal <= 0) return;
    //        var totalProgress = args.EntriesSaved * 100 / args.EntriesTotal;
    //        if (totalProgress <= lastProgress) return;
    //        lastProgress = totalProgress;
    //        progress?.Invoke(lastProgress);
    //    };
    //    zip.Save(targetFile);
    //}

    ///// <summary>
    ///// compress files
    ///// </summary>
    ///// <param name="files">files to compress</param>
    ///// <param name="targetFile">target file path</param>
    ///// <param name="password">compress password</param>
    ///// <param name="progress">compress progress</param>
    ///// <exception cref="ArgumentNullException">if file path is null or empty</exception>
    ///// <exception cref="FileNotFoundException">if file not found</exception>
    //public static void Compress(this IEnumerable<string> files, string targetFile, string? password = null, Action<int>? progress = null)
    //{
    //    foreach (var file in files)
    //    {
    //        file.EnsureFileExist();
    //    }
    //    using var zip = new Ionic.Zip.ZipFile();
    //    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
    //    if (password.NotNullOrEmpty()) zip.Password = password;
    //    zip.AddFiles(files, false, "");
    //    var lastProgress = 0;
    //    zip.SaveProgress += (sender, args) =>
    //    {
    //        if (args.EntriesTotal <= 0) return;
    //        var totalProgress = args.EntriesSaved * 100 / args.EntriesTotal;
    //        if (totalProgress <= lastProgress) return;
    //        lastProgress = totalProgress;
    //        progress?.Invoke(lastProgress);
    //    };
    //    zip.Save(targetFile);
    //}

  

    ///// <summary>
    ///// extract normal zip file
    ///// </summary>
    ///// <param name="sourceFile">zip source file path</param>
    ///// <param name="targetPath">extract target path</param>
    ///// <param name="password">zip file password</param>
    ///// <param name="progress">progress notify</param>
    ///// <exception cref="ArgumentNullException">if file path is null or empty</exception>
    ///// <exception cref="FileNotFoundException">if file not found</exception>
    //public static void Extract(this string sourceFile, string targetPath, string? password = null, Action<int>? progress = null)
    //{
    //    sourceFile.EnsureFileExist();
    //    targetPath.EnsureDirectoryExist();

    //    using var stream = new FileStream(sourceFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
    //    using var reader = ReaderFactory.Open(stream, new ReaderOptions { Password = password });
    //    var extractOption = new ExtractionOptions() { ExtractFullPath = true, Overwrite = true };

    //    if (progress is not null) reader.EntryExtractionProgress += (sender, e) => progress.Invoke(e.ReaderProgress?.PercentageRead ?? 0);
    //    while (reader.MoveToNextEntry())
    //    {
    //        if (reader.Entry.IsDirectory) continue;
    //        reader.WriteEntryToDirectory(targetPath, extractOption);
    //    }
    //}
}
