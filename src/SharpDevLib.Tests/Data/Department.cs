using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Data;

public class Department : ITreeNode<Department>
{
    private Department()
    {
        Id = Guid.NewGuid();
        Children = new List<Department>();
        Name = string.Empty;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public int Order { get; set; }
    public List<Department> Children { get; set; }

    public static Department Create()
    {
        return new Department
        {
            Name = "IT",
            Order = 1
        };
    }

    public static List<Department> CreateList()
    {
        var list = new List<Department>();

        var softwareStruct = new Department { Name="Software",Order=1 };
        list.Add(softwareStruct);

        var itStruct = new Department { Name = "IT", Order = 1,ParentId=softwareStruct.Id };
        var netStruct = new Department { Name="Net",Order=1,ParentId=itStruct.Id };
        var androidStruct = new Department { Name="Android",Order=2,ParentId=itStruct.Id };
        var javaStruct = new Department { Name="Java",Order=3, ParentId = itStruct.Id };
        itStruct.Children.Add(netStruct);
        itStruct.Children.Add(androidStruct);
        itStruct.Children.Add(javaStruct);
        softwareStruct.Children.Add(itStruct);

        var maintainStruct = new Department { Name = "Maintain", Order = 2 };
        softwareStruct.Children.Add(maintainStruct);

        var testStruct = new Department { Name = "Test", Order = 3 };
        softwareStruct.Children.Add(testStruct);

        var retailStruct = new Department { Name = "Retail", Order = 2 };
        list.Add(retailStruct);

        return list;
    }
}
