using CarDealer.Application.Interfaces;
using CarDealer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarDealer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SellersController : ControllerBase
{
    private readonly ISellerRepository _sellerRepository;

    public SellersController(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Seller>>> GetAll()
    {
        var sellers = await _sellerRepository.GetAllAsync();
        return Ok(sellers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Seller>> GetById(int id)
    {
        var seller = await _sellerRepository.GetByIdAsync(id);
        if (seller == null)
            return NotFound();
        return Ok(seller);
    }

    [HttpPost]
    public async Task<ActionResult<Seller>> Create(Seller seller)
    {
        await _sellerRepository.AddAsync(seller);
        return CreatedAtAction(nameof(GetById), new { id = seller.Id }, seller);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Seller seller)
    {
        if (id != seller.Id)
            return BadRequest();
        await _sellerRepository.UpdateAsync(seller);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sellerRepository.DeleteAsync(id);
        return NoContent();
    }
} 