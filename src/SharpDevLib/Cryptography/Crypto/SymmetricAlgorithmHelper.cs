using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// 对称加密算法扩展
/// </summary>
public static class SymmetricAlgorithmHelper
{
    const int bufferSize = 4096;

    /// <summary>
    /// 将加密的字节数组解密为原始数据
    /// </summary>
    /// <param name="algorithm">对称加密算法示例</param>
    /// <param name="data">已加密的字节数组</param>
    /// <returns>解密后的原始字节数组</returns>
    /// <exception cref="ArgumentNullException">当data参数为null或空数组时抛出</exception>
    public static byte[] Decrypt(this SymmetricAlgorithm algorithm, byte[] data)
    {
        if (data.IsNullOrEmpty()) throw new ArgumentNullException(nameof(data));

        using var inputStream = new MemoryStream(data);
        using var outputStream = new MemoryStream();
        algorithm.Decrypt(inputStream, outputStream);
        return outputStream.ToArray();
    }

    /// <summary>
    /// 将加密的流解密到目标流
    /// </summary>
    /// <param name="algorithm">对称加密算法示例</param>
    /// <param name="inputStream">已加密的输入流</param>
    /// <param name="outputStream">解密后的输出流</param>
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
    /// 将原始字节数组加密为密文
    /// </summary>
    /// <param name="algorithm">对称加密算法示例</param>
    /// <param name="data">需要加密的原始字节数组</param>
    /// <returns>加密后的密文字节数组</returns>
    /// <exception cref="ArgumentNullException">当data参数为null或空数组时抛出</exception>
    public static byte[] Encrypt(this SymmetricAlgorithm algorithm, byte[] data)
    {
        if (data.IsNullOrEmpty()) throw new ArgumentNullException(nameof(data));

        using var inputStream = new MemoryStream(data);
        using var outputStream = new MemoryStream();
        algorithm.Encrypt(inputStream, outputStream);
        return outputStream.ToArray();
    }

    /// <summary>
    /// 将原始流加密到目标流
    /// </summary>
    /// <param name="algorithm">对称加密算法示例</param>
    /// <param name="inputStream">需要加密的原始输入流</param>
    /// <param name="outputStream">加密后的输出流</param>
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
    /// 设置对称算法的密钥，自动截取或补全密钥长度以符合算法要求
    /// <para>处理步骤：</para>
    /// <para>1.循环检查允许的密钥长度[L1, L2, ..., Ln]</para>
    /// <para>2.如果密钥长度等于Li，直接使用该密钥</para>
    /// <para>3.如果密钥长度小于Li，返回[key + 补零字节]</para>
    /// <para>4.如果不是最后一次循环，则进入下一轮，否则返回key的前Li个字节</para>
    /// <para>支持算法：AES(16/24/32字节)、DES(8字节)、TripleDES(16/24字节)</para>
    /// </summary>
    /// <param name="algorithm">对称加密算法示例</param>
    /// <param name="key">要设置的密钥字节数组</param>
    /// <exception cref="NotImplementedException">当算法不支持时抛出</exception>
    public static void SetKeyAutoPad(this SymmetricAlgorithm algorithm, byte[] key)
    {
        var allowedSize = algorithm.GetAllowedKeySize();
        var paddingKey = key.PaddingBytes(allowedSize, out var usedLength);
        algorithm.KeySize = usedLength * 8;
        algorithm.Key = paddingKey;
    }

    static int[] GetAllowedKeySize(this SymmetricAlgorithm algorithm)
    {
        if (algorithm is Aes) return [16, 24, 32];
        if (algorithm is DES) return [8];
        if (algorithm is TripleDES) return [16, 24];
        else throw new NotImplementedException($"暂不支持的算法: '{algorithm.GetType().FullName}'");
    }

    /// <summary>
    /// 设置对称算法的初始化向量(IV)，自动截取或补全长度以符合算法要求
    /// <para>处理步骤：</para>
    /// <para>1.设对称算法需要的初始化向量长度为L</para>
    /// <para>2.如果IV长度等于L，直接使用该IV</para>
    /// <para>3.如果IV长度小于L，返回[iv + 补零字节]</para>
    /// <para>4.如果IV长度大于L，返回iv的前L个字节</para>
    /// <para>支持算法：AES(16字节)、DES(8字节)、TripleDES(8字节)</para>
    /// </summary>
    /// <param name="algorithm">对称加密算法示例</param>
    /// <param name="iv">要设置的初始化向量字节数组</param>
    /// <exception cref="NotImplementedException">当算法不支持时抛出</exception>
    public static void SetIVAutoPad(this SymmetricAlgorithm algorithm, byte[] iv)
    {
        var size = algorithm.GetIVSize();
        algorithm.IV = iv.PaddingBytes(size);
    }

    static int GetIVSize(this SymmetricAlgorithm algorithm)
    {
        if (algorithm is Aes) return 16;
        if (algorithm is DES) return 8;
        if (algorithm is TripleDES) return 8;
        else throw new NotImplementedException($"暂不支持的算法: '{algorithm.GetType().FullName}'");
    }

    static byte[] PaddingBytes(this byte[] bytes, int length)
    {
        if (bytes.Length == length) return bytes;
        if (bytes.Length > length) return [.. bytes.Take(length)];

        var paddingBytes = new byte[length - bytes.Length];
        return [.. bytes, .. paddingBytes];
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

            if (i == allowedLength.Length - 1) return [.. bytes.Take(allowedLength[i])];
        }
        return bytes;
    }
}