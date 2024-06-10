using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace probKol2.Models;

[Table("Client")]
public class Client
{
    [Key]
    public int ID { get; set; }

    [MaxLength(100)] public String FirstName { get; set; } = String.Empty;

    [MaxLength(120)] public String LastName { get; set; } = String.Empty;

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}