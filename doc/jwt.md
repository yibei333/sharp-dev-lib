# JWT - JSON Web Token

提供 JWT 的生成和验证功能。

## 类

### JwtHelper

JWT 帮助类，提供 JWT 的生成和验证功能。

## 扩展方法

### GenerateToken

生成 JWT 令牌。

#### 方法签名

```csharp
public static string GenerateToken(string secret, IDictionary<string, object> payload, int expiryMinutes = 60)
```

#### 参数

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| secret | string | - | 密钥 |
| payload | IDictionary\<string, object\> | - | 载荷数据 |
| expiryMinutes | int | 60 | 过期时间（分钟） |

#### 返回值

JWT 令牌字符串。

### ValidateToken

验证 JWT 令牌。

#### 方法签名

```csharp
public static bool ValidateToken(string token, string secret)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| token | string | JWT 令牌字符串 |
| secret | string | 密钥 |

#### 返回值

验证是否成功。

### DecodeToken

解码 JWT 令牌。

#### 方法签名

```csharp
public static IDictionary<string, object> DecodeToken(string token)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| token | string | JWT 令牌字符串 |

#### 返回值

载荷数据字典。

## 示例

### 生成 JWT 令牌

```csharp
var payload = new Dictionary<string, object>
{
    { "userId", 123 },
    { "username", "张三" },
    { "role", "admin" }
};

var token = JwtHelper.GenerateToken("mySecretKey", payload, 60);
Console.WriteLine(token);
```

### 验证 JWT 令牌

```csharp
var isValid = JwtHelper.ValidateToken(token, "mySecretKey");
Console.WriteLine(isValid ? "验证成功" : "验证失败");
```

### 解码 JWT 令牌

```csharp
var payload = JwtHelper.DecodeToken(token);
Console.WriteLine($"用户ID: {payload["userId"]}");
Console.WriteLine($"用户名: {payload["username"]}");
Console.WriteLine($"角色: {payload["role"]}");
```

## 特性

- 支持生成 JWT 令牌
- 支持验证 JWT 令牌
- 支持解码 JWT 令牌获取载荷
- 支持设置过期时间
