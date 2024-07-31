using System;
using System.Collections.Generic;
using System.Text;

namespace SharpDevLib
{
    /// <summary>
    /// test class with metadata
    /// </summary>
    /// <typeparam name="T">metadata type</typeparam>
    /// <remarks>
    /// instance of type Test
    /// </remarks>
    /// <param name="metadata">metadata</param>
    public class Test<T>(T metadata)
    {

        /// <summary>
        /// 获取元数据
        /// </summary>
        public T Metadata { get; } = metadata;

        /// <summary>
        /// foo method
        /// </summary>
        /// <param name="data">data</param>
        public void Foo(T data)
        {

        }
    }

    /// <summary>
    /// test class
    /// </summary>
    public class Test : Test<int>
    {
        /// <summary>
        /// create instance of type test
        /// </summary>
        public Test():base(1)
        {
            Name = string.Empty;
        }

        /// <summary>
        /// create instance of type test
        /// </summary>
        /// <param name="name">name</param>
        public Test(string name) : base(1)
        {
            Name = name;
        }

        /// <summary>
        /// name property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// bar method
        /// </summary>
        /// <typeparam name="A">typeof of a</typeparam>
        /// <param name="a">a</param>
        /// <param name="data">data</param>
        /// <param name="name">name</param>
        public void Bar<A>(A a, int data, string name)
        {

        }

        /// <summary>
        /// Age field
        /// </summary>
        public int Age;

        /// <summary>
        /// test event
        /// </summary>
        public event EventHandler TestEvent = null!;
    }
}
