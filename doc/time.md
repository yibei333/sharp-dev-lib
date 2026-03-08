# 时间戳操作

SharpDevLib 提供了时间戳转换和时间格式化功能。

## 时间戳转换

##### DateTime转UTC时间戳

```csharp
var time = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Local);
var timestamp = time.ToUtcTimestamp();
Console.WriteLine(timestamp);
//1704081600000
```

##### UTC时间戳转DateTime

```csharp
var timestamp = 1704100800000L;
var time = timestamp.ToUtcTime();
Console.WriteLine(time);
//2024/1/1 9:20:00 (UTC时间)
```

##### 当前时间转时间戳

```csharp
var now = DateTime.Now;
var timestamp = now.ToUtcTimestamp();
Console.WriteLine(timestamp);
//当前时间的UTC时间戳
```

##### 时间戳转当前本地时间

```csharp
var timestamp = 1704100800000L;
var utcTime = timestamp.ToUtcTime();
var localTime = utcTime.ToLocalTime();
Console.WriteLine(localTime);
//2024/1/1 17:20:00 (本地时间)
```

## 时间格式化

##### 格式化为字符串

```csharp
var time = new DateTime(2024, 1, 1, 12, 30, 45);
var formatted = time.ToTimeString();
Console.WriteLine(formatted);
//2024-01-01 12:30:45
```

##### 自定义格式

```csharp
var time = new DateTime(2024, 1, 1, 12, 30, 45);
var formatted = time.ToTimeString("yyyy/MM/dd HH:mm");
Console.WriteLine(formatted);
//2024/01/01 12:30
```

##### 使用标准格式

```csharp
var time = new DateTime(2024, 1, 1, 12, 30, 45);
var formatted = time.ToTimeString("yyyyMMdd");
Console.WriteLine(formatted);
//20240101
```

## 相关文档

- [基础扩展](../README.md#基础扩展)
