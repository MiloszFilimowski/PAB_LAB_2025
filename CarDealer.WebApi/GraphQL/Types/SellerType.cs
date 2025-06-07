using CarDealer.Domain;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Types;

public class SellerType : ObjectType<Seller>
{
    protected override void Configure(IObjectTypeDescriptor<Seller> descriptor)
    {
        descriptor.Field(s => s.Id).Type<NonNullType<IdType>>();
        descriptor.Field(s => s.Name).Type<NonNullType<StringType>>();
    }
} 