namespace CarDealer.Domain;

public class Transaction
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public required Car Car { get; set; }
    public int CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public int SellerId { get; set; }
    public required Seller Seller { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
} 