﻿using BullPerks_TestWork.Constants;
using BullPerks_TestWork.Models;
using BullPerks_TestWork.Models.JSON;
using BullPerks_TestWork.Services.Interfaces;
using Newtonsoft.Json;

namespace BullPerks_TestWork.Services.Implementations
{
    public class CryptoWalletService : ICryptoWalletService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CryptoWalletService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<float> GetTokenTotalSupplyByContractAddress(string contractAddress)
        {
            var baseUrl = _configuration.GetSection("API_Url").GetSection("BscScanApi").GetSection("BaseUrl").Value;
            var bscApiKey = _configuration.GetSection("API_Keys").GetSection("BscScan").Value;

            var requestUrl = $"{baseUrl}" +
                $"?module=stats" +
                $"&action={BscScanConstants.GET_TOKEN_SUPPLY}" +
                $"&contractaddress={contractAddress}" +
                $"&apikey={bscApiKey}";
            var responseJson = await _httpClient.GetStringAsync(requestUrl);

            var resultModel = JsonConvert.DeserializeObject<BscScanGetTokenTotalSupplyModel>(responseJson);
            return (float)(Double.Parse(resultModel?.Result) / 1e18f);
        }

        public async Task<IEnumerable<CoinStatsGetWalletBalanceModel>> GetWalletTokensByContractAddress(string contractAddress)
        {
            var baseUrl = _configuration.GetSection("API_Url").GetSection("CoinStatsApi").GetSection("BaseUrl").Value;
            var bscApiKey = _configuration.GetSection("API_Keys").GetSection("CoinStats").Value;

            var requestUrl = $"{baseUrl}" +
                $"{CoinStatsConstants.GET_WALLET_BALANCE}" +
                $"?address={contractAddress}" +
                $"&connectionId={CoinStatsConstants.BINANCE_SMART_CHAIN}";
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUrl),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("X-API-KEY", bscApiKey);
            var responseJson = await _httpClient.SendAsync(request).Result.Content.ReadAsStringAsync();

            var resultModel = JsonConvert.DeserializeObject<IEnumerable<CoinStatsGetWalletBalanceModel>>(responseJson);
            return resultModel;
        }
    }
}
