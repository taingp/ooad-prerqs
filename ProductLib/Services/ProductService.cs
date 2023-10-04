using ProductLib.Extensions;

namespace ProductLib;
public class ProductService 
{
    private ProductRepo _repo = new();
    private bool isInit = false;

    public List<string?> Initialize()
    {
        if (isInit) return new();
        isInit = !isInit;
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
        return reqs.Select(x => Create(x)).ToList();
    }

    public bool Exist(string key)
    {
        return _repo.GetQueryable().Any(x => x.Id == key || x.Code.ToLower() == key.ToLower());
    }
    public string? Create(ProductCreateReq req)
    {
        lock (_repo)
        {
            if (Exist(req.Code)) return null;
            Product entity = req.ToEntity();
            _repo.Create(entity);
            return entity.Id;
        }
    }
    
    public List<ProductResponse> ReadAll()
    {
        return _repo.GetQueryable().Select(x => x.ToResponse()).ToList();
    }
    public ProductResponse? Read(string key)
    {
        var entity = _repo.GetQueryable().FirstOrDefault(x => x.Id == key || x.Code.ToLower()==key.ToLower());
        return entity?.ToResponse();
    }

    public bool Update(ProductUpdateReq req)
    {
        lock (_repo)
        {
            var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == req.Key) || (x.Code.ToLower() == req.Key.ToLower()));
            if (found == null) return false;
            var entity = found.Clone();
            entity.Copy(req);
            return _repo.Update(entity);
        }
    }
    public bool Delete(string key)
    {
        lock (_repo)
        {
            var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == key) || (x.Code.ToLower() == key.ToLower()));
            if (found == null) return false;
            return _repo.Delete(found.Id);
        }
    }
}