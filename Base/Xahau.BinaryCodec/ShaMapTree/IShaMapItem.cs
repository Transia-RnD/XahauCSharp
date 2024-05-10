using Xahau.BinaryCodec.Binary;
using Xahau.BinaryCodec.Hashing;

namespace Xahau.BinaryCodec.ShaMapTree
{
    public interface IShaMapItem<out T>
    {
        void ToBytes(IBytesSink sink);
        IShaMapItem<T> Copy();
        T Value();
        HashPrefix Prefix();
    }
}
