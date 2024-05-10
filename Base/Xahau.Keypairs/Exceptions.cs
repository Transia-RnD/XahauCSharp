using System;

namespace Xahau.AddressCodec
{
    public class KeypairException : Exception
    {
        public KeypairException() { }

        public KeypairException(string message) : base(message){ }
    }
}
