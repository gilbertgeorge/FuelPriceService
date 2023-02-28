namespace FuelPriceService.models;

public class Settings
{
    /// <summary>
    /// URL of API for getting fuel prices
    /// </summary>
    public string apiUrl { get; set; }
    
    /// <summary>
    /// Delay interval in seconds
    /// </summary>
    public int delay { get; set; }
    
    /// <summary>
    /// Look back days count
    /// </summary>
    public int daysCount { get; set; }
    
    /// <summary>
    /// SQL connection string
    /// </summary>
    public string sqlConnectionString { get; set; }
}