# Request - 请求模型

提供常用的请求模型类，用于封装 API 请求和方法参数。

##### 使用 IdRequest

```csharp
var request = new IdRequest<int>(1);
Console.WriteLine(request.Id); // 输出: 1

var request2 = new IdRequest<string>("user-001");
Console.WriteLine(request2.Id); // 输出: user-001
```

##### 使用 NameRequest

```csharp
var request = new NameRequest("张三");
Console.WriteLine(request.Name); // 输出: 张三
```

##### 使用 DataRequest

```csharp
var request = new DataRequest<int>(10);
Console.WriteLine(request.Data); // 输出: 10

var request2 = new DataRequest<string>("foo");
Console.WriteLine(request2.Data); // 输出: foo
```

##### 使用 NameDataRequest

```csharp
var request = new NameDataRequest<int>("foo",10);
Console.WriteLine(request.Name); // 输出: foo
Console.WriteLine(request.Data); // 输出: 10
```

##### 使用 IdNameRequest

```csharp
var request = new IdNameRequest<int>(1, "张三");
Console.WriteLine(request.Id); // 输出: 1
Console.WriteLine(request.Name); // 输出: 张三

var request2 = new IdNameRequest<Guid>(Guid.Empty,"李四");
Console.WriteLine(request2.Id); // 输出: 00000000-0000-0000-0000-000000000000
Console.WriteLine(request2.Name); // 输出: 李四
```

##### 使用 IdDataRequest

```csharp
var request = new IdDataRequest<int, int>(1, 10);
Console.WriteLine(request.Id); // 输出: 1
Console.WriteLine(request.Data); // 输出: 10

var request2 = new IdDataRequest<int, string>(1,"foo");
Console.WriteLine(request2.Id); // 输出: 1
Console.WriteLine(request2.Data); // 输出: foo
```

##### 使用 IdNameDataRequest

```csharp
var request = new IdNameDataRequest<int, int>(1, "用户", 20);
Console.WriteLine(request.Id); // 输出: 1
Console.WriteLine(request.Name); // 输出: 用户
Console.WriteLine(request.Data); // 输出: 20

var request2 = new IdNameDataRequest<int, string>(1,"用户","hello");
Console.WriteLine(request2.Id); // 输出: 1
Console.WriteLine(request2.Name); // 输出: 用户
Console.WriteLine(request2.Data); // 输出: hello
```

##### 使用 PageRequest

```csharp
// 使用默认参数
var request1 = new PageRequest();
Console.WriteLine(request1.Index); // 输出: 0
Console.WriteLine(request1.Size); // 输出: 20

// 自定义参数
var request2 = new PageRequest(1, 10);
Console.WriteLine(request2.Index); // 输出: 1
Console.WriteLine(request2.Size); // 输出: 10
```

## 相关文档
- [响应模型](response.md)
- [DTO](dto.md)
- [模型](../README.md#模型)