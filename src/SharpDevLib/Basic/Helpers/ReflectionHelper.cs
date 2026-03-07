using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// 反射扩展，提供类型、方法和构造函数的反射信息获取功能
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// 获取类型定义名称，支持泛型类型但不支持嵌套类型
    /// </summary>
    /// <param name="type">要获取名称的类型</param>
    /// <param name="isFullName">是否返回类型的完全限定名，默认为false</param>
    /// <returns>类型定义名称字符串，泛型类型格式如"TypeName&lt;T1, T2&gt;"</returns>
    public static string GetTypeDefinitionName(this Type type, bool isFullName = false)
    {
        if (!type.IsGenericType) return type.GetTypeName(isFullName);

        var names = new List<string>();
        foreach (var item in type.GetGenericArguments())
        {
            names.Add(GetTypeDefinitionName(item, isFullName));
        }
        ;
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
    /// 获取对象的类型定义名称，支持泛型类型
    /// </summary>
    /// <param name="obj">要获取类型名称的对象</param>
    /// <param name="isFullName">是否返回类型的完全限定名，默认为false</param>
    /// <returns>类型定义名称字符串，如果对象为null则返回空字符串</returns>
    public static string GetTypeDefinitionName(this object obj, bool isFullName = false) => obj?.GetType()?.GetTypeDefinitionName(isFullName) ?? string.Empty;

    /// <summary>
    /// 获取方法定义名称
    /// </summary>
    /// <param name="methodInfo">方法信息对象</param>
    /// <param name="containParameterName">是否包含参数名称，默认为false</param>
    /// <param name="isFullName">是否使用类型的完全限定名，默认为false</param>
    /// <returns>方法定义名称字符串，格式如"MethodName&lt;T&gt;(ParamType1 paramName1, ParamType2 paramName2)"</returns>
    public static string GetMethodDefinitionName(this MethodInfo methodInfo, bool containParameterName, bool isFullName = false)
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

        if (methodInfo.CustomAttributes.Any(x => x.AttributeType == typeof(ExtensionAttribute)))
        {
            builder.Append("this ");
        }
        var parameters = methodInfo.GetParameters();
        if (parameters.Length != 0)
        {
            builder.Append(string.Join(", ", methodInfo.GetParameters().Select(x => $"{x.ParameterType.GetTypeDefinitionName(isFullName)}{(containParameterName ? $" {x.Name}" : "")}")));
        }
        builder.Append(')');

        return builder.ToString();
    }

    /// <summary>
    /// 获取构造函数定义名称
    /// </summary>
    /// <param name="constructorInfo">构造函数信息对象</param>
    /// <param name="containParameterName">是否包含参数名称，默认为false</param>
    /// <param name="isFullName">是否使用类型的完全限定名，默认为false</param>
    /// <returns>构造函数定义名称字符串，格式如"TypeName(ParamType1 paramName1, ParamType2 paramName2)"</returns>
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

