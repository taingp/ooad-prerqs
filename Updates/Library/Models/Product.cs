namespace ProductLib;
public class Product
    :ProductBase
    , IKey 
{
    public string Id {get;set;}=default!;
    public string Code {get;set;}=default!;
    public Category Category {get;set;} = Category.None;
    public DateTime? CreatedOn {get;set;}= default;
    public DateTime? LastUpdatedOn {get;set;} = default;
}
