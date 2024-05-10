using Newtonsoft.Json.Linq;
using Xahau.BinaryCodec.Types;

namespace Xahau.BinaryCodec.Enums
{
    public delegate ISerializedType FromJson(JToken token);
}
