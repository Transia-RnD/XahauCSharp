﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Xahau.AddressCodec.XahauCodec;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/ripple-address-codec/src/xrp-codec.test.js

namespace Xahau.AddressCodec.Tests
{
    [TestClass]
    public class TestUMiscXahauCodec
    {
        public void EncodeDecodeAccountIDTest(string base58, string hex)
        {
            string actual = XahauCodec.EncodeAccountID(hex.FromHex());
            Assert.AreEqual(actual, base58);
            byte[] buffer = XahauCodec.DecodeAccountID(base58);
            Assert.AreEqual(buffer.ToHex(), hex);
        }

        [TestMethod]
        public void TestEncodeDecodeAccountID()
        {
            EncodeDecodeAccountIDTest(
                "rJrRMgiRgrU6hDF4pgu5DXQdWyPbY35ErN",
                "BA8E78626EE42C41B46D46C3048DF3A1C3C87072"
            );
        }

        public void EncodeDecodeNodePublicTest(string base58, string hex)
        {
            string actual = XahauCodec.EncodeNodePublic(hex.FromHex());
            Assert.AreEqual(actual, base58);
            byte[] buffer = XahauCodec.DecodeNodePublic(base58);
            Assert.AreEqual(buffer.ToHex(), hex);
        }

        [TestMethod]
        public void TestEncodeDecodeNodePublic()
        {
            EncodeDecodeNodePublicTest(
                "n9MXXueo837zYH36DvMc13BwHcqtfAWNJY5czWVbp7uYTj7x17TH",
                "0388E5BA87A000CB807240DF8C848EB0B5FFA5C8E5A521BC8E105C0F0A44217828"
            );
        }

        public void EncodeDecodeAccountPublicTest(string base58, string hex)
        {
            string actual = XahauCodec.EncodeAccountPublic(hex.FromHex());
            Assert.AreEqual(actual, base58);
            byte[] buffer = XahauCodec.DecodeAccountPublic(base58);
            Assert.AreEqual(buffer.ToHex(), hex);
        }

        [TestMethod]
        public void TestEncodeDecodeAccountPublic()
        {
            EncodeDecodeAccountPublicTest(
                "aB44YfzW24VDEJQ2UuLPV2PvqcPCSoLnL7y5M1EzhdW4LnK5xMS3",
                "023693F15967AE357D0327974AD46FE3C127113B1110D6044FD41E723689F81CC6"
            );
        }

        [TestMethod]
        public void TestDecodeArbitrarySeed()
        {
            XahauCodec.DecodedSeed decoded = XahauCodec.DecodeSeed("sEdTM1uX8pu2do5XvTnutH6HsouMaM2");
            Assert.AreEqual(decoded.Bytes.ToHex(), "4C3A1D213FBDFB14C7C28D609469B341");
            Assert.AreEqual(decoded.Type, "ed25519");

            XahauCodec.DecodedSeed decoded1 = XahauCodec.DecodeSeed("sn259rEFXrQrWyx3Q7XneWcwV6dfL");
            Assert.AreEqual(decoded1.Bytes.ToHex(), "CF2DE378FBDD7E2EE87D486DFB5A7BFF");
            Assert.AreEqual(decoded1.Type, "secp256k1");
        }

        [TestMethod]
        public void TestDecodeTypeSeed()
        {
            string edSeed = "sEdTM1uX8pu2do5XvTnutH6HsouMaM2";
            XahauCodec.DecodedSeed decoded = XahauCodec.DecodeSeed(edSeed);
            string type = "ed25519";
            Assert.AreEqual(decoded.Bytes.ToHex(), "4C3A1D213FBDFB14C7C28D609469B341");
            Assert.AreEqual(decoded.Type, type);
            Assert.AreEqual(XahauCodec.EncodeSeed(decoded.Bytes, type), edSeed);
        }

        [TestMethod]
        public void TestValidClassicSECP()
        {
            Assert.IsTrue(XahauCodec.IsValidClassicAddress("rU6K7V3Po4snVhBBaU29sesqs2qTQJWDw1"));
        }

        [TestMethod]
        public void TestValidClassicED()
        {
            Assert.IsTrue(XahauCodec.IsValidClassicAddress("rLUEXYuLiQptky37CqLcm9USQpPiz5rkpD"));
        }

        [TestMethod]
        public void TestInvalidClassic()
        {
            Assert.IsFalse(XahauCodec.IsValidClassicAddress("rU6K7V3Po4snVhBBaU29sesqs2qTQJWDw2"));
        }

        [TestMethod]
        public void TestInvalidClassicEmpty()
        {
            Assert.IsFalse(XahauCodec.IsValidClassicAddress(""));
        }
    }

    [TestClass]
    public class TestUEncodeXahauCodec
    {
        [TestMethod]
        public void TestEncodeSECP()
        {
            string result = XahauCodec.EncodeSeed("CF2DE378FBDD7E2EE87D486DFB5A7BFF".FromHex(), "secp256k1");
            Assert.AreEqual(result, "sn259rEFXrQrWyx3Q7XneWcwV6dfL");
        }

        [TestMethod]
        public void TestEncodeLowSECP()
        {
            string result = XahauCodec.EncodeSeed("00000000000000000000000000000000".FromHex(), "secp256k1");
            Assert.AreEqual(result, "sp6JS7f14BuwFY8Mw6bTtLKWauoUs");
        }

