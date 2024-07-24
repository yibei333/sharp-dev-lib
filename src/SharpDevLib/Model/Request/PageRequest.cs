namespace SharpDevLib;

/// <summary>
/// 分页request
/// </summary>
public class PageRequest : BaseRequest
{
    /// <summary>
    /// 实例化分页request
    /// </summary>
    public PageRequest() : this(0, 20)
    {
    }

    /// <summary>
    /// 实例化分页request
    /// </summary>
    /// <param name="index">索引(当前位置),默认为1</param>
    /// <param name="size">每页数据条数</param>
    /// <exception cref="ArgumentException">index和size需要大于等于0,否则引发异常</exception>
    public PageRequest(int index, int size)
    {
        if (index < 0) throw new ArgumentException("index must greater than equal 0");
        if (size < 0) throw new ArgumentException("index must greater than equal 0");

        Index = index;
        Size = size;
    }

    /// <summary>
    /// 索引(当前位置),默认为1
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 每页数据条数
    /// </summary>
    public int Size { get; set; }
}