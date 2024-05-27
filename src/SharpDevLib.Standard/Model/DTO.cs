namespace SharpDevLib.Standard;

/// <summary>
/// dto基类,或许可以在反射或者泛型中用
/// </summary>
public class BaseDto
{
}

/// <summary>
/// id dto
/// </summary>
public class IdDto : BaseDto
{
    /// <summary>
    /// 实例化id dto
    /// </summary>
    public IdDto()
    {

    }

    /// <summary>
    /// 实例化id dto
    /// </summary>
    /// <param name="id">id</param>
    public IdDto(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }
}

/// <summary>
/// id dto
/// </summary>
public class IdDto<TId> : BaseDto
{
    /// <summary>
    /// 实例化id dto
    /// </summary>
    public IdDto()
    {

    }

    /// <summary>
    /// 实例化id dto
    /// </summary>
    /// <param name="id">id</param>
    public IdDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
}

/// <summary>
/// name dto
/// </summary>
public class NameDto : BaseDto
{
    /// <summary>
    /// 实例化name dto
    /// </summary>
    public NameDto()
    {

    }

    /// <summary>
    /// 实例化name dto
    /// </summary>
    /// <param name="name">name</param>
    public NameDto(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// id name dto
/// </summary>
public class IdNameDto : IdDto
{
    /// <summary>
    /// 实例化id name dto
    /// </summary>
    public IdNameDto()
    {

    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDto(Guid id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDto(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDto(Guid id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// id name dto
/// </summary>
public class IdNameDto<TId> : IdDto<TId>
{
    /// <summary>
    /// 实例化id name dto
    /// </summary>
    public IdNameDto()
    {

    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDto(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDto(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDto(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// data dto
/// </summary>
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

/// <summary>
/// id data dto
/// </summary>
public class IdDataDto<TData> : DataDto<TData>
{
    /// <summary>
    /// 实例化id data dto
    /// </summary>
    public IdDataDto()
    {
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="data">data</param>
    public IdDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="id">id</param>
    public IdDataDto(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdDataDto(Guid id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }
}

/// <summary>
/// id data dto
/// </summary>
public class IdDataDto<TId, TData> : DataDto<TData>
{
    /// <summary>
    /// 实例化id data dto
    /// </summary>
    public IdDataDto()
    {
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="data">data</param>
    public IdDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="id">id</param>
    public IdDataDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdDataDto(TId id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
}

/// <summary>
/// id name data dto
/// </summary>
public class IdNameDataDto<TData> : DataDto<TData>
{
    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    public IdNameDataDto()
    {
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="data">data</param>
    public IdNameDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDataDto(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDataDto(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdNameDataDto(Guid id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDataDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataDto(string name, TData? data) : base(data)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataDto(Guid id, string name, TData? data) : base(data)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// id name data dto
/// </summary>
public class IdNameDataDto<TId, TData> : DataDto<TData>
{
    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    public IdNameDataDto()
    {
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="data">data</param>
    public IdNameDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDataDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDataDto(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdNameDataDto(TId id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDataDto(TId id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataDto(string name, TData? data) : base(data)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataDto(TId id, string name, TData? data) : base(data)
    {
        Id = id;
        Name = name;
    }
    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}