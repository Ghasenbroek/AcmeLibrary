using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class BookShelf
  {
    [Key]
    public int BookShelfId { get; set; }
    [Column(TypeName = "nvarchar(25)")]
    public string RowNumber { get; set; }
    [Column(TypeName = "int")]
    public int ShelfNumber { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string SubSection { get; set; }
  }
}
