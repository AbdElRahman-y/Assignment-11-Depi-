namespace DelegatesEvents.Section1;

public delegate string BookFunctionPtr(Book B);

public class LibraryEngine
{
    public static void ProcessBooks(List<Book> bList, BookFunctionPtr fPtr)
    {
        foreach (Book B in bList)
        {
            Console.WriteLine(fPtr(B));
        }
    }

    public static void ProcessBooksWithFunc(List<Book> bList, Func<Book, string> fPtr)
    {
        foreach (Book B in bList)
        {
            Console.WriteLine(fPtr(B));
        }
    }
}
