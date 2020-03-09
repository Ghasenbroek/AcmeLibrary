using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class CheckedOutBook
  {
    [Key]
    public int CheckedOutBookId { get; set; }
    [ForeignKey("Book"), Required]
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    [ForeignKey("User"), Required]
    public int UserID { get; set; }
    public virtual User User { get; set; }
    [Column(TypeName = "datetime"), Required]
    public DateTime CreatedDateTime { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDateTime { get; set; }
    [Column(TypeName = "bit")]
    public bool? Returned { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ExpectedReturnDate
    {
      get
      {
        return this.ExpectedReturnDate;
      }
      set
      {
        if (CreatedDateTime != null)
        {
          this.CreatedDateTime.AddDays(42);
        }
        else
        {
          DateTime.Now.AddDays(42);
        }
      }
    }
    [Column(TypeName = "datetime")]
    public DateTime ReturnedDate { get; set; }
    [Column(TypeName = "nvarchar(200)")]
    public string Quality { get; set; }

    public bool IsBookLate()
    {
      var weekDifference = (CreatedDateTime - ReturnedDate).TotalDays /7;
      return weekDifference > 5;
    }

  }
}
