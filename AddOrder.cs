
using System.ComponentModel.DataAnnotations;

namespace probKol2.DTOs;

public class AddOrder
{
    [Required]
    public int employeeId { get; set; }
    [Required]
    public DateTime acceptedAt { get; set; }
    [MaxLength(300)]
    public String comments { get; set; }

    [Required] 
    public IEnumerable<PastryDto2> PastryDto2s { get; set; } = new List<PastryDto2>();
}

public class PastryDto2
{
    [Required]
    public String name { get; set; } = null!;
    [Required]
    [Range(1, int.MaxValue)]
    public int amount { get; set; }
    public String comments { get; set; }
}