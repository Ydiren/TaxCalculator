using FluentAssertions;
using Moq;
using TaxCalculator.Application.Data;
using TaxCalculator.Application.TaxCalculation;
using TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;
using TaxCalculator.Domain.Models;

namespace TaxCalculator.Application.Tests.TaxCalculation;

public class TaxCalculatorServiceTests
{
    [Test]
    public async Task CalculateTax_WhenSalaryIsZero_ReturnsZeroInAllProperties()
    {
        var expectedResult = new CalculateTaxResponse(0);
        var repo = CreateMockRepository(new[]
        {
            new TaxBand
            {
                LowerLimit = 0,
                UpperLimit = -1,
                Rate = 20
            }
        });

        var sut = new TaxCalculatorService(repo.Object);

        var result = await sut.CalculateTax(0);

        result.Should()
              .BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task CalculateTax_WhenNoTaxBandsExist_ReturnsZeroInAllProperties()
    {
        var expectedResult = new CalculateTaxResponse(0);
        var repo = CreateMockRepository(new List<TaxBand>());
        
        var sut = new TaxCalculatorService(repo.Object);

        var result = await sut.CalculateTax(0);

        result.Should()
              .BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task CalculateTax_WhenOneTaxBandExists_CalculatesExpectedTaxValues()
    {
        var salary = 20000;
        var taxRate = 20;
        var annualTax = salary * (taxRate / 100m);
        var expectedResult = new CalculateTaxResponse(salary)
        {
            NetAnnualSalary = salary - annualTax,
            AnnualTax = annualTax
        };
        
        var repo = CreateMockRepository(new[]
        {
            new TaxBand
            {
                LowerLimit = 0,
                UpperLimit = -1,
                Rate = taxRate
            }
        });

        var sut = new TaxCalculatorService(repo.Object);

        var actualResult = await sut.CalculateTax(salary);

        actualResult.Should()
                    .BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task CalculateTax_WhenTwoTaxBandsExists_CalculatesExpectedTaxValues()
    {
        var salary = 20000;
        var lowerTaxRate = 20;
        var higherTaxRate = 40;
        var annualTax = 5000 * (lowerTaxRate / 100m);
        annualTax += 5000 * (higherTaxRate / 100m);
        var expectedResult = new CalculateTaxResponse(salary)
        {
            NetAnnualSalary = salary - annualTax,
            AnnualTax = annualTax
        };
        
        var repo = CreateMockRepository(new[]
        {
            new TaxBand
            {
                LowerLimit = 5000,
                UpperLimit = 10000,
                Rate = lowerTaxRate
            },
            new TaxBand
            {
                LowerLimit = 10000,
                UpperLimit = 15000,
                Rate = higherTaxRate
            }
        });

        var sut = new TaxCalculatorService(repo.Object);

        var actualResult = await sut.CalculateTax(salary);

        actualResult.Should()
                    .BeEquivalentTo(expectedResult);
    }
    [Test]
    public async Task CalculateTax_WhenThreeUnevenlyDistributedTaxBandsExists_CalculatesExpectedTaxValues()
    {
        var salary = 50000;
        var lowerTaxRate = 20;
        var higherTaxRate = 40;
        var annualTax = 20000 * (lowerTaxRate / 100m);
        annualTax += 10000 * (higherTaxRate / 100m);
        var expectedResult = new CalculateTaxResponse(salary)
        {
            NetAnnualSalary = salary - annualTax,
            AnnualTax = annualTax
        };
        
        var repo = CreateMockRepository(new[]
        {
            new TaxBand
            {
                LowerLimit = 0,
                UpperLimit = 5000,
                Rate = 0
            },
            new TaxBand
            {
                LowerLimit = 20000,
                UpperLimit = 40000,
                Rate = lowerTaxRate
            },
            new TaxBand
            {
                LowerLimit = 40000,
                UpperLimit = -1,
                Rate = higherTaxRate
            }
        });

        var sut = new TaxCalculatorService(repo.Object);

        var actualResult = await sut.CalculateTax(salary);

        actualResult.Should()
                    .BeEquivalentTo(expectedResult);
    }

    private static Mock<ITaxBandRepository> CreateMockRepository(IEnumerable<TaxBand> taxBands)
    {
        var repo = new Mock<ITaxBandRepository>();
        repo.Setup(x => x.GetAll())
            .ReturnsAsync(taxBands.ToList());

        return repo;
    }
}