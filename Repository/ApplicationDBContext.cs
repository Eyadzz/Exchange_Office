using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ExchangeHistory>()
            .HasOne<Currency>()
            .WithMany()
            .HasForeignKey(p => p.CurrencyId);
        
        builder.Entity<Currency>().HasQueryFilter(cur => cur.IsActive);

        builder.Entity<Currency>(entity =>
        {
            entity.HasIndex(currency => currency.Name).IsUnique();
        });
    }

    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeHistory> ExchangeHistories { get; set; }
}