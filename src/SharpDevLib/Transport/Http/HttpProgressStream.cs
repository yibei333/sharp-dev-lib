namespace SharpDevLib;

internal class HttpProgressStream(Stream innerStream, Action<long> onProgress) : Stream
{
    readonly Stream _innerStream = innerStream;
    readonly Action<long> _onProgress = onProgress;
    long _bytesTransfered = 0;

    public override bool CanRead => _innerStream.CanRead;

    public override bool CanSeek => _innerStream.CanSeek;

    public override bool CanWrite => _innerStream.CanWrite;

    public override long Length => _innerStream.Length;

    public override long Position { get => _innerStream.Position; set => _innerStream.Position = value; }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _innerStream.Read(buffer, offset, count);
        ReportBytesTransfered(bytesRead);
        return bytesRead;
    }

    public override int ReadByte()
    {
        int byteRead = _innerStream.ReadByte();
        ReportBytesTransfered(byteRead);
        return byteRead;
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int readCount = await _innerStream.ReadAsync(buffer, offset, count, cancellationToken);
        ReportBytesTransfered(readCount);
        return readCount;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _innerStream.Write(buffer, offset, count);
        ReportBytesTransfered(count);
    }

    public override void WriteByte(byte value)
    {
        _innerStream.WriteByte(value);
        ReportBytesTransfered(1);
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        await _innerStream.WriteAsync(buffer, offset, count, cancellationToken);
        ReportBytesTransfered(count);
    }

    internal void ReportBytesTransfered(int transfered)
    {
        if (transfered <= 0) return;

        _bytesTransfered += transfered;
        _onProgress(_bytesTransfered);
    }

    public override void Flush()
    {
        _innerStream.Flush();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _innerStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _innerStream.SetLength(value);
    }
}
