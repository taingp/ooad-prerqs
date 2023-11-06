using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductLib;
using ProductLib.Extensions;

namespace ProductApi;

public class SqlDbContext : DbContext, IDbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
        Products = Set<Product>();
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfig());
        SeedProductData(modelBuilder.Entity<Product>());
    }

    private void SeedProductData(EntityTypeBuilder<Product> entityTypeBuilder)
    {
        var reqs = new List<ProductCreateReq>()
        {
            new()
            {
                Code = "PRD001",
                Name = "Coca",
                Category= "Food"
            },
            new()
            {
                Code = "PRD002",
                Name = "Dream 125",
                Category= "Vehicle"
            },
            new()
            {
                Code = "PRD003",
                Name = "TShirt-SEA game 2023",
                Category= "Cloth"
            }
        };
        entityTypeBuilder.HasData(reqs.Select(x => x.ToEntity()));
    }
}
