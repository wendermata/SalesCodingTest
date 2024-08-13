using Application.UseCases.Product.RemoveProduct;
using Application.UseCases.Product.RemoveProduct.Inputs;
using AutoFixture;
using DomainProduct = Domain.Entities.Product;
using Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Unit.Application.UseCases.Product.RemoveProduct
{
    public class RemoveProductUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IProductRepository _repository;
        private readonly ILogger<RemoveProductUseCase> _logger;

        private readonly RemoveProductUseCase _useCase;

        public RemoveProductUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<RemoveProductUseCase>>();

            _useCase = new RemoveProductUseCase(_repository, _logger);
        }

        [Fact]
        public async Task ShouldFailWhenProductIsNotFound()
        {
            //arrange
            var request = _fixture.Create<RemoveProductInput>();

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
            var request = _fixture.Create<RemoveProductInput>();

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
            var request = _fixture.Create<RemoveProductInput>();
            var product = _fixture.Create<DomainProduct>();

            _repository.GetByIdAsync(request.Id, _cancellationToken).Returns(product);

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().Contain($"Product id: {request.Id} removed successfully");
        }
    }
}
