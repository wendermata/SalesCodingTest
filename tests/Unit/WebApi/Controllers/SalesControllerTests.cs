using Application.Common;
using Application.UseCases.Sale.CancelSale.Inputs;
using Application.UseCases.Sale.CreateSale.Inputs;
using Application.UseCases.Sale.ListSales.Inputs;
using Application.UseCases.Sale.ListSales.Outputs;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WebApi.Controllers;
namespace Unit.WebApi.Controllers
{
    public class SalesControllerTests
    {
        private readonly Fixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly ILogger<SalesController> _logger;
        private IMediator _mediator;

        private SalesController _controller;

        public SalesControllerTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();
            _logger = Substitute.For<ILogger<SalesController>>();
            _mediator = Substitute.For<IMediator>();

            _controller = new SalesController(_logger, _mediator);
        }

        [Fact]
        public async Task ShouldCreateAsyncReturnBadRequestWhenResultIsNotValid()
        {
            //arrange
            var input = _fixture.Create<CreateSaleInput>();
            var output = _fixture.Create<Output>();
            output.ErrorMessages.Add("fail");
            
            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.CreateAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var resultContent = (Output)result.As<BadRequestObjectResult>().Value!;
            resultContent.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldCreateAsyncReturnSuccess()
        {
            //arrange
            var input = _fixture.Create<CreateSaleInput>();
            var output = _fixture.Build<Output>()
                .Without(x => x.ErrorMessages)
                .Create();

            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.CreateAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
            var resultContent = (Output)result.As<CreatedResult>().Value!;
            resultContent.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldListSalesAsyncReturnBadRequestWhenResultIsNotValid()
        {
            //arrange
            var input = _fixture.Create<ListSalesInput>();
            var output = _fixture.Create<ListSalesOutput>();
            output.ErrorMessages.Add("fail");

            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.ListSalesAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var resultContent = (Output)result.As<BadRequestObjectResult>().Value!;
            resultContent.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldListSalesAsyncReturnSuccess()
        {
            //arrange
            var input = _fixture.Create<ListSalesInput>();
            var output = _fixture.Build<ListSalesOutput>()
                .Without(x => x.ErrorMessages)
                .Create();

            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.ListSalesAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var resultContent = (Output)result.As<OkObjectResult>().Value!;
            resultContent.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldCancelSaleAsyncReturnBadRequestWhenResultIsNotValid()
        {
            //arrange
            var id = Guid.NewGuid();
            var input = _fixture.Build<CancelSaleInput>().With(x => x.SaleId, id);
            var output = _fixture.Create<Output>();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is<CancelSaleInput>(x => x.SaleId == id), _cancellationToken).Returns(output);

            //act
            var result = await _controller.CancelSaleAsync(id, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var resultContent = (Output)result.As<BadRequestObjectResult>().Value!;
            resultContent.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldCancelSaleAsyncReturnSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var input = _fixture.Build<CancelSaleInput>().With(x => x.SaleId, id);
            var output = _fixture.Build<Output>().Without(x => x.ErrorMessages).Create();

            _mediator.Send(Arg.Is<CancelSaleInput>(x => x.SaleId == id), _cancellationToken).Returns(output);

            //act
            var result = await _controller.CancelSaleAsync(id, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var resultContent = (Output)result.As<OkObjectResult>().Value!;
            resultContent.IsValid.Should().BeTrue();
        }
    }
}
