
# sharp-dev-lib
## 介绍
sharp-dev-lib是c#中经常用到的工具库，基本都采用扩展的方式，方便客户端调用。

## 依赖说明
sharp-dev-lib使用.netstandard2.1框架，依赖第三方库如下：

* **Newtonsoft.Json (>= 13.0.1)**
    用于对序列化及对json格式美化

* **SixLabors.Fonts (>= 1.0.0-beta17)**
* **SixLabors.ImageSharp.Drawing (>= 1.0.0-beta14)**
    用于画验证码

* **DotNetZip (>= 1.16.0)**
    用于文件压缩

* **SharpCompress (>= 0.31.0)**
    用于文件解压

## 安装教程

1. 从nuget包管理器中搜索安装[SharpDevLib](https://www.nuget.org/packages/SharpDevLib)

2. 在Package Manager中执行命令
```
Install-Package SharpDevLib -Version 1.1.0
```

3. 在dotnet cli中执行命令

```
dotnet add package SharpDevLib --version 1.1.0
```

## 使用说明
如果我们想对一个字符串进行md5 hash,代码示例如下:
```
//using SharpDevLib;
 public void Md5Test()
 {
    var sampleString="HelloWorld";
    var md5Hash=sampleString.MD5Hash();
    Console.WriteLine(md5Hash);
 }

 //结果：68e109f0f40ca72a15e05cc22786f8e6
```
sharp-dev-lib包含如下表格的工具

| 名称 |描述  |使用  |
| --- | --- | --- |
| [CloneUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/CloneUtil.cs) |提供对象的深克隆扩展方法  |扩展方法  |
| [ConvertUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/ConvertUtil.cs) |提供格式转换的扩展方法(string转Guid,Boolean)  |扩展方法  |
| [EncodeUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/EncodeUtil.cs) |提供Base64,Url,Base64Url的编码和解码扩展方法  |扩展方法  |
| [EnumerableUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/EnumerableUtil.cs) |提供集合中对对象去重的扩展方法  |扩展方法  |
| [EnumUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/EnumUtil.cs) |提供枚举的取值和转换的扩展方法  |扩展方法  |
| [HashUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/HashUtil.cs) |提供常用Hash以及文件的扩展方法  |扩展方法  |
| [JsonUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/JsonUtil.cs) |提供Json序列化和反序列化及美化的扩展方法  |扩展方法  |
| [NullCheckUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/NullCheckUtil.cs) |提供对象的空引用和空值断言的扩展方法  |扩展方法  |
| [ReflectionUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/ReflectionUtil.cs) |提供对象的类型名称以及确保对象是否包含公有无参构造函数的扩展方法  |扩展方法  |
| [TimeUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/TimeUtil.cs) |提供时间的UTC时间戳转换和输出格式的扩展方法  |扩展方法  |
| [TreeUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/TreeUtil.cs) |提供树形结构构建的扩展方法  |实现ITreeNode接口，且在对象集合上使用扩展方法  |
| [UrlUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/UrlUtil.cs) |提供路径合并的扩展方法  |扩展方法  |
| [VerifyCodeUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/VerifyCodeUtil.cs) |提供验证码的生成和保存为图片的扩展方法  |扩展方法  |
| [ZipUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/ZipUtil.cs) |文件的压缩和解压扩展方法  |扩展方法  |
| [FileUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/FileUtil.cs) |文件保存扩展方法  |扩展方法  |
| [StringUtil](https://github.com/yibei333/sharp-dev-lib/blob/master/src/SharpDevLib/Utils/StringUtil.cs) |Trim扩展方法  |扩展方法  |

## 版本说明
| 时间 |版本  |描述  |
| --- | --- | --- |
| 2024-04-29 |v1.1.0 |添加StringUtil，清理了代码  |
| 2022-07-09 |v1.0.5 |VerifyCodeUtil依赖库由"System.Drawing.Common"变更为"SixLabors.ImageSharp"  |
| 2022-05-19 |v1.0.4 |HashUtil中GetHexString方法重命名为ToHexString，增加FromHexString方法  |
| 2022-05-13 |v1.0.3 |增加1个工具（FileUtil）  |
| 2022-05-09 |v1.0.2 |ConvertUtil工具移除list和DataTable的转换，转换将在SharpDevLib.Extensions包中体现  |
| 2022-04-10 |v1.0.1 |增加1个工具（ZipUtil）,添加nuget打包的注释  |
| 2022-04-08 |v1.0.0 |初始化13个工具（CloneUtil,ConvertUtil,EncodeUtil,EnumerableUtil,EnumUtil,HashUtil,JsonUtil,NullCheckUtil,ReflectionUtil,TimeUtil,TreeUtil,UrlUtil,VerifyCodeUtil）  |