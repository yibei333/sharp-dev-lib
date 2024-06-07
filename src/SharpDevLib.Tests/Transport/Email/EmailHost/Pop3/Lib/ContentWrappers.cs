using System.Text;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

internal static class ContentWrappers
{
    private static readonly UTF8Encoding UTF8_NO_BOM = new(false);

    internal static long MessageSizeByRetrieval(POP3Events.OnMessageRetrievalDelegate onRetrieveHandler, string mailboxID, string messageUniqueID)
    {
        var request = new POP3MessageRetrievalRequest(mailboxID, messageUniqueID, -1);
        onRetrieveHandler(request);

        long byteCountSoFar = 0;
        while (true)
        {
            string line = request.OnNextLine();
            if (line == null) return byteCountSoFar;
            byteCountSoFar += UTF8_NO_BOM.GetByteCount(line) + 2;
        }
    }

    internal static NextLineFn WrapForTop(POP3MessageRetrievalRequest request, long lineCount)
    {
        lineCount += 1;
        bool insideMesageBody = false;
        bool calledClose = false;
        void CloseStream()
        {
            if (calledClose) return;
            request.OnClose();
            calledClose = true;
        }
        return Impl;

        string Impl()
        {
            if (lineCount == 0)
            {
                CloseStream();
                return null!;
            }

            string line = request.OnNextLine();
            if (line == null)
            {
                CloseStream();
                return null!;
            }

            if (insideMesageBody == false && line.Length == 0) insideMesageBody = true;
            if (insideMesageBody) lineCount -= 1;
            return line;
        }
    }

    internal static NextLineFn WrapForRetr(POP3MessageRetrievalRequest request)
    {
        bool endOfMessage = false;
        return Wrap;

        string Wrap()
        {
            if (endOfMessage) return null!;
            string line = request.OnNextLine();
            if (line == null)
            {
                request.OnClose();
                endOfMessage = true;
                return null!;
            }
            return line;
        }
    }
}

