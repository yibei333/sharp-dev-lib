using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Data;
using System;
using System.IO;
using System.Text;

namespace SharpDevLib.Tests;

[TestClass]
public class HashUtilTests
{
    [TestMethod]
    public void ToHexStringTest()
    {
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.ToHexString());
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.ToHexString());
        Assert.AreEqual("48656c6c6f2c576f726c6421", Encoding.UTF8.GetBytes("Hello,World!").ToHexString());
    }

    [TestMethod]
    public void FromHexStringTest()
    {
        Assert.AreEqual(Array.Empty<byte>(), string.Empty.FromHexString());
        Assert.AreEqual("Hello,World!", Encoding.UTF8.GetString("48656c6c6f2c576f726c6421".FromHexString()));
    }

    #region MD5
    [TestMethod]
    public void MD5HashObjectTest()
    {
        Department? department = null;
        Assert.AreEqual(string.Empty, department.MD5Hash());
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("1816e3c7c98219ab", department.MD5Hash(MD5Length.Sixteen));
        Assert.AreEqual("1816e3c7c98219ab", department.MD5Hash(MD5Length.Sixteen, WordCase.LowerCase));
        Assert.AreEqual("1816e3c7c98219ab".ToUpper(), department.MD5Hash(MD5Length.Sixteen, WordCase.UpperCase));
        Assert.AreEqual("b054482d1816e3c7c98219abc418529f", department.MD5Hash());
        Assert.AreEqual("b054482d1816e3c7c98219abc418529f", department.MD5Hash(MD5Length.ThirtyTwo));
        Assert.AreEqual("b054482d1816e3c7c98219abc418529f", department.MD5Hash(MD5Length.ThirtyTwo, WordCase.LowerCase));
        Assert.AreEqual("b054482d1816e3c7c98219abc418529f".ToUpper(), department.MD5Hash(MD5Length.ThirtyTwo, WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("HelloWorld", "68e109f0f40ca72a15e05cc22786f8e6")]
    public void MD5HashStringTest(string str, string expected)
    {
        var actual = str.MD5Hash();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void MD5HashBytesTest()
    {
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.MD5Hash());
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.MD5Hash());
        Assert.AreEqual("68e109f0f40ca72a15e05cc22786f8e6", Encoding.UTF8.GetBytes("HelloWorld").MD5Hash());
    }

    [TestMethod]
    public void FileMD5HashTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var actual = path.FileMD5Hash();
        Assert.AreEqual("98f97a791ef1457579a5b7e88a495063", actual);
        Assert.AreEqual("1ef1457579a5b7e8", path.FileMD5Hash(MD5Length.Sixteen,WordCase.LowerCase));
        Assert.AreEqual("1ef1457579a5b7e8".ToUpper(), path.FileMD5Hash(MD5Length.Sixteen,WordCase.UpperCase));

        //big file test
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //path = @"a:\a.iso";
        //actual = path.FileMD5Hash();
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //Assert.AreEqual("5b44da6018e1d04fa91aaa3840e3870c", actual);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void FileMD5HashExceptionTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.text");
        _ = path.FileMD5Hash();
    }

    [TestMethod]
    public void FileMD5HashStreamTest()
    {
        Stream? stream = new MemoryStream();
        Assert.AreEqual(string.Empty, stream.FileMD5Hash());
        stream = null;
        Assert.AreEqual(string.Empty, stream.FileMD5Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        stream = File.OpenRead(path);
        Assert.AreEqual("98f97a791ef1457579a5b7e88a495063", stream.FileMD5Hash());
    }

    [TestMethod]
    public void HMACMD5HashObjectTest()
    {
        var secret = "123456";
        Department? department = null;
        Assert.AreEqual(string.Empty, department.HMACMD5Hash(secret));
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("215b6a8ea8cc0069", department.HMACMD5Hash(secret, MD5Length.Sixteen));
        Assert.AreEqual("215b6a8ea8cc0069", department.HMACMD5Hash(secret, MD5Length.Sixteen, WordCase.LowerCase));
        Assert.AreEqual("215b6a8ea8cc0069".ToUpper(), department.HMACMD5Hash(secret, MD5Length.Sixteen, WordCase.UpperCase));
        Assert.AreEqual("e67bb7b9215b6a8ea8cc00697a780f70", department.HMACMD5Hash(secret));
        Assert.AreEqual("e67bb7b9215b6a8ea8cc00697a780f70", department.HMACMD5Hash(secret, MD5Length.ThirtyTwo));
        Assert.AreEqual("e67bb7b9215b6a8ea8cc00697a780f70", department.HMACMD5Hash(secret, MD5Length.ThirtyTwo, WordCase.LowerCase));
        Assert.AreEqual("e67bb7b9215b6a8ea8cc00697a780f70".ToUpper(), department.HMACMD5Hash(secret, MD5Length.ThirtyTwo, WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow(null, "", "")]
    [DataRow("HelloWorld", "123456", "5a9d6cad10013b16cf76af249754c02c")]
    [DataRow("HelloWorld", "", "dfa0ebcd46d978e041febdf4972aa274")]
    public void HMACMD5HashStringTest(string str, string secret, string expected)
    {
        var actual = str.HMACMD5Hash(secret);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void HMACMD5HashExceptionTest()
    {
        var secret = "01234567890123456789012345678901234567890123456789012345678901234";
        _ = "abc".HMACMD5Hash(secret);
    }

    [TestMethod]
    public void HMACMD5HashBytesTest()
    {
        var secret = "123456";
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.HMACMD5Hash(secret));
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.HMACMD5Hash(secret));
        Assert.AreEqual("5a9d6cad10013b16cf76af249754c02c", Encoding.UTF8.GetBytes("HelloWorld").HMACMD5Hash(secret));
    }
    #endregion

    #region SHA128
    [TestMethod]
    public void SHA128HashObjectTest()
    {
        Department? department = null;
        Assert.AreEqual(string.Empty, department.SHA128Hash());
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("6eacf0b7c9c2e004a6f59e9b55565e8a70415eab", department.SHA128Hash());
        Assert.AreEqual("6eacf0b7c9c2e004a6f59e9b55565e8a70415eab", department.SHA128Hash(WordCase.LowerCase));
        Assert.AreEqual("6eacf0b7c9c2e004a6f59e9b55565e8a70415eab".ToUpper(), department.SHA128Hash(WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("HelloWorld", "db8ac1c259eb89d4a131b253bacfca5f319d54f2")]
    public void SHA128HashStringTest(string str, string expected)
    {
        var actual = str.SHA128Hash();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SHA128HashBytesTest()
    {
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.SHA128Hash());
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.SHA128Hash());
        Assert.AreEqual("db8ac1c259eb89d4a131b253bacfca5f319d54f2", Encoding.UTF8.GetBytes("HelloWorld").SHA128Hash());
    }

    [TestMethod]
    public void FileSHA128HashTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var actual = path.FileSHA128Hash();
        Assert.AreEqual("16ad856b462e68f965f6e93f66282a7ae891fdbc", actual);
        Assert.AreEqual("16ad856b462e68f965f6e93f66282a7ae891fdbc", path.FileSHA128Hash(WordCase.LowerCase));
        Assert.AreEqual("16ad856b462e68f965f6e93f66282a7ae891fdbc".ToUpper(), path.FileSHA128Hash(WordCase.UpperCase));

        //big file test
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //path = @"a:\a.iso";
        //actual = path.FileSHA128Hash();
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //Assert.AreEqual("b7472802105507ad5706634f104976a109d6fae7", actual);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void FileSHA128HashExceptionTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.text");
        _ = path.FileSHA128Hash();
    }

    [TestMethod]
    public void FileSHA128HashStreamTest()
    {
        Stream? stream = new MemoryStream();
        Assert.AreEqual(string.Empty, stream.FileSHA128Hash());
        stream = null;
        Assert.AreEqual(string.Empty, stream.FileSHA128Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        stream = File.OpenRead(path);
        Assert.AreEqual("16ad856b462e68f965f6e93f66282a7ae891fdbc", stream.FileSHA128Hash());
    }

    [TestMethod]
    public void HMACSHA128HashObjectTest()
    {
        var secret = "123456";
        Department? department = null;
        Assert.AreEqual(string.Empty, department.HMACSHA128Hash(secret));
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("cc75338e5f9952ad416b7216e79c7ec164774efe", department.HMACSHA128Hash(secret));
        Assert.AreEqual("cc75338e5f9952ad416b7216e79c7ec164774efe", department.HMACSHA128Hash(secret, WordCase.LowerCase));
        Assert.AreEqual("cc75338e5f9952ad416b7216e79c7ec164774efe".ToUpper(), department.HMACSHA128Hash(secret, WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow(null, "", "")]
    [DataRow("HelloWorld", "123456", "6c9b4005380a35182dc932ba18206cd2a2c6c51e")]
    [DataRow("HelloWorld", "", "23e531ab48f131c1e3c1c06b7a0d2ef20b0d2735")]
    public void HMACSHA128HashStringTest(string str, string secret, string expected)
    {
        var actual = str.HMACSHA128Hash(secret);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HMACSHA128HashBytesTest()
    {
        var secret = "123456";
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.HMACSHA128Hash(secret));
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.HMACSHA128Hash(secret));
        Assert.AreEqual("6c9b4005380a35182dc932ba18206cd2a2c6c51e", Encoding.UTF8.GetBytes("HelloWorld").HMACSHA128Hash(secret));
    }
    #endregion

    #region SHA256
    [TestMethod]
    public void SHA256HashObjectTest()
    {
        Department? department = null;
        Assert.AreEqual(string.Empty, department.SHA256Hash());
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("416b7bc48d34adbcfb080fdb47f4a8822d457c8cb413cc633457ca866217d03c", department.SHA256Hash());
        Assert.AreEqual("416b7bc48d34adbcfb080fdb47f4a8822d457c8cb413cc633457ca866217d03c", department.SHA256Hash(WordCase.LowerCase));
        Assert.AreEqual("416b7bc48d34adbcfb080fdb47f4a8822d457c8cb413cc633457ca866217d03c".ToUpper(), department.SHA256Hash(WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("HelloWorld", "872e4e50ce9990d8b041330c47c9ddd11bec6b503ae9386a99da8584e9bb12c4")]
    public void SHA256HashStringTest(string str, string expected)
    {
        var actual = str.SHA256Hash();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SHA256HashBytesTest()
    {
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.SHA256Hash());
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.SHA256Hash());
        Assert.AreEqual("872e4e50ce9990d8b041330c47c9ddd11bec6b503ae9386a99da8584e9bb12c4", Encoding.UTF8.GetBytes("HelloWorld").SHA256Hash());
    }

    [TestMethod]
    public void FileSHA256HashTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var actual = path.FileSHA256Hash();
        Assert.AreEqual("8f4ec1811c6c4261c97a7423b3a56d69f0f160074f39745af20bb5fcf65ccf78", actual);

        //big file test
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //path = @"a:\a.iso";
        //actual = path.FileSHA256Hash();
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //Assert.AreEqual("f476e2c4fa66aef5b1aca9be40c03315dd3cfff5c8eca167cf711c5b4b823b96", actual);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void FileSHA256HashExceptionTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.text");
        _ = path.FileSHA256Hash();
    }

    [TestMethod]
    public void FileSHA256HashStreamTest()
    {
        Stream? stream = new MemoryStream();
        Assert.AreEqual(string.Empty, stream.FileSHA256Hash());
        stream = null;
        Assert.AreEqual(string.Empty, stream.FileSHA256Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        stream = File.OpenRead(path);
        Assert.AreEqual("8f4ec1811c6c4261c97a7423b3a56d69f0f160074f39745af20bb5fcf65ccf78", stream.FileSHA256Hash());
    }

    [TestMethod]
    public void HMACSHA256HashObjectTest()
    {
        var secret = "123456";
        Department? department = null;
        Assert.AreEqual(string.Empty, department.HMACSHA256Hash(secret));
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("6f9716ecf8a93bcc1efd7f0d6b0ab1ae074fdf9ecef51c46322fbe6de89ccc72", department.HMACSHA256Hash(secret));
        Assert.AreEqual("6f9716ecf8a93bcc1efd7f0d6b0ab1ae074fdf9ecef51c46322fbe6de89ccc72", department.HMACSHA256Hash(secret, WordCase.LowerCase));
        Assert.AreEqual("6f9716ecf8a93bcc1efd7f0d6b0ab1ae074fdf9ecef51c46322fbe6de89ccc72".ToUpper(), department.HMACSHA256Hash(secret, WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow(null, "", "")]
    [DataRow("HelloWorld", "123456", "09fe2d09af902698dd3e1515f2e2347d84f493e3ec5f9e925b23a8d95f240611")]
    [DataRow("HelloWorld", "", "a77d3694491c2109157bc896a06b5eb92eb1510b6d8c5ed8932da221c022aa0e")]
    public void HMACSHA256HashStringTest(string str, string secret, string expected)
    {
        var actual = str.HMACSHA256Hash(secret);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HMACSHA256HashBytesTest()
    {
        var secret = "123456";
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.HMACSHA256Hash(secret));
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.HMACSHA256Hash(secret));
        Assert.AreEqual("09fe2d09af902698dd3e1515f2e2347d84f493e3ec5f9e925b23a8d95f240611", Encoding.UTF8.GetBytes("HelloWorld").HMACSHA256Hash(secret));
    }
    #endregion

    #region SHA384
    [TestMethod]
    public void SHA384HashObjectTest()
    {
        Department? department = null;
        Assert.AreEqual(string.Empty, department.SHA384Hash());
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("71ae5740aa5345eb193026aa557d6057a3abd8e14b08ceaf37f23aac3e18b03bc20b025b2ada775018a95f899f187c25", department.SHA384Hash());
        Assert.AreEqual("71ae5740aa5345eb193026aa557d6057a3abd8e14b08ceaf37f23aac3e18b03bc20b025b2ada775018a95f899f187c25", department.SHA384Hash(WordCase.LowerCase));
        Assert.AreEqual("71ae5740aa5345eb193026aa557d6057a3abd8e14b08ceaf37f23aac3e18b03bc20b025b2ada775018a95f899f187c25".ToUpper(), department.SHA384Hash(WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("HelloWorld", "293cd96eb25228a6fb09bfa86b9148ab69940e68903cbc0527a4fb150eec1ebe0f1ffce0bc5e3df312377e0a68f1950a")]
    public void SHA384HashStringTest(string str, string expected)
    {
        var actual = str.SHA384Hash();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SHA384HashBytesTest()
    {
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.SHA384Hash());
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.SHA384Hash());
        Assert.AreEqual("293cd96eb25228a6fb09bfa86b9148ab69940e68903cbc0527a4fb150eec1ebe0f1ffce0bc5e3df312377e0a68f1950a", Encoding.UTF8.GetBytes("HelloWorld").SHA384Hash());
    }

    [TestMethod]
    public void FileSHA384HashTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var actual = path.FileSHA384Hash();
        Assert.AreEqual("75cc227fe8076e0456123113694e0fed43d28f45f8a4a67894732fe9893b0ab6194cb86b57f5f67316263382e2d72c4b", actual);

        //big file test
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //path = @"a:\a.iso";
        //actual = path.FileSHA384Hash();
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //Assert.AreEqual("b9ff505c91b60b20e1adffbfbe189eb513634853c457963434c1d33c64f108b61efca8e25f1b0815006db4a74185d98b", actual);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void FileSHA384HashExceptionTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.text");
        _ = path.FileSHA384Hash();
    }

    [TestMethod]
    public void FileSHA384HashStreamTest()
    {
        Stream? stream = new MemoryStream();
        Assert.AreEqual(string.Empty, stream.FileSHA384Hash());
        stream = null;
        Assert.AreEqual(string.Empty, stream.FileSHA384Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        stream = File.OpenRead(path);
        Assert.AreEqual("75cc227fe8076e0456123113694e0fed43d28f45f8a4a67894732fe9893b0ab6194cb86b57f5f67316263382e2d72c4b", stream.FileSHA384Hash());
    }

    [TestMethod]
    public void HMACSHA384HashObjectTest()
    {
        var secret = "123456";
        Department? department = null;
        Assert.AreEqual(string.Empty, department.HMACSHA384Hash(secret));
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("6d09ecb444d6054441a1e3d9163eb61eb91de37371032509bab623dcb384a63392c4ef1d39385a2180c4ec4d1fea65d7", department.HMACSHA384Hash(secret));
        Assert.AreEqual("6d09ecb444d6054441a1e3d9163eb61eb91de37371032509bab623dcb384a63392c4ef1d39385a2180c4ec4d1fea65d7", department.HMACSHA384Hash(secret, WordCase.LowerCase));
        Assert.AreEqual("6d09ecb444d6054441a1e3d9163eb61eb91de37371032509bab623dcb384a63392c4ef1d39385a2180c4ec4d1fea65d7".ToUpper(), department.HMACSHA384Hash(secret, WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow(null, "", "")]
    [DataRow("HelloWorld", "123456", "e7f7e79d9c196b0dd6ed6119e8e79244fa9f3f7f1e754cc718efee4f9b408b88487a05802c04720bbec5195aec520d0f")]
    [DataRow("HelloWorld", "", "0f3afd555211ed2df2ea88c643795b997f0f66833f274e08fe6b86edb8641a57c55ea2068c2b1b896c14e897aefa6262")]
    public void HMACSHA384HashStringTest(string str, string secret, string expected)
    {
        var actual = str.HMACSHA384Hash(secret);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HMACSHA384HashBytesTest()
    {
        var secret = "123456";
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.HMACSHA384Hash(secret));
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.HMACSHA384Hash(secret));
        Assert.AreEqual("e7f7e79d9c196b0dd6ed6119e8e79244fa9f3f7f1e754cc718efee4f9b408b88487a05802c04720bbec5195aec520d0f", Encoding.UTF8.GetBytes("HelloWorld").HMACSHA384Hash(secret));
    }
    #endregion

    #region SHA512
    [TestMethod]
    public void SHA512HashObjectTest()
    {
        Department? department = null;
        Assert.AreEqual(string.Empty, department.SHA512Hash());
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("1088932d5f33d39e5411d95cfbc9257e9a018f99293291be5153f386bf37388e3edd3a24c8cf3bf42ae840a9a838ca290766e94473b501f3ef72c805d4fd7e27", department.SHA512Hash());
        Assert.AreEqual("1088932d5f33d39e5411d95cfbc9257e9a018f99293291be5153f386bf37388e3edd3a24c8cf3bf42ae840a9a838ca290766e94473b501f3ef72c805d4fd7e27", department.SHA512Hash(WordCase.LowerCase));
        Assert.AreEqual("1088932d5f33d39e5411d95cfbc9257e9a018f99293291be5153f386bf37388e3edd3a24c8cf3bf42ae840a9a838ca290766e94473b501f3ef72c805d4fd7e27".ToUpper(), department.SHA512Hash(WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("HelloWorld", "8ae6ae71a75d3fb2e0225deeb004faf95d816a0a58093eb4cb5a3aa0f197050d7a4dc0a2d5c6fbae5fb5b0d536a0a9e6b686369fa57a027687c3630321547596")]
    public void SHA512HashStringTest(string str, string expected)
    {
        var actual = str.SHA512Hash();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SHA512HashBytesTest()
    {
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.SHA512Hash());
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.SHA512Hash());
        Assert.AreEqual("8ae6ae71a75d3fb2e0225deeb004faf95d816a0a58093eb4cb5a3aa0f197050d7a4dc0a2d5c6fbae5fb5b0d536a0a9e6b686369fa57a027687c3630321547596", Encoding.UTF8.GetBytes("HelloWorld").SHA512Hash());
    }

    [TestMethod]
    public void FileSHA512HashTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var actual = path.FileSHA512Hash();
        Assert.AreEqual("75fbed00cbfdfaa6eba25791df285ec7fd6135fe8443adf86d4dc2bbfe061e84c3061e280c01366286c0366276976903666627c6d8e0a69b192c665bae8adb0b", actual);

        //big file test
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //path = @"a:\a.iso";
        //actual = path.FileSHA512Hash();
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //Assert.AreEqual("3fecf30c244b7e958f7c7c58c8bac58090bb9fd96e6275e25685c84a219e122a342a788fa12944eed3d79d29dc550e040b9e214bdd862e3ed729e54be0702ac6", actual);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void FileSHA512HashExceptionTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.text");
        _ = path.FileSHA512Hash();
    }

    [TestMethod]
    public void FileSHA512HashStreamTest()
    {
        Stream? stream = new MemoryStream();
        Assert.AreEqual(string.Empty, stream.FileSHA512Hash());
        stream = null;
        Assert.AreEqual(string.Empty, stream.FileSHA512Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        stream = File.OpenRead(path);
        Assert.AreEqual("75fbed00cbfdfaa6eba25791df285ec7fd6135fe8443adf86d4dc2bbfe061e84c3061e280c01366286c0366276976903666627c6d8e0a69b192c665bae8adb0b", stream.FileSHA512Hash());
    }

    [TestMethod]
    public void HMACSHA512HashObjectTest()
    {
        var secret = "123456";
        Department? department = null;
        Assert.AreEqual(string.Empty, department.HMACSHA512Hash(secret));
        department = Department.Create();
        department.Id = Guid.Empty;
        Assert.AreEqual("832737444a80b424a9d3d65c4f50949070cc63f177b4f02d48ea4d38e89cf1c94d87c13ee0c2b01f9473e15fcdb2c3768a744281142dfdaa870945d617b437bf", department.HMACSHA512Hash(secret));
        Assert.AreEqual("832737444a80b424a9d3d65c4f50949070cc63f177b4f02d48ea4d38e89cf1c94d87c13ee0c2b01f9473e15fcdb2c3768a744281142dfdaa870945d617b437bf", department.HMACSHA512Hash(secret, WordCase.LowerCase));
        Assert.AreEqual("832737444a80b424a9d3d65c4f50949070cc63f177b4f02d48ea4d38e89cf1c94d87c13ee0c2b01f9473e15fcdb2c3768a744281142dfdaa870945d617b437bf".ToUpper(), department.HMACSHA512Hash(secret, WordCase.UpperCase));
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow(null, "", "")]
    [DataRow("HelloWorld", "123456", "bda7013253bc356f14af2b2bd11c8ec0606385299d26e345835be196922ac617ce745f6cf1008dbb5739710b2afe0faaa656a40fb8d66ef674f1dac7f2e8f044")]
    [DataRow("HelloWorld", "", "224b7ae7552078cf59e539c9911932155dc6ae1d0ddcff3ba3991d943f50a9f841f553c05710e1905a1b6d358ed7e97522a589ba7b07399f90b91dad1141f81c")]
    public void HMACSHA512HashStringTest(string str, string secret, string expected)
    {
        var actual = str.HMACSHA512Hash(secret);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HMACSHA512HashBytesTest()
    {
        var secret = "123456";
        var bytes = Array.Empty<byte>();
        Assert.AreEqual(string.Empty, bytes.HMACSHA512Hash(secret));
        bytes = null;
        Assert.AreEqual(string.Empty, bytes.HMACSHA512Hash(secret));
        Assert.AreEqual("bda7013253bc356f14af2b2bd11c8ec0606385299d26e345835be196922ac617ce745f6cf1008dbb5739710b2afe0faaa656a40fb8d66ef674f1dac7f2e8f044", Encoding.UTF8.GetBytes("HelloWorld").HMACSHA512Hash(secret));
    }
    #endregion
}
