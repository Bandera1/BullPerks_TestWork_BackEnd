using BullPerks_TestWork.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BullPerks_TestWork.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICryptoWalletService _cryptoWalletService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICryptoWalletService cryptoWalletService)
        {
            _logger = logger;
            _cryptoWalletService = cryptoWalletService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cryptoWalletService.GetWalletTokensByContractAddress("0xfE1d7f7a8f0bdA6E415593a2e4F82c64b446d404"));

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}