        [TestMethod]
        public void TestEncodeHighSECP()
        {
            string result = XahauCodec.EncodeSeed("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF".FromHex(), "secp256k1");
            Assert.AreEqual(result, "saGwBRReqUNKuWNLpUAq8i8NkXEPN");
        }

        [TestMethod]
        public void TestEncodeED()
        {
            string result = XahauCodec.EncodeSeed("4C3A1D213FBDFB14C7C28D609469B341".FromHex(), "ed25519");
            Assert.AreEqual(result, "sEdTM1uX8pu2do5XvTnutH6HsouMaM2");
        }

        [TestMethod]
        public void TestEncodeLowED()
        {
            string result = XahauCodec.EncodeSeed("00000000000000000000000000000000".FromHex(), "ed25519");
            Assert.AreEqual(result, "sEdSJHS4oiAdz7w2X2ni1gFiqtbJHqE");
        }

        [TestMethod]
        public void TestEncodeHighED()
        {
            string result = XahauCodec.EncodeSeed("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF".FromHex(), "ed25519");
            Assert.AreEqual(result, "sEdV19BLfeQeKdEXyYA4NhjPJe6XBfG");
        }

        [TestMethod]
        public void TestSeedLess16Bytes()
        {
            Assert.ThrowsException<EncodingFormatException>(() => XahauCodec.EncodeSeed("CF2DE378FBDD7E2EE87D486DFB5A7B".FromHex(), "secp256k1"));
        }

        [TestMethod]
        public void TestSeedGreater16Bytes()
        {
            Assert.ThrowsException<EncodingFormatException>(() => XahauCodec.EncodeSeed("CF2DE378FBDD7E2EE87D486DFB5A7BFFFF".FromHex(), "secp256k1"));
        }
    }

    [TestClass]
    public class TestUDecodeXahauCodec
    {
        [TestMethod]
        public void TestEncodeED()
        {
            XahauCodec.DecodedSeed decoded = XahauCodec.DecodeSeed("sEdTM1uX8pu2do5XvTnutH6HsouMaM2");
            Assert.AreEqual(decoded.Bytes.ToHex(), "4C3A1D213FBDFB14C7C28D609469B341");
            Assert.AreEqual(decoded.Type, "ed25519");
        }

        [TestMethod]
        public void TestEncodeSECP()
        {
            XahauCodec.DecodedSeed decoded = XahauCodec.DecodeSeed("sn259rEFXrQrWyx3Q7XneWcwV6dfL");
            Assert.AreEqual(decoded.Bytes.ToHex(), "CF2DE378FBDD7E2EE87D486DFB5A7BFF");
            Assert.AreEqual(decoded.Type, "secp256k1");
        }
    }

    [TestClass]
    public class TestUEncodeAccountIDXahauCodec
    {
        [TestMethod]
        public void TestEncodeAccountID()
        {
            string encoded = XahauCodec.EncodeAccountID("BA8E78626EE42C41B46D46C3048DF3A1C3C87072".FromHex());
            Assert.AreEqual(encoded, "rJrRMgiRgrU6hDF4pgu5DXQdWyPbY35ErN");
        }

        [TestMethod]
        public void TestInvalidAccountID()
        {
            Assert.ThrowsException<EncodingFormatException>(() => XahauCodec.EncodeAccountID("ABCDEF".FromHex()));
        }
    }

    [TestClass]
    public class TestUDecodeNodePublic
    {
        [TestMethod]
        public void TestDecodeNodePublic()
        {
            byte[] decoded = XahauCodec.DecodeNodePublic("n9MXXueo837zYH36DvMc13BwHcqtfAWNJY5czWVbp7uYTj7x17TH");
            Assert.AreEqual(decoded.ToHex(), "0388E5BA87A000CB807240DF8C848EB0B5FFA5C8E5A521BC8E105C0F0A44217828");
        }
    }

    // TODO: Add missing tests and uncomment/fix errors generated
    [TestClass]
    public class TestUEncodeDecode
    {

        //private static readonly B58 B58;
        //[TestMethod]
        //public void TestEncode123456789()
        //{
        //    B58.Version version = B58.Version.With(versionByte: 0, expectedLength: 9);
        //    byte[] bytes = Encoding.ASCII.GetBytes("123456789");
        //    Assert.AreEqual(B58.Encode(bytes, version), "rnaC7gW34M77Kneb78s");
        //}

        //[TestMethod]
        //public void TestDecodeExpectedLen()
        //{
        //    B58.Version version = B58.Version.With(versionByte: 0, expectedLength: 9);
        //    Assert.AreEqual(B58.Decode("123456789", version), "rnaC7gW34M77Kneb78s");
        //}

        //[TestMethod]
        //public void TestDecodeInvalidLenUnder()
        //{
        //    B58.Version version = B58.Version.With(versionByte: 0, expectedLength: 8);
        //    Assert.AreEqual(B58.Decode("rnaC7gW34M77Kneb78s", version), "rnaC7gW34M77Kneb78s");
        //}

        //[TestMethod]
        //public void TestDecodeInvalidLenOver()
        //{
        //    B58.Version version = B58.Version.With(versionByte: 0, expectedLength: 10);
        //    Assert.AreEqual(B58.Decode("rnaC7gW34M77Kneb78s", version), "rnaC7gW34M77Kneb78s");
        //}
    }
}