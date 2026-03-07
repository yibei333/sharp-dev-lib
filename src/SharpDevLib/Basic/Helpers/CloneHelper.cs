namespace SharpDevLib;

/// <summary>
/// 克隆扩展，提供对象的深拷贝功能
/// </summary>
public static class CloneHelper
{
    /// <summary>
    /// 通过序列化方式对对象进行深拷贝
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="source">需要拷贝的源对象</param>
    /// <returns>深拷贝后的新对象</returns>
    public static T DeepClone<T>(this T source) where T : class => source.Serialize().DeSerialize<T>();
}
