using BullPerks_TestWork.Api.Repositories.Interfaces;
using BullPerks_TestWork.Domain.Constants;
using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.Interfaces.Services;
using BullPerks_TestWork.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;

namespace BullPerks_TestWork.Api.Controllers
{
    [Authorize(Roles = ProjectRoles.ADMIN)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ICryptoWalletService _cryptoWalletService;
        private readonly IRepository<DbToken> _dbTokenRepository;
        private readonly IConfiguration _configuration;

        public TokenController(ICryptoWalletService cryptoWalletService, IConfiguration configuration, IRepository<DbToken> dbTokenRepository)
        {
            _cryptoWalletService = cryptoWalletService;
            _configuration = configuration;        
            _dbTokenRepository = dbTokenRepository;
        }

        [AllowAnonymous]
        [HttpGet("LoadAllBLPTokens")]
        public async Task<IActionResult> LoadAllBLPTokens()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tokenContractAddress = _configuration.GetSection("TokensAddresses").GetSection("BplToken").Value; // Can be replaced by the address coming from the request
            var tokens = await _cryptoWalletService.GetWalletTokensByContractAddress(tokenContractAddress);

            var dbTokens = tokens.Select(token =>
            {
                return TinyMapper.Map<DbToken>(token);
            }).ToList();

            for (int i = 0; i < dbTokens.Count(); i++)
            {
                if (dbTokens[i].ContractAddress.Equals(String.Empty))
                {
                    dbTokens[i].TotalSupply = 0f;
                    continue;
                }

                dbTokens[i].TotalSupply = await _cryptoWalletService.GetTokenTotalSupplyByContractAddress(dbTokens[i].ContractAddress);
                await Task.Delay(20); // This is required here due to BscScan API limitations of 5 requests per second.
            }

            var viewModels = dbTokens.Select(token =>
            {
                return TinyMapper.Map<TokenViewModel>(token);
            });
           
            if (_dbTokenRepository.GetCount() > 0)
            {
                _dbTokenRepository.DeleteAll();
            }
            _dbTokenRepository.InsertRange(dbTokens);         

            return Ok(viewModels);
        }
    }
}
