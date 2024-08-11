using Application.Common;
using Application.UseCases.Product.CreateProduct;
using Application.UseCases.Product.CreateProduct.Inputs;
using Application.UseCases.Product.ListProducts.Inputs;
using Application.UseCases.Product.ListProducts.Outputs;
using Application.UseCases.Product.RemoveProduct.Inputs;
using Application.UseCases.Product.UpdateProduct;
using Application.UseCases.Product.UpdateProduct.Inputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Output), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return new CreatedResult(string.Empty, result);

            return BadRequest(result);
        }

        [HttpGet("list-products")]
        [ProducesResponseType(typeof(ListProductsOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ListProductsOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListProductsAsync([FromQuery] ListProductsInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPatch("update")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveProductAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var input = new RemoveProductInput { Id = id };
            var result = await _mediator.Send(input, cancellationToken); 

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
