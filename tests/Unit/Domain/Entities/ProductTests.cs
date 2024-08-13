using AutoFixture;
using Domain.Entities;
using FluentAssertions;

namespace Unit.Domain.Entities
{
    public class ProductTests
    {
        private readonly IFixture _fixture;

        public ProductTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldUpdate()
        {
            //arrange
            var product = new Product(Guid.NewGuid(),
                _fixture.Create<string>(),
                _fixture.Create<decimal>(),
                _fixture.Create<int>(),
                true,
                _fixture.Create<DateTime>());

            //act
            var name = _fixture.Create<string>();
            var price = _fixture.Create<decimal>();
            var quantity = _fixture.Create<int>();

            product.Update(name, price, quantity);

            //assert    
            product.Should().NotBeNull();
            product.Name.Should().Be(name);
            product.Price.Should().Be(price);
            product.StockQuantity.Should().Be(quantity);
        }

        [Fact]
        public void ShouldRemove()
        {
            //arrange
            var product = new Product(Guid.NewGuid(),
                _fixture.Create<string>(),
                _fixture.Create<decimal>(),
                _fixture.Create<int>(),
                true,
                _fixture.Create<DateTime>());

            //act
            product.Remove();

            //assert    
            product.Should().NotBeNull();
            product.IsActive.Should().BeFalse();
        }

        [Fact]
        public void ShouldSetStockQuantity()
        {
            //arrange
            var product = new Product(Guid.NewGuid(),
                _fixture.Create<string>(),
                _fixture.Create<decimal>(),
                _fixture.Create<int>(),
                true,
                _fixture.Create<DateTime>());

            //act
            var quantity = _fixture.Create<int>();
            product.SetStockQuantity(quantity);

            //assert    
            product.Should().NotBeNull();
            product.StockQuantity.Should().Be(quantity);
        }
    }
}
