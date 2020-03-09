using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AcmeLibrary.UnitTest.ControllerTests
{
  public class CheckedOutController
  {
    public static int testCaseId = 1;
    //[Fact]
    //public async void CanABookBeCreatedFromController()
    //{
    //  var testHelper = new BookTestHelper();
    //  var context = testHelper.SetupLibraryDbContext();
    //  var testRecord = testHelper.SetupTestBook(testCaseId);
    //  BookController bookController = new BookController(context);
    //  var result = bookController.PostBook(testRecord).Value;
    //  testHelper.DeleteBookByRecord(context, result);
    //  Assert.NotEqual(0, result.BookId);
    //}

    //[Fact]
    //public async void CanABookBeDeletedFromController()
    //{
    //  var testHelper = new BookTestHelper();
    //  var context = testHelper.SetupLibraryDbContext();
    //  var testRecord = testHelper.SetupTestBook(testCaseId);
    //  var result = testHelper.DoesBookISBNExist(context, testRecord);
    //  BookController bookController = new BookController(context);
    //  await bookController.DeleteBook(result.BookId);
    //  var hasRecordBeenRemoved = testHelper.FindBookByISBN(context, result);

    //  Assert.Null(hasRecordBeenRemoved);
    //}

    //[Fact]
    //public async void CanABookBeEditedFromController()
    //{
    //  var titleChange = "Charlie and the Chocolate Factory";
    //  var testHelper = new BookTestHelper();
    //  var context = testHelper.SetupLibraryDbContext();
    //  var testRecord = testHelper.SetupTestBook(testCaseId);
    //  var result = testHelper.DoesBookISBNExist(context, testRecord);
    //  result.Title = titleChange;
    //  BookController bookController = new BookController(context);
    //  await bookController.PutBook(result);
    //  var updatedRecord = testHelper.FindBookByISBN(context, testRecord);
    //  testHelper.DeleteBookByRecord(context, updatedRecord);

    //  Assert.Equal(titleChange, updatedRecord.Title);
    //}
  }
}
