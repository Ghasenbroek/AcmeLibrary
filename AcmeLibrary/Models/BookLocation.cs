using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class BookLocation
  {
    [Key]
    public int BookLocationId { get; set; }
    [ForeignKey("Book"), Required]
    public int BookId { get; set; }
    public Book Book { get; set; }
    [ForeignKey("BookShelfId"),Required]
    public int BookShelfId { get; set; }
    public virtual BookShelf BookShelf { get; set; }
    public ICollection<Book> Books { get; set; }
  }
}
