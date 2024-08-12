using Application.Boundaries.Services.ViaCEP.Client.Response;

namespace Application.Boundaries.Services.ViaCEP
{
    public interface IViaCEPService
    {
        Task<ViaCEPResponse> GetAddressByZipCodeAsync(string zipCode);
    }
}
