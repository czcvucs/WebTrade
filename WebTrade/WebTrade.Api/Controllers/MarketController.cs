using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebTrade.Application.Market;
using WebTrade.Application.Market.GetMarkets;

namespace WebTrade.Api.Controllers
{
	[Route("api/[controller]")]
	public class MarketController : ApiController
	{
		[HttpGet]
		[Route("GetMarkets")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<MarketDto>> GetMarkets()
		{
			return Ok(await Mediator.Send(new GetMarketsQuery()));
		}
	}
}
