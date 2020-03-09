using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AcmeLibrary.Models.DbContext;
using AcmeLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeLibrary.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BookController : ControllerBase
  {
    private readonly LibraryDbContext _context;
    public BookController(LibraryDbContext context)
    {
      _context = context;
    }

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
      return await _context.Books.ToListAsync();
    }

    [HttpGet("/BookSearch")]
    public ActionResult<List<Book>> GetBooks(int id, string searchQuery)
    {
      return id != 0 ? GetBooksByID(id) : GetBooksByName(searchQuery);
    }

    private ActionResult<List<Book>> GetBooksByID(int id)
    {
      _context.Books.Load();
      var books = _context.Books.Local.Where(c => c.BookId == id).ToList();

      if (books == null)
      {
        return NotFound();
      }
      return books;
    }

    private ActionResult<List<Book>> GetBooksByName(string searchQuery)
    {
      _context.Books.Load();
      var resultList = _context.Books.Where(c => c.Title.Contains(searchQuery) || c.Authors.Contains(searchQuery)).ToList();

      if (resultList == null)
      {
        return NotFound();
      }
      return resultList;
    }

    [HttpGet("/BookFind")]
    public ActionResult<List<Structures.BookResult>> GetBooksByFind(Book book)
    {
      _context.Books.Load();
      var resultList = _context.Books.Where(c => c.Title.Contains(book.Title)
                                            || c.Authors.Contains(book.Authors)
                                            || c.ISBN.Contains(book.ISBN)).ToList();
      List<Structures.BookResult> bookResults = new List<Structures.BookResult>();
      int counter = 0;
      foreach (var item in resultList)
      {
        var checkedOutBook = _context.CheckedOutBook.FirstOrDefault(c => c.BookId == item.BookId);
        var location = _context.BookLocations.FirstOrDefault(c => c.BookId == item.BookId);
        var bookShelf = _context.BookShelf.FirstOrDefault(bs => bs.BookShelfId == location.BookShelfId);
        bookResults.Add(new Structures.BookResult
        {
          BookResultId = counter,
          BookId = item.BookId,
          Authors = item.Authors,
          Title = item.Title,
          ISBN = item.ISBN,
          IsCheckedOut = checkedOutBook.Returned,
          AvailableDate = checkedOutBook.ExpectedReturnDate,
          BookShelfRow = bookShelf.RowNumber,
          BookShelfNumber = bookShelf.ShelfNumber,
          BookShelfSubSection = bookShelf.SubSection
        });
        counter++;
      }

      if (resultList == null)
      {
        return NotFound();
      }
      return bookResults;
    }

    private ActionResult<List<Book>> GetBooksByObject(Book book)
    {
      _context.Books.Load();
      var resultList = _context.Books.Where(c => c.Title.Contains(book.Title) 
                                            || c.Authors.Contains(book.Authors)
                                            || c.ISBN.Contains(book.ISBN)).ToList();
      List<CheckedOutBook> checkedOut = new List<CheckedOutBook>();
      List<BookLocation> bookLocations = new List<BookLocation>();
      List<BookShelf> bookShelves = new List<BookShelf>();
      List<Structures.BookResult> bookResults = new List<Structures.BookResult>();
      int counter = 0;
      foreach (var item in resultList)
      {
        var checkedOutBook = _context.CheckedOutBook.FirstOrDefault(c => c.BookId == item.BookId);
        checkedOut.Add(checkedOutBook);
        var location = _context.BookLocations.FirstOrDefault(c => c.BookId == item.BookId);
        bookLocations.Add(location);
        var bookShelf = _context.BookShelf.FirstOrDefault(bs => bs.BookShelfId == location.BookShelfId);
        bookShelves.Add(bookShelf);
        bookResults.Add(new Structures.BookResult
        {
          BookResultId = counter,
          BookId = item.BookId,
          Authors = item.Authors,
          Title = item.Title,
          ISBN = item.ISBN,
          IsCheckedOut = checkedOutBook.Returned,
          AvailableDate = checkedOutBook.ExpectedReturnDate,
          BookShelfRow = bookShelf.RowNumber,
          BookShelfNumber = bookShelf.ShelfNumber,
          BookShelfSubSection = bookShelf.SubSection
        });
        counter++;
      }
      if (checkedOut != null)

      if (resultList == null)
      {
        return NotFound();
      }
      return resultList;
    }

    // POST: api/Book
    [HttpPost]
    public ActionResult<Book> PostBook(Book book)
    {
      try
      {
        _context.Books.Load();
        _context.Books.Add(book);
        _context.SaveChanges();
      }
      catch (Exception e)
      {
        throw e;
      }
      return book;
    }

    [HttpPut]
    public async Task<ActionResult<Book>> PutBook(Book book)
    {
      _context.Books.Load();
      var currentUser = (BookExists(book) || BookExists(book.BookId));
      if (currentUser == false)
      {
        return NotFound();
      }
      try
      {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }
      var result = await _context.Books.FindAsync(book.BookId);
      return result;
    }

    private bool BookExists(int id)
    {
      var searchResult = GetBooksByID(id);
      return searchResult.Value.Any();
    }

    private bool BookExists(Book book)
    {
      var searchResult = GetBooksByObject(book);
      return searchResult.Value.Any();
    }

    [HttpDelete]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
      _context.Books.Load();
      var book = await _context.Books.FindAsync(id);
      if (book == null)
      {
        return NotFound();
      }
      try
      {
        _context.Books.Remove(book);
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return book;
    }
  }
}
