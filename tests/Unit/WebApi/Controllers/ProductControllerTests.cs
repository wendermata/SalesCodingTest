using Application.Common;
using Application.UseCases.Product.CreateProduct.Inputs;
using Application.UseCases.Product.ListProducts.Inputs;
using Application.UseCases.Product.ListProducts.Outputs;
using Application.UseCases.Product.RemoveProduct.Inputs;
using Application.UseCases.Product.UpdateProduct.Inputs;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WebApi.Controllers;

namespace Unit.WebApi.Controllers
{
    public class ProductControllerTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        private readonly ProductController _controller;

        public ProductControllerTests()
        {

            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _logger = Substitute.For<ILogger<ProductController>>();
            _mediator = Substitute.For<IMediator>();

            _controller = new ProductController(_logger, _mediator);
        }

        [Fact]
        public async Task ShouldCreateAsyncReturnBadRequestWhenOutputIsInvalid()
        {
            //arrange
            var input = _fixture.Create<CreateProductInput>();
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
            var input = _fixture.Create<CreateProductInput>();
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
        public async Task ShouldListProductsAsyncReturnBadRequestWhenOutputIsInvalid()
        {
            //arrange
            var input = _fixture.Create<ListProductsInput>();
            var output = _fixture.Create<ListProductsOutput>();
            output.ErrorMessages.Add("fail");

            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.ListProductsAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var resultContent = (Output)result.As<BadRequestObjectResult>().Value!;
            resultContent.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldListProductsAsyncReturnSuccess()
        {
            //arrange
            var input = _fixture.Create<ListProductsInput>();
            var output = _fixture.Build<ListProductsOutput>()
                .Without(x => x.ErrorMessages)
                .Create();

            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.ListProductsAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var resultContent = (Output)result.As<OkObjectResult>().Value!;
            resultContent.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldUpdateProductAsyncReturnBadRequestWhenOutputIsInvalid()
        {
            //arrange
            var input = _fixture.Create<UpdateProductInput>();
            var output = _fixture.Create<Output>();
            output.ErrorMessages.Add("fail");

            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.UpdateProductAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var resultContent = (Output)result.As<BadRequestObjectResult>().Value!;
            resultContent.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldUpdateProductAsyncReturnSuccess()
        {
            //arrange
            var input = _fixture.Create<UpdateProductInput>();
            var output = _fixture.Build<Output>()
                .Without(x => x.ErrorMessages)
                .Create();

            _mediator.Send(input, _cancellationToken).Returns(output);

            //act
            var result = await _controller.UpdateProductAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var resultContent = (Output)result.As<OkObjectResult>().Value!;
            resultContent.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldRemoveProductAsyncReturnBadRequestWhenResultIsNotValid()
        {
            //arrange
            var id = Guid.NewGuid();
            var input = _fixture.Build<RemoveProductInput>().With(x => x.Id, id);
            var output = _fixture.Create<Output>();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is<RemoveProductInput>(x => x.Id == id), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RemoveProductAsync(id, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var resultContent = (Output)result.As<BadRequestObjectResult>().Value!;
            resultContent.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldRemoveProductAsyncReturnSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var input = _fixture.Build<RemoveProductInput>().With(x => x.Id, id);
            var output = _fixture.Build<Output>().Without(x => x.ErrorMessages).Create();

            _mediator.Send(Arg.Is<RemoveProductInput>(x => x.Id == id), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RemoveProductAsync(id, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var resultContent = (Output)result.As<OkObjectResult>().Value!;
            resultContent.IsValid.Should().BeTrue();
        }

    }
}
