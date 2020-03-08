using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class Employee
  {
    [Key]
    public int EmployeeID { get; set; }
    [Column(TypeName = "nvarchar(250)")]
    public string FirstName { get; set; }
    [Column(TypeName = "nvarchar(250)")]
    public string Surname { get; set; }
    [ForeignKey("User")]
    public string UserID { get; set; }
    public virtual User User { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedDateTime { get; set; }
  }
}
