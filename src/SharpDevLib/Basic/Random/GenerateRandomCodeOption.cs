namespace SharpDevLib;

/// <summary>
/// 生成随机码选项
/// </summary>
public class GenerateRandomCodeOption
{
    /// <summary>
    /// 获取默认的随机码生成选项实例
    /// </summary>
    public static GenerateRandomCodeOption Default { get; set; } = new();

    /// <summary>
    /// 长度，默认为6
    /// </summary>
    public int Length { get; set; } = 6;

    /// <summary>
    /// 字符种子数据，包含所有可用于生成随机码的字符
    /// </summary>
    /// <remarks>默认包含数字、小写字母、大写字母及特殊符号</remarks>
    public string Seed { get; set; } = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+{}:<>?.,/';\"[]\\|-=`";
}