using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class Book
  {
    [Key]
    public int BookID { get; set; }
    [Column(TypeName = "nvarchar(250)")]
    public string Title { get; set; }
    [Column(TypeName = "nvarchar(500)")]
    public string Authors { get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public string ISBN { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string Condition { get; set; }
  }
}
