namespace FuelPriceService.models;

public class FuelPrice
{
    /// <summary>
    /// Date of fuel price
    /// </summary>
    public DateTime FuelPriceDate { get; set; }
    
    /// <summary>
    /// Fuel price amount
    /// </summary>
    public decimal FuelPriceAmount { get; set; }
}