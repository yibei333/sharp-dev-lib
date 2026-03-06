namespace SharpDevLib;

/// <summary>
/// 生成随机码选项
/// </summary>
public class GenerateRandomCodeOption
{
    /// <summary>
    /// 默认
    /// </summary>
    public static GenerateRandomCodeOption Default { get; set; } = new();

    /// <summary>
    /// 长度，默认为6
    /// </summary>
    public int Length { get; set; } = 6;

    /// <summary>
    /// 数字种子数据
    /// </summary>
    public string Seed { get; set; } = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+{}:<>?.,/';\"[]\\|-=`";
}