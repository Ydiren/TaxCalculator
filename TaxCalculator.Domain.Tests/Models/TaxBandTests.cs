using FluentAssertions;
using TaxCalculator.Domain.Models;

namespace TaxCalculator.Domain.Tests.Models;

public class TaxBandTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CalculateTax_WhenSalaryIsZero_ReturnsZero()
    {
        var sut = new TaxBand();

        var result = sut.CalculateTax(0);

        result.Should()
              .Be(0);
    }

    [TestCase(0, -1, 20, 10000, "2000")]
    [TestCase(0, 5000, 20, 10000, "1000")]
    [TestCase(5000, -1, 20, 10000, "1000")]
    [TestCase(5000, 10000, 20, 20000, "1000")]
    [TestCase(5000, 10000, 40, 20000, "2000")]
    [TestCase(15000, 20000, 20, 10000, "0")]
    [TestCase(15000, 20000, 0, 50000, "0")]
    [TestCase(5000, 10000, 20, 4999, "0")]
    [TestCase(5000, 10000, 20, 5000, "0")]
    [TestCase(5000, 10000, 20, 5001, "0.2")]
    public void CalculateTax_WhenLimitsRateAndSalaryAreGiven_ReturnsExpectedValue(int lowerLimit, int upperLimit, int rate, int salary, decimal expectedResult)
    {
        var sut = new TaxBand
        {
            LowerLimit = lowerLimit,
            UpperLimit = upperLimit,
            Rate = rate
        };

        var result = sut.CalculateTax(salary);

        result.Should()
              .Be(expectedResult);
    }
}