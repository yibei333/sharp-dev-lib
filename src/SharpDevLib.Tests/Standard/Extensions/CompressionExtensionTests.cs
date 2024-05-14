using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using SharpDevLib.Standard;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class CompressionExtensionTests
{
    [TestMethod]
    public void Test()
    {
        var sourceDirectory = @"D:\aa";
        var directoryInfo = new DirectoryInfo(sourceDirectory);
        var targetPath = @"D:\bb.zip";
        var rootName = directoryInfo.Name;
        var rootPath = directoryInfo.FullName;

        using var stream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var zipStream = new ZipOutputStream(stream);
        zipStream.Password = "abc";
        var factory = new ZipEntryFactory();

        directoryInfo.GetDirectories().ToList().ForEach(directory =>
        {
            var entry = factory.MakeDirectoryEntry(directory.FullName, true);
            zipStream.PutNextEntry(entry);
        });
        directoryInfo.GetFiles().ToList().ForEach(file =>
        {
            var entry = factory.MakeFileEntry(file.FullName, true);
            zipStream.PutNextEntry(entry);
        });
        zipStream.Flush();
    }
}
