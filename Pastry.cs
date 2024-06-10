using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;

namespace probKol2.Models;

[Table("Pastry")]
public class Pastry
{
   [Key] 
   public int ID { get; set; }
   [MaxLength(150)] 
   public String Name { get; set; } = String.Empty;
    
    
    [DataType("decimal")]
    [Precision(10,2)]
    public decimal Price { get; set; }

    [MaxLength(40)] 
    public String Type { get; set; } = String.Empty;

    public ICollection<OrderPastry> OrderPastries { get; set; } = new HashSet<OrderPastry>();
}