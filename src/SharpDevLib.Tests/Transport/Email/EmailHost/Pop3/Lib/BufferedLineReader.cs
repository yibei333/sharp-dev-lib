using System;
using System.IO;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

public class BufferedLineReader(Stream stream, int maxLineLength)
{
    const byte CR = 13;
    const byte LF = 10;
    readonly Stream stream = stream;
    readonly byte[] buffer = new byte[maxLineLength];
    int startIndex = 0;
    int usedLength = 0;
    int AvailIndex => startIndex + usedLength;
    int AvailLength => buffer.Length - AvailIndex;
    bool expectLF = false;
    bool streamHasClosed = false;

    public delegate void OnLineReadDelegate(ByteString? line, bool isCompleteLine);

    public void ReadLine(OnLineReadDelegate onLineRead)
    {
        Scan();

        void Scan()
        {
            if (expectLF && usedLength > 0)
            {
                if (buffer[startIndex] == LF)
                {
                    startIndex += 1;
                    usedLength -= 1;
                }

                expectLF = false;
            }

            for (int offset = 0; offset < usedLength; offset++)
            {
                byte atScan = buffer[startIndex + offset];
                if (atScan == CR || atScan == LF)
                {
                    var line = ByteString.FromBytes(buffer, startIndex, offset);

                    int lineWithEndLength = offset + 1;
                    startIndex += lineWithEndLength;
                    usedLength -= lineWithEndLength;

                    if (atScan == CR) expectLF = true;

                    onLineRead(line, true);

                    return;
                }
            }

            if (streamHasClosed)
            {
                if (usedLength > 0)
                {
                    var line = ByteString.FromBytes(buffer, startIndex, usedLength);
                    startIndex = 0;
                    usedLength = 0;

                    onLineRead(line, true);
                }
                else onLineRead(null, true);

                return;
            }

            if (startIndex == 0 && usedLength == buffer.Length)
            {
                var line = ByteString.FromBytes(buffer);

                startIndex = 0;
                usedLength = 0;

                onLineRead(line, false);
                return;
            }

            if (usedLength == 0 && startIndex > 0) startIndex = 0;

            if (AvailLength == 0 && startIndex > 0)
            {
                Buffer.BlockCopy(buffer, startIndex, buffer, 0, usedLength);
                startIndex = 0;
            }
            stream.BeginRead(buffer, AvailIndex, AvailLength, OnEndRead, null);
        }

        void OnEndRead(IAsyncResult iar)
        {
            int bytesIn = 0;
            Helpers.TryCallCatch(OnEndReadInternal);
            void OnEndReadInternal()
            {
                bytesIn = stream.EndRead(iar);
            }
            usedLength += bytesIn;

            if (bytesIn == 0) streamHasClosed = true;

            Scan();
        }
    }
}
