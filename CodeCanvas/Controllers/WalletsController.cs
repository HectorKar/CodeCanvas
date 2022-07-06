using System;
using CodeCanvas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCanvas.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WalletsController : ControllerBase
	{
		private readonly ILogger<WalletsController> _logger;

		public WalletsController(ILogger<WalletsController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		[ProducesResponseType(typeof(CurrencyRateModel[]), StatusCodes.Status200OK)]
		public IActionResult AdjustBalance([FromQuery] string exchangeRateStrategy, [FromBody] AdjustBalancePayload payload)
		{
			// todo: implement WalletsController.AdjustBalance()

			// use IWalletAdjustmentService.AdjustBalance() to adjust the balance of the wallet

			// return 400 (bad request) in case ther is no sufficient balance to subtract

			// return new balance

			throw new NotImplementedException();
		}
	}
}
