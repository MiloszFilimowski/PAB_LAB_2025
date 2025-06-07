using CarDealer.Domain;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Types;

public class CustomerType : ObjectType<Customer>
{
    protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
    {
        descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
        descriptor.Field(c => c.Name).Type<NonNullType<StringType>>();
        descriptor.Field(c => c.Email).Type<NonNullType<StringType>>();
    }
} 