using AcmeLibrary.Models;
using AcmeLibrary.Models.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcmeLibrary.UnitTest.Helpers
{
  class CheckedOutTestHelper
  {
    public LibraryDbContext SetupLibraryDbContext()
    {
      return new HelperBase().SetupLibraryDbContext();
    }

    public CheckedOutBook FindCheckedOutBookByBookId(LibraryDbContext context, CheckedOutBook checkedOutBook)
    {
      return context.CheckedOutBook.FirstOrDefault(b => b.BookId == checkedOutBook.BookId);
    }

    public CheckedOutBook FindCheckedOutBookByUserId(LibraryDbContext context, CheckedOutBook checkedOutBook)
    {
      return context.CheckedOutBook.FirstOrDefault(b => b.UserID == checkedOutBook.UserID);
    }

    public void DeleteCheckedOutBookByRecord(LibraryDbContext context, CheckedOutBook book)
    {
      try
      {
        context.CheckedOutBook.Remove(book);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public CheckedOutBook DoesCheckedOutBookBookIdExist(LibraryDbContext context, CheckedOutBook testRecord)
    {
      var result = this.FindCheckedOutBookByBookId(context, testRecord);
      if (result == null)
      {
        context = this.SetupAndCreateCheckedOutBook(context, testRecord);
      }
      return this.FindCheckedOutBookByBookId(context, testRecord);
    }

    public CheckedOutBook DoesCheckedOutBookUserExist(LibraryDbContext context, CheckedOutBook testRecord)
    {
      var result = this.FindCheckedOutBookByUserId(context, testRecord);
      if (result == null)
      {
        context = this.SetupAndCreateCheckedOutBook(context, testRecord);
      }
      return this.FindCheckedOutBookByUserId(context, testRecord);
    }

    public LibraryDbContext SetupAndCreateCheckedOutBook(LibraryDbContext context, CheckedOutBook testRecord)
    {
      try
      {
        context.CheckedOutBook.Add(testRecord);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return context;
    }

    public Book SetupBookRecord(LibraryDbContext context, int testCaseId)
    {
      BookTestHelper bookTestHelper = new BookTestHelper();
      var bookrecord = bookTestHelper.SetupTestBook(testCaseId);
      bookTestHelper.SetupAndCreateBook(context, bookrecord);
      return bookTestHelper.FindBookByTitle(context, bookrecord);
    }

    public User SetupUserRecord(LibraryDbContext context, int testCaseId)
    {
      UserTestHelper userTestHelper = new UserTestHelper();
      var userRecord = userTestHelper.SetupTestUser(testCaseId);
      userTestHelper.SetupAndCreateUser(context, userRecord);
      return userTestHelper.FindUserByIdNumber(context, userRecord);
    }

    public LibraryDbContext SetupAndCreateCheckedOutBook(LibraryDbContext context, int testCaseId)
    {
      try
      {
        var book = SetupBookRecord(context, testCaseId);
        var user = SetupUserRecord(context, testCaseId);

        var stubCheckedOutBook = SetupTestCheckedOutBook(testCaseId, book.BookId, user.UserId);

        context.CheckedOutBook.Add(stubCheckedOutBook);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return context;
    }

    public CheckedOutBook SetupTestCheckedOutBook(int id, int bookId, int userId)
    {
      CheckedOutBook checkedOutBook = new CheckedOutBook();
      switch (id)
      {
        case 1:
          checkedOutBook = new CheckedOutBook()
          {
            CheckedOutBookId = id,
            BookId = bookId,
            UserID = userId,
            CreatedDateTime = DateTime.Now,
            Returned = false,
            Quality = "Good condition"
          };
          break;
        case 2:
          checkedOutBook = new CheckedOutBook()
          {
            CheckedOutBookId = id,
            BookId = bookId,
            UserID = userId,
            CreatedDateTime = DateTime.Now,
            Returned = false,
            Quality = "slightly worn"
          };
          break;
      }
      return checkedOutBook;
    }
  }
}
