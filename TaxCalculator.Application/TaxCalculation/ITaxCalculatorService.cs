using TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;

namespace TaxCalculator.Application.TaxCalculation;

public interface ITaxCalculatorService
{
    Task<CalculateTaxResponse> CalculateTax(int salary);
}