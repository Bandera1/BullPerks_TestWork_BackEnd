using BullPerks_TestWork.Models;
using BullPerks_TestWork.Models.JSON;

namespace BullPerks_TestWork.Services.Interfaces
{
    public interface ICryptoWalletService
    {
        Task<IEnumerable<CoinStatsGetWalletBalanceModel>> GetWalletTokensByContractAddress(string contractAddress);
        Task<float> GetTokenTotalSupplyByContractAddress(string contractAddress);
    }
}
