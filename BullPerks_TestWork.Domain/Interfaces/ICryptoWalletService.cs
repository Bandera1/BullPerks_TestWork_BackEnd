using BullPerks_TestWork.Api.Models.JSON;

namespace BullPerks_TestWork.Api.Services.Interfaces
{
    public interface ICryptoWalletService
    {
        Task<IEnumerable<CoinStatsGetWalletBalanceModel>> GetWalletTokensByContractAddress(string contractAddress);
        Task<float> GetTokenTotalSupplyByContractAddress(string contractAddress);
    }
}
