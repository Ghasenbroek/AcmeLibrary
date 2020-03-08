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
    [ForeignKey("Book")]
    public string BookId { get; set; }
    public virtual Book Book { get; set; }
    [ForeignKey("BookShelf")]
    public string BookShelfId { get; set; }
    public virtual BookShelf BookShelf { get; set; }
  }
}
