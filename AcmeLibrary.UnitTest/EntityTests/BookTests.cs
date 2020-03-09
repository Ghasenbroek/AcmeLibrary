using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using AcmeLibrary.UnitTest.Helpers;

namespace AcmeLibrary.UnitTest.EntityTest
{
  public class BookTests
  {

  public static int testCaseId = 1;

    [Fact]
    public void CanABookBeCreated()
    {
      var testHelper = new BookTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      context = testHelper.SetupAndCreateBook(context, testCaseId);
      var dbResult = testHelper.FindBookByISBN(context, testRecord);
      testHelper.DeleteBookByRecord(context, dbResult);

      Assert.Equal(testRecord.ISBN, dbResult.ISBN);
    }

    [Fact]
    public void CanABookBeChanged()
    {
      string titleChange = "The BFG";
      var testHelper = new BookTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      var result = testHelper.DoesBookISBNExist(context, testRecord);
      result.Title = titleChange;
      context.Entry(result).State = EntityState.Modified;
      context.SaveChanges();
      var updatedRecord = testHelper.FindBookByISBN(context, testRecord);
      testHelper.DeleteBookByRecord(context, updatedRecord);

      Assert.Equal(titleChange, updatedRecord.Title);
    }

    [Fact]
    public void CanABookBeRemoved()
    {
      var testHelper = new BookTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      var result = testHelper.FindBookByISBN(context, testRecord);
      if (result != null)
      {
        context.Remove(result);
        context.SaveChanges();
      }
      var resultNotFound = testHelper.FindBookByISBN(context, testRecord);

      Assert.Null(resultNotFound);
    }

    [Fact]
    public void CanABookBeFoundFromISBN()
    {
      var testHelper = new BookTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      var result = testHelper.DoesBookISBNExist(context, testRecord);
      testHelper.DeleteBookByRecord(context, result);

      Assert.NotNull(result);
    }

    [Fact]
    public void CanABookBeFoundFromTitle()
    {
      var testHelper = new BookTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      var result = testHelper.DoesBookTitleExist(context, testRecord);
      testHelper.DeleteBookByRecord(context, result);

      Assert.NotNull(result);
    }

    [Fact]
    public void CanABookBeFoundFromAuthor()
    {
      var testHelper = new BookTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      var result = testHelper.DoesBookAuthorExist(context, testRecord);
      testHelper.DeleteBookByRecord(context, result);

      Assert.NotNull(result);
    }
  }
}
