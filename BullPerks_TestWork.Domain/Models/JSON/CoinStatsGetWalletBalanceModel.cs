namespace BullPerks_TestWork.Api.Models.JSON
{
    public class CoinStatsGetWalletBalanceModel
    {
        public string? Chain { get; set; }
        public string? CoinId { get; set; }
        public float? Amount { get; set; }
    }
}
