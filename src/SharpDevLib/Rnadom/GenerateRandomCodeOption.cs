namespace SharpDevLib;

/// <summary>
/// 生成随机码选项
/// </summary>
public class GenerateRandomCodeOption
{
    /// <summary>
    /// 长度，默认为6
    /// </summary>
    public int Length { get; set; } = 6;

    /// <summary>
    /// 数字种子数据
    /// </summary>
    public const string NumberSeed = "0123456789";

    /// <summary>
    /// 是否生成带数字的随机码,默认为true
    /// </summary>
    public bool UseNumber { get; set; } = true;

    /// <summary>
    /// 小写字母种子数据
    /// </summary>
    public const string LowerLetterSeed = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// 是否生成带小写字母的随机码,默认为true
    /// </summary>
    public bool UseLowerLetter { get; set; } = true;

    /// <summary>
    /// 大写字母种子数据
    /// </summary>
    public const string UpperLetterSeed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// 是否生成带大写字母的随机码,默认为true
    /// </summary>
    public bool UseUpperLetter { get; set; } = true;

    /// <summary>
    /// 特殊字符种子数据
    /// </summary>
    public const string SpecialSymbolSeed = "~!@#$%^&*()_+{}:<>?.,/';\"[]\\|-=`";

    /// <summary>
    /// 是否生成带特殊字符的随机码,默认为false
    /// </summary>
    public bool UseSpecialSymbol { get; set; }

    /// <summary>
    /// 自定义种子数据
    /// </summary>
    public string? CustomSeed { get; set; }

    /// <summary>
    /// 是否生成自定义字符的随机码,默认为false,如果为true,UseNumber、UseLowerLetter、UseUpperLetter、UseSpecialSymbol将忽略完全按照自定义字符生成
    /// </summary>
    public bool UseCustomSeed { get; set; }

    /// <summary>
    /// 种子数据
    /// </summary>
    public string Seed
    {
        get
        {
            if (UseCustomSeed) return CustomSeed ?? string.Empty;
            string result = string.Empty;
            if (UseNumber) result += NumberSeed;
            if (UseLowerLetter) result += LowerLetterSeed;
            if (UseUpperLetter) result += UpperLetterSeed;
            if (UseSpecialSymbol) result += SpecialSymbolSeed;
            return result;
        }
    }
}