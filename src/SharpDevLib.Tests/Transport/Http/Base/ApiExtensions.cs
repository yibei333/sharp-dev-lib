using EmbedIO;
using System.Text;

namespace SharpDevLib.Tests.Transport.Http.Base;

public static class ApiExtensions
{
    public static void WriteObject(this IHttpContext context, object? obj)
    {
        context.Response.ContentType = MimeType.PlainText;
        using var writer = context.OpenResponseText(Encoding.UTF8, true);

        var text = string.Empty;
        if (obj is not null)
        {
            var type = obj.GetType();
            if (type.IsClass && type != typeof(string))
            {
                text = obj.Serialize();
                context.Response.ContentType = MimeType.Json;
            }
            else text = obj.ToString();
        }
        writer.Write(text);

        context.Response.ContentLength64 = text.IsNullOrWhiteSpace() ? 0 : text.Utf8Decode().Length;
    }
}