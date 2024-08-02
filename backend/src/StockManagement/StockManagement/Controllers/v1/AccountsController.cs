using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Queries;
using StockManagement.Core.Dtos;

namespace StockManagement.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController(IMediator mediator, ILogger<AccountsController> logger) : ControllerBase
    {

        [HttpGet("{userId}", Name = nameof(GetAccount))]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccount(Guid userId)
        {
            logger.LogDebug("Trying to find client data");
            var result = await mediator.Send(new GetClientQuery { UserId = userId });
            return Ok(result);
        }
    }
}
