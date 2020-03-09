using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeLibrary.Models;
using AcmeLibrary.Models.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcmeLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckedOutBookController : ControllerBase
    {
    private readonly LibraryDbContext _context;
    public CheckedOutBookController(LibraryDbContext context)
    {
      _context = context;
    }

    // GET: api/CheckedOutBook
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CheckedOutBook>>> GetCheckedOutBook()
    {
      return await _context.CheckedOutBook.ToListAsync();
    }

    [HttpGet("/CheckedOutBookSearch")]
    public ActionResult<List<CheckedOutBook>> GetCheckedOut(int id, CheckedOutBook checkedOutBook)
    {
      return id != 0 ? GetCheckedOutBooksByID(id) : GetCheckedOutBooksByBookId(checkedOutBook);
    }

    private ActionResult<List<CheckedOutBook>> GetCheckedOutBooksByID(int id)
    {
      _context.CheckedOutBook.Load();
      var checkedOutBook = _context.CheckedOutBook.Local.Where(c => c.CheckedOutBookId == id).ToList();

      if (checkedOutBook == null)
      {
        return NotFound();
      }
      return checkedOutBook;
    }

    private ActionResult<List<CheckedOutBook>> GetCheckedOutBooksByBookId(CheckedOutBook checkedOutBook)
    {
      _context.CheckedOutBook.Load();
      var resultList = _context.CheckedOutBook.Where(c => c.BookId == checkedOutBook.BookId).ToList();

      if (resultList == null)
      {
        return NotFound();
      }
      return resultList;
    }

    private ActionResult<List<CheckedOutBook>> GetCheckedOutBookByUserId(CheckedOutBook checkedOutBook)
    {
      _context.CheckedOutBook.Load();
      var resultList = _context.CheckedOutBook.Where(c => c.UserID == checkedOutBook.UserID).ToList();

      if (resultList == null)
      {
        return NotFound();
      }
      return resultList;
    }

    // POST: api/CheckedOutBook
    [HttpPost]
    public ActionResult<CheckedOutBook> PostCheckedOutBook(CheckedOutBook checkedOutBook)
    {
      try
      {
        _context.CheckedOutBook.Load();
        _context.CheckedOutBook.Add(checkedOutBook);
        _context.SaveChanges();
      }
      catch (Exception e)
      {
        throw e;
      }
      return checkedOutBook;
    }

    [HttpPut]
    public async Task<ActionResult<CheckedOutBook>> PutCheckedOutBook(CheckedOutBook checkedOutBook)
    {
      _context.CheckedOutBook.Load();
      var currentCheckedOutBook = (CheckedOutBookExists(checkedOutBook) || CheckedOutBookExists(checkedOutBook.CheckedOutBookId));
      if (currentCheckedOutBook == false)
      {
        return NotFound();
      }
      try
      {
        _context.Entry(checkedOutBook).State = EntityState.Modified;
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }
      var result = await _context.CheckedOutBook.FindAsync(checkedOutBook.CheckedOutBookId);
      return result;
    }

    private bool CheckedOutBookExists(int id)
    {
      var searchResult = GetCheckedOutBooksByID(id);
      return searchResult.Value.Any();
    }

    private bool CheckedOutBookExists(CheckedOutBook checkedOutBook)
    {
      var searchResult = GetCheckedOutBookByUserId(checkedOutBook);
      return searchResult.Value.Any();
    }

    [HttpDelete]
    public async Task<ActionResult<CheckedOutBook>> DeleteCheckedOutBook(int id)
    {
      _context.CheckedOutBook.Load();
      var checkedOutBook = await _context.CheckedOutBook.FindAsync(id);
      if (checkedOutBook == null)
      {
        return NotFound();
      }
      if (checkedOutBook.Returned == null || checkedOutBook.Returned == false)
      {
        //Book has not been returned yet
        return BadRequest();
      }
      try
      {
        _context.CheckedOutBook.Remove(checkedOutBook);
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return checkedOutBook;
    }
  }
}
