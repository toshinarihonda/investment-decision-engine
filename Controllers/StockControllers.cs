using InvestmentAnalysis.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAnalysis.Api.Controllers
{
    [ApiController]
    [Route("stock")]
    public class StockController : ControllerBase
    {
        private readonly JQuantsService _jquantsService;
        public StockController(JQuantsService jquantsService)
        {
            _jquantsService = jquantsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _jquantsService.GetDailyStockPriceAsync("72030", "2026-02-05");
            return Ok(result);
        }
    }
}