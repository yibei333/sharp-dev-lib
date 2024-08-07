namespace SharpDevLib;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
internal class BelongDirectoryAttribute(string path) : Attribute
{
    public string Path { get; } = path;
}
