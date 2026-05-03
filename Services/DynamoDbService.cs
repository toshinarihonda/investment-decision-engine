using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace InvestmentAnalysis.Api.Services;

public class DynamoDbService
{
    private readonly IAmazonDynamoDB _dynamoDb;

    public DynamoDbService(IAmazonDynamoDB dynamoDb)
    {
        _dynamoDb = dynamoDb;
    }

    public async Task<List<string>> ListTablesAsync()
    {
        var response = await _dynamoDb.ListTablesAsync();
        return response.TableNames;
    }
}