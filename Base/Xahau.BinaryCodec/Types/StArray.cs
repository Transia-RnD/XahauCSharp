﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xahau.BinaryCodec.Binary;
using Xahau.BinaryCodec.Enums;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/ripple-binary-codec/src/types/st-array.ts

namespace Xahau.BinaryCodec.Types
{
    /// <summary>
    /// Class for serializing and deserializing Arrays of Objects
    /// </summary>
    public class StArray : List<StObject>, ISerializedType
    {
        /// <summary>
        ///  Construct an STArray from an Array of JSON Objects
        /// </summary>
        /// <param name="collection"> Array of JSON Objects</param>
        public StArray(IEnumerable<StObject> collection) : base(collection)
        {
        }
        /// <summary>
        ///  Construct an STArray 
        /// </summary>
        public StArray()
        {
        }

        /// <inheritdoc />
        public void ToBytes(IBytesSink sink)
        {
            foreach (var so in this)
            {
                so.ToBytes(sink);
            }
        }

        /// <inheritdoc />
        public JToken ToJson()
        {
            var arr = new JArray();
            foreach (var so in this)
            {
                arr.Add(so.ToJson());
            }
            return arr;
        }
        /// <summary> Deserialize StArray </summary>
        /// <param name="token">json token</param>
        /// <returns></returns>
        public static StArray FromJson(JToken token)
        {
            return new StArray(token.Select(StObject.FromJson));
        }
        /// <summary>
        /// Construct a StArray from a BinaryParser
        /// </summary>
        /// <param name="parser">A BinaryParser to read StArray from</param>
        /// <returns></returns>
        public static StArray FromParser(BinaryParser parser, int? hint = null)
        {
            var stArray = new StArray();
            while (!parser.End())
            {
                var field = parser.ReadField();
                if (field == Field.ArrayEndMarker)
                {
                    break;
                }
                var so = new StObject();
                var sizeHint = field.IsVlEncoded ? parser.ReadVlLength() : (int?)null;
                so.Fields[field] = field.FromParser(parser);
                stArray.Add(so);
            }
            return stArray;
        }
    }
}