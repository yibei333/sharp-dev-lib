using System.Reflection;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// 反射扩展
/// </summary>
public static class ReflectionExtension
{
    /// <summary>
    /// 获取类型定义名称(支持泛型)
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="isFullName">是否全名</param>
    /// <returns>名称</returns>
    public static string GetTypeDefinitionName(this Type type, bool isFullName = false)
    {
        if (!type.IsGenericType) return isFullName ? type.FullName ?? type.Name : type.Name;

        var names = new List<string>();
        foreach (var item in type.GetGenericArguments())
        {
            names.Add(GetTypeDefinitionName(item, isFullName));
        };
        var typeName = isFullName ? type.FullName ?? type.Name : type.Name;
        return $"{typeName.Split('`')[0]}<{string.Join(",", names)}>";
    }

    /// <summary>
    /// 获取对象类型定义名称(支持泛型)
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="isFullName">是否全名</param>
    /// <returns>名称</returns>
    public static string GetTypeDefinitionName(this object obj, bool isFullName = false) => obj?.GetType()?.GetTypeDefinitionName(isFullName) ?? string.Empty;

    /// <summary>
    /// 获取方法定义名称
    /// </summary>
    /// <param name="methodInfo">methodInfo</param>
    /// <param name="isParameterTypeFullName">参数类型是否全名</param>
    /// <returns>方法定义名称</returns>
    public static string GetMethodDefinitionName(this MethodInfo methodInfo, bool isParameterTypeFullName = false)
    {
        var builder = new StringBuilder();
        builder.Append(methodInfo.Name);
        if (methodInfo.IsGenericMethod)
        {
            builder.Append('<');
            builder.Append(string.Join(",", methodInfo.GetGenericArguments().Select(x => x.Name)));
            builder.Append('>');
        }
        builder.Append('(');
        var parameters = methodInfo.GetParameters();
        if (parameters.Any())
        {
            builder.Append(string.Join(",", methodInfo.GetParameters().Select(x => $"{x.ParameterType.GetTypeDefinitionName(isParameterTypeFullName)} {x.Name}")));
        }
        builder.Append(')');

        return builder.ToString();
    }
}
