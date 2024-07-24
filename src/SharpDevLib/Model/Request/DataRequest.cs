namespace SharpDevLib;

/// <summary>
/// data requesst
/// </summary>
/// <typeparam name="TData">data type</typeparam>
public class DataRequest<TData> : BaseRequest
{
    /// <summary>
    /// 实例化data requesst
    /// </summary>
    public DataRequest()
    {
    }

    /// <summary>
    /// 实例化data requesst
    /// </summary>
    /// <param name="data">data</param>
    public DataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// data
    /// </summary>
    public TData? Data { get; set; }
}