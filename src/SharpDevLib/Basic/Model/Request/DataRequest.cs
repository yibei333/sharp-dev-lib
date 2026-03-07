namespace SharpDevLib;

/// <summary>
/// 包含数据的请求对象
/// 用于包装单个数据对象进行传输,适用于 API 请求或方法参数
/// </summary>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class DataRequest<TData> : BaseRequest
{
    /// <summary>
    /// 实例化 DataRequest 对象
    /// </summary>
    public DataRequest()
    {
    }

    /// <summary>
    /// 实例化 DataRequest 对象并初始化数据
    /// </summary>
    /// <param name="data">要包装的数据对象</param>
    public DataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 数据对象
    /// </summary>
    public TData? Data { get; set; }
}