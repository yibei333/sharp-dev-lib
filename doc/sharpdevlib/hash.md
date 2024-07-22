哈希结果字符串都为16进制字符串

## 1. ```Hash```
哈希，支持```MD5```,```SHA128```,```SHA256```,```SHA384```,```SHA512```

#### 1.1 ```MD5```
MD5哈希

##### 1.1.1 ```MD5Hash(byte[] bytes, MD5OutputLength length = MD5OutputLength.ThirtyTwo)```
字节数组哈希

``` csharp
//示例
var bytes="foobar".ToUtf8Bytes();
Console.WriteLine(bytes.MD5Hash());
//3858f62230ac3c915f300c664312c63f
Console.WriteLine(bytes.MD5Hash(MD5OutputLength.Sixteen));
//30ac3c915f300c66
```

##### 1.1.2 ```MD5Hash(Stream stream, MD5OutputLength length = MD5OutputLength.ThirtyTwo)```
将流中的字节数组哈希,可用于文件哈希

``` csharp
//示例
var path="foo.txt";
using var stream=File.OpenRead(path);
var hash=stream.MD5Hash();
```

#### 1.2 SHA
支持```SHA128```,```SHA256```,```SHA384```,```SHA512```,以```SHA256```为例,其余使用方法相同

##### 1.2.1 ```SHA256Hash(byte[] bytes, MD5OutputLength length = MD5OutputLength.ThirtyTwo)```
字节数组哈希

``` csharp
//示例
var bytes="foobar".ToUtf8Bytes();
Console.WriteLine(bytes.SHA256Hash());
//c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2
```

##### 1.2.2 ```SHA256Hash(Stream stream, MD5OutputLength length = MD5OutputLength.ThirtyTwo)```
将流中的字节数组哈希,可用于文件哈希

``` csharp
//示例
var path="foo.txt";
using var stream=File.OpenRead(path);
var hash=stream.SHA256Hash();
```


## 2. ```HMACHash```
支持```HMACMD5```,```HMACSHA128```,```HMACSHA256```,```HMACSHA384```,```HMACSHA512```

和上面哈希的方法类似，只是多了一个```secret```参数，也是包含字节数据哈希和流中的字节数组哈希，以```HMACSHA256```为例

``` csharp
//示例
var secret="123456".ToUtf8Bytes();
var bytes="foobar".ToUtf8Bytes();
var hash=bytes.HMACSHA256Hash(secret);
//9708280ad4dad450336407679f91be06a815ec7aacf8e489e0c2e5dcbd103087
```