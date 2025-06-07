using CarDealer.Application.Interfaces;
using CarDealer.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Repositories;

public class CarRepository : ICarRepository
{
    private readonly CarDealerDbContext _context;
    public CarRepository(CarDealerDbContext context)
    {
        _context = context;
    }

    public async Task<Car?> GetByIdAsync(int id)
        => await _context.Cars.FindAsync(id);

    public async Task<List<Car>> GetAllAsync()
        => await _context.Cars.ToListAsync();

    public async Task AddAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
} 