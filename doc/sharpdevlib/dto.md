#### ```BaseDto```
```
dto基类,或许可以在反射或者泛型中用
```
#### ```IdDto```
``` csharp
new IdDto();
//{"Id":"00000000-0000-0000-0000-000000000000"}

new IdDto(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"));
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"}
```
#### ```IdDto<TId>```
``` csharp
new IdDto<int>()
//{"Id":0}

new IdDto<int>(1)
//{"Id":1}
```
#### ```NameDto```
``` csharp
new NameDto()
//{"Name":null}

new NameDto("foo")
//{"Name":"foo"}
```
#### ```IdNameDto```
``` csharp
new IdNameDto()
//{"Id":"00000000-0000-0000-0000-000000000000","Name":null}

new IdNameDto(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"),"foo")
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac","Name":"foo"}
```
#### ```IdNameDto<TId>```
``` csharp
new IdNameDto<int>()
//{"Id":0,"Name":null}

new IdNameDto<int>(1,"foo")
//{"Id":1,"Name":"foo"}
```
#### ```DataDto<TData>```
``` csharp
new DataDto<int>()
//{"Data":0}

new DataDto<int>(1)
//{"Data":1}
```
#### ```IdDataDto<TData>```
``` csharp
new IdDataDto<int>()
//{"Id":"00000000-0000-0000-0000-000000000000","Data":0}

new IdDataDto<int>(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"),1)
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac","Data":1}
```
#### ```IdDataDto<TId,TData>```
``` csharp
new IdDataDto<int,string>()
//{"Id":0,"Data":null}

new IdDataDto<int,string>(1,"foo")
//{"Id":1,"Data":"foo"}
```
#### ```IdNameDataDto<TData>```
``` csharp
new IdNameDataDto<int>()
//{"Id":"00000000-0000-0000-0000-000000000000","Name":null,"Data":0}

new IdNameDataDto<int>(Guid.Parse("d9bdcc1f-5776-49e6-ab28-3bdffa9756ac"),"foo",2)
//{"Id":"d9bdcc1f-5776-49e6-ab28-3bdffa9756ac","Name":"foo","Data":2}
```
#### ```IdNameDataDto<TId,TData>```
``` csharp
new IdNameDataDto<int,decimal>()
//{"Id":0,"Name":null,"Data":0}

new IdNameDataDto<int,decimal>(1,"foo",2.1d)
//{"Id":1,"Name":"foo","Data":2.1}
```