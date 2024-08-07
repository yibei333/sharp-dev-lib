using SharpDevLib;
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
    public class Test : Test<int>
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
    /// test interface
    /// </summary>
    public interface ITest
    {

    }

    /// <summary>
    /// aa
    /// </summary>
    /// <typeparam name="T1">bb</typeparam>
    /// <typeparam name="T2">cc</typeparam>
    /// <typeparam name="T3">dd</typeparam>
    /// <typeparam name="T4">ee</typeparam>
    public abstract class AA<T1, T2, T3, T4> : ITest where T1 : Test, ITest, new() where T2 : struct where T3 : class, new()
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
        public string Name = "foo";

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
        /// 获取或设置年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 获取Foo
        /// </summary>
        public int Foo { get; }

        /// <summary>
        /// 设置Foo
        /// </summary>
        public int Bar { private get; set; }

        /// <summary>
        /// xx
        /// </summary>
        /// <param name="a">x</param>
        /// <param name="b">y</param>
        /// <param name="c">z</param>
        /// <param name="d">xx</param>
        public void Test(IEnumerable<T2> a, List<IEnumerable<T1>> b, string c, List<int> d)
        {

        }

        /// <summary>
        /// 测试事件
        /// </summary>
        public event EventHandler TestHandler = null!;
    }
}
