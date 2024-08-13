using Application.Common.Helpers;
using Application.UseCases.Product.ListProducts.Inputs;
using Application.UseCases.Product.ListProducts.Mapping;
using Application.UseCases.Product.ListProducts.Outputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Product.ListProducts
{
    public class ListProductsUseCase : IListProductsUseCase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ListProductsUseCase> _logger;

        public ListProductsUseCase(IProductRepository repository, ILogger<ListProductsUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ListProductsOutput> Handle(ListProductsInput request, CancellationToken cancellationToken)
        {
            var output = new ListProductsOutput();
            try
            {
                if (request is null)
                {
                    _logger.LogError($"Invalid request: {request}");
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                var searchInput = request.MapToSearchInput();
                var searchResult = await _repository.Search(searchInput, cancellationToken);
                if (searchResult.Items is null || searchResult.Items.Count == 0)
                {
                    _logger.LogWarning($"No products founded");
                    output.Messages.Add($"No products founded");
                    return output;
                }

                output = searchResult.MapToOutput();
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while listing products: {ex.Message} request: {SerializeHelper.SerializeObjectToJson(request)}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
