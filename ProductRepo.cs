using ProductLib.Extensions;

namespace ProductLib;
public class ProductRepo 
{
    private List<Product> store = new();
    private bool isInit =false;

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
    public string? Create(ProductCreateReq req)
    {
        Product entity = req.ToEntity();
        store.Add(entity);
        return entity.Id;
    }
    
    public List<ProductResponse> ReadAll()
    {
        return store.Select(x => x.ToResponse()).ToList();
    }
    public ProductResponse? Read(string key)
    {
        var entity = store.FirstOrDefault(x => x.Id == key || x.Code.ToLower()==key.ToLower());
        return entity?.ToResponse();
    }
}