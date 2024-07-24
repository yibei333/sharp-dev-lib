namespace SharpDevLib;

/// <summary>
/// data dto
/// </summary>
/// <typeparam name="TData">data type</typeparam>
public class DataDto<TData> : BaseDto
{
    /// <summary>
    /// 实例化data dto
    /// </summary>
    public DataDto()
    {
    }

    /// <summary>
    /// 实例化data dto
    /// </summary>
    /// <param name="data">data</param>
    public DataDto(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// data
    /// </summary>
    public TData? Data { get; set; }
}