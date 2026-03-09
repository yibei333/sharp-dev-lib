# DTO - 数据传输对象

提供常用的数据传输对象（DTO）类，用于封装和传输数据。

##### 使用 IdDto

```csharp
var idDto = new IdDto<int>(1);
Console.WriteLine(idDto.Id); // 输出: 1

var idDto2 = new IdDto<string>("user-001");
Console.WriteLine(idDto2.Id); // 输出: user-001
```

##### 使用 NameDto

```csharp
var nameDto = new NameDto("张三");
Console.WriteLine(nameDto.Name); // 输出: 张三
```

##### 使用 DataDto

```csharp
var dataDto = new DataDto<int>(1);
Console.WriteLine(dataDto.Data); // 输出: 1

var dataDto2 = new DataDto<string>("foo");
Console.WriteLine(dataDto2.Data); // 输出: foo
```

##### 使用 IdNameDto
```csharp
var idNameDto = new IdNameDto<int>(1, "张三");
Console.WriteLine(idNameDto.Id); // 输出: 1
Console.WriteLine(idNameDto.Name); // 输出: 张三

var idNameDto2 = new IdNameDto<string>("some-id","李四");
Console.WriteLine(idNameDto2.Id); // 输出: some-id
Console.WriteLine(idNameDto2.Name); // 输出: 李四
```

##### 使用 IdDataDto

```csharp
var idDataDto = new IdDataDto<int, int>(1, 1);
Console.WriteLine(idDataDto.Id); // 输出: 1
Console.WriteLine(idDataDto.Data); // 输出: 1

var idDataDto2 = new IdDataDto<int, string>(1,"foo");
Console.WriteLine(idDataDto2.Id); // 输出: 1
Console.WriteLine(idDataDto2.Data); // 输出: foo
```

##### 使用 IdNameDataDto

```csharp
var idNameDataDto = new IdNameDataDto<int, int>(1, "用户", 10);
Console.WriteLine(idNameDataDto.Id); // 输出: 1
Console.WriteLine(idNameDataDto.Name); // 输出: 用户
Console.WriteLine(idNameDataDto.Data); // 输出: 10

var idNameDataDto2 = new IdNameDataDto<int, string>(1,"用户","foo");
Console.WriteLine(idNameDataDto2.Id); // 输出: 1
Console.WriteLine(idNameDataDto2.Name); // 输出: 用户
Console.WriteLine(idNameDataDto2.Data); // 输出: foo
```

## 相关文档
- [请求模型](request.md)
- [响应模型](response.md)
- [模型](../README.md#模型)