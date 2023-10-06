namespace BookLib;

public class BookRepo 
{
    private static List<Book> store = new();
   
    public void Create(Book entity)
    {
        store.Add(entity.Clone());
    }
    public IQueryable<Book> GetQueryable()
    {
        return store.AsQueryable();
    }
  
    public bool Update(Book entity)
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