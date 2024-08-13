using Application.UseCases.Product.CreateProduct.Inputs;
using Application.UseCases.Product.CreateProduct.Mapping;
using AutoFixture;
using FluentAssertions;

namespace Unit.Application.UseCases.Product.CreateProduct.Mapping
{
    public class CreateProductInputMapperTests
    {
        private readonly IFixture _fixture;

        public CreateProductInputMapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldNotMapToDomainWhenInputIsNull()
        {
            // Arrange
            CreateProductInput input = null;

            // Act
            var result = input.MapToDomain();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void ShouldMapToDomain()
        {
            // Arrange
            var input = _fixture.Create<CreateProductInput>();

            // Act
            var result = input.MapToDomain();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.Name, result.Name);
            Assert.Equal(input.Price, result.Price);
            Assert.Equal(input.StockQuantity, result.StockQuantity);
        }
    }
}
