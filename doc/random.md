# 随机数生成

SharpDevLib 提供了随机码生成功能。

## 生成随机码

##### 使用默认选项

```csharp
var random = new Random();
var code = random.GenerateCode();
Console.WriteLine(code);
//生成8位数字和字母的随机码
```

##### 自定义长度

```csharp
var random = new Random();
var option = new GenerateRandomCodeOption { Length = 16 };
var code = random.GenerateCode(option);
Console.WriteLine(code);
//生成16位数字和字母的随机码
```

##### 使用自定义字符集

```csharp
var random = new Random();
var option = new GenerateRandomCodeOption
{
    Length = 10,
    Seed = "0123456789"
};
var code = random.GenerateCode(option);
Console.WriteLine(code);
//生成10位纯数字的随机码
```

##### 使用大写字母

```csharp
var random = new Random();
var option = new GenerateRandomCodeOption
{
    Length = 8,
    Seed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
};
var code = random.GenerateCode(option);
Console.WriteLine(code);
//生成8位大写字母的随机码
```

##### 使用特殊字符

```csharp
var random = new Random();
var option = new GenerateRandomCodeOption
{
    Length = 12,
    Seed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*"
};
var code = random.GenerateCode(option);
Console.WriteLine(code);
//生成12位包含特殊字符的随机码
```

## 默认选项

##### 查看默认配置

```csharp
var defaultOption = GenerateRandomCodeOption.Default;
Console.WriteLine(defaultOption.Length);
//8

Console.WriteLine(defaultOption.Seed);
//ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789
```

## 实际应用

##### 生成验证码

```csharp
var random = new Random();
var option = new GenerateRandomCodeOption
{
    Length = 6,
    Seed = "0123456789"
};
var verificationCode = random.GenerateCode(option);
Console.WriteLine($"验证码: {verificationCode}");
```

##### 生成密码

```csharp
var random = new Random();
var option = new GenerateRandomCodeOption
{
    Length = 16,
    Seed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*"
};
var password = random.GenerateCode(option);
Console.WriteLine($"密码: {password}");
```

##### 生成订单号

```csharp
var random = new Random();
var timestamp = DateTime.Now.ToUtcTimestamp().ToString();
var option = new GenerateRandomCodeOption { Length = 6, Seed = "0123456789" };
var randomPart = random.GenerateCode(option);
var orderId = $"{timestamp}{randomPart}";
Console.WriteLine($"订单号: {orderId}");
```

##### 生成临时token

```csharp
var random = new Random();
var option = new GenerateRandomCodeOption
{
    Length = 32,
    Seed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
};
var token = random.GenerateCode(option);
Console.WriteLine($"Token: {token}");
```

## 注意事项

- 随机码长度必须大于0
- 种子数据不能为空且长度必须大于0
- 使用`System.Random`生成，不建议用于安全敏感场景

## 相关文档

- [字符串操作](string.md)
- [基础扩展](../README.md#基础扩展)
