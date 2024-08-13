###### [主页](./Index.md "主页")

#### Send(Byte[] bytes, Boolean throwIfException) 方法

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**所属类型** : [TcpClient](./SharpDevLib.Transport.TcpClient.md "TcpClient")

``` csharp
public Void Send(Byte[] bytes, Boolean throwIfException)
```

**注释**

*发送*



**返回类型** : [Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")


**参数**

|名称|类型|注释|
|---|---|---|
|bytes|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|字节数组|
|throwIfException|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|发送失败是否抛出异常,默认false,可以订阅Error事件|


