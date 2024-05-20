using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Standard.Http;

[TestClass]
public class HttpGetTests : HttpBaseTests
{
    [TestMethod]
    public void GetTest()
    {
        var parameters = new Dictionary<string, string>
        {
            { "wd","foo" }
        };
        var request = new HttpKeyValueRequest("https://www.baidu.com", parameters)
        {
            OnSendProgress = p => Console.WriteLine($"send->{p}"),
            OnReceiveProgress = p => Console.WriteLine($"receive->{p}")
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Console.WriteLine(request);
        Console.WriteLine(response);
    }

    [TestMethod]
    public void CookieTest()
    {
        HttpGlobalSettings.TimeOut = TimeSpan.FromSeconds(4);
        HttpGlobalSettings.RetryCount = 3;
        HttpGlobalSettings.BaseUrl = "http://localhost:5268";

        var url = "WeatherForecast/cookie";
        var request = new HttpKeyValueRequest(url)
        {
            TimeOut = TimeSpan.FromSeconds(10),
            RetryCount = 2,
            Headers = new Dictionary<string, IEnumerable<string>>
            {
                { "Cookie",new string[]{ "BIDUPSID=601145944A9976FC12AF00B3136B48F0; PSTM=1715422096; BAIDUID=59DA881A28C93BDBA3A461A5358B8861:FG=1; delPer=0; PSINO=7; ZFY=m:Bnz:BEloKMh6w68:AuIR5F:Bb:BXP9xty8LFvFaveyp9H4:C; BAIDUID_BFESS=59DA881A28C93BDBA3A461A5358B8861:FG=1; H_WISE_SIDS=60175_60269_60274_60289_60299; H_PS_PSSID=60269_60274_60289_60299; BA_HECTOR=80aga10g04050ga5ak8lak85dtl3dc1j4lbe61v; BDRCVFR[S4-dAuiWMmn]=I67x6TjHwwYf0; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; RT=\"z=1&dm=baidu.com&si=7b9c824a-f453-4b8f-af11-4d5eae458720&ss=lw8my2mv&sl=0&tt=0&bcn=https%3A%2F%2Ffclog.baidu.com%2Flog%2Fweirwood%3Ftype%3Dperf\"" } }
            }
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Console.WriteLine(response);
    }
}
