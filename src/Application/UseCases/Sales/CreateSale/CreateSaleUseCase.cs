using Application.Boundaries.Services.ViaCEP;
using Application.Common;
using Application.Common.Helpers;
using Application.UseCases.Sales.CreateSale.Inputs;
using Application.UseCases.Sales.CreateSale.Mapping;
using Domain.Repository;
using Microsoft.Extensions.Logging;
using DomainProduct = Domain.Entities.Product;

namespace Application.UseCases.Sales.CreateSale
{
    public class CreateSaleUseCase : ICreateSaleUseCase
    {
        private readonly ILogger<CreateSaleUseCase> _logger;
        private readonly ISalesRepository _salesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IViaCEPService _viaCEPService;

        public CreateSaleUseCase(ILogger<CreateSaleUseCase> logger,
            ISalesRepository salesRepository,
            IProductRepository productRepository,
            IViaCEPService viaCEPService)
        {
            _logger = logger;
            _salesRepository = salesRepository;
            _productRepository = productRepository;
            _viaCEPService = viaCEPService;
        }

        public async Task<Output> Handle(CreateSaleInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                if (request.Items is null || request.Items.Count == 0)
                {
                    _logger.LogError("Items is empty.");
                    output.ErrorMessages.Add("Items is empty.");
                    return output;
                }

                if (!ZipCodeHelper.IsValid(request.ZipCode))
                {
                    _logger.LogError($"Invalid zip code. format should be \"0000-000\". Request: {SerializeHelper.SerializeObjectToJson(request)}");
                    output.ErrorMessages.Add("Invalid zip code. format should be \"0000-000\".");
                    return output;
                }

                var products = new List<DomainProduct>();
                foreach (var itemOutput in request.Items)
                {
                    var product = await _productRepository.GetByIdAsync(itemOutput.ProductId, cancellationToken);
                    if (product is null)
                    {
                        _logger.LogError($"Product not found. Id: {itemOutput.ProductId}");
                        output.ErrorMessages.Add($"Product not found");
                        return output;
                    }

                    if (itemOutput.Quantity < product.StockQuantity)
                    {
                        _logger.LogError($"Requested stock quantity unavailable. Product: {product.Id}, Requested quantity: {itemOutput.Quantity}, Available quantity: {product.StockQuantity}");
                        output.ErrorMessages.Add("Requested stock quantity unavailable.");
                        return output;
                    }
                    products.Add(product);
                    product.SetStockQuantity(product.StockQuantity - itemOutput.Quantity);
                    await _productRepository.UpdateAsync(product, cancellationToken);
                    _logger.LogInformation($"Product stock updated. Product: {product.Id}, New quantity: {product.StockQuantity}");
                }

                var address = await _viaCEPService.GetAddressByZipCodeAsync(request.ZipCode);

                var sale = request.MapToDomain(products, address);
                await _salesRepository.InsertAsync(sale, cancellationToken);
                _logger.LogInformation($"Sale created. Id: {sale.Id}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while creating sale: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
