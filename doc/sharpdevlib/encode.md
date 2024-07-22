## 1. Utf8
Utf8编码扩展

#### 1.1 ToUtf8String
将字节数组转换为Utf8字符串
``` csharp
//示例
var bytes=new byte[]{102, 111, 111};
var str=bytes.ToUtf8String();
Console.WriteLine(str);
//foo
```

#### 1.2 ToUtf8Bytes
将字符串转换为Utf8字节数组
``` csharp
//示例
var str="foo";
var bytes=str.ToUtf8Bytes();
Console.WriteLine(string.Join(",",bytes));
//102,111,111
```