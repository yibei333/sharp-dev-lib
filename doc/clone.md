# 克隆操作

SharpDevLib 提供了对象的深拷贝功能。

##### 对象深拷贝

```csharp
var original = new IdNameDto<int>(25, "Alice");
var cloned = original.DeepClone();
Console.WriteLine(ReferenceEquals(original, cloned));
//False

cloned.Name = "Bob";
cloned.Id = 30;

Console.WriteLine(original.Serialize());
//{"Name":"Alice","Id":25}
Console.WriteLine(cloned.Serialize());
//{"Name":"Bob","Id":30}
```

## 注意事项

- 深拷贝使用序列化和反序列化实现，要求对象必须可序列化
- 对于复杂对象图，深拷贝可能会产生性能开销
- 确保对象的所有属性都是可序列化的

## 相关文档

- [JSON 操作](json.md)
- [基础扩展](../README.md#基础扩展)
