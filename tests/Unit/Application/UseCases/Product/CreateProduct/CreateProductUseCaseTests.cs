using Application.UseCases.Product.CreateProduct;
using Application.UseCases.Product.CreateProduct.Inputs;
using AutoFixture;
using Domain.Repository;
using DomainProduct = Domain.Entities.Product;
using Microsoft.Extensions.Logging;
using NSubstitute;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;

namespace Unit.Application.UseCases.Product.CreateProduct
{
    public class CreateProductUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IProductRepository _repository;
        private readonly ILogger<CreateProductUseCase> _logger;
        private readonly CreateProductUseCase _useCase;

        public CreateProductUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<CreateProductUseCase>>();

            _useCase = new CreateProductUseCase(_repository, _logger);
        }

        [Fact]
        public async Task ShouldReturnInvalidWhenProductIsNull()
        {
            //arrange
            CreateProductInput request = null;

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain("Request is invalid.");

            _repository.ReceivedCalls().Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnInvalidWhenExceptionIsThrown()
        {
            //arrange
            var request = _fixture.Create<CreateProductInput>();

            _repository.InsertAsync(Arg.Any<DomainProduct>(), _cancellationToken)
                .Throws(new Exception("An error occurred."));

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            //arrange
            var request = _fixture.Create<CreateProductInput>();
            _repository.InsertAsync(Arg.Any<DomainProduct>(), _cancellationToken).Returns(Task.CompletedTask);

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().Contain("Product created successfully");
        }
    }
}
