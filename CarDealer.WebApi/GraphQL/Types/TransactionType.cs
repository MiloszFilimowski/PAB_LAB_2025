using CarDealer.Domain;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Types;

public class TransactionType : ObjectType<Transaction>
{
    protected override void Configure(IObjectTypeDescriptor<Transaction> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.CarId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.CustomerId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.SellerId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Date).Type<NonNullType<DateTimeType>>();
        descriptor.Field(t => t.Amount).Type<NonNullType<FloatType>>();
    }
} 