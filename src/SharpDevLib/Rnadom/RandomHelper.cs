using System.Text;

namespace SharpDevLib;

/// <summary>
/// 随机数扩展，提供随机码生成功能
/// </summary>
public static class RandomHelper
{
    /// <summary>
    /// 生成指定长度和字符集的随机码
    /// </summary>
    /// <param name="random">随机数生成器实例</param>
    /// <param name="option">随机码生成选项，包含长度和字符种子。若为null则使用默认选项</param>
    /// <returns>生成的随机码字符串</returns>
    /// <exception cref="ArgumentException">当输出长度小于等于0时引发异常</exception>
    /// <exception cref="ArgumentException">当种子数据为空或长度小于等于0时引发异常</exception>
    public static string GenerateCode(this Random random, GenerateRandomCodeOption? option = null)
    {
        var generateOption = option ?? GenerateRandomCodeOption.Default;
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