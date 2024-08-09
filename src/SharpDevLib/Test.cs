using SharpDevLib;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SharpDevLib
{
    /// <summary>
    /// test class with metadata
    /// </summary>
    /// <typeparam name="T">metadata type</typeparam>
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
    public class Test : Test<int>, ITest1
    {
        /// <summary>
        /// create instance of type test
        /// </summary>
        public Test() : base(1)
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
        /// xx
        /// </summary>
        public string TestClassField = "qux";

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

    /// <summary>
    /// test1 interface
    /// </summary>
    public interface ITest1
    {

    }

    /// <summary>
    /// test2 interface
    /// </summary>
    [Test("baz")]
    public interface ITest2 : ITest1
    {

    }

    /// <summary>
    /// aa
    /// </summary>
    /// <typeparam name="T1">bb</typeparam>
    /// <typeparam name="T2">cc</typeparam>
    /// <typeparam name="T3">dd</typeparam>
    /// <typeparam name="T4">ee</typeparam>
    [Test("foo")]
    [Test("bar", Age = 2)]
    public abstract class AA<T1, T2, T3, T4> : Test, ITest2, IEnumerable<Test> where T1 : Test, ITest1, new() where T2 : struct where T3 : class, new()
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AA()
        {

        }

        /// <summary>
        /// aa
        /// </summary>
        /// <param name="a"></param>
        public AA(T1 a)
        {

        }

        /// <summary>
        /// xx
        /// </summary>
        /// <param name="a">aa</param>
        /// <param name="b">bb</param>
        /// <param name="c">cc</param>
        /// <param name="d">dd</param>
        public AA(IEnumerable<T2> a, List<IEnumerable<T1>> b, string c, List<int> d)
        {
        }

        /// <summary>
        /// cc
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public AA(T2 a, T1 b)
        {
        }

        /// <summary>
        /// 名称字段
        /// </summary>
        [XmlAttribute]
        public new string Name = "foo";

        /// <summary>
        /// 名称字段
        /// </summary>
        [XmlAttribute]
        public static string StaticField = "bar";

        ///// <summary>
        ///// 静态名称字段
        ///// </summary>
        //[XmlAttribute]
        //public static string StaticName = "bar";

        /// <summary>
        /// aaa
        /// </summary>
        public List<string>? Users;

        /// <summary>
        /// 静态属性
        /// </summary>
        public static int StaticProperty { get; set; }

        /// <summary>
        /// 虚拟属性
        /// </summary>
        public virtual int VirtualProperty { get; set; }

        /// <summary>
        /// 抽象属性
        /// </summary>
        public abstract int AbstractProperty { get; set; }

        /// <summary>
        /// 私有属性
        /// </summary>
        public int PrivateProperty { get; private set; }

        /// <summary>
        /// 内部属性
        /// </summary>
        public int InternalProperty { get; internal set; }

        /// <summary>
        /// 保护属性
        /// </summary>
        public int ProtectedProperty { get; protected set; }

        /// <summary>
        /// 获取或设置年龄
        /// </summary>
        public new int Age { get; set; }

        /// <summary>
        /// 获取Foo
        /// </summary>
        public new int Foo { get; }

        /// <summary>
        /// 设置Foo
        /// </summary>
        public int Bar { private get; set; }

        /// <summary>
        /// xx
        /// </summary>
        /// <typeparam name="T">type of t</typeparam>
        /// <param name="x">xxx</param>
        /// <param name="a">x</param>
        /// <param name="b">y</param>
        /// <param name="c">z</param>
        /// <param name="d">xx</param>
        public void Test<T>(T x, IEnumerable<T2> a, List<IEnumerable<T1>> b, string c, List<int> d) where T : class, new()
        {
        }

        public IEnumerator<Test> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        protected string GetData() { return string.Empty; }

        /// <summary>
        /// 测试事件
        /// </summary>
        public event EventHandler TestHandler = null!;

        /// <summary>
        /// TestInnerClass1 summary
        /// </summary>
        public class TestInnerClass1<T6>
        {
            /// <summary>
            /// TestInnerClass2 summary
            /// </summary>
            public class TestInnerClass2
            {
            }

            /// <summary>
            /// aaa
            /// </summary>
            public string? Name { get; set; }
        }
    }

    /// <summary>
    /// aa
    /// </summary>
    public class TestDerivedA : AA<Test, int, object, object>
    {
        /// <summary>
        /// aa
        /// </summary>
        /// <param name="a">aa</param>
        public TestDerivedA(Test a) : base(a)
        {
        }

        public override int AbstractProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    /// <summary>
    /// aa
    /// </summary>
    public class TestDerivedB : AA<Test, int, object, object>
    {
        /// <summary>
        /// aa
        /// </summary>
        /// <param name="a">aa</param>
        public TestDerivedB(Test a) : base(a)
        {
        }

        public override int AbstractProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    /// <summary>
    /// test attribute
    /// </summary>
    /// <param name="name"></param>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class TestAttribute(string name) : Attribute
    {
        public string Name { get; } = name;
        public int Age { get; set; }

        /// <summary>
        /// test b delegate
        /// </summary>
        public delegate void TestBDelegate();

        /// <summary>
        /// test event
        /// </summary>
        public event TestBDelegate TestEvent = null!;
    }

    /// <summary>
    /// TestADelegate summber
    /// </summary>
    /// <param name="a">xx</param>
    /// <returns>yy</returns>
    public delegate string TestADelegate(object a);
}

