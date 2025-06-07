using CarDealer.Domain;
using CarDealer.Infrastructure;
using HotChocolate;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class SellerQueries
{
    public IQueryable<Seller> GetSellers([Service] CarDealerDbContext context) => context.Sellers;
    
    public Seller? GetSeller(int id, [Service] CarDealerDbContext context) => context.Sellers.Find(id);
} 