using Microsoft.EntityFrameworkCore;

namespace ProductLib
{
    public interface IDbContext
    {
        DbSet<Product> Products { get; set; }

        int SaveChanges();
    }
}