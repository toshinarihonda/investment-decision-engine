using InvestmentAnalysis.Api.Services;
using Amazon;
using Amazon.DynamoDBv2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<JQuantsService>();

var awsRegion = builder.Configuration["AWS:Region"] ?? "ap-northeast-1";
var awsServiceUrl = builder.Configuration["AWS:ServiceURL"];

builder.Services.AddSingleton<IAmazonDynamoDB>(_ =>
{
    var config = new AmazonDynamoDBConfig();

    if (!string.IsNullOrWhiteSpace(awsServiceUrl))
    {
        config.ServiceURL = awsServiceUrl;
        config.AuthenticationRegion = awsRegion;
        config.UseHttp = awsServiceUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase);
    }
    else
    {
        config.RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegion);
    }

    return new AmazonDynamoDBClient("dummy", "dummy", config);
});
builder.Services.AddScoped<DynamoDbService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
