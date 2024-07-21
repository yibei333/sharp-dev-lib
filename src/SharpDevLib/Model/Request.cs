namespace SharpDevLib;

/// <summary>
/// request基类,或许可以在反射或者泛型中用
/// </summary>
public class BaseRequest
{
}

/// <summary>
/// id request
/// </summary>
public class IdRequest : BaseRequest
{
    /// <summary>
    /// 实例化id request
    /// </summary>
    public IdRequest()
    {
    }

    /// <summary>
    /// 实例化id request
    /// </summary>
    /// <param name="id">id</param>
    public IdRequest(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }
}

/// <summary>
/// id request
/// </summary>
public class IdRequest<TId> : BaseRequest
{
    /// <summary>
    /// 实例化id request
    /// </summary>
    public IdRequest()
    {
    }

    /// <summary>
    /// 实例化id request
    /// </summary>
    /// <param name="id">id</param>
    public IdRequest(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
}

/// <summary>
/// name request
/// </summary>
public class NameRequest : BaseRequest
{
    /// <summary>
    /// 实例化name request
    /// </summary>
    public NameRequest()
    {
    }

    /// <summary>
    /// 实例化name request
    /// </summary>
    /// <param name="name">name</param>
    public NameRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// id name request
/// </summary>
public class IdNameRequest : IdRequest
{
    /// <summary>
    /// 实例化id name request
    /// </summary>
    public IdNameRequest()
    {
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="id">id</param>
    public IdNameRequest(Guid id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="name">name</param>
    public IdNameRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameRequest(Guid id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// id name request
/// </summary>
public class IdNameRequest<TId> : IdRequest<TId>
{
    /// <summary>
    /// 实例化id name request
    /// </summary>
    public IdNameRequest()
    {
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="id">id</param>
    public IdNameRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="name">name</param>
    public IdNameRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameRequest(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// data requesst
/// </summary>
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

/// <summary>
/// id data requesst
/// </summary>
public class IdDataRequest<TData> : IdRequest
{
    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    public IdDataRequest()
    {
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="data">data</param>
    public IdDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="id">id</param>
    public IdDataRequest(Guid id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdDataRequest(Guid id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// data
    /// </summary>
    public TData? Data { get; set; }
}

/// <summary>
/// id data requesst
/// </summary>
public class IdDataRequest<TId, TData> : IdRequest<TId>
{
    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    public IdDataRequest()
    {
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="data">data</param>
    public IdDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="id">id</param>
    public IdDataRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdDataRequest(TId id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// data
    /// </summary>
    public TData? Data { get; set; }
}

/// <summary>
/// id name data request
/// </summary>
public class IdNameDataRequest<TData> : IdDataRequest<TData>
{
    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    public IdNameDataRequest()
    {
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="data">data</param>
    public IdNameDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDataRequest(Guid id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDataRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(Guid id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDataRequest(Guid id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(string? name, TData? data)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(Guid id, string? name, TData? data) : base(id)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// id name data request
/// </summary>
public class IdNameDataRequest<TId, TData> : IdDataRequest<TId, TData>
{
    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    public IdNameDataRequest()
    {
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="data">data</param>
    public IdNameDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDataRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDataRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(TId id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDataRequest(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(string? name, TData? data)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(TId id, string? name, TData? data) : base(id)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

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
    public PageRequest(int index, int size)
    {
        if (index < 0) throw new Exception("index must greater than equal 0");
        if (size < 0) throw new Exception("index must greater than equal 0");

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