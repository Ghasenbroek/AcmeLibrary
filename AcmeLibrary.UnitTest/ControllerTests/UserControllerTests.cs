using AcmeLibrary.Controllers;
using AcmeLibrary.UnitTest.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AcmeLibrary.UnitTest.ControllerTests
{
  public class UserControllerTests
  {
    public static int testCaseId = 1;
    [Fact]
    public async void CanAUserBeCreatedFromController()
    {
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      UserController bookController = new UserController(context);
      var result = bookController.PostUser(testRecord).Value;
      testHelper.DeleteUserByRecord(context, result);
      Assert.NotEqual(0,result.UserId);
    }

    [Fact]
    public async void CanAUserBeDeletedFromController()
    {
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      var result = testHelper.DoesUserIdNumberExist(context, testRecord);
      UserController bookController = new UserController(context);
      await bookController.DeleteUser(result.UserId);
      var hasRecordBeenRemoved = testHelper.FindUserByIdNumber(context, result);

      Assert.Null(hasRecordBeenRemoved);
    }

    [Fact]
    public async void CanAUserBeEditedFromController()
    {
      var name = "Freddy";
      var surname = "Mercury";
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      var result = testHelper.DoesUserIdNumberExist(context, testRecord);
      result.Name = name;
      result.Surname = surname;
      UserController bookController = new UserController(context);
      await bookController.PutUser(result);
      var updatedRecord = testHelper.FindUserByIdNumber(context, testRecord);
      testHelper.DeleteUserByRecord(context, updatedRecord);

      Assert.True((name == updatedRecord.Name && surname ==updatedRecord.Surname));
    }
  }
}
