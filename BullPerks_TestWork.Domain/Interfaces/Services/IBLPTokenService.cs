using BullPerks_TestWork.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullPerks_TestWork.Domain.Interfaces.Services
{
    public interface IBLPTokenService
    {
        /// <summary>
        /// Received BLP tokens from API by conract address and store they to database
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>BPL tokens in TokenViewModel format</returns>
        Task<IEnumerable<TokenViewModel>> GetBLPTokensAndStoreToDbAsync(string contractAddress);

        /// <summary>
        /// Load Total supply and calculate Circulating supply for token from database
        /// </summary>
        /// <returns>BPL tokens with supply in TokenViewModel format</returns>
        Task<IEnumerable<TokenViewModel>> LoadTokenSupplyAsync(string[] contractAddresses);
    }
}
