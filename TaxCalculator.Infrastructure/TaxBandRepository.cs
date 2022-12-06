using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.Data;
using TaxCalculator.Domain.Models;

namespace TaxCalculator.Infrastructure;

public class TaxBandRepository : ITaxBandRepository
{
    private readonly TaxDbContext _taxDbContext;

    public TaxBandRepository(TaxDbContext taxDbContext)
    {
        _taxDbContext = taxDbContext;
    }
    
    public async Task<IEnumerable<TaxBand>> GetAll()
    {
        var entities = await _taxDbContext.TaxBands.ToListAsync();
        return entities.Select(x => new TaxBand
                       {
                           LowerLimit = x.LowerLimit,
                           UpperLimit = x.UpperLimit,
                           Rate = x.Rate
                       })
                       .ToList();
    }
}