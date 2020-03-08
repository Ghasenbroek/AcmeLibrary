using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class CheckedOut
  {
    [Key]
    public int CheckedOutId { get; set; }
    [ForeignKey("Book")]
    public string BookId { get; set; }
    public virtual Book Book { get; set; }
    [ForeignKey("User")]
    public string UserID { get; set; }
    public virtual User User { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedDateTime { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDateTime { get; set; }
    [Column(TypeName = "bit")]
    public bool Returned { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ReturnedDate { get; set; }
    [Column(TypeName = "nvarchar(200)")]
    public string Quality { get; set; }
  }
}
