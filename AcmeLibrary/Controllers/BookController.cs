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

    //// GET: api/Book/5
    //[HttpGet("{id}", Name = "Get")]
    //public string Get(int id)
    //{
    //  return "value";
    //}


    [HttpGet("/BookSearch")]
    public ActionResult<List<Book>> GetBooks(int id, string title)
    {
      return id != 0 ? GetBooksByID(id) : GetBooksByName(title);
    }

    private ActionResult<List<Book>> GetBooksByID(int id)
    {
      var dCandidate = _context.Books.Where(c => c.BookID == id).ToList();

      if (dCandidate == null)
      {
        return NotFound();
      }
      return dCandidate;
    }

    private ActionResult<List<Book>> GetBooksByName(string title)
    {
      var resultList = _context.Books.Where(c => c.Title == title).ToList();

      if (resultList == null)
      {
        return NotFound();
      }
      return resultList;
    }

    // POST: api/Book
    [HttpPost]
    public async Task<ActionResult<Book>> PostDCandidate(Book book)
    {
      try
      {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
      }
      catch (Exception e)
      {
        throw e;
      }
      return book;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> PutBook(int id, Book book)
    {
      book.BookID = id;

      _context.Entry(book).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BookExists(id))
        {
          return NotFound();
        }
        throw;
      }
      var result = await _context.Books.FindAsync(id);
      return new ActionResult<Book>(result);
    }

    private bool BookExists(int id)
    {
      var searchResult = GetBooksByID(id);
      return searchResult.Value.Any();
    }

    private bool BookExists(int id, string search)
    {
      var searchResult = id != 0 ? GetBooksByID(id) : GetBooksByName(search);
      return searchResult.Value.Any();
    }

    [HttpDelete]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
      var book = await _context.Books.FindAsync(id);
      if (book == null)
      {
        return NotFound();
      }
      try
      {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return book;
    }
  }
}
