using CarDealer.Domain;

namespace CarDealer.Domain;

public class Seller
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<Car> Cars { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
} 