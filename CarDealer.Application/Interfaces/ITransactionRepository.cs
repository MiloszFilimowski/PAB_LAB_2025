using CarDealer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDealer.Application.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(int id);
    Task<List<Transaction>> GetAllAsync();
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(int id);
} 