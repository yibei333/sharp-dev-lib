using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// 对称加密算法扩展
/// </summary>
public static class SymmetricAlgorithmExtension
{
    const int bufferSize = 4096;

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="algorithm">对称算法</param>
    /// <param name="data">已加密的字节数组</param>
    /// <returns>解密的字节数组</returns>
    public static byte[] Decrypt(this SymmetricAlgorithm algorithm, byte[] data)
    {
        if (data.IsNullOrEmpty()) throw new ArgumentNullException(nameof(data));

        using var inputStream = new MemoryStream(data);
        using var outputStream = new MemoryStream();
        algorithm.Decrypt(inputStream, outputStream);
        return outputStream.ToArray();
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="algorithm">对称算法</param>
    /// <param name="inputStream">已加密的流</param>
    /// <param name="outputStream">解密的流</param>
    public static void Decrypt(this SymmetricAlgorithm algorithm, Stream inputStream, Stream outputStream)
    {
        using var transform = algorithm.CreateDecryptor();
        var cryptoStream = new CryptoStream(outputStream, transform, CryptoStreamMode.Write);
        var buffer = new byte[bufferSize];
        var length = -1;
        while ((length = inputStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            cryptoStream.Write(buffer, 0, length);
        }
        cryptoStream.FlushFinalBlock();
        outputStream.Flush();
        outputStream.Seek(0, SeekOrigin.Begin);
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="algorithm">对称算法</param>
    /// <param name="data">需要加密的字节数组</param>
    /// <returns>加密的字节数组</returns>
    public static byte[] Encrypt(this SymmetricAlgorithm algorithm, byte[] data)
    {
        if (data.IsNullOrEmpty()) throw new ArgumentNullException(nameof(data));

        using var inputStream = new MemoryStream(data);
        using var outputStream = new MemoryStream();
        algorithm.Encrypt(inputStream, outputStream);
        return outputStream.ToArray();
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="algorithm">对称算法</param>
    /// <param name="inputStream">需要加密的流</param>
    /// <param name="outputStream">加密的流</param>
    public static void Encrypt(this SymmetricAlgorithm algorithm, Stream inputStream, Stream outputStream)
    {
        using var transform = algorithm.CreateEncryptor();
        var cryptoStream = new CryptoStream(outputStream, transform, CryptoStreamMode.Write);
        var buffer = new byte[bufferSize];
        var length = -1;
        while ((length = inputStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            cryptoStream.Write(buffer, 0, length);
        }
        cryptoStream.FlushFinalBlock();
        outputStream.Flush();
        outputStream.Seek(0, SeekOrigin.Begin);
    }

    /// <summary>
    /// 设置对称算法的密钥和密钥长度,自动截取或补全密钥长度,步骤如下
    /// <para>1.循环允许的密钥长度[L1,L2,...,Ln]</para>
    /// <para>2.如果密钥长度等于Ln,直接返回密钥</para>
    /// <para>3.如果密钥长度小于Ln,返回[key,(补上足够的0字节)]</para>
    /// <para>4.如果不是最后一次循环,则进入下个循环,否则返回key[...Ln]</para>
    /// </summary>
    /// <param name="algorithm">对称算法</param>
    /// <param name="key">密钥</param>
    public static void SetKey(this SymmetricAlgorithm algorithm, byte[] key)
    {
        var allowedSize = algorithm.GetAllowedKeySize();
        var paddingKey = key.PaddingBytes(allowedSize, out var usedLength);
        algorithm.KeySize = usedLength * 8;
        algorithm.Key = paddingKey;
    }

    static int[] GetAllowedKeySize(this SymmetricAlgorithm algorithm)
    {
        if (algorithm is Aes) return new int[] { 16, 24, 32 };
        if (algorithm is DES) return new int[] { 8 };
        if (algorithm is TripleDES) return new int[] { 16, 24 };
        else throw new NotImplementedException($"algorithm '{algorithm.GetType().FullName}' not supported yet");
    }

    /// <summary>
    /// 设置对称算法的初始化向量,自动截取或补全密钥长度,步骤如下
    /// <para>1.设对称算法需要的初始化向量的长度为L</para>
    /// <para>2.如果密钥长度等于L,直接返回</para>
    /// <para>3.如果密钥长度小于L,返回[iv,(补上(L-iv.Length)个0字节)]</para>
    /// <para>4.返回iv[...L]</para>
    /// </summary>
    /// <param name="algorithm">对称算法</param>
    /// <param name="iv">初始化向量</param>
    public static void SetIV(this SymmetricAlgorithm algorithm, byte[] iv)
    {
        var size = algorithm.GetIVSize();
        algorithm.IV = iv.PaddingBytes(size);
    }

    static int GetIVSize(this SymmetricAlgorithm algorithm)
    {
        if (algorithm is Aes) return 16;
        if (algorithm is DES) return 8;
        if (algorithm is TripleDES) return 8;
        else throw new NotImplementedException($"algorithm '{algorithm.GetType().FullName}' not supported yet");
    }

    static byte[] PaddingBytes(this byte[] bytes, int length)
    {
        if (bytes.Length == length) return bytes;
        if (bytes.Length > length) return bytes.Take(length).ToArray();

        var paddingBytes = new byte[length - bytes.Length];
        return bytes.Concat(paddingBytes).ToArray();
    }

    static byte[] PaddingBytes(this byte[] bytes, int[] allowedLength, out int usedLength)
    {
        usedLength = bytes.Length;
        Array.Sort(allowedLength);
        for (int i = 0; i < allowedLength.Length; i++)
        {
            usedLength = allowedLength[i];
            if (bytes.Length == allowedLength[i]) return bytes;
            if (bytes.Length < allowedLength[i]) return bytes.PaddingBytes(allowedLength[i]);

            if (i == allowedLength.Length - 1) return bytes.Take(allowedLength[i]).ToArray();
        }
        return bytes;
    }
}