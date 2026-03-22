namespace SharpDevLib;

/// <summary>
/// 包含数据的响应对象
/// 继承自 BaseReply,在基础响应字段上增加了数据字段,适用于需要返回具体数据的场景
/// </summary>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class DataReply<TData> : BaseReply
{
    /// <summary>
    /// 数据对象,用于承载具体的业务数据
    /// </summary>
    public TData? Data { get; set; }
}

/// <summary>
/// 数据响应的静态工厂类,提供便捷的响应构建方法
/// </summary>
public static class DataReply
{
    /// <summary>
    /// 构建成功的数据响应
    /// </summary>
    /// <typeparam name="TData">数据类型</typeparam>
    /// <param name="data">要返回的数据对象</param>
    /// <param name="description">可选的成功描述信息</param>
    /// <returns>Success 为 true 且包含数据的数据响应对象</returns>
    public static DataReply<TData> Succeed<TData>(TData data, string? description = null) => new() { Success = true, Message = description, Data = data };
    /// <summary>
    /// 构建失败的数据响应
    /// </summary>
    /// <typeparam name="TData">数据类型</typeparam>
    /// <param name="description">可选的失败描述信息</param>
    /// <returns>Success 为 false 的数据响应对象</returns>
    public static DataReply<TData> Failed<TData>(string? description = null) => new() { Success = false, Message = description };
}