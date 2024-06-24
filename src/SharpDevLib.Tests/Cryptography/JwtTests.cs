using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Cryptography;
using System;

namespace SharpDevLib.Tests.Cryptography;

[TestClass]
public class JwtTests
{
    [TestMethod]
    public void JwtCreateWithHMACSHA256Test()
    {
        var expected = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJqYXJ2YW4iLCJleHAiOjE2NDMzMzU0NDMsInN1YiI6MTIzLCJuYW1lIjoibGVlIn0.3-k3ooDm1tFi1odWiI2p7UqNL56vc7JeeJ3h-5OSgFo";

        var key = "12345678901234567890123456789012";
        var payload = new
        {
            iss = "jarvan",
            exp = 1643335443,
            sub = 123,
            name = "lee"
        };
        var result = JwtExtension.Create(new JwtCreateWithHMACSHA256Request(payload, key.ToUtf8Bytes()));
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void JwtCreateWithRS256Test()
    {
        var expected = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJqYXJ2YW4iLCJleHAiOjE2NDMzMzU0NDMsInN1YiI6MTIzLCJuYW1lIjoibGVlIn0.ThyZS0v3_Kf187HUdXbwQjXN7CaFPgzNxejPWSeWS2bjnWTd2aQnNSYQwRRNbWtXNWktoiUbm1Dcdodx_vEfHItfDXm0HV4Ziga4-pXKc6daEfboxG0IbGtfUb6ufswHotz8zYiotY75Y3h2co-z3O2y-as8uGyEECpUXdx_v7eEa2MzgJVFM1n6VX_nJ8_-ogBHNiCND5MNsLmtYcpaL8eeuulLNsdCo2UFXxJEHDcVOXVNsGLnt23W6oDV5JATDkaFvSqpYMc2uS0Sn5dwV92PCOQjLO864e0tC4Gz6Jmg1nAX0Ilv9ttKr4YYymVtq07yrX1hhg-17iym1hpEUQ";

        var key = @"-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAtcY+oX+hBWlQDg4QaCoaccjc+pd3Ykxkd62sB97uC5wwacxO
KgDtLs+TX2bkTmmx+cleRfsNAlMiLLDlFR4vOXVYc1SR+m66e5qDHq2QBDgOnR07
XCHnWI7IBlrH4EzUDFVin9fuLN4Kz1b2WK96aMFgx9IThUgVsVlBfyTIOh4hMCsS
kDFAVxghvUhmKLQt9t6wvF4q02afl7npk4s3LyWW+Iqr7JFIsIWLmp/xaeX7JD7W
Av319BYCO4kyLYQxVn+Gq+X6cjYNcCWCrtRcRfsPXQS8w967FIDM/AhFvni2yvYQ
JU2L8hNNC6it91ehpMx1KkxeZKFxDLRt1jTcnQIDAQABAoIBAQCrnsS7TfkFVu0S
mAy9jCLzkyWwIxnAYUfqBrsJo1008P50pUSXO9C0BZ+qz0Z3EivLHmg3wlQCAeOe
NlXTnnAP287q9MnunGTNFpD9gfkbQqHpjRPiZNA8OHJ6LXPRPjhmfKdlK0XgbrPe
Fsj36pW0Gf/6KUns6KYyj4bbOCsjUn+1a7cbYB3c67hD2+GxYlB8JfPETAc6CDXB
ctxpYv3R8yoQnHSg7mU0xjjoKTraqjE2HATRVViQ2rjEbipNiP8BRbiciI22vX42
nEABi1NVaOqP/QF309/giwseYqCJbz/S2FQ/nKaKj1A+yz4Rrvec6kPKX4jqHrNT
X79qvLWBAoGBANtTZ55ruVY5ttMHdhgRM3x81/ypGEWUhww7wAsIEe3ZZZr+s6to
wizkM5xqOwKcwts1SJstu6H3Lc9cBvsfOWXifVXy055+9BzTgUA3kWuKS00mdeqc
VwpeEiIQs4o9nQMAw4DWC8hcF9BaZ84OmPAB4F8SxYelgA2bn9XC+FwFAoGBANQr
ZCk6VUnUyeTHxtz0v9IieH8K0TV3vSM3AKWU05lQQGPBz28TFGhsqvqQpvwftqlf
n7cDSkOLoWk1K/+zLM4oaJ9LqPKmgWs1SBHR/LOp2G/Pjqp7gwZNslr7B2s9RaUv
S80WjjZoXXJoF2YR5gaqtYmA6jyIt9kZIAjZDHm5AoGAA0NPZFT38edz59nRkPQY
jv6QfArL+KUICU+OQNvC4IX+c/rcE23AjchrWCVgcV6Bq580UFKy2usBfHdDB1Nk
mDZxZOjy6wW6ff9LifgJqs7o5eHvmSjwRpA1ttoGwcS+5D+LqQwGKtnr5MajirY+
4js06lUuKSF2MT2ieBypcz0CgYEAh12L0PYDtlBJ42pGiXCp9dLCWCO5qMhVZuNP
yVTzz+wwuLTNuMtOiPVT/PtPXqqJKvT0fJpfxkqO2AXxpXlWi82iOofWKcJr7c+X
xK7Z8HLbwTKGjmSxCtOFiKCCcjwsdCCB1z6dyz646CZbT2b6AKTnn+wdBjQgQCXU
l3CLkhkCgYANu3AoOG3PAoGJdfvar/AAsMQIaZDfohH+iCVv3oMVJnd2B+M5wDWL
FNZu+9DwgQsa8xdlpjzSSqVMRJctAGPSuSH+MCkRweLjg/ZHTkfebKgEBdxWRiaU
yyovRlVVcYhuJKJU4Zf7iY/cr0ZekQsecl6h5bW04xqwinfmMXyUWQ==
-----END RSA PRIVATE KEY-----";

        var payload = new
        {
            iss = "jarvan",
            exp = 1643335443,
            sub = 123,
            name = "lee"
        };
        var result = JwtExtension.Create(new JwtCreateWithRS256Request(payload, key, null));
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void JwtVerifyWithHMACSHA256Test()
    {
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJqYXJ2YW4iLCJleHAiOjE2NDMzMzU0NDMsInN1YiI6MTIzLCJuYW1lIjoibGVlIn0.3-k3ooDm1tFi1odWiI2p7UqNL56vc7JeeJ3h-5OSgFo";
        var key = "12345678901234567890123456789012";
        var result = JwtExtension.Verify(new JwtVerifyWithHMACSHA256Request(token, key.ToUtf8Bytes()));
        Console.WriteLine(result.Payload);
        Assert.IsTrue(result.IsVerified);
    }

    [TestMethod]
    public void JwtVerifyWithRS256Test()
    {
        var token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJqYXJ2YW4iLCJleHAiOjE2NDMzMzU0NDMsInN1YiI6MTIzLCJuYW1lIjoibGVlIn0.ThyZS0v3_Kf187HUdXbwQjXN7CaFPgzNxejPWSeWS2bjnWTd2aQnNSYQwRRNbWtXNWktoiUbm1Dcdodx_vEfHItfDXm0HV4Ziga4-pXKc6daEfboxG0IbGtfUb6ufswHotz8zYiotY75Y3h2co-z3O2y-as8uGyEECpUXdx_v7eEa2MzgJVFM1n6VX_nJ8_-ogBHNiCND5MNsLmtYcpaL8eeuulLNsdCo2UFXxJEHDcVOXVNsGLnt23W6oDV5JATDkaFvSqpYMc2uS0Sn5dwV92PCOQjLO864e0tC4Gz6Jmg1nAX0Ilv9ttKr4YYymVtq07yrX1hhg-17iym1hpEUQ";
        var key = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtcY+oX+hBWlQDg4QaCoa
ccjc+pd3Ykxkd62sB97uC5wwacxOKgDtLs+TX2bkTmmx+cleRfsNAlMiLLDlFR4v
OXVYc1SR+m66e5qDHq2QBDgOnR07XCHnWI7IBlrH4EzUDFVin9fuLN4Kz1b2WK96
aMFgx9IThUgVsVlBfyTIOh4hMCsSkDFAVxghvUhmKLQt9t6wvF4q02afl7npk4s3
LyWW+Iqr7JFIsIWLmp/xaeX7JD7WAv319BYCO4kyLYQxVn+Gq+X6cjYNcCWCrtRc
RfsPXQS8w967FIDM/AhFvni2yvYQJU2L8hNNC6it91ehpMx1KkxeZKFxDLRt1jTc
nQIDAQAB
-----END PUBLIC KEY-----";
        var result = JwtExtension.Verify(new JwtVerifyWithRS256Request(token, key));
        Console.WriteLine(result.Payload);
        Assert.IsTrue(result.IsVerified);
    }
}
