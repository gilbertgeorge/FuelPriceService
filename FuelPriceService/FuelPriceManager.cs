using System.Data.SqlClient;
using Dapper;
using FuelPriceService.models;

namespace FuelPriceService;

public class FuelPriceManager
{
    private readonly SqlConnection _connection = new();
    
    public FuelPriceManager(string sqlConnectionString)
    {
        _connection.ConnectionString = sqlConnectionString;
        _connection.Open();
    }

    public async Task<int> InsertFuelPricesAsync(List<FuelPrice> fuelPrices)
    {
        var result = 0;
        foreach (var fuelPrice in fuelPrices)
        {
            if (!await CheckIfFuelPriceExistsAsync(fuelPrice))
            {
                var sql = "INSERT INTO FuelPrices (FuelPriceDate, FuelPriceAmount) VALUES (@FuelPriceDate, @FuelPriceAmount)";
                result += await _connection.ExecuteAsync(sql, fuelPrice);
            }
        }

        return result;
    }
    
    private async Task<bool> CheckIfFuelPriceExistsAsync(FuelPrice fuelPrice)
    {
        var sql = "SELECT COUNT(*) FROM FuelPrices WHERE FuelPriceDate = @FuelPriceDate";
        var count = await _connection.ExecuteScalarAsync<int>(sql, fuelPrice);
        return count > 0;
    }
}