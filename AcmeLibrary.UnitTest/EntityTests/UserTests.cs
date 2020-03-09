using AcmeLibrary.UnitTest.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AcmeLibrary.UnitTest.EntityTests
{
  public class UserTests
  {
    public static int testCaseId = 1;

    [Fact]
    public void CanAUserBeCreated()
    {
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      context = testHelper.SetupAndCreateUser(context, testCaseId);
      var dbResult = testHelper.FindUserByIdNumber(context, testRecord);
      testHelper.DeleteUserByRecord(context, dbResult);

      Assert.Equal(testRecord.IdNumber, dbResult.IdNumber);
    }

    [Fact]
    public void CanAUserBeChanged()
    {
      string name = "Dwayne";
      string surname = "Johnson";
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      var result = testHelper.FindUserByIdNumber(context, testRecord);
      if (result == null)
      {
        context = testHelper.SetupAndCreateUser(context, testCaseId);
        result = testHelper.FindUserByIdNumber(context, testRecord);
      }
      result.Name = name;
      result.Surname = surname;
      context.Entry(result).State = EntityState.Modified;
      context.SaveChanges();
      var updatedRecord = testHelper.FindUserByIdNumber(context, testRecord);
      testHelper.DeleteUserByRecord(context, updatedRecord);

      Assert.Equal(name, updatedRecord.Name);
    }

    [Fact]
    public void CanAUserBeRemoved()
    {
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      var result = testHelper.DoesUserIdNumberExist(context, testRecord);
      context.Remove(result);
      context.SaveChanges();
      var resultNotFound = testHelper.FindUserByIdNumber(context, testRecord);

      Assert.Null(resultNotFound);
    }

    [Fact]
    public void CanAUserBeFoundFromName()
    {
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      var result = testHelper.DoesUserIdNumberExist(context, testRecord);

      Assert.NotNull(result);
    }

    [Fact]
    public void CanAUserBeFoundFromIdNumber()
    {
      var testHelper = new UserTestHelper();
      var context = testHelper.SetupLibraryDbContext();
      var testRecord = testHelper.SetupTestUser(testCaseId);
      var result = testHelper.FindUserByIdNumber(context, testRecord);
      if (result == null)
      {
        context = testHelper.SetupAndCreateUser(context, testCaseId);
      }
      result = testHelper.FindUserByIdNumber(context, testRecord);

      Assert.NotNull(result);
    }


  }
}
