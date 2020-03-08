using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AcmeLibrary.UnitTest.EntityTests
{
  public class BookTests
  {

  public static int testCaseId = 1;

    [Fact]
    public void CanABookBeCreated()
    {
      var testHelper = new Helper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      context = testHelper.SetupBooksToInsert(context, testCaseId);
      context.SaveChangesAsync();
      var dbResult = context.Books.FirstOrDefault(b => b.ISBN == testRecord.ISBN);
      Assert.Equal(testRecord, dbResult);

    }

    [Fact]
    public void CanABookBeRemoved()
    {
      var testHelper = new Helper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestBook(testCaseId);
      var result = context.Books.FirstOrDefault(c => c.ISBN == testRecord.ISBN);
      if (result != null)
      {
        context.Remove(result);
        context.SaveChangesAsync();
      }
      var resultNotFound = context.Books.FirstOrDefault(c => c.ISBN == testRecord.ISBN);
      Assert.Null(resultNotFound);
    }
  }
}
