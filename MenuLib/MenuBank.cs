namespace MenuLib;
public class MenuBank
{
    public string Title { get; set; } = "";
    public string ChosenText { get; set; } = "Index";
    public string LeadingText { get; set; } = "  ";
    public List<Menu> Menus { get; set; } = new();

    public void Show()
    {
        Console.WriteLine($"[{Title} menu]");
        for(int k=0; k<Menus.Count; k++)
        {
            var menu = Menus[k];
            Console.WriteLine($"{k + 1,3}-{menu.Text}");
        }
    }
    public Menu GetMenu()
    {
        int input = Input(1, Menus.Count, ChosenText, LeadingText);
        return Menus[input - 1];
    }

    public void MenuSimulate(Action? leadingAction=null)
    {
        while (true)
        {
            if (leadingAction != null) leadingAction();
            Show();
            var actingMenu = GetMenu();
            actingMenu.Action();
        }
    }
    public static int Input(int lower, int upper, string text, string leading="")
    {
        while (true)
        {
            Console.Write($"{leading}{text}({lower}-{upper}): ");
            if (int.TryParse(Console.ReadLine(), out var input) == false)
            {
                Console.WriteLine($"{leading}>Invalid input format");
                continue;
            }
            if (lower <= input && input <= upper) return input;
            Console.WriteLine($"{leading}>The input is {input}, the input must be in [{lower}, {upper}]");
        }
    }

    
}
