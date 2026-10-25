namespace DelegatesEvents.Section1;

public static class Section1Demo
{
    public static void Run()
    {
        List<Book> books = new List<Book>
        {
            new Book("111-1", "C# in Depth", new[] { "Jon Skeet" }, new DateTime(2019, 3, 1), 45.5m),
            new Book("222-2", "Clean Code", new[] { "Robert Martin" }, new DateTime(2008, 8, 1), 38m),
            new Book("333-3", "Design Patterns", new[] { "Gamma", "Helm", "Johnson", "Vlissides" }, new DateTime(1994, 10, 21), 55m)
        };

        Console.WriteLine("--- 1) User-defined delegate (GetTitle, GetAuthors, GetPrice) ---");
        LibraryEngine.ProcessBooks(books, BookFunctions.GetTitle);
        LibraryEngine.ProcessBooks(books, BookFunctions.GetAuthors);
        LibraryEngine.ProcessBooks(books, BookFunctions.GetPrice);

        Console.WriteLine("--- 2) Built-in delegate Func<Book, string> ---");
        LibraryEngine.ProcessBooksWithFunc(books, BookFunctions.GetTitle);

        Console.WriteLine("--- 3) Anonymous method (GetISBN) ---");
        LibraryEngine.ProcessBooksWithFunc(books, delegate (Book B) { return B.ISBN; });

        Console.WriteLine("--- 4) Lambda expression (GetPublicationDate) ---");
        LibraryEngine.ProcessBooksWithFunc(books, B => B.PublicationDate.ToShortDateString());
    }
}
