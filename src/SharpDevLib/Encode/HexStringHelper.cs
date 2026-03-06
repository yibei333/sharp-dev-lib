using System.Text;

namespace SharpDevLib;

/// <summary>
/// 16进制编码扩展，提供16进制格式的编码与解码功能
/// </summary>
public static class HexStringHelper
{
    /// <summary>
    /// 将字节数组编码为16进制字符串
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>16进制字符串，每个字节转换为两个16进制字符</returns>
    public static string HexStringEncode(this byte[] bytes)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// 将16进制字符串解码为字节数组
    /// </summary>
    /// <param name="hexString">16进制字符串</param>
    /// <returns>原始字节数组</returns>
    /// <exception cref="InvalidDataException">当16进制字符串长度为奇数时引发异常</exception>
    public static byte[] HexStringDecode(this string hexString)
    {
        if (hexString.IsNullOrWhiteSpace()) return [];
        if (hexString.Length % 2 != 0) throw new InvalidDataException($"'{hexString}' is not a valid hex string");
        var list = new List<byte>();

        for (int i = 0; i < hexString.Length / 2; i++)
        {
            list.Add(Convert.ToByte(string.Join("", hexString.Skip(i * 2).Take(2)), 16));
        }
        return [.. list];
    }
}
