using AcmeLibrary.UnitTest.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AcmeLibrary.UnitTest.EntityTests
{
  public class CheckedOutBookTests
  {
    public static int testCaseId = 1;
    [Fact]
    public void CanABookBeCreated()
    {
      var testHelper = new CheckedOutTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var book = testHelper.SetupBookRecord(context, testCaseId);
      var user = testHelper.SetupUserRecord(context, testCaseId);
      var checkedOutBook = testHelper.SetupTestCheckedOutBook(testCaseId, book.BookId, user.UserId);
      context = testHelper.SetupAndCreateCheckedOutBook(context, checkedOutBook);
      var dbResult = testHelper.FindCheckedOutBookByBookId(context, checkedOutBook);
      testHelper.DeleteCheckedOutBookByRecord(context, dbResult);

      Assert.NotNull(dbResult);
    }
  }
}
