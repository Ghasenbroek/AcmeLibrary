using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeLibrary.Structures
{
  public class BookResult
  {
    public int BookResultId { get; set; }
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public string ISBN { get; set; }
    public bool? IsCheckedOut { get; set; }
    public DateTime AvailableDate { get; set; }
    public string BookShelfRow { get; set; }
    public int BookShelfNumber { get; set; }
    public string BookShelfSubSection { get; set; }
  }
}
