using AcmeLibrary.Models.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeLibrary.UnitTest.Helpers
{
  public class HelperBase
  {
    public LibraryDbContext SetupLibraryDbContext()
    {
      var builder = new DbContextOptionsBuilder<LibraryDbContext>()
        .EnableSensitiveDataLogging()
        .UseSqlServer("Server=DESKTOP-IMJC493\\GHASENBROEK; Database=AcmeLibrary; Trusted_Connection=True;");
      var context = new LibraryDbContext(builder.Options);
      context.Database.OpenConnection();
      context.Database.EnsureCreated();

      return context;
    }
  }
}
