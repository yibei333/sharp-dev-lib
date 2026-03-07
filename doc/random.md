# 随机数生成

SharpDevLib 提供了随机码生成功能。

## GenerateCode

### 基本用法

```csharp
var random = new Random();

// 使用默认选项生成随机码
var code = random.GenerateCode();
// 默认: 6 位，包含数字和小写字母
// 示例: "a3b8c9"
```

### 自定义长度

```csharp
var random = new Random();

var code = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 10
});
// 示例: "a3b8c9d2e1"
```

### 自定义字符集

```csharp
var random = new Random();

// 只包含数字
var code = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 6,
    Seed = "0123456789"
});
// 示例: "123456"

// 只包含大写字母
var code = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 8,
    Seed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
});
// 示例: "ABCDEFGH"

// 包含特殊字符
var code = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 12,
    Seed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^&*"
});
// 示例: "A3b8C9d2E!f"
```

## 预定义选项

### 默认选项

```csharp
var random = new Random();

// 使用 Default 选项
var code = random.GenerateCode(GenerateRandomCodeOption.Default);
// 长度: 6
// 字符集: "0123456789abcdefghijklmnopqrstuvwxyz"
```

## 完整示例

### 生成验证码

```csharp
var random = new Random();

// 生成 6 位数字验证码
var code = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 6,
    Seed = "0123456789"
});

Console.WriteLine($"验证码: {code}");
```

### 生成密码

```csharp
var random = new Random();

// 生成 12 位强密码
var password = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 12,
    Seed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^&*"
});

Console.WriteLine($"密码: {password}");
```

### 生成订单号

```csharp
var random = new Random();

// 生成 16 位订单号（数字+大写字母）
var orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") +
               random.GenerateCode(new GenerateRandomCodeOption
               {
                   Length = 4,
                   Seed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
               });

Console.WriteLine($"订单号: {orderNo}");
// 示例: "20231104124416A3B8"
```

### 生成邀请码

```csharp
var random = new Random();

// 生成 8 位邀请码（去除易混淆字符）
var inviteCode = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 8,
    Seed = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ"
});

Console.WriteLine($"邀请码: {inviteCode}");
```

### 批量生成

```csharp
var random = new Random();

// 生成 10 个随机码
var codes = new List<string>();
for (int i = 0; i < 10; i++)
{
    var code = random.GenerateCode(new GenerateRandomCodeOption
    {
        Length = 8,
        Seed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    });
    codes.Add(code);
}

codes.ForEach(Console.WriteLine);
```

## 常用字符集

### 数字

```csharp
"0123456789"
```

### 小写字母

```csharp
"abcdefghijklmnopqrstuvwxyz"
```

### 大写字母

```csharp
"ABCDEFGHIJKLMNOPQRSTUVWXYZ"
```

### 字母数字

```csharp
"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
```

### 去除易混淆字符

```csharp
// 去除 0, O, 1, I, l
"23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz"
```

## 注意事项

### 随机数生成器

建议使用单个 `Random` 实例生成多个随机码，而不是为每个码创建新的 `Random` 实例，这样可以避免重复和模式。

```csharp
// 推荐
var random = new Random();
for (int i = 0; i < 10; i++)
{
    var code = random.GenerateCode();
}

// 不推荐
for (int i = 0; i < 10; i++)
{
    var random = new Random();  // 每次都创建新实例
    var code = random.GenerateCode();
}
```

### 种子不能为空

```csharp
// 会抛出异常
var code = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 6,
    Seed = ""  // 种子为空
});
// 抛出: ArgumentException (种子数据至少包含一个字符)
```

### 长度必须大于 0

```csharp
// 会抛出异常
var code = random.GenerateCode(new GenerateRandomCodeOption
{
    Length = 0,
    Seed = "0123456789"
});
// 抛出: ArgumentException (随机码长度必须大于 0)
```

## 相关文档

- [基础扩展](../README.md#基础扩展)
