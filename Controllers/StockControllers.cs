using Microsoft.AspNetCore.Mvc;

namespace InvestmentAnalysis.Api.Controllers
{
    [ApiController]
    [Route("stock")]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Stock API is working fine");
        }
    }
}