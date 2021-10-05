using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTrade.Application.Market;
using WebTrade.Application.Market.GetMarkets;
using WebTrade.Application.Market.UpdateMarket;

namespace WebTrade.Api.Controllers
{
	[Route("api/[controller]")]
	public class MarketController : ApiController
	{
		[HttpGet]
		[Route("GetMarkets")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<MarketDto>>> GetMarkets()
		{
			return Ok(await Mediator.Send(new GetMarketsQuery()));
		}

		[HttpPut]
		[Route("UpdateMarket")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddTrade([FromBody] UpdateMarketCommand command)
		{
			return Ok(await Mediator.Send(command));
		}
	}
}
