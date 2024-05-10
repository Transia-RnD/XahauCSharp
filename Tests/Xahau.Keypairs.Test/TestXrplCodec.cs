using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xahau.AddressCodec;
using Xahau.Keypairs;
using static Xahau.AddressCodec.XahauCodec;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/ripple-keypairs/test/xrp-codec-test.js

namespace Xahau.Keypairs.Tests
{
    [TestClass]
    public class TestUEncodeSeed
    {
        [TestMethod]
        public void EncodeSECPSeed()
        {
            string result = XahauCodec.EncodeSeed("CF2DE378FBDD7E2EE87D486DFB5A7BFF".FromHex(), "secp256k1");
            Assert.AreEqual(result, "sn259rEFXrQrWyx3Q7XneWcwV6dfL");
        }

        [TestMethod]
        public void EncodeLowSECPSeed()
        {
            string result = XahauCodec.EncodeSeed("00000000000000000000000000000000".FromHex(), "secp256k1");
            Assert.AreEqual(result, "sp6JS7f14BuwFY8Mw6bTtLKWauoUs");
        }

        [TestMethod]
        public void EncodeHighSECPSeed()
        {
            string result = XahauCodec.EncodeSeed("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF".FromHex(), "secp256k1");
            Assert.AreEqual(result, "saGwBRReqUNKuWNLpUAq8i8NkXEPN");
        }

        [TestMethod]
        public void EncodeEDSeed()
        {
            string result = XahauCodec.EncodeSeed("4C3A1D213FBDFB14C7C28D609469B341".FromHex(), "ed25519");
            Assert.AreEqual(result, "sEdTM1uX8pu2do5XvTnutH6HsouMaM2");
        }

        [TestMethod]
        public void EncodeLowEDSeed()
        {
            string result = XahauCodec.EncodeSeed("00000000000000000000000000000000".FromHex(), "ed25519");
            Assert.AreEqual(result, "sEdSJHS4oiAdz7w2X2ni1gFiqtbJHqE");
        }

        [TestMethod]
        public void EncodeHighEDSeed()
        {
            string result = XahauCodec.EncodeSeed("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF".FromHex(), "ed25519");
            Assert.AreEqual(result, "sEdV19BLfeQeKdEXyYA4NhjPJe6XBfG");
        }
    }

    [TestClass]
    public class TestUDecodeSeed
    {
        [TestMethod]
        public void DecodeEDSeed()
        {
            DecodedSeed decodedSeed = XahauCodec.DecodeSeed("sEdTM1uX8pu2do5XvTnutH6HsouMaM2");
            Assert.AreEqual(decodedSeed.Bytes.ToHex(), "4C3A1D213FBDFB14C7C28D609469B341");
            Assert.AreEqual(decodedSeed.Type, "ed25519");
        }

        [TestMethod]
        public void DecodeSECPSeed()
        {
            DecodedSeed decodedSeed = XahauCodec.DecodeSeed("sn259rEFXrQrWyx3Q7XneWcwV6dfL");
            Assert.AreEqual(decodedSeed.Bytes.ToHex(), "CF2DE378FBDD7E2EE87D486DFB5A7BFF");
            Assert.AreEqual(decodedSeed.Type, "secp256k1");
        }
    }

    [TestClass]
    public class TestUEncodeAccountID
    {
        [TestMethod]
        public void EncodeAccountID()
        {
            string result = XahauCodec.EncodeAccountID("BA8E78626EE42C41B46D46C3048DF3A1C3C87072".FromHex());
            Assert.AreEqual(result, "rJrRMgiRgrU6hDF4pgu5DXQdWyPbY35ErN");
        }
    }

    [TestClass]
    public class TestUDecodeNodePublic
    {
        [TestMethod]
        public void DecodeNodePublic()
        {
            byte[] result = XahauCodec.DecodeNodePublic("n9MXXueo837zYH36DvMc13BwHcqtfAWNJY5czWVbp7uYTj7x17TH");
            Assert.AreEqual(result.ToHex(), "0388E5BA87A000CB807240DF8C848EB0B5FFA5C8E5A521BC8E105C0F0A44217828");
        }
    }
}