namespace TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;

public class CalculateTaxResponse
{

    public CalculateTaxResponse(int salary)
    {
        GrossAnnualSalary = salary;
    }

    public int GrossAnnualSalary { get; }
    public decimal GrossMonthlySalary => GrossAnnualSalary / 12m;
    public decimal NetAnnualSalary { get; set; }
    public decimal NetMonthlySalary => NetAnnualSalary / 12m;
    public decimal AnnualTax { get; set; }
    public decimal MonthlyTax => AnnualTax / 12m;
}