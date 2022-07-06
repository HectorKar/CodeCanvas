using System;
using System.Linq;
using System.Threading.Tasks;
using CodeCanvas.Models;
using CodeCanvas.Services;
using EuropeanCentralBank;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CodeCanvas.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RatesController : ControllerBase
	{
		private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _ratesService;

		public RatesController(ILogger<RatesController> logger, IRatesService ratesService)
		{
			_logger = logger;
            _ratesService = ratesService;
        }

		[HttpGet]
		[ProducesResponseType(typeof(CurrencyRateModel[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetRates([FromQuery] DateTime date) {
            try {
                var rates = await _ratesService.GetRatesByDate(date);
                if (rates.Count > 0) {
                    Log.Logger.Information("GetRates for {date} ran successfully", date);
                    return Ok(rates);
                }
                else {
                    Log.Logger.Information("GetRates for {date} return no results!", date);
                    return NotFound();
                }
			}
            catch (Exception e) {
                Log.Logger.Information("Something went wrong when trying to run GetRates for {date}. Exception: {e}", date, e);
                return NotFound();
            }
        }
	}
}
