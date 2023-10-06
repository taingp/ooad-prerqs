namespace BookLib;

public static class BookExtensions
{
    public static Book Clone(this Book book)
    {
        return new Book()
        {
            Id = book.Id,
            Title = book.Title,
            Pages = book.Pages 
        };
    }
    public static void Copy(this Book book, Book other)
    {
        book.Id = other.Id;
        book.Title = other.Title;
        book.Pages = other.Pages;
    }
}
