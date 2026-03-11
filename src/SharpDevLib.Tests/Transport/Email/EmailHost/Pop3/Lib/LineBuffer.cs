using System.Linq;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

public class LineBuffer(int bufferSize)
{
    const byte CR = 13;
    const byte LF = 10;
    readonly byte[] buffer = new byte[bufferSize];
    int startIndex = 0;
    int usedLength = 0;
    bool expectLF = false;

    public byte[] Buffer => buffer;
    public int VacantStart => startIndex + usedLength;
    public int VacantLength => buffer.Length - VacantStart;

    public void UpdateUsedBytes(int bytesIn)
    {
        usedLength += bytesIn;
    }

    public ByteString GetLine()
    {
        ConsumeLF();
        var line = ScanForEndOfLine();

        if (line != null)
        {
            ConsumeLF();
            if (usedLength == 0) startIndex = 0;
        }
        else if (startIndex == 0 && usedLength == buffer.Length)
        {
            line = ByteString.FromBytes(buffer);
            usedLength = 0;
        }
        else if (VacantLength == 0 && startIndex > 0)
        {
            System.Buffer.BlockCopy(buffer, startIndex, buffer, 0, usedLength);
            startIndex = 0;
        }
        return line!;
    }

    void ConsumeLF()
    {
        if (usedLength > 0 && expectLF)
        {
            if (buffer[startIndex] == LF)
            {
                startIndex += 1;
                usedLength -= 1;
            }
            expectLF = false;
        }
    }

    ByteString ScanForEndOfLine()
    {
        foreach (int offset in Enumerable.Range(0, usedLength))
        {
            var atOffset = buffer[startIndex + offset];
            if (atOffset == CR || atOffset == LF)
            {
                var line = ByteString.FromBytes(buffer, startIndex, offset);
                startIndex += offset + 1;
                usedLength -= offset + 1;
                if (atOffset == CR) expectLF = true;
                return line;
            }
        }
        return null!;
    }

    internal void Clear()
    {
        startIndex = 0;
        usedLength = 0;
        expectLF = false;
    }
}
