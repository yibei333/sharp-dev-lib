###### [主页](./Index.md "主页")
# ValueConverter 属性
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [DataTableTransferColumn](./SharpDevLib.DataTableTransferColumn.md "DataTableTransferColumn")
``` csharp
public Func<Object, DataRow, Object> ValueConverter { get; set; }
```
**注释**
*值转换器,参数说明如下*
* 1.第一个参数为源单元格的值
* 2.第二个参数为DataRow
* 3.第三个参数为需返回转换后的结果,注意返回的类型需要和TargetType类型一致

