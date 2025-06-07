using CarDealer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDealer.Application.Interfaces;

public interface ISellerRepository
{
    Task<Seller?> GetByIdAsync(int id);
    Task<List<Seller>> GetAllAsync();
    Task AddAsync(Seller seller);
    Task UpdateAsync(Seller seller);
    Task DeleteAsync(int id);
} 