# 进程操作

SharpDevLib 提供了进程启动、执行和管理功能。

## 执行命令

### RunCommand

```csharp
// 执行命令
var result = "dir".RunCommand();
Console.WriteLine(result.Output);

if (result.ExitCode != 0)
{
    Console.WriteLine($"错误: {result.Error}");
}
```

### 带配置执行命令

```csharp
var result = new ProcessStartRequest
{
    Filename = "cmd.exe",
    Args = "/c echo Hello World",
    WorkingDirectory = "C:\\",
    LogInfo = true
}.RunCommand();
```

### 异步执行命令

```csharp
var request = new ProcessStartRequest
{
    Filename = "cmd.exe",
    Args = "/c dir",
    LogInfo = true
};

var result = await request.RunCommandAsync();
```

## 进程管理

### StartAndWaitForExitAsync

```csharp
var process = new Process();

var request = new ProcessStartRequest
{
    Filename = "notepad.exe",
    Args = "test.txt",
    WorkingDirectory = "C:\\Temp"
};

var result = await process.StartAndWaitForExitAsync(request);
Console.WriteLine($"退出码: {result.ExitCode}");
```

## 进程检查

### IsProcessRunning

```csharp
// 检查进程是否存在
var exists = "notepad".IsProcessRunning();
```

### GetProcess

```csharp
// 获取进程
var process = "notepad".GetProcess();

if (process != null)
{
    Console.WriteLine($"进程 ID: {process.Id}");
    Console.WriteLine($"进程名称: {process.ProcessName}");
}
```

## ProcessStartRequest 配置

### 基本配置

```csharp
var request = new ProcessStartRequest
{
    Filename = "cmd.exe",
    Args = "/c echo Hello",
    WorkingDirectory = "C:\\",
    LogInfo = true
};
```

### 带取消令牌

```csharp
var cts = new CancellationTokenSource();

var request = new ProcessStartRequest
{
    Filename = "long-running-command.exe",
    Args = "--arg1 --arg2",
    CancellationToken = cts.Token
};

// 5 秒后取消
cts.CancelAfter(TimeSpan.FromSeconds(5));
```

### 不创建窗口

```csharp
var request = new ProcessStartRequest
{
    Filename = "background-process.exe",
    CreateNoWindow = true
};
```

## ProcessResult

### 读取结果

```csharp
var result = "dir".RunCommand();

// 退出码
Console.WriteLine($"退出码: {result.ExitCode}");

// 标准输出
Console.WriteLine($"输出:\n{result.Output}");

// 错误输出
if (!result.Error.IsNullOrWhiteSpace())
{
    Console.WriteLine($"错误:\n{result.Error}");
}

// 请求配置
Console.WriteLine($"命令: {result.Request.Filename} {result.Request.Args}");
```

## 完整示例

### 执行命令并处理结果

```csharp
var request = new ProcessStartRequest
{
    Filename = "cmd.exe",
    Args = "/c dir",
    WorkingDirectory = "C:\\",
    LogInfo = true
};

var result = await request.RunCommandAsync();

if (result.ExitCode == 0)
{
    Console.WriteLine("执行成功:");
    Console.WriteLine(result.Output);
}
else
{
    Console.WriteLine($"执行失败 (退出码: {result.ExitCode}):");
    Console.WriteLine(result.Error);
}
```

### 超时控制

```csharp
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

var request = new ProcessStartRequest
{
    Filename = "long-running.exe",
    CancellationToken = cts.Token,
    LogInfo = true
};

try
{
    var result = await request.RunCommandAsync();
    Console.WriteLine($"完成: {result.ExitCode}");
}
catch (OperationCanceledException)
{
    Console.WriteLine("命令执行超时，已取消");
}
```

### 批量执行命令

```csharp
var commands = new[]
{
    "dir",
    "type file.txt",
    "echo done"
};

foreach (var command in commands)
{
    var result = command.RunCommand();
    Console.WriteLine($"执行: {command}");
    Console.WriteLine($"退出码: {result.ExitCode}");

    if (result.ExitCode != 0)
    {
        Console.WriteLine($"错误: {result.Error}");
    }
}
```

## 相关文档

- [文件](file.md)
- [基础扩展](../README.md#基础扩展)
