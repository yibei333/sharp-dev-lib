# Random - 随机数生成

提供密码学安全的随机码生成功能。

## 随机数类型

| 类型 | 说明 | 字符集 |
| --- | --- | --- |
| `Number` | 纯数字 | 0-9 |
| `LetterLower` | 小写字母 | a-z |
| `LetterUpper` | 大写字母 | A-Z |
| `Letter` | 字母 | a-z, A-Z |
| `NumberAndLetter` | 数字+字母 | 0-9, a-z, A-Z |
| `Mix` | 混合 | 数字+字母+特殊字符 |

## 方法

### GenerateCode

生成随机码。

#### 方法签名

```csharp
public static string GenerateCode(this RandomType type, byte length)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| type | RandomType | 随机数类型 |
| length | byte | 长度，为0时返回空字符串 |

#### 返回值

随机码字符串。

## 示例

### 生成纯数字随机码

```csharp
var code = RandomType.Number.GenerateCode(6);
Console.WriteLine(code); // 输出: 123456
```

### 生成小写字母随机码

```csharp
var code = RandomType.LetterLower.GenerateCode(10);
Console.WriteLine(code); // 输出: abcdefghij
```

### 生成大写字母随机码

```csharp
var code = RandomType.LetterUpper.GenerateCode(10);
Console.WriteLine(code); // 输出: ABCDEFGHIJ
```

### 生成字母随机码

```csharp
var code = RandomType.Letter.GenerateCode(10);
Console.WriteLine(code); // 输出: AbCdEfGhIj
```

### 生成数字+字母随机码

```csharp
var code = RandomType.NumberAndLetter.GenerateCode(16);
Console.WriteLine(code); // 输出: AbCdEf123456GhIj
```

### 生成混合随机码（包含特殊字符）

```csharp
var code = RandomType.Mix.GenerateCode(20);
Console.WriteLine(code); // 输出: AbCdEf@1234#GhIj$5678
```

### 生成零长度随机码

```csharp
var code = RandomType.Number.GenerateCode(0);
Console.WriteLine(code); // 输出: (空字符串)
Console.WriteLine(code == string.Empty); // 输出: True
```

### 生成自定义长度随机码

```csharp
var code1 = RandomType.NumberAndLetter.GenerateCode(1);  // 1位
var code2 = RandomType.NumberAndLetter.GenerateCode(5);  // 5位
var code3 = RandomType.NumberAndLetter.GenerateCode(10); // 10位
var code4 = RandomType.NumberAndLetter.GenerateCode(50); // 50位
var code5 = RandomType.NumberAndLetter.GenerateCode(100); // 100位
var code6 = RandomType.NumberAndLetter.GenerateCode(255); // 255位（最大值）
Console.WriteLine(code1.Length); // 输出: 1
Console.WriteLine(code2.Length); // 输出: 5
Console.WriteLine(code3.Length); // 输出: 10
Console.WriteLine(code4.Length); // 输出: 50
Console.WriteLine(code5.Length); // 输出: 100
Console.WriteLine(code6.Length); // 输出: 255
```

## 特性

- 使用密码学安全的随机数生成器 (`RandomNumberGenerator`)
- 使用拒绝采样算法确保字符分布均匀
- 生成的随机码具有良好的随机性和唯一性
- 适用于生成验证码、密码、随机ID等场景
