using CarDealer.Domain;
using CarDealer.Infrastructure;
using HotChocolate;
using HotChocolate.Types;

namespace CarDealer.WebApi.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class TransactionQueries
{
    public IQueryable<Transaction> GetTransactions([Service] CarDealerDbContext context) => context.Transactions;
    
    public Transaction? GetTransaction(int id, [Service] CarDealerDbContext context) => context.Transactions.Find(id);
} 