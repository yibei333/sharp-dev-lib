#### ```BaseRequest```
```
request基类,或许可以在反射或者泛型中用
```
#### ```IdRequest```
``` csharp
new IdRequest();
//{"Id":"00000000-0000-0000-0000-000000000000"}

new IdRequest(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"));
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"}
```
#### ```IdRequest<TId>```
``` csharp
new IdRequest<int>()
//{"Id":0}

new IdRequest<int>(1)
//{"Id":1}
```
#### ```NameRequest```
``` csharp
new NameRequest()
//{"Name":null}

new NameRequest("foo")
//{"Name":"foo"}
```
#### ```IdNameRequest```
``` csharp
new IdNameRequest()
//{"Id":"00000000-0000-0000-0000-000000000000","Name":null}

new IdNameRequest(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"),"foo")
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac","Name":"foo"}
```
#### ```IdNameRequest<TId>```
``` csharp
new IdNameRequest<int>()
//{"Id":0,"Name":null}

new IdNameRequest<int>(1,"foo")
//{"Id":1,"Name":"foo"}
```
#### ```DataRequest<TData>```
``` csharp
new DataRequest<int>()
//{"Data":0}

new DataRequest<int>(1)
//{"Data":1}
```
#### ```IdDataRequest<TData>```
``` csharp
new IdDataRequest<int>()
//{"Id":"00000000-0000-0000-0000-000000000000","Data":0}

new IdDataRequest<int>(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"),1)
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac","Data":1}
```
#### ```IdDataRequest<TId,TData>```
``` csharp
new IdDataRequest<int,string>()
//{"Id":0,"Data":null}

new IdDataRequest<int,string>(1,"foo")
//{"Id":1,"Data":"foo"}
```
#### ```IdNameDataRequest<TData>```
``` csharp
new IdNameDataRequest<int>()
//{"Id":"00000000-0000-0000-0000-000000000000","Name":null,"Data":0}

new IdNameDataRequest<int>(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"),"foo",2)
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac","Name":"foo","Data":2}
```
#### ```IdNameDataRequest<TId,TData>```
``` csharp
new IdNameDataRequest<int,decimal>()
//{"Id":0,"Name":null,"Data":0}

new IdNameDataRequest<int,decimal>(1,"foo",2.1d)
//{"Id":1,"Name":"foo","Data":2.1}
```

#### ```PageRequest```
``` csharp
new PageRequest()
//{"Index":0,Size:20}

new PageRequest(1,30)
//{"Index":1,Size:30}
```
实用示例
``` csharp
static void Main(){
    var request=new GetUsersRequest{Name="b"};
    var reply=GetUsers(request);
    Console.WriteLine(reply.Serialize());

    //{
    //   "Data": [
    //     {"Name": "bar"},
    //     {"Name": "baz"}
    //   ],
    //   "Description": null,
    //   "ExtraData": null,
    //   "Index": 0,
    //   "PageCount": 1,
    //   "Size": 20,
    //   "Success": true,
    //   "TotalCount": 2
    // }
}

//分页获取用户
public PageReply<NameDto> GetUsers(GetUsersRequest request)
{
    IEnumerable<NameDto> query = _repository;
    if(!string.IsNullOrWhiteSpace(request.Name))
    {
        query=query.Where(u=>u.Name!.Contains(request.Name));
    }
    var totalCount=query.Count();
    var data=query
        .OrderBy(u=>u.Name)
        .Skip(request.Index*request.Size)
        .Take(request.Size)
        .ToList();
    return Reply.PageSucceed(data,totalCount,request);
}

static readonly List<NameDto> _repository=new List<NameDto>
{
    new NameDto("foo"),
    new NameDto("bar"),
    new NameDto("baz"),
};

public class GetUsersRequest:PageRequest
{
    public string? Name{get;set;}
}
```