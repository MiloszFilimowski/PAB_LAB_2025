using CarDealer.Application.Interfaces;
using CarDealer.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly CarDealerDbContext _context;
    public TransactionRepository(CarDealerDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(int id)
        => await _context.Transactions.FindAsync(id);

    public async Task<List<Transaction>> GetAllAsync()
        => await _context.Transactions.ToListAsync();

    public async Task AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
} 