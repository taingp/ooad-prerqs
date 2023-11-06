using ProductLib.Extensions;

namespace ProductLib;
public class ProductService
    :IProductService
{
    private readonly IRepo<Product> _repo = default!;
    public ProductService(IRepo<Product> repo) { _repo = repo; }

    public Result<bool> Exist(string key)
    {
        var result = _repo.GetQueryable().Any(x => x.Id == key
                                                || x.Code.ToLower() == key.ToLower());
        return Result<bool>.Success(result);
    }
    public Result<string?> Create(ProductCreateReq req)
    {
        if (Exist(req.Code).Data == true)
            return Result<string?>.Fail($"The product with the code, {req.Code}, does already exist");
        Product entity = req.ToEntity();
        try
        {
            _repo.Create(entity);
            return Result<string?>.Success(entity.Id, "Successfully created");
        }
        catch (Exception ex)
        {
            return Result<string?>.Fail(ex.Message);
        }
    }

    public Result<List<ProductResponse>> ReadAll()
    {
        var result = _repo.GetQueryable().Select(x => x.ToResponse()).ToList();
        return Result<List<ProductResponse>>.Success(result);
    }
    public Result<ProductResponse?> Read(string key)
    {
        var entity = _repo.GetQueryable().FirstOrDefault(x => x.Id == key || x.Code.ToLower() == key.ToLower());
        return Result<ProductResponse?>.Success(entity?.ToResponse());
    }

    public Result<string?> Update(ProductUpdateReq req)
    {
        var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == req.Key)
                                                          || (x.Code.ToLower() == req.Key.ToLower()));
        if (found == null)
            return Result<string?>.Fail($"No product with id/code, {req.Key}");
        var entity = found.Clone();
        entity.Copy(req);
        try
        {
            var result = _repo.Update(entity);
            return result == true ? Result<string?>.Success(entity.Id, "Successfully updated")
                    : Result<string?>.Fail($"Failed to update product with id/code, {req.Key}");
        }
        catch (Exception ex)
        {
            return Result<string?>.Fail(ex.Message);
        }
    }
    public Result<string?> Delete(string key)
    {
        var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == key)
                                                          || (x.Code.ToLower() == key.ToLower()));
        if (found == null)
            return Result<string?>.Fail($"No product with id/code, {key}");
        try
        {
            var result = _repo.Delete(found.Id);
            return result == true ? Result<string?>.Success(found.Id, "Successfully deleted")
                    : Result<string?>.Fail($"Failed to delete product with id/code, {key}");
        }
        catch (Exception ex)
        {
            return Result<string?>.Fail(ex.Message);
        }
    }
}
