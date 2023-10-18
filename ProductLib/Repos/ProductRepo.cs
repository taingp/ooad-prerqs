using ProductLib.Extensions;

namespace ProductLib;
public class ProductRepo 
{
    private static List<Product> store = new();
   
    public void Create(Product entity)
    {
        store.Add(entity.Clone());
    }
    public IQueryable<Product> GetQueryable()
    {
        return store.AsQueryable();
    }
  
    public bool Update(Product entity)
    {
        var found = GetQueryable().FirstOrDefault(x => x.Id == entity.Id);
        if (found != null) found.Copy(entity);
        return found != null;
    }
    public bool Delete(string id)
    {
        var found = store.FirstOrDefault(x => x.Id == id);
        return found==null? false : store.Remove(found);
    }
}