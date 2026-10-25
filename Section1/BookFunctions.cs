namespace DelegatesEvents.Section1;

public class BookFunctions
{
    public static string GetTitle(Book B) => B.Title;

    public static string GetAuthors(Book B) => string.Join(", ", B.Authors);

    public static string GetPrice(Book B) => B.Price.ToString("C");

    public static string GetISBN(Book B) => B.ISBN;

    public static string GetPublicationDate(Book B) => B.PublicationDate.ToShortDateString();
}
