using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AcmeLibrary.Models.DbContext
{
  public class LibraryDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
    : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<BookLocation> BookLocations { get; set; }
  }
}

