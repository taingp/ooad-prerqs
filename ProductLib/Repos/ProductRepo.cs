using Microsoft.EntityFrameworkCore;
using ProductLib.Extensions;

namespace ProductLib;
public class ProductRepo 
{
    private readonly IDbContext _context = default!;
    public ProductRepo(IDbContext context)
    {
        _context = context;
    }
    public void Create(Product entity)
    {
        try
        {
            _context.Products.Add(entity.Clone());
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create > {ex.Message}");
        }
    }
    public IQueryable<Product> GetQueryable()
    {
        return _context.Products.AsQueryable();
    }

    public bool Update(Product entity)
    {
        var found = GetQueryable().FirstOrDefault(x => x.Id == entity.Id);
        if (found != null)
        {
            try
            {
                found.Copy(entity);
                _context.Products.Update(found);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update > {ex.Message}");
            }
        }
        return found != null;
    }
    public bool Delete(string id)
    {
        var found = GetQueryable().FirstOrDefault(x => x.Id == id);
        if (found != null)
        {
            try
            {
                _context.Products.Remove(found);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete > {ex.Message}");
            }
        }
        return false;
    }
    public IDbContext DbContext => _context;
}