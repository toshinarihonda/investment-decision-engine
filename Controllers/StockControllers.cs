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
        public async Task<IActionResult> Get(string code = "72030", string date ="2026-02-05")
        {
            var result = await _jquantsService.GetDailyStockPriceAsync(code, date);
            return Ok(result);
        }
    }
}