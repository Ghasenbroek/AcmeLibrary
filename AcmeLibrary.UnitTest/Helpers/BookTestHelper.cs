using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeLibrary.Models;
using AcmeLibrary.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AcmeLibrary.UnitTest.Helpers
{
  public class BookTestHelper
  {

    public LibraryDbContext SetupLibraryDbContext()
    {

      return new HelperBase().SetupLibraryDbContext();
    }

    public Book FindBookByISBN(LibraryDbContext context, Book book)
    {
      return context.Books.FirstOrDefault(b => b.ISBN == book.ISBN);
    }

    public Book FindBookByTitle(LibraryDbContext context, Book book)
    {
      return context.Books.FirstOrDefault(b => b.Title.Contains(book.Title));
    }

    public Book FindBookByAuthor(LibraryDbContext context, Book book)
    {
      return context.Books.FirstOrDefault(b => b.Authors.Contains(book.Authors));
    }

    public void DeleteBookByRecord(LibraryDbContext context, Book book)
    {
      try
      {
        context.Books.Remove(book);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Book DoesBookISBNExist(LibraryDbContext context, Book testRecord)
    {
      var result = this.FindBookByISBN(context, testRecord);
      if (result == null)
      {
        context = this.SetupAndCreateBook(context, testRecord);
      }
      return this.FindBookByISBN(context, testRecord);
    }

    public Book DoesBookTitleExist(LibraryDbContext context, Book testRecord)
    {
      var result = this.FindBookByTitle(context, testRecord);
      if (result == null)
      {
        context = this.SetupAndCreateBook(context, testRecord);
      }
      return this.FindBookByTitle(context, testRecord);
    }

    public Book DoesBookAuthorExist(LibraryDbContext context, Book testRecord)
    {
      var result = this.FindBookByAuthor(context, testRecord);
      if (result == null)
      {
        context = this.SetupAndCreateBook(context, testRecord);
      }
      return this.FindBookByAuthor(context, testRecord);
    }

    public LibraryDbContext SetupAndCreateBook(LibraryDbContext context, Book testRecord)
    {
      try
      {
        if (context.Books.Any(u => u.ISBN == testRecord.ISBN))
        {
          //user already exists
          return context;
        }
        else
        {
          context.Books.Add(testRecord);
          context.SaveChanges();
        }
          
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return context;
    }

    public LibraryDbContext SetupAndCreateBook(LibraryDbContext context, int testCaseId)
    {
      try
      {
        var stubBook = SetupTestBook(testCaseId);
        context.Books.Add(stubBook);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
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
            BookId = 0,
            Title = "James and the Giant Peach",
            Authors = "Roald Dahl",
            ISBN = "x81a2q6",
            Condition = "Great"
          };
          break;
        case 2:
          book = new Book()
          {
            BookId = 0,
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
