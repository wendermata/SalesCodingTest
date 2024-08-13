using Application.UseCases.Product.ListProducts;
using Application.UseCases.Product.ListProducts.Inputs;
using AutoFixture;
using DomainProduct = Domain.Entities.Product;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Application.UseCases.Product.CreateProduct.Inputs;
using NSubstitute.ExceptionExtensions;

namespace Unit.Application.UseCases.Product.ListProducts
{
    public class ListProductsUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IProductRepository _repository;
        private readonly ILogger<ListProductsUseCase> _logger;
        private readonly ListProductsUseCase _useCase;

        public ListProductsUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<ListProductsUseCase>>();
            _useCase = new ListProductsUseCase(_repository, _logger);
        }

        [Fact]
        public async Task ShouldFailWhenRequestIsNull()
        {
            //arrange
            ListProductsInput input = null;

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.StartsWith("Invalid request"));

            _repository.ReceivedCalls().Count().Should().Be(0);
        }


        [Fact]
        public async Task ShouldReturnInvalidWhenExceptionIsThrown()
        {
            //arrange
            var request = _fixture.Create<ListProductsInput>();

            _repository.Search(Arg.Any<SearchInput>(), _cancellationToken)
                .Throws(new Exception("An error occurred."));

            //act
            var result = await _useCase.Handle(request, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldFailWhenProductsNotFound()
        {
            //arrange
            var input = _fixture.Create<ListProductsInput>();
            var output = new SearchOutput<DomainProduct>(1, 10, 0, null);

            _repository.Search(Arg.Any<SearchInput>(), _cancellationToken).Returns(output);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.Messages.Should().Contain("No products founded");
            result.ErrorMessages.Should().BeNullOrEmpty();
            _repository.ReceivedCalls().Count().Should().Be(1);

        }

        [Fact]
        public async Task ShouldSuccess()
        {
            //arrange
            var input = _fixture.Create<ListProductsInput>();
            var output = _fixture.Create<SearchOutput<DomainProduct>>();

            _repository.Search(Arg.Any<SearchInput>(), _cancellationToken).Returns(output);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().BeNullOrEmpty();

            _repository.ReceivedCalls().Count().Should().Be(1);

        }
    }
}
