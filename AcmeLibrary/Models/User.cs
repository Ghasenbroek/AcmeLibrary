using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string UserName { get; set; }
    [Column(TypeName = "varbinary(max)")]
    public string Password { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string Name { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string Surname { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string IdNumber { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedDateTime { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDateTime { get; set; }
    [Column(TypeName = "bit")]
    public bool IsActive { get; set; }
    public ICollection<Member> Members { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<CheckedOutBook> CheckedOutBooks { get; set; }
  }
}
