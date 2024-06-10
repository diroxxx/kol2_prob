namespace probKol2.DTOs;

public class OrderDto
{
    public int ID { get; set; }
    public DateTime acceptedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    public String? comments { get; set; }
    public ICollection<PastryDto> Pastrys { get; set; } = null!;

}

public class PastryDto
{
    public String name { get; set; } = null!;
    public decimal price { get; set; }
    public int amount { get; set; }
}

