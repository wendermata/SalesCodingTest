using Application.Common;
using Application.Common.Helpers;
using Application.UseCases.Product.CreateProduct.Inputs;
using Application.UseCases.Product.CreateProduct.Mapping;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Product.CreateProduct
{
    public class CreateProduct : ICreateProduct
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CreateProduct> _logger;

        public CreateProduct(IProductRepository repository, ILogger<CreateProduct> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Output> Handle(CreateProductInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var product = request.MapToDomain();
                if(product is null)
                {
                    _logger.LogError($"Request is invalid. {SerializeHelper.SerializeObjectToJson(request)}");
                    output.ErrorMessages.Add("Request is invalid.");
                }

                await _repository.InsertAsync(product, cancellationToken);
                output.Messages.Add("Product created successfully");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while creating product: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
