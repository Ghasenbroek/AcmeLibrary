using System;
using System.Collections.Generic;
using System.Text;
using AcmeLibrary.Models;
using AcmeLibrary.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AcmeLibrary.UnitTest
{
  class Helper
  {

    public LibraryDbContext SetupLibraryDbContext()
    {
      var builder = new DbContextOptionsBuilder<LibraryDbContext>()
        .EnableSensitiveDataLogging()
        .UseSqlServer("Server=DESKTOP-IMJC493\\GHASENBROEK; Database=DonationDB; Trusted_Connection=True;");
      var context = new LibraryDbContext(builder.Options);
      context.Database.OpenConnection();
      context.Database.EnsureCreated();

      return context;
    }

    public LibraryDbContext SetupBooksToInsert(LibraryDbContext context, int testCaseId)
    {
      context.Books.Add(SetupTestBook(testCaseId));
      return context;
    }

    public Book SetupTestBook(int id)
    {
      Book book = new Book();
      switch (id)
      {
        case 1:
          book = new Book()
          {
            BookID = 0,
            Title = "James and the Giant Peach",
            Authors = "Roald Dahl",
            ISBN = "x81a2q6",
            Condition = "Great"
          };
          break;
        case 2:
          book = new Book()
          {
            BookID = 0,
            Title = "Lord of the Rings, The Return of the King",
            Authors = "J. R. R. Tolkien",
            ISBN = "g2f5skcs",
            Condition = "Slightly worn"
          };
          break;
      }
      return book;
    }
  }
}
