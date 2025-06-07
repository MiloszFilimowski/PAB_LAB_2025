using CarDealer.Domain;
using CarDealer.Infrastructure;
using HotChocolate;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class CustomerQueries
{
    public IQueryable<Customer> GetCustomers([Service] CarDealerDbContext context) => context.Customers;
    
    public Customer? GetCustomer(int id, [Service] CarDealerDbContext context) => context.Customers.Find(id);
} 