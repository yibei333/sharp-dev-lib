namespace SharpDevLib.OpenXML.References.ExcelEncryption;

internal class UriHelper
{
    internal static Uri ResolvePartUri(Uri sourceUri, Uri targetUri)
    {
        if (targetUri.OriginalString.StartsWith("/") || targetUri.OriginalString.Contains("://")) return targetUri;
        var source = sourceUri.OriginalString.Split('/');
        var target = targetUri.OriginalString.Split('/');
        var t = target.Length - 1;
        var s = sourceUri.OriginalString.EndsWith("/") ? source.Length - 1 : source.Length - 2;
        var file = target[t--];

        while (t >= 0)
        {
            if (target[t] == ".") break;
            else if (target[t] == "..")
            {
                s--;
                t--;
            }
            else file = target[t--] + "/" + file;
        }

        if (s >= 0)
        {
            for (int i = s; i >= 0; i--) file = source[i] + "/" + file;
        }
        return new Uri(file, UriKind.RelativeOrAbsolute);
    }
}
