using BullPerks_TestWork.Api.Repositories.Interfaces;
using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.Interfaces.Services;
using BullPerks_TestWork.Domain.Models.JSON;
using BullPerks_TestWork.Domain.ViewModels;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullPerks_TestWork.Services.Services
{
    public class BLPTokenService : IBLPTokenService
    {
        private readonly ICryptoWalletService _cryptoWalletService;
        private readonly IRepository<DbToken> _dbTokenRepository;

        public BLPTokenService(ICryptoWalletService cryptoWalletService, IRepository<DbToken> dbTokenRepository)
        {
            _cryptoWalletService = cryptoWalletService;
            _dbTokenRepository = dbTokenRepository;
        }

        public async Task<IEnumerable<TokenViewModel>> GetBLPTokensAndStoreToDbAsync(string contractAddress)
        {
            var tokens = await _cryptoWalletService.GetWalletTokensByContractAddress(contractAddress);

            var dbTokens = tokens.Select(token =>
            {
                return TinyMapper.Map<DbToken>(token);
            });

            var viewModels = dbTokens.Select(token =>
            {
                return TinyMapper.Map<TokenViewModel>(token);
            });

            if (_dbTokenRepository.GetCountAsync() > 0)
            {
                await _dbTokenRepository.DeleteAllAsync();
            }
            _dbTokenRepository.InsertRange(dbTokens);

            return viewModels;
        }

        public async Task<IEnumerable<TokenViewModel>> LoadTokenSupplyAsync(string[] contractAddresses)
        {
            var dbTokens = (await _dbTokenRepository.GetAllAsync()).ToList();
            var tokensForCalculatingAmount = new List<float>();

            for (int i = 0; i < contractAddresses.Count(); i++)
            {
                tokensForCalculatingAmount.Add((await _cryptoWalletService.GetWalletTokensByContractAddress(contractAddresses[i])).Sum(x => (float)x.Amount));
            }

            for (int i = 0; i < dbTokens.Count(); i++)
            {
                if (dbTokens[i].ContractAddress.Equals(String.Empty))
                {
                    dbTokens[i].TotalSupply = 0f;
                }
                else
                {
                    dbTokens[i].TotalSupply = await _cryptoWalletService.GetTokenTotalSupplyByContractAddress(dbTokens[i].ContractAddress);
                    await Task.Delay(20); // This is required here due to BscScan API limitations of 5 requests per second.
                }

                if (dbTokens[i].TotalSupply > 0f)
                {
                    dbTokens[i].CirculatingSupply = dbTokens[i].TotalSupply - tokensForCalculatingAmount.Sum();
                }
            }
            _dbTokenRepository.SaveAsync();

            var viewModels = dbTokens.Select(token =>
            {
                return TinyMapper.Map<TokenViewModel>(token);
            });


            return viewModels;
        }
    }
}
