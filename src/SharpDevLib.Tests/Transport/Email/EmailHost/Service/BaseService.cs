using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Service;

public abstract class BaseService<T>
{
    protected List<T> Data { get; } = [];

    public List<T> Get(Func<T, bool> predicate) => Data.Where(predicate).ToList();

    public ReadOnlyCollection<T> All => new(Data);
}
