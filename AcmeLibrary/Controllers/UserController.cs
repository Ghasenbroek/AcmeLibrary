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
    public class UserController : ControllerBase
    {
    private readonly LibraryDbContext _context;
    public UserController(LibraryDbContext context)
    {
      _context = context;
    }

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
      return await _context.User.ToListAsync();
    }

    [HttpGet("/UserSearch")]
    public ActionResult<User> GetUsers(int id, string searchQuery)
    {
      return id != 0 ? GetUserByID(id) : GetUserByIdNumber(searchQuery);
    }

    private ActionResult<User> GetUserByID(int id)
    {
      _context.User.Load();
      var user = _context.User.FirstOrDefault(c => c.UserId == id);

      if (user == null)
      {
        return NotFound();
      }
      return user;
    }

    private ActionResult<User> GetUserByIdNumber(string searchQuery)
    {
      _context.User.Load();
      var result = _context.User.FirstOrDefault(c => c.IdNumber == searchQuery);

      if (result == null)
      {
        return NotFound();
      }
      return result;
    }

    // POST: api/User
    [HttpPost]
    public ActionResult<User> PostUser(User user)
    {
      try
      {
        _context.User.Load();
        var duplicateUser = _context.User.Where(u => u.IdNumber == user.IdNumber).Any();
        if (duplicateUser)
        {
          return BadRequest();
        }
        _context.User.Add(user);
        _context.SaveChanges();
      }
      catch (Exception e)
      {
        throw e;
      }
      return user;
    }

    [HttpPut]
    public async Task<ActionResult<User>> PutUser(User user)
    {
      _context.User.Load();
      var currentUser = (UserExists(user.IdNumber) || UserExists(user.UserId));
      if (currentUser == false)
      {
        return NotFound();
      }
      try
      {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }
      var result = await _context.User.FindAsync(user.UserId);
      return result;
    }

    private bool UserExists(int id)
    {
      var searchResult = GetUserByID(id);
      return searchResult.Value != null;
    }

    private bool UserExists(string IdNumber)
    {
      var searchResult = GetUserByIdNumber(IdNumber);
      return searchResult.Value != null;
    }

    [HttpDelete]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
      var user = await _context.User.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      try
      {
        _context.User.Remove(user);
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return user;
    }
  }
}
