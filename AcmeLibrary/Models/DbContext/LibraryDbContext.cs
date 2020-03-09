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
    public DbSet<BookShelf> BookShelf { get; set; }
    public DbSet<CheckedOutBook> CheckedOutBook { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<User> User { get; set; }

  }
}

