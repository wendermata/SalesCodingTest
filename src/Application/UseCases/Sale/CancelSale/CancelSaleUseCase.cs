using Application.Common;
using Application.UseCases.Sale.CancelSale.Inputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sale.CancelSale
{
    public class CancelSaleUseCase : ICancelSaleUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CancelSaleUseCase> _logger;

        public CancelSaleUseCase(ISalesRepository salesRepository, IProductRepository productRepository, ILogger<CancelSaleUseCase> logger)
        {
            _salesRepository = salesRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Output> Handle(CancelSaleInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var sale = await _salesRepository.GetByIdAsync(request.SaleId, cancellationToken);
                if (sale is null)
                {
                    _logger.LogError($"Sale not found. Id: {request.SaleId}");
                    output.ErrorMessages.Add($"Sale not found");
                    return output;
                }

                foreach(var item in sale.Items)
                {
                    var product = await _productRepository.GetByIdAsync(item.Id, cancellationToken);
                    if (product is null)
                    {
                        _logger.LogError($"Product not found. Id: {item.Id}");
                        output.ErrorMessages.Add($"Product not found");
                        return output;
                    }
                    product.SetStockQuantity(product.StockQuantity + item.Quantity);
                    await _productRepository.UpdateAsync(product, cancellationToken);
                }

                sale.Cancel();
                await _salesRepository.UpdateAsync(sale, cancellationToken);
                _logger.LogInformation($"Sale canceled. Id: {sale.Id}");

                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while canceling sale: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
