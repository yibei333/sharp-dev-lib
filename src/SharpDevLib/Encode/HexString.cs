using System.Text;

namespace SharpDevLib;

/// <summary>
/// 16进制编码扩展
/// </summary>
public static class HexString
{
    /// <summary>
    /// 将字节数组转换为16进制字符串
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>16进制字符串</returns>
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
    /// 将16进制字符串转换为字节数组
    /// </summary>
    /// <param name="hexString">16进制字符串</param>
    /// <returns>原始字节数组</returns>
    /// <exception cref="InvalidDataException">当解码失败是引发异常</exception>
    public static byte[] HexStringDecode(this string hexString)
    {
        if (hexString.IsNullOrWhiteSpace()) return Array.Empty<byte>();
        if (hexString.Length % 2 != 0) throw new InvalidDataException($"'{hexString}' is not a valid hex string");
        var list = new List<byte>();

        for (int i = 0; i < hexString.Length / 2; i++)
        {
            list.Add(Convert.ToByte(string.Join("", hexString.Skip(i * 2).Take(2)), 16));
        }
        return list.ToArray();
    }
}
