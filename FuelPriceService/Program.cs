using FuelPriceService.models;
using Microsoft.Extensions.Configuration;

namespace FuelPriceService
{
    public static class Program
    {
        static async Task Main()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appSettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();

                if (settings != null)
                {
                    var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(settings.delay));
                    while (await periodicTimer.WaitForNextTickAsync())
                    {
                        var fuelPrices = await FuelPriceClient.GetFuelPricesAsync(settings.apiUrl, settings.daysCount);
                        var fuelPriceManager = new FuelPriceManager(settings.sqlConnectionString);
                        var rowsInserted = await fuelPriceManager.InsertFuelPricesAsync(fuelPrices);
                        Console.WriteLine($"Inserted {rowsInserted} rows");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}