using MediatR;

namespace TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;

public record CalculateTaxQuery(int Salary) : IRequest<CalculateTaxResponse>;