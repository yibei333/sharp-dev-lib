using System;
using System.Collections.Generic;
using System.Text;

namespace SharpDevLib.Foo;

/// <summary>
/// Bar class
/// </summary>
[Test(2)]

[Test(1,Text = "<summary>")]
public class Bar
{
    /// <summary>
    /// Bar Test method
    /// </summary>
    public static void Test()
    {

    }
}

public class Bar <T>
{

}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class TestAttribute : Attribute
{
    public TestAttribute(int count)
    {
        Count = count;
    }

    public int Count { get; }
    public string? Text { get; set; }
}