using System.Reflection;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// 反射扩展
/// </summary>
[BelongDirectory("Extensions")]
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
        if (!type.IsGenericType) return type.GetTypeName(isFullName);

        var names = new List<string>();
        foreach (var item in type.GetGenericArguments())
        {
            names.Add(GetTypeDefinitionName(item, isFullName));
        };
        var typeName = type.GetTypeName(isFullName);
        return $"{typeName.Split('`')[0]}<{string.Join(", ", names)}>";
    }

    static string GetTypeName(this Type type, bool isFullName)
    {
        if (type.IsGenericParameter) return type.Name;
        if (!isFullName) return type.Name;
        var typeName = type.FullName;
        if (typeName.IsNullOrWhiteSpace())
        {
            typeName = $"{type.Namespace}.{type.Name}";
        }
        return typeName;
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
    /// <param name="isFullName">是否全名</param>
    /// <returns>方法定义名称</returns>
    public static string GetMethodDefinitionName(this MethodInfo methodInfo, bool isFullName = false)
    {
        var builder = new StringBuilder();
        builder.Append(methodInfo.Name);
        if (methodInfo.IsGenericMethod)
        {
            builder.Append('<');
            builder.Append(string.Join(", ", methodInfo.GetGenericArguments().Select(x => x.Name)));
            builder.Append('>');
        }
        builder.Append('(');
        var parameters = methodInfo.GetParameters();
        if (parameters.Length != 0)
        {
            builder.Append(string.Join(", ", methodInfo.GetParameters().Select(x => $"{x.ParameterType.GetTypeDefinitionName(isFullName)} {x.Name}")));
        }
        builder.Append(')');

        return builder.ToString();
    }

    /// <summary>
    /// 获取构造方法定义名称
    /// </summary>
    /// <param name="constructorInfo">constructorInfo</param>
    /// <param name="containParameterName">是否包含参数名称</param>
    /// <param name="isFullName">是否全名</param>
    /// <returns>构造方法定义名称</returns>
    public static string GetConstructorDefinitionName(this ConstructorInfo constructorInfo, bool containParameterName, bool isFullName = false)
    {
        var builder = new StringBuilder();
        builder.Append(constructorInfo.DeclaringType?.Name?.Split('`')[0]);
        builder.Append('(');
        var parameters = constructorInfo.GetParameters();
        if (parameters.Length != 0)
        {
            builder.Append(string.Join(", ", parameters.Select(x => $"{x.ParameterType.GetTypeDefinitionName(isFullName)}{(containParameterName ? $" {x.Name}" : "")}")));
        }
        builder.Append(')');

        return builder.ToString();
    }
}

