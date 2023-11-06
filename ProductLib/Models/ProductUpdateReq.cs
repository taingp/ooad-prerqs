namespace ProductLib;

public class ProductUpdateReq
{
    public string Key{ get; set; } = default!;
    public string? Name { get; set; } = default;
    public string? Category { get; set; } = default;
}