using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;

namespace TaxCalculator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaxCalculatorController : ControllerBase
{
    [HttpGet("calculatetax")]
    public async Task<CalculateTaxResponse> CalculateTax([FromQuery] int salary, [FromServices] ISender sender)
    {
        var query = new CalculateTaxQuery(salary);
        return await sender.Send(query);
    }
}