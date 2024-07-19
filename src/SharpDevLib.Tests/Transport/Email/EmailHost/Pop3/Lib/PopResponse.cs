using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

internal class PopResponse
{
    private readonly ResponseClass respType;
    private readonly NextLineFn nextLineGen;

    internal bool IsQuit => respType == ResponseClass.OKQuit || respType == ResponseClass.Critical;
    internal bool IsNormal => IsQuit == false;
    internal bool IsOK => respType == ResponseClass.OK || respType == ResponseClass.OKQuit;
    internal bool IsError => IsOK == false;
    internal bool IsSingleLine => nextLineGen == null;
    internal bool IsMultiLine => IsSingleLine == false;
    internal string Code { get; }
    internal string Text { get; }

    private PopResponse(ResponseClass respType, string code, string text, NextLineFn nextlineGen)
    {
        if (text.Contains('[') || text.Contains(']') || text.Contains('\r') || text.Contains('\n') || text.EndsWith(" _")) throw new ApplicationException("Response text contains disallowed characters.");

        this.respType = respType;
        Code = code;
        Text = text;
        nextLineGen = nextlineGen;
    }

    private enum ResponseClass
    {
        OK,
        OKQuit,
        Error,
        Critical
    }

    internal static PopResponse OKSingle(string text) => new(ResponseClass.OK, null!, text, null!);

    internal static PopResponse OKSingle(string code, string text) => new(ResponseClass.OK, code, text, null!);

    internal static PopResponse OKMulti(string text, IEnumerable<string> lines) => new(ResponseClass.OK, null!, text, ItterableToLineGen(lines));

    internal static PopResponse OKMulti(string text, NextLineFn nextLineGen) => new(ResponseClass.OK, null!, text, nextLineGen);

    internal static PopResponse ERR(string text) => new(ResponseClass.Error, null!, text, null!);

    internal static PopResponse ERR(string code, string text) => new(ResponseClass.Error, code, text, null!);

    internal static PopResponse Critical(string code, string text) => new(ResponseClass.Critical, code, text, null!);

    internal static PopResponse Quit(string text) => new(ResponseClass.OKQuit, null!, text, null!);

    static NextLineFn ItterableToLineGen(IEnumerable<string> lines)
    {
        var lineIter = lines.GetEnumerator();

        return NextLineInternal;
        string NextLineInternal()
        {
            if (lineIter.MoveNext()) return lineIter.Current;
            else return null!;
        }
    }

    internal string NextLine()
    {
        if (IsSingleLine) return null!;
        return nextLineGen();
    }

    internal string FirstLine => (IsOK ? "+OK" : "-ERR") + " " + (Code == null ? "" : $"[{Code}] ") + Text;
}

