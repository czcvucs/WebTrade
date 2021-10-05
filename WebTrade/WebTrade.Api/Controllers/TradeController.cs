using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTrade.Application.Trades;
using WebTrade.Application.Trades.AddTrade;
using WebTrade.Application.Trades.DeleteTrade;
using WebTrade.Application.Trades.GetTrades;

namespace WebTrade.Api.Controllers
{
	[Route("api/[controller]")]
	public class TradeController : ApiController
	{
		[HttpGet]
		[Route("GetTrades")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<TradeDto>>> GetTrades()
		{
			return Ok(await Mediator.Send(new GetTradesQuery()));
		}

		[HttpPost]
		[Route("AddTrade")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddTrade([FromBody] AddTradeCommand command)
		{
			var trade = await Mediator.Send(command);
			return Created($"{Request.Path}", trade);
		}

		[HttpDelete]
		[Route("DeleteTrade/{tradeId:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> DeleteTrade(Guid tradeId)
		{
			return Ok(await Mediator.Send(new DeleteTradeCommand { TradeId = tradeId }));
		}
	}
}
