﻿//https://xrpl.org/serialization.html#uint-fields
namespace Xahau.BinaryCodec.Enums
{
    public class Uint64Field : Field {
        public Uint64Field(string name, int nthOfType,
            bool isSigningField = true, bool isSerialised = true) :
                base(name, nthOfType, FieldType.Uint64,
                    isSigningField, isSerialised) {}
    }
}
