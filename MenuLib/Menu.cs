namespace MenuLib;
public class Menu
{
    public string Text { get; set; } = "";
    public Action Action { get; set; } = default!;
}