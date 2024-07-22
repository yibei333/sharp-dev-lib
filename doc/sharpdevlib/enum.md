## 1. ```ToEnum<TEnum>```
值转换为枚举

#### 1.1 ```ToEnum<TEnum>(int intValue)```
将int值转换为枚举

``` csharp
//示例
public void ToEnum()
{
    int intValue=1;
    var enumValue=intValue.ToEnum<Gender>();
    Console.WriteLine(enumValue.ToString());
    //Male

    //int intValue=4;
    //intValue.ToEnum<Gender>()将引发异常
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3,
}
```

#### 1.2 ```ToEnum<TEnum>(string stringValue,bool ignoreCase)```
将字符串值转换为枚举

``` csharp
//示例
public void ToEnum()
{
    int stringValue="male";
    var enumValue=stringValue.ToEnum<Gender>();
    Console.WriteLine(enumValue.ToString());
    //Male

    //stringValue.ToEnum<Gender>(false)将引发异常
    //"male1".ToEnum<Gender>()将引发异常
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3,
}
```

## 2. ```GetKeyValues<TEnum>```
获取枚举类型的所有KeyValue集合
``` csharp
//示例
public void GetKeyValues()
{
    IEnumerable<KeyValuePair<string, int>> keyValues = EnumExtension.GetKeyValues<Gender>();
    //[    
    //   {
    //     "Key": "Male",
    //     "Value": 1
    //   },
    //   {
    //     "Key": "Female",
    //     "Value": 2
    //   },
    //   {
    //     "Key": "Other",
    //     "Value": 3
    //   }
    //]
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3,
}
```

## 3. ```GetDictionary<TEnum>```
获取枚举类型的所有KeyValue集合，并转换为字典
``` csharp
//示例
public void GetDictionary()
{
    Dictionary<string, int> dic = EnumExtension.GetDictionary<Gender>();
    //{"Male":1,"Female":2,"Other":3}
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3,
}
```