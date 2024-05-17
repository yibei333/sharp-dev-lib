using ICSharpCode.SharpZipLib.BZip2;

namespace SharpDevLib.Standard.Compression.DeCompress;

internal class Bz2DeCompressHandler : DeCompressHandler
{
    public Bz2DeCompressHandler(DeCompressOption option) : base(option)
    {
    }

    public override async Task HandleAsync()
    {
        try//tar ball
        {
            await base.HandleAsync();
        }
        catch//single file
        {
            var fileName = Option.SourceFile.GetFileName(false);
            Option.CurrentName = fileName;

            using var sourceStream = new FileInfo(Option.SourceFile).OpenOrCreate();
            Option.Total = sourceStream.Length;
            using var inputStream = new BZip2InputStream(sourceStream);
            using var outputStream = new FileInfo(Option.TargetPath.CombinePath(fileName)).OpenOrCreate();
            await inputStream.CopyToAsync(outputStream, Option.CancellationToken, transfered => Option.Transfered += transfered);

            //bzip2 don't have uncompressed size meta data,so complete manual
            if (Option.OnProgress is not null && Option.Total > 0 && Option.Transfered != Option.Total)
            {
                Option.Transfered = Option.Total;
            }
        }
    }
}
