using System.Globalization;
using FuelPriceService.models;
using FuelPriceService.models.API;
using Newtonsoft.Json;

namespace FuelPriceService;

public static class FuelPriceClient
{
    public static async Task<List<FuelPrice>> GetFuelPricesAsync(string? apiUrl, int daysBack)
    {
        var client = new HttpClient();
       
        var response = await client.GetAsync(apiUrl);
        var content = await response.Content.ReadAsStringAsync();
        var fuelPriceObjects = JsonConvert.DeserializeObject<FuelPriceRoot>(content);

        var result = new List<FuelPrice>();
        if (fuelPriceObjects is {series.Count: > 0} &&
            fuelPriceObjects.series[0].data.Count > 0)
        {
            foreach (var data in fuelPriceObjects.series[0].data)
            {
                var fuelPrice = new FuelPrice
                {
                    FuelPriceDate = DateTime.ParseExact((string) data[0], "yyyyMMdd", CultureInfo.InvariantCulture),
                    FuelPriceAmount = decimal.Parse((data[1].ToString() ?? "0"), CultureInfo.InvariantCulture)
                };
                result.Add(fuelPrice);
            }
        }
        
        var dateFilter = DateTime.Now.AddDays(-daysBack);
        result = result.Where(fuelPrice => fuelPrice.FuelPriceDate >= dateFilter).ToList();
        
        return result;
    }
}