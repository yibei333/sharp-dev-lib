# Random - 随机数生成

提供密码学安全的随机码生成功能。

## 特性

- 使用密码学安全的随机数生成器 (`RandomNumberGenerator`)
- 使用拒绝采样算法确保字符分布均匀
- 生成的随机码具有良好的随机性和唯一性
- 适用于生成验证码、密码、随机ID等场景

## RandomType随机数类型

| 类型 | 说明 | 字符集 |
| --- | --- | --- |
| `Number` | 纯数字 | 0-9 |
| `LetterLower` | 小写字母 | a-z |
| `LetterUpper` | 大写字母 | A-Z |
| `Letter` | 字母 | a-z, A-Z |
| `NumberAndLetter` | 数字+字母 | 0-9, a-z, A-Z |
| `Mix` | 混合 | 数字+字母+特殊字符 |


## 示例

### 生成随机码

```csharp
var code = RandomType.Number.GenerateCode(6);
Console.WriteLine(code); 
//806532（示例）
```

## 相关文档
- [基础扩展](../README.md#基础扩展)