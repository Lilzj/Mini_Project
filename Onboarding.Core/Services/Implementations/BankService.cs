using Microsoft.Extensions.Options;
using Onboarding.Core.Services.Interfaces;
using Onboarding.DTOs.Response;
using Onboarding.Utilities.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Core.Services.Implementations
{
    public class BankService : IBankService
    {
        private readonly RestClient _client;
        private readonly BankConfig _Config;

        public BankService(IOptions<BankConfig> Config)
        {
            _Config = Config.Value;
            _client = new RestClient(_Config.BaseUrl);
        }

        public async Task<BankResponseDto> GetBanksAsync()
        {
            var request = new RestRequest(_Config.Resource, Method.Get);
            request.AddHeader("Ocp-Apim-Subscription-Key", _Config.Key);
            var response = await _client.ExecuteAsync<BankResponseDto>(request);

            return response.Data;
        }
    }
}
