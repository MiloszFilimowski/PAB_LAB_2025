using CarDealer.Domain;
using CarDealer.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.WebUi.Pages.Cars;

public class IndexModel : PageModel
{
    private readonly CarDealerDbContext _context;

    public IndexModel(CarDealerDbContext context)
    {
        _context = context;
    }

    public IList<Car> Cars { get; set; } = new List<Car>();

    public async Task OnGetAsync()
    {
        Cars = await _context.Cars.ToListAsync();
    }
} 