using TaxCalculator.Domain.Models;

namespace TaxCalculator.Application.Data;

public interface ITaxBandRepository
{
    Task<IEnumerable<TaxBand>> GetAll();
}