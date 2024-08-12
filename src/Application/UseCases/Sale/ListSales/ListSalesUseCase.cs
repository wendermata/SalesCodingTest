using Application.Common.Helpers;
using Application.UseCases.Sale.ListSales.Inputs;
using Application.UseCases.Sale.ListSales.Mapping;
using Application.UseCases.Sale.ListSales.Outputs;
using DomainProduct = Domain.Entities.Product;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sale.ListSales
{
    public class ListSalesUseCase : IListSalesUseCase
    {
        private readonly ISalesRepository _repository;
        private readonly ILogger<ListSalesUseCase> _logger;

        public ListSalesUseCase(ISalesRepository repository, ILogger<ListSalesUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ListSalesOutput> Handle(ListSalesInput request, CancellationToken cancellationToken)
        {
            var output = new ListSalesOutput();
            try
            {
                if (request is null)
                {
                    _logger.LogError($"Invalid request: {request}");
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                var searchInput = request.MapToSearchInput();
                var sales = await _repository.Search(searchInput, cancellationToken);
                if (sales.Items.Count == 0)
                {
                    _logger.LogWarning($"No sales founded");
                    output.Messages.Add($"No sales founded");
                    return output;
                }

                return sales.MapToOutput();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while listing sales: {ex.Message} request: {SerializeHelper.SerializeObjectToJson(request)}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
