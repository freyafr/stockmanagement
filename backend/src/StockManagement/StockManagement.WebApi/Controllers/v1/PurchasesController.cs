using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Commands;
using StockManagement.Application.Queries;
using StockManagement.Core.Dtos;

namespace StockManagement.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PurchasesController(IMediator mediator, ILogger<PurchasesController> logger) : ControllerBase
    {

        [HttpGet("{userId}", Name = nameof(GetPurchases))]
        [ProducesResponseType(typeof(IEnumerable<PurchaseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPurchases(Guid userId)
        {
            logger.LogDebug("Trying to find client purchases");
            var result = await mediator.Send(new GetClientStocksQuery { UserId = userId });
            return Ok(result);
        }

        [HttpPost("validate", Name = nameof(ValidatePurchase))]
        [ProducesResponseType(typeof(PurchaseCreateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidatePurchase([FromBody] PurchaseCreateDto purchase)
        {
            logger.LogDebug("Trying to check if purchases is possible");
            var result = await mediator.Send(new AddStockPurchaseCommand { Purchase = purchase, ValidateOnly = true });
            return Ok(result);
        }

        [HttpPost(Name = nameof(CreatePurchase))]
        [ProducesResponseType(typeof(PurchaseCreateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseCreateDto purchase)
        {
            logger.LogDebug("Trying to purchases a stock");
            var result = await mediator.Send(new AddStockPurchaseCommand { Purchase = purchase, ValidateOnly = false });
            return Ok(result);
        }
    }
}
