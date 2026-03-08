using System.Security.Cryptography;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// 随机数类型
/// </summary>
public enum RandomType
{
    /// <summary>
    /// 纯数字
    /// </summary>
    Number,
    /// <summary>
    /// 小写字母a-z
    /// </summary>
    LetterLower,
    /// <summary>
    /// 大写字母A-Z
    /// </summary>
    LetterUpper,
    /// <summary>
    /// 字母a-z和A-Z
    /// </summary>
    Letter,
    /// <summary>
    /// 数字+字母
    /// </summary>
    NumberAndLetter,
    /// <summary>
    /// 混合,数字+字母+特殊字符
    /// </summary>
    Mix
}

/// <summary>
/// 随机数扩展，提供随机码生成功能
/// </summary>
public static class RandomHelper
{
    /// <summary>
    /// 生成随机码
    /// </summary>  
    /// <param name="type">类型</param>
    /// <param name="length">长度</param>
    /// <returns>随机码</returns>
    public static string GenerateCode(this RandomType type, byte length)
    {
        if (length == 0) return string.Empty;
        string chars = GetCharSet(type);
        int max = chars.Length;

        var result = new StringBuilder(length);
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] buffer = new byte[4];
            // 计算用于拒绝采样的最大值，确保均匀分布
            int maxValid = int.MaxValue - (int.MaxValue % max) - 1;

            for (int i = 0; i < length; i++)
            {
                int randomValue;
                do
                {
                    rng.GetBytes(buffer);
                    randomValue = BitConverter.ToInt32(buffer, 0) & int.MaxValue;
                } while (randomValue > maxValid);

                int index = randomValue % max;
                result.Append(chars[index]);
            }
        }

        return result.ToString();
    }

    static string GetCharSet(RandomType type)
    {
        return type switch
        {
            RandomType.Number => "0123456789",
            RandomType.LetterLower => "abcdefghijklmnopqrstuvwxyz",
            RandomType.LetterUpper => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            RandomType.Letter => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
            RandomType.NumberAndLetter => "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
            RandomType.Mix => "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=[]{}|;:,.<>?",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}