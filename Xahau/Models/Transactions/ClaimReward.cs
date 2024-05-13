//https://xrpl.org/uritokencreateselloffer.html
using System;

namespace Xahau.Models.Transactions
{
    [Flags]
    public enum ClaimRewardFlags : uint
    {
        tfOptOut = 1,
    }

    public class ClaimReward : TransactionCommon, IClaimReward
    {
        public ClaimReward()
        {
            TransactionType = TransactionType.ClaimReward;
        }

        public new ClaimRewardFlags? Flags { get; set; }

        public string? Issuer { get; set; }
    }

    public interface IClaimReward : ITransactionCommon
    {
        new ClaimRewardFlags? Flags { get; set; }
        
        string? Issuer { get; set; }
    }

    public class ClaimRewardResponse : TransactionResponseCommon, IClaimReward
    {
        public new ClaimRewardFlags? Flags { get; set; }
        
        public string? Issuer { get; set; }
    }

}
