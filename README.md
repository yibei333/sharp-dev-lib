
# ![GitHub license](assets/c-sharp-small.png) SharpDevLib 
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/yibei333/sharp-dev-lib/main/LICENSE) 
[![](https://img.shields.io/nuget/v/SharpDevLib.svg)](https://www.nuget.org/packages/SharpDevLib)

一个简洁高效的 .NET 开发库，提供常用的工具类和扩展方法，简化日常开发工作。

## ✨ 特性

- ✅ **简洁易用**：提供清晰的 API，降低学习成本
- ✅ **广泛兼容**：基于netstandard2.0框架，意味着兼容.netframework461+
- ✅ **任意使用**：只依赖官方库和使用MIT协议的三方库
- ✅ **扩展性强**：基于扩展方法设计，与现有代码无缝集成
- ✅ **功能完整**：覆盖日常开发的常见需求

## 📦 安装

```bash
dotnet add package SharpDevLib
```

## 📚 文档
- **📁 基础扩展**
  - [Json](doc/json.md)
  - [字符串](doc/string.md)
  - [枚举](doc/enum.md)
  - [集合](doc/collection.md)
  - [文件](doc/file.md)
  - [进程](doc/process.md)
  - [时间戳](doc/time.md)
  - [反射](doc/reflection.md)
  - [克隆](doc/clone.md)
  - [空值断言](doc/nullcheck.md)
  - [随机数](doc/random.md)
  - [DataTable](doc/datatable.md)
  - **模型**
    - [DTO](doc/dto.md)
    - [请求模型](doc/request.md)
    - [响应模型](doc/response.md)
  - **编码**
    - [UTF8](doc/utf8.md)
    - [Base64](doc/base64.md)
    - [Url](doc/url.md)
    - [Base64Url](doc/base64url.md)
    - [HEX](doc/hex.md)
  - **哈希**
    - [MD5](doc/md5.md)
    - [HmacMD5](doc/hmacmd5.md)
    - [SHA](doc/sha.md)
    - [HmacSHA](doc/hmacsha.md)

- **🗜️压缩 / 解压缩**
  - [压缩](doc/compression.md)
  - [解压缩](doc/decompression.md)

- **🗄️ 数据**
  - [SqlHelper](doc/sql.md)

- **📊 OpenXML**
    - [Excel](doc/excel.md)
    - [Spreadsheet](doc/spreadsheet.md)

- **🔐 加解密**
    - [对称加密](doc/symmetric.md)
    - [RSA密钥](doc/rsakey.md)
    - [JWT](doc/jwt.md)
    - [X.509证书](doc/x509.md)
- **🌐 网络传输**
    - **HTTP**
        - [GET](doc/http-get.md)
        - [POST](doc/http-post.md)
        - [PUT](doc/http-put.md)
        - [DELETE](doc/http-delete.md)
    - [Email](doc/email.md)
    - [TCP](doc/tcp.md)
    - [UDP](doc/udp.md)
