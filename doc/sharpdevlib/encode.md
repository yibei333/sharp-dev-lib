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

## 2. HexString
16进制字符串扩展

#### 2.1 ToHexString
将字节数组转换为16进制字符串
``` csharp
//示例
var bytes=new byte[]{102, 111, 111};
var str=bytes.ToHexString();
Console.WriteLine(str);
//666f6f
```

#### 2.2 FromHexString
将16进制字符串转换为字节数组
``` csharp
//示例
var str="666f6f";
var bytes=str.FromHexString();
Console.WriteLine(string.Join(",",bytes));
//102,111,111
```

## 3. Base64
Base64编码扩展

#### 3.1 Base64Encode
将字节数组转换为Base64字符串
``` csharp
//示例
var bytes=new byte[]{102, 111, 111};
var str=bytes.Base64Encode();
Console.WriteLine(str);
//Zm9v
```

#### 3.2 Base64Decode
将Base64字符串转换为字节数组
``` csharp
//示例
var str="Zm9v";
var bytes=str.Base64Decode();
Console.WriteLine(string.Join(",",bytes));
//102,111,111
```

## 4. Url
Url编码扩展

#### 4.1 UrlEncode
将字节数组转换为Base64字符串
``` csharp
//示例
var url="https://foo.com/bar?query=baz";
var bytes=url.ToUtf8Bytes();
var str=bytes.UrlEncode();
Console.WriteLine(str);
//https%3A%2F%2Ffoo.com%2Fbar%3Fquery%3Dbaz
```

#### 4.2 UrlDecode
将Base64字符串转换为字节数组
``` csharp
//示例
var str="https%3A%2F%2Ffoo.com%2Fbar%3Fquery%3Dbaz";
var bytes=str.UrlDecode();
var url=bytes.ToUtf8String();
Console.WriteLine(url);
//https://foo.com/bar?query=baz
```

## 5. Base64Url
Base64Url编码扩展

#### 5.1 Base64UrlEncode
将字节数组转换为Base64Url字符串
``` csharp
//示例
var url="https://foo.com/bar?query=baz";
var bytes=url.ToUtf8Bytes();
var str=bytes.Base64UrlEncode();
Console.WriteLine(str);
//aHR0cHM6Ly9mb28uY29tL2Jhcj9xdWVyeT1iYXo
```

#### 5.2 Base64UrlDecode
将Base64Url字符串转换为字节数组
``` csharp
//示例
var str="aHR0cHM6Ly9mb28uY29tL2Jhcj9xdWVyeT1iYXo";
var bytes=str.Base64UrlDecode();
var url=bytes.ToUtf8String();
Console.WriteLine(url);
//https://foo.com/bar?query=baz
```