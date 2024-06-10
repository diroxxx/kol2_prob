using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace probKol2.Models;
[Table("Employee")]
public class Employee
{
    public int ID { get; set; }
    [MaxLength(100)] public String FirstName { get; set; } = String.Empty;
    [MaxLength(100)] public String LastName { get; set; } = String.Empty;

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}