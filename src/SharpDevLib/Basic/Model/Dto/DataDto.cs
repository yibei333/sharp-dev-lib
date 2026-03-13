namespace SharpDevLib;

/// <summary>
/// 包含数据的数据传输对象
/// 用于包装单个数据对象进行传输,适用于 API 响应或方法返回值
/// </summary>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class DataDto<TData> : BaseDto
{
    /// <summary>
    /// 示例化 DataDto 对象
    /// </summary>
    public DataDto()
    {
    }

    /// <summary>
    /// 示例化 DataDto 对象并初始化数据
    /// </summary>
    /// <param name="data">要包装的数据对象</param>
    public DataDto(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 数据对象
    /// </summary>
    public TData? Data { get; set; }
}