using ICSharpCode.SharpZipLib.BZip2;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal class Bz2DeCompressHandler(DeCompressRequest request) : DeCompressHandler(request)
{
    public override async Task HandleAsync()
    {
        try//tar ball
        {
            await base.HandleAsync();
        }
        catch//single file
        {
            var fileName = Request.SourceFile.GetFileName(false);
            Request.CurrentName = fileName;

            using var sourceStream = new FileInfo(Request.SourceFile).OpenOrCreate();
            Request.Total = sourceStream.Length;
            using var inputStream = new BZip2InputStream(sourceStream);
            using var outputStream = new FileInfo(Request.TargetPath.CombinePath(fileName)).OpenOrCreate();
            await inputStream.CopyToAsync(outputStream, Request.CancellationToken ?? CancellationToken.None, transfered => Request.Transfered += transfered);

            //bzip2 don't have uncompressed size meta data,so complete manual
            if (Request.OnProgress is not null && Request.Total > 0 && Request.Transfered != Request.Total)
            {
                Request.Transfered = Request.Total;
            }
        }
    }
}
