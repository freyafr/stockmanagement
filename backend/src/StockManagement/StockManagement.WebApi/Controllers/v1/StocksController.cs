using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Queries;
using StockManagement.Core.Dtos;

namespace StockManagement.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StocksController(IMediator mediator, ILogger<StocksController> logger) : ControllerBase
    {
        [HttpGet(Name = nameof(GetStocks))]
        [ProducesResponseType(typeof(IEnumerable<StockDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStocks()
        {
            logger.LogDebug("Start retrieving stocks");
            var result = await mediator.Send(new GetStocksQuery());
            return Ok(result);
        }
    }
}
