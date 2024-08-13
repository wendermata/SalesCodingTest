using Application.UseCases.Product.ListProducts.Inputs;
using AutoFixture;
using Application.UseCases.Product.ListProducts.Mapping;
using FluentAssertions;

namespace Unit.Application.UseCases.Product.ListProducts.Mapping
{
    public class ListProductsInputMapperTests
    {
        private readonly IFixture _fixture;

        public ListProductsInputMapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldReturnNullWhenInputIsNull()
        {
            //arrange
            ListProductsInput input = null;

            //act
            var result = input.MapToSearchInput();

            //assert
            result.Should().BeNull();
        }

        [Fact]
        public void ShouldMapToSearchInput()
        {
            //arrange
            var input = _fixture.Create<ListProductsInput>();

            //act
            var result = input.MapToSearchInput();

            //assert
            result.Should().NotBeNull();
            result.Page.Should().Be(input.Page);
            result.PageSize.Should().Be(input.PageSize);
            result.Search.Should().Be(input.Search);
            result.OrderBy.Should().Be(input.Sort);
            result.Order.Should().Be(input.Dir);
        }
    }
}
