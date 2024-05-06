namespace SharpDevLib.Standard;

/// <summary>
/// 文件扩展
/// </summary>
public static class FileExtension
{
    /// <summary>
    /// 获取文件的扩展名(包含.)
    /// </summary>
    /// <param name="filePath">文件路径,文件名也可以</param>
    /// <returns>扩展名</returns>
    public static string GetFileExtension(this string? filePath)
    {
        if (filePath.IsNullOrWhiteSpace()) return string.Empty;
        return new FileInfo(filePath).Extension;
    }

    /// <summary>
    /// 获取文件名(包含.)
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>文件名</returns>
    public static string GetFileName(this string? filePath)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new FileNotFoundException("file path is empty", filePath);
        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists) throw new FileNotFoundException("file not found", filePath);
        return fileInfo.Name;
    }

    /// <summary>
    /// 获取文件名(不包含扩展名)
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>文件名</returns>
    public static string GetFileNameWithoutExtension(this string? filePath)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new FileNotFoundException("file path is empty", filePath);
        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists) throw new FileNotFoundException("file not found", filePath);
        return fileInfo.Name.TrimEnd(fileInfo.Extension);
    }

    /// <summary>
    /// 将字节数组保存到文件中
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="filePath">文件路径</param>
    /// <param name="throwIfFileExist">当文件已存在时是否抛出异常,true-抛出异常,false-覆盖</param>
    public static void SaveToFile(this byte[]? bytes, string? filePath, bool throwIfFileExist = false)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(filePath));
        if (bytes.IsNullOrEmpty()) throw new InvalidOperationException($"unable to save empty bytes to file");
        if (File.Exists(filePath))
        {
            if (throwIfFileExist) throw new InvalidOperationException($"file '{filePath}' already existed");
            File.Delete(filePath);
        }
        using var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        stream.Write(bytes);
        stream.Flush();
    }

    /// <summary>
    /// 将流保存到文件中
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="filePath">文件路径</param>
    /// <param name="throwIfFileExist">当文件已存在时是否抛出异常,true-抛出异常,false-覆盖</param>
    public static void SaveFile(this Stream? stream, string? filePath, bool throwIfFileExist = false)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(filePath));
        if (stream is null || stream.Length <= 0) throw new InvalidOperationException($"unable to save empty stream to file");
        if (File.Exists(filePath))
        {
            if (throwIfFileExist) throw new InvalidOperationException($"file '{filePath}' already existed");
            File.Delete(filePath);
        }

        using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        stream.CopyTo(fileStream);
        fileStream.Flush();
    }

    /// <summary>
    /// 确保文件夹存在,如果不存在则创建
    /// </summary>
    /// <param name="directory">文件夹路径</param>
    public static void EnsureDirectoryExist(this string? directory)
    {
        if (directory.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(directory));
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }

    /// <summary>
    /// 确保文件存在,如果不存在则抛出异常
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public static void EnsureFileExist(this string? filePath)
    {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(filePath));
        if (!File.Exists(filePath)) throw new FileNotFoundException($"file not found", filePath);
    }

    //todo:size,mime,combine path,format path
}
