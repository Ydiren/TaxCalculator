using Microsoft.EntityFrameworkCore;
using TaxCalculator.Infrastructure.Entities;

namespace TaxCalculator.Infrastructure;

public class TaxDbContext : DbContext
{
    public TaxDbContext(DbContextOptions<TaxDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<TaxBandEntity> TaxBands { get; set; }
}