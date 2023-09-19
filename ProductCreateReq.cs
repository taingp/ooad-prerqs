namespace ProductLib;
public class ProductCreateReq
{
    public string Code {get;set;}=default!;
    public string? Name {get;set;}=default;
    public string? Category {get;set;} = default;
}