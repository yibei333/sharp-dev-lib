namespace SharpDevLib.Standard;

/// <summary>
/// file util
/// </summary>
public static class FileExtension
{
    /// <summary>
    /// get extension from file path
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <returns>file extensnion</returns>
    public static string GetFileExtension(this string? filePath)
    {
        if (filePath is null) return string.Empty;
        return new FileInfo(filePath).Extension;
    }

    /// <summary>
    /// get name(include extension) from file path
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <returns>file extensnion</returns>
    public static string GetFileName(this string? filePath)
    {
        if (filePath is null) return string.Empty;
        return new FileInfo(filePath).Name;
    }

    /// <summary>
    /// get name(not include extension) from file path
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <returns>file extensnion</returns>
    public static string GetFileNameWithoutExtension(this string? filePath)
    {
        if (filePath is null) return string.Empty;
        return new FileInfo(filePath).Name.TrimEnd(filePath.GetFileExtension());
    }

    /// <summary>
    /// save bytes to file
    /// </summary>
    /// <param name="bytes">bytes to save</param>
    /// <param name="filePath">target file path</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static void SaveFile(this byte[]? bytes, string? filePath)
    {
        if (filePath is null) throw new ArgumentNullException("path can not be null");
        if (bytes.IsNullOrEmpty()) throw new InvalidOperationException($"unable to save empty bytes to file");
        if (File.Exists(filePath)) File.Delete(filePath);
        using var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        stream.Write(bytes);
    }

    /// <summary>
    /// save stream to file
    /// </summary>
    /// <param name="stream">stream to save</param>
    /// <param name="filePath">target file path</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static void SaveFile(this Stream? stream, string? filePath)
    {
        if (filePath is null) throw new ArgumentNullException("path can not be null");
        if (stream is null) throw new InvalidOperationException($"unable to save empty stream to file");
        if (File.Exists(filePath)) File.Delete(filePath);

        using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        stream.CopyTo(fileStream);
        fileStream.Flush();
    }

    /// <summary>
    /// ensure directory exist,if not exist,create it
    /// </summary>
    /// <param name="directory">directory path</param>
    /// <exception cref="ArgumentNullException">if directory is nulll or empty</exception>
    public static void EnsureDirectoryExist(this string? directory)
    {
        if (directory is null) throw new ArgumentNullException($"directory('{directory}') can't be empty");
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }

    /// <summary>
    /// ensure file exist
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <exception cref="ArgumentNullException">if file path is null or empty</exception>
    /// <exception cref="FileNotFoundException">if file not found</exception>
    public static void EnsureFileExist(this string? filePath)
    {
        if (filePath is null) throw new ArgumentNullException();
        if (!File.Exists(filePath)) throw new FileNotFoundException($"file('{filePath}') not exist");
    }
}
