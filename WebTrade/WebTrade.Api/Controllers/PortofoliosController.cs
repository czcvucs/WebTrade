using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTrade.Application.Portofolios;
using WebTrade.Application.Portofolios.GetPortofolios;

namespace WebTrade.Api.Controllers
{
	[Route("api/[controller]")]
	public class PortofoliosController : ApiController
	{
		[HttpGet]
		[Route("GetPortofolios")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<PortofolioDto>>> GetPortofolios()
		{
			return Ok(await Mediator.Send(new GetPortofoliosQuery()));
		}
	}
}
