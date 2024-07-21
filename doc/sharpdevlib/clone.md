采用序列化的方式实现,如果数据量较大,不建议使用

``` csharp
var user=new NameDto("foo");
var clonedObj=user.DeepClone();
```