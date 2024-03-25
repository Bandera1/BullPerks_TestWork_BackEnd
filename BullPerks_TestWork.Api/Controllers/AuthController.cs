using BullPerks_TestWork.Domain.Interfaces.Services;
using BullPerks_TestWork.Services.Services;
using DiplomeProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BullPerks_TestWork.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return JWT token if register was success</returns>
        [SwaggerResponse(200, Type = typeof(string), Description = "Register new user and return JWT token")]
        [HttpPost("Register")]
        public async Task<IActionResult> Registration([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.RegisterUser(model);

            if (result == null)
            {
                return BadRequest("Validation error");
            }

            return Ok(result);
        }

        /// <summary>
        /// Login user in system
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Login user and return JWT token</returns>
        [SwaggerResponse(200, Type = typeof(string), Description = "Login user and return JWT token")]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.LoginUser(model);

            if (result == null)
            {
                return BadRequest("Wrong credentials");
            }

            return Ok(result);
        }
    }
}
