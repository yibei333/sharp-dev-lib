using System;

namespace SharpDevLib.Tests.Data;

public class ToDoItem
{
    public ToDoItem()
    {
        Task = string.Empty;
        DeadLine = DateTime.Now;
    }

    public string Task { get; set; }
    public DateTime DeadLine { get; set; }
}
