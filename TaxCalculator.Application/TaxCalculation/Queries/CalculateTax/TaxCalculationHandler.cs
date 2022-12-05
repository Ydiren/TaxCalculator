using MediatR;

namespace TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;

public class TaxCalculationHandler : IRequestHandler<CalculateTaxQuery, CalculateTaxResponse>
{
    private readonly ITaxCalculatorService _taxCalculationService;

    public TaxCalculationHandler(ITaxCalculatorService taxCalculationService)
    {
        _taxCalculationService = taxCalculationService;
    }

    public async Task<CalculateTaxResponse> Handle(CalculateTaxQuery request, CancellationToken cancellationToken)
    {
        return await _taxCalculationService.CalculateTax(request.Salary);
    }
}