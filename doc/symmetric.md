# Symmetric - 对称加密

提供对称加密功能。

## 类

### SymmetricAlgorithmHelper

对称加密帮助类，提供 AES、DES、3DES 等对称加密算法。

## 扩展方法

### Encrypt

使用对称算法加密数据。

#### 方法签名

```csharp
public static string Encrypt(this string plainText, string key, string iv, SymmetricAlgorithmType algorithm = SymmetricAlgorithmType.AES)
```

#### 参数

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| plainText | string | - | 明文 |
| key | string | - | 密钥 |
| iv | string | - | 初始化向量 |
| algorithm | SymmetricAlgorithmType | AES | 对称算法类型 |

#### 返回值

加密后的密文（Base64 编码）。

### Decrypt

使用对称算法解密数据。

#### 方法签名

```csharp
public static string Decrypt(this string cipherText, string key, string iv, SymmetricAlgorithmType algorithm = SymmetricAlgorithmType.AES)
```

#### 参数

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| cipherText | string | - | 密文（Base64 编码） |
| key | string | - | 密钥 |
| iv | string | - | 初始化向量 |
| algorithm | SymmetricAlgorithmType | AES | 对称算法类型 |

#### 返回值

解密后的明文。

## 示例

### AES 加密解密

```csharp
var key = "0123456789ABCDEF0123456789ABCDEF";
var iv = "0123456789ABCDEF";

// 加密
var encrypted = "Hello World".Encrypt(key, iv, SymmetricAlgorithmType.AES);
Console.WriteLine(encrypted);

// 解密
var decrypted = encrypted.Decrypt(key, iv, SymmetricAlgorithmType.AES);
Console.WriteLine(decrypted); // 输出: Hello World
```

## 特性

- 支持多种对称加密算法（AES、DES、3DES）
- 使用密钥和初始化向量进行加密
- 输出 Base64 编码的密文
