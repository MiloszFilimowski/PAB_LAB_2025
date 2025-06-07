using CarDealer.Application.Interfaces;
using CarDealer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarDealer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CarsController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    public CarsController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Car>>> GetAll()
    {
        var cars = await _carRepository.GetAllAsync();
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetById(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
            return NotFound();
        return Ok(car);
    }

    [HttpPost]
    public async Task<ActionResult<Car>> Create(Car car)
    {
        await _carRepository.AddAsync(car);
        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Car car)
    {
        if (id != car.Id)
            return BadRequest();
        await _carRepository.UpdateAsync(car);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _carRepository.DeleteAsync(id);
        return NoContent();
    }
} 