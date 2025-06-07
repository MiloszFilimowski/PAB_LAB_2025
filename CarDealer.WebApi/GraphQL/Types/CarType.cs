using CarDealer.Domain;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Types;

public class CarType : ObjectType<Car>
{
    protected override void Configure(IObjectTypeDescriptor<Car> descriptor)
    {
        descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
        descriptor.Field(c => c.Brand).Type<NonNullType<StringType>>();
        descriptor.Field(c => c.Model).Type<NonNullType<StringType>>();
        descriptor.Field(c => c.Year).Type<NonNullType<IntType>>();
        descriptor.Field(c => c.Price).Type<NonNullType<FloatType>>();
    }
} 