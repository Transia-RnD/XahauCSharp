// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/models/trustSet.ts

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xahau.Client.Exceptions;
using Xahau.Models.Transactions;

namespace XahauTests.Xahau.Models
{
    /// <summary>
    /// TrustSet Transaction Verification Testing.<br/>
    /// Providing runtime verification testing for each specific transaction type.
    /// </summary>
    [TestClass]
    public class TestUTrustSet
    {

        public static Dictionary<string, dynamic> trustSet;

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            trustSet = new Dictionary<string, dynamic>
            {
                { "TransactionType", "TrustSet" },
                {"Account", "rUn84CUYbNjRoTQ6mSW7BVJPSVJNLb1QLo"},
                {"LimitAmount",new Dictionary<string,dynamic>()
                {
                    {"currency","XAH"},
                    {"issuer","rcXY84C4g14iFp6taFXjjQGVeHqSCh9RX"},
                    {"value","4329.23"}
                }},
                {"QualityIn", 1234u},
                {"QualityOut", 4321u}
            };
        }

        [TestMethod]
        public async Task TestVerifyValid()
        {
            //verifies valid TrustSet
            await Validation.ValidateTrustSet(trustSet);
            await Validation.Validate(trustSet);

            //throws when LimitAmount is missing
            trustSet.Remove("LimitAmount");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateTrustSet(trustSet), "TrustSet: missing field LimitAmount - no ERROR");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(trustSet), "TrustSet: missing field LimitAmount - no ERROR");

            //throws when LimitAmount is invalid
            trustSet.Add("LimitAmount", 1234);
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateTrustSet(trustSet), "TrustSet: invalid LimitAmount - no ERROR");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(trustSet), "TrustSet: invalid LimitAmount - no ERROR");

            //throws when QualityIn is not a number
            trustSet["QualityIn"] = "1234";
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateTrustSet(trustSet), "TrustSet: QualityIn must be a number - no ERROR");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(trustSet), "TrustSet: QualityIn must be a number - no ERROR");
            //throws when QualityOut is not a number
            trustSet["QualityOut"] = "4321";
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateTrustSet(trustSet), "TrustSet: QualityOut must be a number - no ERROR");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(trustSet), "TrustSet: QualityOut must be a number - no ERROR");

        }
    }
}

