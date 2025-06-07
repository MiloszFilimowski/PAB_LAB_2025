using CarDealer.Domain;

namespace CarDealer.Domain;

public class Car
{
    public int? Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Year { get; set; }
    public int SellerId { get; set; }
    public Seller? Seller { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
} 