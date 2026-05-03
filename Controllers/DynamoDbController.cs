using InvestmentAnalysis.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAnalysis.Api.Controllers
{
    [ApiController]
    [Route("dynamodb")]
    public class DynamoDbController : ControllerBase
    {
        private readonly DynamoDbService _dynamoDbService;

        public DynamoDbController(DynamoDbService dynamoDbService)
        {
            _dynamoDbService = dynamoDbService;
        }

        [HttpGet("tables")]
        public async Task<IActionResult> GetTables()
        {
            var tables = await _dynamoDbService.ListTablesAsync();
            return Ok(tables);
        }
    }
}
