using CarDealer.Domain;
using CarDealer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.WebUi.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly CarDealerDbContext _context;

        public DeleteModel(CarDealerDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            var car = await _context.Cars.FindAsync(Car.Id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
} 