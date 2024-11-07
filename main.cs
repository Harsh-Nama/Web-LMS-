using System;
using System.Collections.Generic;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public float Price { get; set; }
    public bool IsIssued { get; set; } // Added to track if a book is issued

    public Book(string title = "", string author = "", int pages = 0, float price = 0.0f)
    {
        Title = title;
        Author = author;
        Pages = pages;
        Price = price;
        IsIssued = false; // Initialize IsIssued to false
    }
}

class Library
{
    private const int MAX_BOOKS = 100; // Maximum number of books
    private List<Book> books; // List of Book objects

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        if (books.Count < MAX_BOOKS)
        {
            books.Add(book); // Add the book to the list
            Console.WriteLine("Book added successfully!");
        }
        else
        {
            Console.WriteLine("Library is full, cannot add more books.");
        }
    }

    public void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }
        Console.WriteLine("Books in the Library:");
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Pages: {book.Pages}, Price: ${book.Price}, Issued: {(book.IsIssued ? "Yes" : "No")}");
        }
    }

    public void SearchBook(string title)
    {
        foreach (var book in books)
        {
            if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Book found:");
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Pages: {book.Pages}, Price: ${book.Price}, Issued: {(book.IsIssued ? "Yes" : "No")}");
                return;
            }
        }
        Console.WriteLine("Book not found.");
    }

    public void ListBooksByAuthor(string author)
    {
        bool found = false;
        foreach (var book in books)
        {
            if (book.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{book.Title}, {book.Author}, Pages: {book.Pages}, Price: ${book.Price}, Issued: {(book.IsIssued ? "Yes" : "No")}");
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine($"No books found by author {author}.");
        }
    }

    public int GetBookCount()
    {
        return books.Count;
    }

    public void IssueBook(string title)
    {
        foreach (var book in books)
        {
            if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                if (!book.IsIssued)
                {
                    book.IsIssued = true; // Mark book as issued
                    Console.WriteLine($"Book '{title}' has been issued.");
                }
                else
                {
                    Console.WriteLine($"Book '{title}' is already issued.");
                }
                return;
            }
        }
        Console.WriteLine("Book not found.");
    }

    public void ReturnBook(string title)
    {
        foreach (var book in books)
        {
            if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                if (book.IsIssued)
                {
                    book.IsIssued = false; // Mark book as not issued
                    Console.WriteLine($"Book '{title}' has been returned.");
                }
                else
                {
                    Console.WriteLine($"Book '{title}' was not issued.");
                }
                return;
            }
        }
        Console.WriteLine("Book not found.");
    }
}

class Program
{
    static void AdminMenu(Library library)
    {
        int choice;
        do
        {
            Console.WriteLine("\nAdmin Menu");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Display Books");
            Console.WriteLine("3. List Books by Author");
            Console.WriteLine("4. Count Books in Library");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter author: ");
                        string author = Console.ReadLine();
                        Console.Write("Enter pages: ");
                        int pages = int.Parse(Console.ReadLine());
                        Console.Write("Enter price: ");
                        float price = float.Parse(Console.ReadLine());
                        library.AddBook(new Book(title, author, pages, price));
                        break;
                    }
                case 2:
                    library.DisplayBooks();
                    break;
                case 3:
                    {
                        Console.Write("Enter author name: ");
                        string author = Console.ReadLine();
                        library.ListBooksByAuthor(author);
                        break;
                    }
                case 4:
                    Console.WriteLine($"Total books in library: {library.GetBookCount()}");
                    break;
                case 5:
                    Console.WriteLine("Exiting admin menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
        } while (choice != 5);
    }

    static void StudentMenu(Library library)
    {
        int choice;
        do
        {
            Console.WriteLine("\nStudent Menu");
            Console.WriteLine("1. Search Book");
            Console.WriteLine("2. Issue Book");
            Console.WriteLine("3. Return Book");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        Console.Write("Enter title to search: ");
                        string title = Console.ReadLine();
                        library.SearchBook(title);
                        break;
                    }
                case 2:
                    {
                        Console.Write("Enter title to issue: ");
                        string title = Console.ReadLine();
                        library.IssueBook(title); // Call the IssueBook function
                        break;
                    }
                case 3:
                    {
                        Console.Write("Enter title to return: ");
                        string title = Console.ReadLine();
                        library.ReturnBook(title); // Call the ReturnBook function
                        break;
                    }
                case 4:
                    Console.WriteLine("Exiting student menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
        } while (choice != 4);
    }

    static void Main()
    {
        Library library = new Library();
        int userType;

        do
        {
            Console.WriteLine("Welcome to the Library Management System");
            Console.WriteLine("Select User Type:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Student");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            userType = int.Parse(Console.ReadLine());

            switch (userType)
            {
                case 1:
                    AdminMenu(library);
                    break;
                case 2:
                    StudentMenu(library);
                    break;
                case 3:
                    Console.WriteLine("Exiting the system...");
                    break;
                default:
                    Console.WriteLine("Invalid user type selected. Please try again.");
                    break;
            }
        } while (userType != 3);
    }
}
