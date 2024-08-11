using Application.Common;
using Application.UseCases.Product.UpdateProduct.Inputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Product.UpdateProduct
{
    public class UpdateProduct : IUpdateProduct
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<UpdateProduct> _logger;

        public UpdateProduct(IProductRepository repository, ILogger<UpdateProduct> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Output> Handle(UpdateProductInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
                if (product is null)
                {
                    _logger.LogError($"Product not found. Id: {request.Id}");
                    output.ErrorMessages.Add("Product not found.");
                    return output;
                }

                product.Update(request.NewName, request.NewPrice, request.NewStockQuantity);
                await _repository.UpdateAsync(product, cancellationToken);

                output.Messages.Add("Product update successfully");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while updating product: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
