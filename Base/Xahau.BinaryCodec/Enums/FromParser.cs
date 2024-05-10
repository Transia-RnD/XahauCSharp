using Xahau.BinaryCodec.Binary;
using Xahau.BinaryCodec.Types;

namespace Xahau.BinaryCodec.Enums
{
    public delegate ISerializedType FromParser(BinaryParser parser, int? hint = null);
}
