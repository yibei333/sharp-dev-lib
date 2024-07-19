using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

public class ByteString(byte[] bytes)
{
    private readonly byte[] bytes = bytes;
    static readonly UTF8Encoding UTF8 = new(false);
    public static readonly ByteString Empty = new([]);

    public static ByteString FromStringAsUTF8(string s) => new(UTF8.GetBytes(s));

    public static ByteString FromBytes(byte[] blob) => new([.. blob]);

    public static ByteString FromBytes(byte[] blob, int start, int count)
    {
        if (count == 0) return Empty;
        if (start == 0 && count == blob.Length) return FromBytes(blob);

        var c = new byte[count];
        Buffer.BlockCopy(blob, start, c, 0, count);
        return new ByteString(c);
    }

    public int Length => bytes.Length;

    public static ByteString operator +(ByteString a, ByteString b)
    {
        if (a.Length == 0 && b.Length == 0) return Empty;
        if (a.Length == 0) return b;
        if (b.Length == 0) return a;

        var c = new byte[a.Length + b.Length];
        Buffer.BlockCopy(a.bytes, 0, c, 0, a.Length);
        Buffer.BlockCopy(b.bytes, 0, c, a.Length, b.Length);
        return new ByteString(c);
    }

    public static ByteString operator +(ByteString a, byte b)
    {
        byte[] c = new byte[a.Length + 1];
        Buffer.BlockCopy(a.bytes, 0, c, 0, a.Length);
        c[a.Length] = b;
        return new ByteString(c);
    }

    public static ByteString operator +(ByteString a, byte[] b) => a + new ByteString(b);

    public ByteString Append(byte[] b, int start, int count)
    {
        var c = new byte[Length + count];
        Buffer.BlockCopy(bytes, 0, c, 0, Length);
        Buffer.BlockCopy(b, start, c, Length, count);
        return new ByteString(c);
    }

    public ByteString Substring(int start, int count)
    {
        if (start == 0 && count == Length) return this;

        return FromBytes(bytes, start, count);
    }

    public ByteString Substring(int start) => Substring(start, Length - start);

    public IEnumerable<byte> Bytes => bytes.AsEnumerable();

    public byte this[int index] => bytes[index];

    public int IndexOf(byte b) => IndexOf(0, b);

    public int IndexOf(int start, byte b)
    {
        for (int index = start; index < Length; index++)
        {
            if (bytes[index] == b) return index;
        }
        return -1;
    }

    public int IndexOf(Predicate<byte> fn) => IndexOf(0, fn);

    public int IndexOf(int start, Predicate<byte> fn)
    {
        for (int index = start; index < Length; index++)
        {
            if (fn(bytes[index])) return index;
        }
        return -1;
    }

    public string AsUTF8 => UTF8.GetString(bytes);
    public string AsASCII => Encoding.ASCII.GetString(bytes);
    public override string ToString() => AsUTF8;
}

public static class ByteStringExtensions
{
    public static ByteString ToUTF8ByteString(this string s) => ByteString.FromStringAsUTF8(s);

    public static ByteString ToByteString(this byte[] b) => ByteString.FromBytes(b);

    public static ByteString ToByteString(this byte[] b, int start, int count) => ByteString.FromBytes(b, start, count);
}
