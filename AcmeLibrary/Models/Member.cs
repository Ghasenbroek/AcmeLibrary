using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class Member
  {
    [Key]
    public int MemberId { get; set; }
    [Column(TypeName = "nvarchar(250)")]
    public string Name { get; set; }
    [Column(TypeName = "nvarchar(250)")]
    public string Surname { get; set; }
    [Column(TypeName = "nvarchar(13)")]
    public string IdNumber { get; set; }
    [ForeignKey("User")]
    public string UserID { get; set; }
    public virtual User User { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedDateTime { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDateTime { get; set; }
    [Column(TypeName = "decimal")]
    public decimal OutstandingFineBalance { get; set; }

  }
}
