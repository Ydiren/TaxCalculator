namespace TaxCalculator.Infrastructure.Entities;

public class TaxBandEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LowerLimit { get; set; }
    public int UpperLimit { get; set; }
    public int Rate { get; set; }
}