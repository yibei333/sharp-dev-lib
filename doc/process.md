# 进程操作

SharpDevLib 提供了进程启动、执行和管理功能。

##### 简单示例

```csharp
//启动并等待执行完毕
var result = await ProcessHelper.StartAndWaitForExitAsync("cmd.exe", "/c echo Hello");
Console.WriteLine(result.Output);
//Hello

//不等待
ProcessHelper.Start("cmd.exe", "/c echo Hello");
```

##### 使用Process对象启动

```csharp
var process = new Process();
var result = await process.StartAndWaitForExitAsync("cmd.exe", "/c echo Hello");
Console.WriteLine(result.Output);
//Hello
```

## 相关文档

- [基础扩展](../README.md#基础扩展)
