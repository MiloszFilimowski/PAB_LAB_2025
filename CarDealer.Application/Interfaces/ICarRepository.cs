using CarDealer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDealer.Application.Interfaces;

public interface ICarRepository
{
    Task<Car?> GetByIdAsync(int id);
    Task<List<Car>> GetAllAsync();
    Task AddAsync(Car car);
    Task UpdateAsync(Car car);
    Task DeleteAsync(int id);
} 