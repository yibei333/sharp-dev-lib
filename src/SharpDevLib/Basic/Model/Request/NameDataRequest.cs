namespace SharpDevLib;

/// <summary>
/// 包含名称和数据的复合请求对象
/// 继承自<see cref="DataRequest{TData}"/>,在标识符和数据基础上增加了名称字段,适用于需要同时传输名称和数据的请求场景
/// </summary>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class NameDataRequest<TData> : DataRequest<TData>
{
    /// <summary>
    /// 示例化 NameDataRequest 对象
    /// </summary>
    public NameDataRequest()
    {
    }

    /// <summary>
    /// 示例化 NameDataRequest 对象并初始化名称和数据
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="data">要包装的数据对象</param>
    public NameDataRequest(string? name, TData? data)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// 名称,用于描述或标识数据对象
    /// </summary>
    public string? Name { get; set; }
}