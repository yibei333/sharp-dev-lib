# 进程操作

SharpDevLib 提供了进程启动、执行和管理功能。

## 启动进程

##### 简单启动

```csharp
var request = new ProcessStartRequest("cmd.exe", "/c echo Hello");
var result = await request.StartAndWaitForExitAsync();
Console.WriteLine(result.Output);
//Hello
```

##### 设置工作目录

```csharp
var request = new ProcessStartRequest("cmd.exe", "/c dir")
{
    WorkingDirectory = @"C:\"
};
var result = await request.StartAndWaitForExitAsync();
Console.WriteLine(result.Output);
```

##### 使用取消令牌

```csharp
var cts = new CancellationTokenSource();
cts.CancelAfter(5000);
var request = new ProcessStartRequest("cmd.exe", "/c timeout 10")
{
    CancellationToken = cts.Token
};
var result = await request.StartAndWaitForExitAsync();
Console.WriteLine(result.IsSuccess);
// False
result.EnsureSucceed();
// 抛出异常
```

## 日志记录

##### 记录信息日志

```csharp
var request = new ProcessStartRequest("cmd.exe", "/c echo Hello")
{
    LogInfo = true
};
var result = await request.StartAndWaitForExitAsync();
// 输出日志: Hello
```

##### 记录错误日志

```csharp
var request = new ProcessStartRequest("cmd.exe", "/c invalid-command")
{
    LogError = true
};
var result = await request.StartAndWaitForExitAsync();
// 输出错误日志
```

## 结果处理

##### 检查执行状态

```csharp
var request = new ProcessStartRequest("cmd.exe", "/c echo Hello");
var result = await request.StartAndWaitForExitAsync();

Console.WriteLine(result.IsSuccess);
//True

Console.WriteLine(result.ExitCode);
//0
```

##### 获取输出内容

```csharp
var request = new ProcessStartRequest("cmd.exe", "/c echo Hello");
var result = await request.StartAndWaitForExitAsync();

Console.WriteLine(result.Output);
//Hello

Console.WriteLine(result.Error);
```

##### 确保成功

```csharp
var request = new ProcessStartRequest("cmd.exe", "/c echo Hello");
var result = await request.StartAndWaitForExitAsync();

result.EnsureSucceed();
// 成功时继续执行

var request2 = new ProcessStartRequest("cmd.exe", "/c invalid-command");
var result2 = await request2.StartAndWaitForExitAsync();
result2.EnsureSucceed();
// 失败时抛出异常
```

## 自定义配置

##### 设置全局日志

```csharp
using Microsoft.Extensions.Logging;

ProcessHelper.Logger = new MyCustomLogger();
```

##### 使用Process对象启动

```csharp
var process = new Process();
var request = new ProcessStartRequest("cmd.exe", "/c echo Hello");
var result = await process.StartAndWaitForExitAsync(request);
Console.WriteLine(result.Output);
//Hello
```

## 相关文档

- [基础扩展](../README.md#基础扩展)
