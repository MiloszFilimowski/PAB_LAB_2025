using CarDealer.Domain;

namespace CarDealer.Infrastructure;

public static class DbInitializer
{
    public static void Initialize(CarDealerDbContext context)
    {
        context.Database.EnsureCreated();

        // Seed Cars
        if (!context.Cars.Any())
        {
            context.Cars.AddRange(
                new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000 },
                new Car { Id = 2, Brand = "Honda", Model = "Civic", Year = 2019, Price = 18000 },
                new Car { Id = 3, Brand = "Ford", Model = "Focus", Year = 2021, Price = 22000 }
            );
        }

        // Seed Customers
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(
                new Customer { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
                new Customer { Id = 3, Name = "Bob Johnson", Email = "bob@example.com" }
            );
        }

        // Seed Sellers
        if (!context.Sellers.Any())
        {
            context.Sellers.AddRange(
                new Seller { Id = 1, Name = "Auto Sales" },
                new Seller { Id = 2, Name = "Car Dealership" },
                new Seller { Id = 3, Name = "Vehicle World" }
            );
        }

        // Seed Transactions
        if (!context.Transactions.Any())
        {
            var car1 = context.Cars.Find(1);
            var car2 = context.Cars.Find(2);
            var car3 = context.Cars.Find(3);
            var customer1 = context.Customers.Find(1);
            var customer2 = context.Customers.Find(2);
            var customer3 = context.Customers.Find(3);
            var seller1 = context.Sellers.Find(1);
            var seller2 = context.Sellers.Find(2);
            var seller3 = context.Sellers.Find(3);

            if (car1 != null && customer1 != null && seller1 != null)
            {
                context.Transactions.Add(new Transaction { Id = 1, CarId = 1, Car = car1, CustomerId = 1, Customer = customer1, SellerId = 1, Seller = seller1, Date = DateTime.Now, Amount = 20000 });
            }

            if (car2 != null && customer2 != null && seller2 != null)
            {
                context.Transactions.Add(new Transaction { Id = 2, CarId = 2, Car = car2, CustomerId = 2, Customer = customer2, SellerId = 2, Seller = seller2, Date = DateTime.Now, Amount = 18000 });
            }

            if (car3 != null && customer3 != null && seller3 != null)
            {
                context.Transactions.Add(new Transaction { Id = 3, CarId = 3, Car = car3, CustomerId = 3, Customer = customer3, SellerId = 3, Seller = seller3, Date = DateTime.Now, Amount = 22000 });
            }
        }

        context.SaveChanges();
    }
} 