using AutoFixture;
using Domain.Aggregates;
using Domain.Entities;
using FluentAssertions;

namespace Unit.Domain.Aggregates
{
    public class SaleAggregateTests
    {
        private readonly IFixture _fixture;

        public SaleAggregateTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldCalculateTotalValue()
        {
            //arrange
            var id = Guid.NewGuid();
            var zipCode = _fixture.Create<string>();
            var shipmentValue = _fixture.Create<decimal>();
            var totalValue = _fixture.Create<decimal>();
            var createdAt = _fixture.Create<DateTime>();
            var cancelledAt = _fixture.Create<DateTime>();

            var saleAggregate = new SaleAggregate(id,
                zipCode,
                shipmentValue);

            var item = new Item(Guid.NewGuid(), 1, 10, 10);
            saleAggregate.Items.Add(item);
            saleAggregate.Items.Add(item);

            var total = shipmentValue + 20;

            //act
            saleAggregate.CalculateTotalValue();

            //assert
            saleAggregate.TotalValue.Should().Be(total);
        }

        [Fact]
        public void ShouldCancel()
        {
            //arrange
            var id = Guid.NewGuid();
            var zipCode = _fixture.Create<string>();
            var shipmentValue = _fixture.Create<decimal>();
            var totalValue = _fixture.Create<decimal>();
            var createdAt = _fixture.Create<DateTime>();
            var cancelledAt = _fixture.Create<DateTime>();

            var saleAggregate = new SaleAggregate(id,
                zipCode,
                shipmentValue);

            //act
            saleAggregate.Cancel();

            //assert
            saleAggregate.IsCancelled.Should().BeTrue();
        }
    }
}
