using System.Formats.Asn1;
using System.Text;

namespace SharpDevLib.Cryptography.Internal.References;

internal static class InternalExtension
{
    const string LFTerminator = "\n";
    internal static void AppendLineWithLFTerminator(this StringBuilder builder, string? content)
    {
        if (content.NotNullOrWhiteSpace()) builder.Append(content);
        builder.Append(LFTerminator);
    }

    internal static void AppendLineWithLFTerminator(this StringBuilder builder)
    {
        builder.Append(LFTerminator);
    }

    internal static void WriteIntegerValue(this AsnWriter writer, byte value)
    {
        writer.WriteInteger(value);
    }

    internal static void WriteIntegerValue(this AsnWriter writer, int value)
    {
        writer.WriteInteger(value);
    }

    internal static void WriteIntegerValue(this AsnWriter writer, byte[] bytes)
    {
        if (bytes.First() < 128)
        {
            while (bytes.Length > 1 && bytes[0] == 0 && bytes[1] < 128)
            {
                //resolve exception:The first 9 bits of the integer value all have the same value. Ensure the input is in big-endian byte order and that all redundant leading bytes have been removed. (Parameter 'value')
                bytes = bytes.Skip(1).ToArray();
            }
            writer.WriteInteger(bytes);
        }
        else writer.WriteIntegerUnsigned(bytes);
    }
}