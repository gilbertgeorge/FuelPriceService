namespace FuelPriceService.models.API;

public class FuelPriceRoot
{
    public Request? request { get; set; }
    public List<Series>? series { get; set; }
}