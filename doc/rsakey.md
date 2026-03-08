# RSA Key - RSA 密钥

提供 RSA 密钥的生成和管理功能。

## 类

### RsaKeyHelper

RSA 密钥帮助类，提供 RSA 密钥的生成、导入和导出功能。

## 扩展方法

### GenerateKeyPair

生成 RSA 密钥对。

#### 方法签名

```csharp
public static (string publicKey, string privateKey) GenerateKeyPair(int keySize = 2048)
```

#### 参数

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| keySize | int | 2048 | 密钥大小（位数） |

#### 返回值

包含公钥和私钥的元组。

### ExportPublicKey

导出公钥。

#### 方法签名

```csharp
public static string ExportPublicKey(this RSA rsa)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| rsa | RSA | RSA 实例 |

#### 返回值

公钥字符串。

### ExportPrivateKey

导出私钥。

#### 方法签名

```csharp
public static string ExportPrivateKey(this RSA rsa)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| rsa | RSA | RSA 实例 |

#### 返回值

私钥字符串。

### ImportPublicKey

导入公钥。

#### 方法签名

```csharp
public static RSA ImportPublicKey(this string publicKey)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| publicKey | string | 公钥字符串 |

#### 返回值

RSA 实例。

### ImportPrivateKey

导入私钥。

#### 方法签名

```csharp
public static RSA ImportPrivateKey(this string privateKey)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| privateKey | string | 私钥字符串 |

#### 返回值

RSA 实例。

## 示例

### 生成密钥对

```csharp
// 生成 2048 位密钥对
var (publicKey, privateKey) = RsaKeyHelper.GenerateKeyPair(2048);
Console.WriteLine($"公钥: {publicKey}");
Console.WriteLine($"私钥: {privateKey}");
```

### 导出和导入密钥

```csharp
// 创建 RSA 实例
using var rsa = RSA.Create();

// 导出公钥和私钥
var publicKey = rsa.ExportPublicKey();
var privateKey = rsa.ExportPrivateKey();

// 导入密钥
var rsa2 = publicKey.ImportPublicKey();
var rsa3 = privateKey.ImportPrivateKey();
```

## 特性

- 支持生成不同大小的 RSA 密钥对
- 支持导出和导入公钥
- 支持导出和导入私钥
- 支持从密钥字符串创建 RSA 实例
