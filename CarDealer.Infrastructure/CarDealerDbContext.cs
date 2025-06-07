using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CarDealer.Domain;

namespace CarDealer.Infrastructure;

public class CarDealerDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options) : base(options) { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
} 