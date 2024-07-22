## 1. ```ForEach<T>```
简化IEnumerable要先ToList才能调用ForEach
``` csharp
//示例
public void ForEachTest()
{
    IEnumerable<int> list = new List<int> { 1, 2, 3 };
    list.ForEach(x => Console.WriteLine(x));
    //输出
    //1
    //2
    //3
}
```

## 2. ```DistinctByObjectValue<T>```
根据对象的值去重,原理是利用对象序列化的值是否一样,所以如果数据量大不建议使用

``` csharp
//示例
var data=new List<NameDto>
{
    new NameDto("foo"),
    new NameDto("foo"),
    new NameDto("bar"),
};
var distincted=data.DistinctByObjectValue();
//结果
//[
//  {"Name":"foo"},
//  {"Name":"bar"}
//]
```

## 3. ```OrderByDynamic<T>(string sortPropertyName, bool descending = false)```
根据动态名称排序,支持IQueryable

``` csharp
//示例
var users=new List<NameDataDto<int>>
{
    new NameDataDto<int>("foo",10),
    new NameDataDto<int>("bar",20),
};

var orderByName=users.OrderByDynamic("Name");
//结果
//[{"Name":"bar","Data":20},{"Name":"foo","Data":10}]

var orderByNameDescending=users.OrderByDynamic("Name",true);
//结果
//[{"Name":"foo","Data":10},{"Name":"bar","Data":20}]

var orderByData=users.OrderByDynamic("Data");
//结果
//[{"Name":"foo","Data":10},{"Name":"bar","Data":20}]

var orderByDataDescending=users.OrderByDynamic("Data",true);
//结果
//[{"Name":"bar","Data":20},{"Name":"foo","Data":10}]
```
