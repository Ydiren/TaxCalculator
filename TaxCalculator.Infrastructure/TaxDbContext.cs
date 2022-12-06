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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxBandEntity>()
                    .HasData(new TaxBandEntity
                             {
                                 Id = 1,
                                 Name = "Tax Band A",
                                 LowerLimit = 0,
                                 UpperLimit = 5000,
                                 Rate = 0
                             },
                             new TaxBandEntity
                             {
                                 Id = 2,
                                 Name = "Tax Band B",
                                 LowerLimit = 5000,
                                 UpperLimit = 20000,
                                 Rate = 20
                             },
                             new TaxBandEntity
                             {
                                 Id = 3,
                                 Name = "Tax Band C",
                                 LowerLimit = 20000,
                                 UpperLimit = -1,
                                 Rate = 40
                             });
        
        base.OnModelCreating(modelBuilder);
    }
}