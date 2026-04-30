namespace InvestmentAnalysis.Api.Services;

public class JQuantsService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public JQuantsService(HttpClient httpClient,IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GetDailyStockPriceAsync(string code, string date)
    {
        var apiKey = _configuration["Jquants:Apikey"];
        if(string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("JQuants API key is not configured.");
        }

        var url = $"https://api.jquants.com/v2/equities/bars/daily?code={code}&date={date}";

        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        
        var response = await _httpClient.GetAsync(url);
        //var body = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

}