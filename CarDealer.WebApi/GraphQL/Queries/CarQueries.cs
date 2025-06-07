using CarDealer.Domain;
using CarDealer.Infrastructure;
using HotChocolate;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class CarQueries
{
    public IQueryable<Car> GetCars([Service] CarDealerDbContext context) => context.Cars;
    
    public Car? GetCar(int id, [Service] CarDealerDbContext context) => context.Cars.Find(id);
} 