using Application.Boundaries.Services.ViaCEP.Client;
using Application.Boundaries.Services.ViaCEP.Client.Response;
using Microsoft.Extensions.Logging;

namespace Application.Boundaries.Services.ViaCEP
{
    public class ViaCEPService : IViaCEPService
    {
        private readonly IViaCEPServiceClient _client;
        private readonly ILogger<ViaCEPService> _logger;

        public ViaCEPService(IViaCEPServiceClient client, ILogger<ViaCEPService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ViaCEPResponse> GetAddressByZipCodeAsync(string zipCode)
        {
            _logger.LogInformation($"Getting address by zip code: {zipCode}");
            var address = await _client.GetCEPAsync(zipCode);
            _logger.LogInformation($"Address: {address}");

            return address;
        }

    }
}
