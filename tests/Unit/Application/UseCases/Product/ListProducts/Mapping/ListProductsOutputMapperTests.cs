using Application.UseCases.Product.ListProducts.Mapping;
using AutoFixture;
using Domain.Repository.Shared.SearchableRepository;
using FluentAssertions;
using DomainProduct = Domain.Entities.Product;


namespace Unit.Application.UseCases.Product.ListProducts.Mapping
{
    public class ListProductsOutputMapperTests
    {
        private readonly IFixture _fixture;

        public ListProductsOutputMapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldReturnNullWhenSearchIsNull()
        {
            //arrange
            SearchOutput<DomainProduct> search = null;

            //act
            var result = search.MapToOutput();

            //assert
            result.Should().NotBeNull();
            result.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ShouldMapToOutput()
        {
            //arrange
            var search = _fixture.Create<SearchOutput<DomainProduct>>();

            //act
            var result = search.MapToOutput();

            //assert
            result.Should().NotBeNull();
            result.Page.Should().Be(search.CurrentPage);
            result.PageSize.Should().Be(search.PageSize);
            result.Total.Should().Be(search.Total);
        }

        [Fact]
        public void ShouldReturnNullWhenDomainIsNull()
        {
            //arrange
            DomainProduct product = null;

            //act
            var result = product.MapToItemOutput();

            //assert
            result.Should().BeNull();
        }

        [Fact]
        public void ShouldMapToItemOutput()
        {
            //arrange
            var product = _fixture.Create<DomainProduct>();

            //act
            var result = product.MapToItemOutput();

            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.Name.Should().Be(product.Name);
            result.Price.Should().Be(product.Price);
            result.StockQuantity.Should().Be(product.StockQuantity);
        }
    }
}
