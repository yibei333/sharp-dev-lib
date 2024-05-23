using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3.Lib;

public static class TlsCertificateValidators
{
    public static byte[] ThumbprintSHA256Bytes(this X509Certificate cert) => SHA256.HashData(cert.GetRawCertData());

    public static string ThumprintSSHA256Hex(this X509Certificate cert) => string.Concat(cert.ThumbprintSHA256Bytes().Select(by => by.ToString("X2")));

    public static string ThumbprintSHA256Base64(this X509Certificate cert) => Convert.ToBase64String(cert.ThumbprintSHA256Bytes());

    public static bool IsThumbprintMatchAny(this X509Certificate cert, params string[] thumbprints) => cert.IsThumbprintMatchAny(thumbprints.AsEnumerable());

    public static bool IsThumbprintMatchAny(this X509Certificate cert, IEnumerable<string> thumbprints) => thumbprints.Any(thumbprint => cert.IsThumbprintMatch(thumbprint));

    public static bool IsThumbprintMatch(this X509Certificate cert, string thumbprint)
    {
        thumbprint = thumbprint.Trim().Replace(":", "").TrimEnd('=');
        if (string.Equals(cert.ThumprintSSHA256Hex(), thumbprint, StringComparison.InvariantCultureIgnoreCase)) return true;
        if (cert.ThumbprintSHA256Base64().TrimEnd('=') == thumbprint) return true;
        return false;
    }

    public static RemoteCertificateValidationCallback And(this RemoteCertificateValidationCallback x, RemoteCertificateValidationCallback y)
    {
        return Internal!;
        bool Internal(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => x(sender, cert, chain, errors) && y(sender, cert, chain, errors);
    }

    public static RemoteCertificateValidationCallback Or(this RemoteCertificateValidationCallback x, RemoteCertificateValidationCallback y)
    {
        return Internal!;
        bool Internal(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => x(sender, cert, chain, errors) || y(sender, cert, chain, errors);
    }

    public static RemoteCertificateValidationCallback All(params RemoteCertificateValidationCallback[] validators) => All(validators.AsEnumerable());

    public static RemoteCertificateValidationCallback All(IEnumerable<RemoteCertificateValidationCallback> validators)
    {
        return Internal!;
        bool Internal(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => validators.All(val => val(sender, cert, chain, errors));
    }

    public static RemoteCertificateValidationCallback Any(params RemoteCertificateValidationCallback[] validators)
        => Any(validators.AsEnumerable());

    public static RemoteCertificateValidationCallback Any(IEnumerable<RemoteCertificateValidationCallback> validators)
    {
        return Internal!;
        bool Internal(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => validators.Any(val => val(sender, cert, chain, errors));
    }

    public static RemoteCertificateValidationCallback AllowPinned(params string[] allowHash) => AllowPinned(allowHash.AsEnumerable());

    public static RemoteCertificateValidationCallback AllowPinned(IEnumerable<string> allowHash)
    {
        return Internal!;
        bool Internal(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => cert.IsThumbprintMatchAny(allowHash);
    }

    public static RemoteCertificateValidationCallback AllowPerPolicy()
    {
        return Internal!;

        static bool Internal(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => errors == SslPolicyErrors.None;
    }
}

