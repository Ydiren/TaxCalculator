namespace TaxCalculator.Domain.Models;

public class TaxBand
{
    private const int NoLimit = -1;
    
    public int LowerLimit { get; set; }
    public int UpperLimit { get; set; }
    public int Rate { get; set; }
    
    public decimal CalculateTax(int salary)
    {
        var taxableAmount = FindTaxableAmount(salary);

        var percentage = Rate / 100m;
        return taxableAmount * percentage;
    }

    private int FindTaxableAmount(int salary)
    {
        if (salary < LowerLimit)
        {
            return 0;
        }
        
        var taxableAmount = AdjustTaxableAmountForUpperLimit(salary);

        taxableAmount = AdjustTaxableAmountForLowerLimit(salary, taxableAmount);

        return taxableAmount;
    }

    private int AdjustTaxableAmountForLowerLimit(int salary, int taxableAmount)
    {
        return salary < LowerLimit
                   ? taxableAmount
                   : taxableAmount - LowerLimit;
    }

    private int AdjustTaxableAmountForUpperLimit(int salary)
    {
        if (UpperLimit == NoLimit)
        {
            return salary;
        }

        if (salary > UpperLimit)
        {
            return UpperLimit;
        }

        return salary;
    }
}