using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTrade.Application.Buyer;
using WebTrade.Application.Buyer.GetBuyers;

namespace WebTrade.Api.Controllers
{
	[Route("api/[controller]")]
	public class BuyerController : ApiController
	{
		/// <summary>
		/// The name of the product
		/// </summary>
		[HttpGet]
		[Route("GetBuyers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<BuyerDto>>> GetBuyers()
		{
			return Ok(await Mediator.Send(new GetBuyersQuery()));
		}
	}
}
