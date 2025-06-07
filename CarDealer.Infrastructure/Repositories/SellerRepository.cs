using CarDealer.Application.Interfaces;
using CarDealer.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Repositories;

public class SellerRepository : ISellerRepository
{
    private readonly CarDealerDbContext _context;
    public SellerRepository(CarDealerDbContext context)
    {
        _context = context;
    }

    public async Task<Seller?> GetByIdAsync(int id)
        => await _context.Sellers.FindAsync(id);

    public async Task<List<Seller>> GetAllAsync()
        => await _context.Sellers.ToListAsync();

    public async Task AddAsync(Seller seller)
    {
        _context.Sellers.Add(seller);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Seller seller)
    {
        _context.Sellers.Update(seller);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller != null)
        {
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
        }
    }
} 