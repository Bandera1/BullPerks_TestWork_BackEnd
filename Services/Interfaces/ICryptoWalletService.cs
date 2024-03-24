using BullPerks_TestWork.Models;

namespace BullPerks_TestWork.Services.Interfaces
{
    public interface ICryptoWalletService
    {
        IEnumerable<CointStatToken> GetWalletTokensByContractAddress(string contractAddress);
        Task<float> GetTokenTotalSupplyByContractAddress(string contractAddress);
    }
}
