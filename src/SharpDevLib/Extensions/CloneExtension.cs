namespace SharpDevLib;

/// <summary>
/// 克隆扩展
/// </summary>
public static class CloneExtension
{
    /// <summary>
    /// 通过序列化的方式深拷贝对象
    /// </summary>
    /// <typeparam name="T">拷贝对象泛型类型</typeparam>
    /// <param name="source">需要拷贝的对象</param>
    /// <returns>深拷贝对象结果</returns>
    public static T DeepClone<T>(this T source) where T : class => source.Serialize().DeSerialize<T>();
}
