namespace SharpDevLib;

/// <summary>
/// 分页请求对象
/// 继承自 BaseRequest,用于分页查询请求场景,包含当前页索引和每页数据条数
/// </summary>
public class PageRequest : BaseRequest
{
    /// <summary>
    /// 示例化 PageRequest 对象,使用默认参数(index=0, size=20)
    /// </summary>
    public PageRequest() : this(0, 20)
    {
    }

    /// <summary>
    /// 示例化 PageRequest 对象
    /// </summary>
    /// <param name="index">当前页索引,从 0 开始,默认为 0</param>
    /// <param name="size">每页数据条数,必须大于等于 0</param>
    /// <exception cref="ArgumentException">当 index 或 size 小于 0 时引发异常</exception>
    public PageRequest(int index, int size)
    {
        if (index < 0) throw new ArgumentException("index must greater than equal 0");
        if (size < 0) throw new ArgumentException("index must greater than equal 0");

        Index = index;
        Size = size;
    }

    /// <summary>
    /// 当前页索引,从 0 开始
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 每页数据条数
    /// </summary>
    public int Size { get; set; }
}