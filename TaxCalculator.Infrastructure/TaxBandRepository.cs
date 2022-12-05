using TaxCalculator.Application.Data;
using TaxCalculator.Domain.Models;

namespace TaxCalculator.Infrastructure;

public class TaxBandRepository : ITaxBandRepository
{
    public Task<IEnumerable<TaxBand>> GetAll()
    {
        throw new NotImplementedException();
    }
}