using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeLibrary.Models;
using AcmeLibrary.Models.DbContext;

namespace AcmeLibrary.UnitTest.Helpers
{
  public class UserTestHelper
  {

    public LibraryDbContext SetupLibraryDbContext()
    {
      return new HelperBase().SetupLibraryDbContext();
    }

    public User FindUserByName(LibraryDbContext context, User user)
    {
      return context.User.FirstOrDefault(u => u.Name == user.Name);
    }

    public User FindUserByIdNumber(LibraryDbContext context, User user)
    {
      return context.User.FirstOrDefault(u => u.IdNumber == user.IdNumber);
    }

    public User DoesUserIdNumberExist(LibraryDbContext context, User testRecord)
    {
      var result = this.FindUserByIdNumber(context, testRecord);
      if (result == null)
      {
        context = this.SetupAndCreateUser(context, testRecord);
      }
      return this.FindUserByIdNumber(context, testRecord);
    }

    public void DeleteUserByRecord(LibraryDbContext context, User user)
    {
      try
      {
        context.User.Remove(user);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public LibraryDbContext SetupAndCreateUser(LibraryDbContext context, User user)
    {
      try
      {
        if (context.User.Any(u => u.IdNumber == user.IdNumber))
        {
          //user already exists
          return context;
        }
        else
        {
          context.User.Add(user);
          context.SaveChanges();
        }

      }
      catch (Exception ex)
      {
        throw ex;
      }
      return context;
    }

    public LibraryDbContext SetupAndCreateUser(LibraryDbContext context, int testCaseId)
    {
      try
      {
        var stubUser = SetupTestUser(testCaseId);
        context.User.Add(stubUser);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return context;
    }

    public User SetupTestUser(int id)
    {
      User user = new User();
      switch (id)
      {
        case 1:
          user = new User()
          {
            UserId = 0,
            UserName = "Test@Email.com",
            Password = "P@ssw0rd",
            Name = "John",
            Surname = "Wick",
            IdNumber = "7005102357102",
            CreatedDateTime = DateTime.Now,
            ModifiedDateTime = DateTime.Now,
            IsActive = true
          };
          break;
        case 2:
          user = new User()
          {
            UserId = 0,
            UserName = "Member@Test.com",
            Password = "Password123",
            Name = "Ryan",
            Surname = "Reynolds",
            IdNumber = "4525746912483",
            CreatedDateTime = DateTime.Now,
            ModifiedDateTime = DateTime.Now,
            IsActive = true
          };
          break;
      }
      return user;
    }
  }
}
