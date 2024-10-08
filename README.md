﻿[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/yibei333/sharp-dev-lib/main/LICENSE)

## 简介
封装c#开发中常用功能,分为如下包

|包名|描述|框架|nuget| 状态|
|----|----|----|----|----|
|[SharpDevLib](./doc/generate/SharpDevLib/Index.md)|简单扩展|netstandard2.0|[![](https://img.shields.io/nuget/v/SharpDevLib.svg)](https://www.nuget.org/packages/SharpDevLib)|[![](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.yml/badge.svg?branch=main)](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.yml)|
|[SharpDevLib.Compression](./doc/generate/SharpDevLib.Compression/Index.md)|压缩包操作|netstandard2.0|[![](https://img.shields.io/nuget/v/SharpDevLib.Compression.svg)](https://www.nuget.org/packages/SharpDevLib.Compression)|[![](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.compression.yml/badge.svg?branch=main)](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.compression.yml)|
|[SharpDevLib.Cryptography](./doc/generate/SharpDevLib.Cryptography/Index.md)|加解密操作|netstandard2.0|[![](https://img.shields.io/nuget/v/SharpDevLib.Cryptography.svg)](https://www.nuget.org/packages/SharpDevLib.Cryptography)|[![](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.cryptography.yml/badge.svg?branch=main)](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.cryptography.yml)|
|[SharpDevLib.OpenXML](./doc/generate/SharpDevLib.OpenXML/Index.md)|Office操作|netstandard2.0|[![](https://img.shields.io/nuget/v/SharpDevLib.OpenXML.svg)](https://www.nuget.org/packages/SharpDevLib.OpenXML)|[![](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.openxml.yml/badge.svg?branch=main)](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.openxml.yml)|
|[SharpDevLib.Transport](./doc/generate/SharpDevLib.Transport/Index.md)|传输操作|netstandard2.0|[![](https://img.shields.io/nuget/v/SharpDevLib.Transport.svg)](https://www.nuget.org/packages/SharpDevLib.Transport)|[![](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.transport.yml/badge.svg?branch=main)](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.transport.yml)|
|[SharpDevLib.Data](./doc/generate/SharpDevLib.Data/Index.md)|数据库相关|netstandard2.0|[![](https://img.shields.io/nuget/v/SharpDevLib.Data.svg)](https://www.nuget.org/packages/SharpDevLib.Data)|[![](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.data.yml/badge.svg?branch=main)](https://github.com/yibei333/sharp-dev-lib/actions/workflows/sharpdevlib.data.yml)|

## 特色
* 均采用netstandard2.0框架，意味着兼容.netframework461+
* 引用的nuget包为官方及开源协议为MIT的第三方包，意味着您可以任意使用和修改源代码
* 简单易用的api且不入侵原有的代码