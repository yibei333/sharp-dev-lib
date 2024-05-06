using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib.Standard;

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
    /// <param name="throwIfNull">当需要拷贝的对象为空时是否抛出异常</param>
    /// <returns>深拷贝对象结果</returns>
    public static T? DeepClone<T>(this T? source, [NotNullWhen(true)] bool throwIfNull = true) where T : class
    {
        if (source is null)
        {
            if (throwIfNull) throw new ArgumentNullException(nameof(source));
            return null;
        }
        return source.Serialize().DeSerialize<T>();
    }
}
