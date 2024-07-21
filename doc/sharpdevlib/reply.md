#### ```Reply```

``` csharp
public Reply Do()
{
    return Reply.Succeed("ok");
    // {
    //     "Success":true,
    //     "Description":"ok",
    //     "ExtraData":null
    // }

    // return Reply.Failed("some error occured");
    // {
    //     "Success":false,
    //     "Description":"some error occured",
    //     "ExtraData":null
    // }
}
```

#### ```Reply<TData>```

``` csharp
public Reply<NameDto> GetUser()
{
    return Reply.Succeed(new NameDto("foo"));
    // {
    //     "Success":true,
    //     "Data":{
    //          "Name":"foo"
    //      }
    //     "Description":null,
    //     "ExtraData":null
    // }

    // return Reply.Failed<NameDto>("some error occured");
    // {
    //     "Success":false,
    //     "Data":null,
    //     "Description":"some error occured",
    //     "ExtraData":null
    // }
}
```

#### ```PageReply<TData>```
``` csharp
public PageReply<NameDto> GetUsers()
{
    var index=0;
    var size=10;
    var users=new List<NameDto>
    {
        new NameDto("foo"),
        new NameDto("bar"),
    };//模拟根据index和size分页获取用户
    var totalCount=49;
    return Reply.PageSucceed(users,totalCount,index,size);
    // {
    //     "Success":true,
    //     "Index":0,
    //     "Size":10,
    //     "TotalCount":49,
    //     "PageCount":5,
    //     "Data":[
    //        {"Name":"foo"},
    //        {"Name":"bar"}
    //      ],
    //     "Description":null,
    //     "ExtraData":null
    // }

    // return Reply.PageFailed<NameDto>("some error occured");
    // {
    //     "Success":false,
    //     "Index":0,
    //     "Size":0,
    //     "TotalCount":0,
    //     "PageCount":0,
    //     "Data":null,
    //     "Description":"some error occured",
    //     "ExtraData":null
    // }
}
```