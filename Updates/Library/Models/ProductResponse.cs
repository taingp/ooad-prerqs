namespace ProductLib;
public class ProductResponse
    :ProductBase
    , IResponse
{
    public string? Id {get;set;}=default;
    public string Code {get;set;}=default!;
    public string? Category {get;set;} = default;
}