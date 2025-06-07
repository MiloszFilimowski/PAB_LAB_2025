using CarDealer.Domain;
using CarDealer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.WebUi.Pages.Cars
{
    public class DetailsModel : PageModel
    {
        private readonly CarDealerDbContext _context;

        public DetailsModel(CarDealerDbContext context)
        {
            _context = context;
        }

        public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            Car = car;
            return Page();
        }
    }
} 