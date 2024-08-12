using Application.Boundaries.Services.ViaCEP.Client.Response;
using Refit;

namespace Application.Boundaries.Services.ViaCEP.Client
{
    public interface IViaCEPServiceClient
    {
        [Get("/{zipcode}/json")]
        Task<ViaCEPResponse> GetCEPAsync(string zipCode);
    }
}
