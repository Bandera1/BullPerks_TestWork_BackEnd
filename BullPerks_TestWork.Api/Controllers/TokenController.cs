using BullPerks_TestWork.Api.Repositories.Interfaces;
using BullPerks_TestWork.Domain.Constants;
using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.Interfaces.Services;
using BullPerks_TestWork.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Xml.Linq;

namespace BullPerks_TestWork.Api.Controllers
{
    [Authorize(Roles = ProjectRoles.ADMIN)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IBLPTokenService _BLPTokenService;
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration, IBLPTokenService BLPTokenService)
        {
            _configuration = configuration;      
            _BLPTokenService = BLPTokenService;
        }

        [AllowAnonymous]
        [HttpGet("LoadAllBLPTokens")]
        [SwaggerResponse(200, Type = typeof(TokenViewModel))]
        public async Task<IActionResult> LoadAllBLPTokens()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tokenContractAddress = _configuration.GetSection("TokensAddresses").GetSection("BplToken").Value; // Can be replaced by the address coming from the request
            var viewModels = await _BLPTokenService.GetBLPTokensAndStoreToDbAsync(tokenContractAddress);     

            return Ok(viewModels);
        }

        [HttpPost("CalculateTokensSupply")]
        [SwaggerResponse(200, Type = typeof(TokenViewModel))]
        public async Task<IActionResult> CalculateTokensSupply()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tokensContractAddresses = _configuration.GetSection("TokensAddresses").GetSection("Others").Get<string[]>();
            var viewModels = await _BLPTokenService.LoadTokenSupplyAsync(tokensContractAddresses);

            return Ok(viewModels);
        }
    }
}
