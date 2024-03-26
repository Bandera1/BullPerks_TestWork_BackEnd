using System.ComponentModel;

namespace BullPerks_TestWork.Domain.Models.JSON
{
    public class CoinStatsGetWalletBalanceModel
    {
        public string? Chain { get; set; }
        public string? CoinId { get; set; }
        public float? Amount { get; set; }
    }
}
