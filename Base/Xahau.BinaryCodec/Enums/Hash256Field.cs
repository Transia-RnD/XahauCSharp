﻿//https://xrpl.org/serialization.html#hash-fields
namespace Xahau.BinaryCodec.Enums
{
    public class Hash256Field : Field {
        public Hash256Field(string name, int nthOfType,
            bool isSigningField = true, bool isSerialised = true) :
                base(name, nthOfType, FieldType.Hash256,
                    isSigningField, isSerialised) {}
    }
}
