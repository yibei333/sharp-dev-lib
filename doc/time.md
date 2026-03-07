# 时间戳操作

SharpDevLib 提供了时间戳转换和时间格式化功能。

## 时间戳转换

### ToUtcTimestamp

```csharp
// DateTime 转时间戳（毫秒）
var timestamp = DateTime.UtcNow.ToUtcTimestamp();
Console.WriteLine(timestamp);
// 示例输出: 1699123456789

var timestamp = DateTime.Now.ToUtcTimestamp();
Console.WriteLine(timestamp);
// 输出本地时间对应的 UTC 时间戳
```

### ToUtcTime

```csharp
// 时间戳转 DateTime（毫秒）
var timestamp = 1699123456789;
var dateTime = timestamp.ToUtcTime();
Console.WriteLine(dateTime);
// 示例输出: 2023-11-04 12:44:16

// 转换为本地时间
var localTime = dateTime.ToLocalTime();
```

## 时间格式化

### ToTimeString

```csharp
// 格式化为字符串（默认格式）
var formatted = DateTime.Now.ToTimeString();
// 示例输出: "2023-11-04 12:44:16"

// 自定义格式
var formatted = DateTime.Now.ToTimeString("yyyy/MM/dd HH:mm:ss");
// 示例输出: "2023/11/04 12:44:16"

var formatted = DateTime.Now.ToTimeString("yyyy年MM月dd日 HH:mm:ss");
// 示例输出: "2023年11月04日 12:44:16"

// 包含毫秒
var formatted = DateTime.Now.ToTimeString("yyyy-MM-dd HH:mm:ss.fff");
// 示例输出: "2023-11-04 12:44:16.789"
```

## 完整示例

### 获取当前时间戳

```csharp
var timestamp = DateTime.UtcNow.ToUtcTimestamp();
Console.WriteLine($"当前时间戳: {timestamp}");
```

### 时间戳转日期时间

```csharp
var timestamp = 1699123456789;
var utcTime = timestamp.ToUtcTime();
var localTime = utcTime.ToLocalTime();

Console.WriteLine($"UTC 时间: {utcTime}");
Console.WriteLine($"本地时间: {localTime}");
```

### 格式化当前时间

```csharp
var now = DateTime.Now;

var defaultFormat = now.ToTimeString();
var customFormat1 = now.ToTimeString("yyyy/MM/dd HH:mm:ss");
var customFormat2 = now.ToTimeString("yyyy年MM月dd日");

Console.WriteLine($"默认格式: {defaultFormat}");
Console.WriteLine($"自定义格式1: {customFormat1}");
Console.WriteLine($"自定义格式2: {customFormat2}");
```

### 计算时间差

```csharp
var startTime = DateTime.UtcNow.ToUtcTimestamp();

// 执行一些操作
Thread.Sleep(1000);

var endTime = DateTime.UtcNow.ToUtcTimestamp();
var duration = endTime - startTime;

Console.WriteLine($"执行时间: {duration} 毫秒");
```

## 常用时间格式

### 标准格式

```csharp
// yyyy-MM-dd HH:mm:ss
"2023-11-04 12:44:16"

// yyyy/MM/dd HH:mm:ss
"2023/11/04 12:44:16"

// yyyy-MM-dd
"2023-11-04"

// HH:mm:ss
"12:44:16"

// yyyyMMdd
"20231104"
```

### 中文格式

```csharp
// yyyy年MM月dd日 HH:mm:ss
"2023年11月04日 12:44:16"

// yyyy年MM月dd日
"2023年11月04日"
```

## 相关文档

- [基础扩展](../README.md#基础扩展)
