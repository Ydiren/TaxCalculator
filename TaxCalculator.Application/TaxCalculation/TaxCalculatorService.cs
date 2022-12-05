using TaxCalculator.Application.Data;
using TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;

namespace TaxCalculator.Application.TaxCalculation;

public class TaxCalculatorService : ITaxCalculatorService
{
    private readonly ITaxBandRepository _taxBandRepository;

    public TaxCalculatorService(ITaxBandRepository taxBandRepository)
    {
        _taxBandRepository = taxBandRepository;
    }

    public async Task<CalculateTaxResponse> CalculateTax(int salary)
    {
        var totalTaxAmount = (await _taxBandRepository.GetAll())
                                               .Sum(taxBand => taxBand.CalculateTax(salary));

        var response = new CalculateTaxResponse(salary)
        {
            NetAnnualSalary = salary - totalTaxAmount,
            AnnualTax = totalTaxAmount
        };
        
        return response;
    }
}