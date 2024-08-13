using Application.UseCases.Product.UpdateProduct;
using Application.UseCases.Product.UpdateProduct.Inputs;
using AutoFixture;
using DomainProduct = Domain.Entities.Product;
using Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Unit.Application.UseCases.Product.UpdateProduct
{
    public class UpdateProductUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IProductRepository _repository;
        private readonly ILogger<UpdateProductUseCase> _logger;

        private readonly UpdateProductUseCase _useCase;

        public UpdateProductUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<UpdateProductUseCase>>();

            _useCase = new UpdateProductUseCase(_repository, _logger);
        }

        [Fact]
        public async Task ShouldFailWhenProductIsNotFound()
        {
            //arrange
            var request = _fixture.Create<UpdateProductInput>();

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain("Product not found.");
        }

        [Fact]
        public async Task ShouldReturnInvalidWhenExceptionIsThrown()
        {
            //arrange
            var request = _fixture.Create<UpdateProductInput>();

            _repository.GetByIdAsync(request.Id, _cancellationToken)
                .Throws(new Exception("An error occurred."));

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldSuccess()
        {
            //arrange
            var request = _fixture.Create<UpdateProductInput>();
            var product = _fixture.Create<DomainProduct>();

            _repository.GetByIdAsync(request.Id, _cancellationToken).Returns(product);

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().Contain($"Product update successfully");
        }
    }
}
