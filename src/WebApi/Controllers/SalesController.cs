using Application.Common;
using Application.UseCases.Sale.CancelSale.Inputs;
using Application.UseCases.Sale.CreateSale.Inputs;
using Application.UseCases.Sale.ListSales.Inputs;
using Application.UseCases.Sale.ListSales.Outputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("sales")]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly IMediator _mediator;

        public SalesController(ILogger<SalesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Output), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSaleInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return new CreatedResult(string.Empty, result);

            return BadRequest(result);
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(ListSalesOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ListSalesOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListSalesAsync([FromQuery] ListSalesInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelSaleAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var input = new CancelSaleInput { SaleId = id };
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
