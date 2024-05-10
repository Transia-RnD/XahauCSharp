﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xahau.Client.Exceptions;

using Xahau.Models.Transaction;
using Xahau.Models.Transactions;

namespace XahauTests.Xahau.Models
{
    [TestClass]
    public class TestUCheckCash
    {
        [TestMethod]
        public async Task TestVerify_Valid_CheckCash()
        {
            var tx = new Dictionary<string, dynamic>
            {
                { "TransactionType", "CheckCash" },
                {"Account", "rfkE1aSy9G8Upk4JssnwBxhEv5p4mn2KTy"},
                {"Amount", "100000000"},
                {"CheckID", "838766BA2B995C00744175F69A1B11E32C3DBC40E64801A4056FCBD657F57334"},
                {"Fee", "12"},
            };
            await Validation.ValidateCheckCash(tx);
            await Validation.Validate(tx);
        }
        [TestMethod]
        public async Task TestVerify_InValid_CheckID()
        {
            var tx = new Dictionary<string, dynamic>
            {
                { "TransactionType", "CheckCash" },
                {"Account", "rfkE1aSy9G8Upk4JssnwBxhEv5p4mn2KTy"},
                {"CheckID", 83876645678567890 },
                {"Amount", "100000000"}
            };
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateCheckCash(tx), "CheckCash: invalid CheckID");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(tx), "CheckCash: invalid CheckID");
        }
        [TestMethod]
        public async Task TestVerify_InValid_Amount()
        {
            var tx = new Dictionary<string, dynamic>
            {
                { "TransactionType", "CheckCash" },
                {"Account", "rfkE1aSy9G8Upk4JssnwBxhEv5p4mn2KTy"},
                {"CheckID", "838766BA2B995C00744175F69A1B11E32C3DBC40E64801A4056FCBD657F57334"},
                {"Amount", 100000000}
            };
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateCheckCash(tx), "CheckCash: invalid Amount");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(tx), "CheckCash: invalid Amount");
        }
        [TestMethod]
        public async Task TestVerify_InValid_having_both_Amount_and_DeliverMin()
        {
            var tx = new Dictionary<string, dynamic>
            {
                { "TransactionType", "CheckCash" },
                {"Account", "rfkE1aSy9G8Upk4JssnwBxhEv5p4mn2KTy"},
                {"CheckID", "838766BA2B995C00744175F69A1B11E32C3DBC40E64801A4056FCBD657F57334"},
                {"Amount", "100000000"},
                {"DeliverMin", 852156963}
            };
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateCheckCash(tx), "CheckCash: cannot have both Amount and DeliverMin");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(tx), "CheckCash: cannot have both Amount and DeliverMin");
        }
        [TestMethod]
        public async Task TestVerify_InValid_DeliverMin()
        {
            var tx = new Dictionary<string, dynamic>
            {
                { "TransactionType", "CheckCash" },
                {"Account", "rfkE1aSy9G8Upk4JssnwBxhEv5p4mn2KTy"},
                {"CheckID", "838766BA2B995C00744175F69A1B11E32C3DBC40E64801A4056FCBD657F57334"},
                {"DeliverMin", 852156963}
            };
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.ValidateCheckCash(tx), "CheckCash: invalid DeliverMin");
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Validation.Validate(tx), "CheckCash: invalid DeliverMin");
        }
    }

}
