namespace ProductLib;
public class Product
{
    public string? Id {get;set;}=default;
    public string Code {get;set;}=default!;
    public string? Name {get;set;}=default;
    public Category Category {get;set;} = Category.None;
    public DateTime? Created {get;set;}= default;
    public DateTime? LastUpdated {get;set;} = default;
}
