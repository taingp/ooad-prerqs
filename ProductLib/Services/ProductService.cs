using ProductLib.Extensions;

namespace ProductLib;
public class ProductService 
{
    private ProductRepo _repo = new();
    private bool isInit = false;

    public Result<List<string?>> Initialize()
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
        var result = reqs.Select(x => Create(x).Data).ToList();
        return Result<List<string?>>.Success(result);
    }

    public Result<bool> Exist(string key)
    {
        var result = _repo.GetQueryable().Any(x => x.Id == key || x.Code.ToLower() == key.ToLower());
        return Result<bool>.Success(result);
    }
    public Result<string?> Create(ProductCreateReq req)
    {
        if (Exist(req.Code).Data == true) return Result<string?>.Fail($"The product with the code, {req.Code}, does already exist");
        Product entity = req.ToEntity();
        _repo.Create(entity);
        return Result<string?>.Success(entity.Id, "Successfully created");
    }
    
    public Result<List<ProductResponse>> ReadAll()
    {
        var result= _repo.GetQueryable().Select(x => x.ToResponse()).ToList();
        return Result<List<ProductResponse>>.Success(result);
    }
    public Result<ProductResponse?> Read(string key)
    {
        var entity = _repo.GetQueryable().FirstOrDefault(x => x.Id == key || x.Code.ToLower()==key.ToLower());
        return Result<ProductResponse?>.Success(entity?.ToResponse());
    }

    public Result<string?> Update(ProductUpdateReq req)
    {
        var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == req.Key) || (x.Code.ToLower() == req.Key.ToLower()));
        if (found == null) return Result<string?>.Fail($"No product with id/code, {req.Key}");
        var entity = found.Clone();
        entity.Copy(req);
        var result= _repo.Update(entity);
        return result == true ? Result<string?>.Success(entity.Id, "Successfully updated")
                : Result<string?>.Fail($"Failed to update product with id/code, {req.Key}");
    }
    public Result<string?> Delete(string key)
    {
        var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == key) || (x.Code.ToLower() == key.ToLower()));
        if (found == null) return Result<string?>.Fail($"No product with id/code, {key}");
        var result= _repo.Delete(found.Id);
        return result == true ? Result<string?>.Success(found.Id, "Successfully deleted")
                : Result<string?>.Fail($"Failed to delete product with id/code, {key}");
    }
}
