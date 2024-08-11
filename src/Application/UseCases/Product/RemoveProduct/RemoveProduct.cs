using Application.Common;
using Application.UseCases.Product.RemoveProduct.Inputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Product.RemoveProduct
{
    public class RemoveProduct : IRemoveProduct
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<RemoveProduct> _logger;

        public RemoveProduct(IProductRepository repository, ILogger<RemoveProduct> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Output> Handle(RemoveProductInput request, CancellationToken cancellationToken)
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

                product.Remove();
                await _repository.UpdateAsync(product, cancellationToken);

                output.Messages.Add($"Product id: {request.Id} removed successfully");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while removing product: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
