using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace probKol2.Models;
[Table("Order")]
public class Order
{
    [Key]
    public int ID { get; set; }
    public DateTime AcceptedAt { get; set; }
    public DateTime? FuffilledAt { get; set; }
    [MaxLength(300)]
    public String? Comments { get; set; }
    public int ClientId { get; set; }
     public int EmployeeID { get; set; }

     [ForeignKey(nameof(ClientId))] 
     public Client Client { get; set; } = null!;
     [ForeignKey(nameof(EmployeeID))] 
     public Employee Employee { get; set; } = null!;

     public ICollection<OrderPastry> OrderPastries { get; set; } = new List<OrderPastry>();




}