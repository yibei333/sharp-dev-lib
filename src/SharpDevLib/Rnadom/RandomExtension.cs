using System.Text;

namespace SharpDevLib;

/// <summary>
/// 随机扩展
/// </summary>
public static class RandomExtension
{
    /// <summary>
    /// 生成随机码
    /// </summary>
    /// <param name="random">random</param>
    /// <param name="option">选项</param>
    /// <returns>随机码</returns>
    /// <exception cref="ArgumentException">当输出长度小于等于0或者种子数据为空时引发异常</exception>
    public static string GenerateCode(this Random random, GenerateRandomCodeOption? option = null)
    {
        var generateOption = option ?? new GenerateRandomCodeOption();
        var seed = generateOption.Seed;
        if (generateOption.Length <= 0) throw new ArgumentException($"length should greater than zero");
        if (seed is null || seed.Length <= 0) throw new ArgumentException($"seed data requires at least one character");
        var builder = new StringBuilder();
        for (int i = 0; i < generateOption.Length; i++)
        {
            builder.Append(seed[random.Next(0, seed.Length)]);
        }
        return builder.ToString();
    }
}