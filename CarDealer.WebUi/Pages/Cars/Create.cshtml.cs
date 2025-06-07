using CarDealer.Domain;
using CarDealer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealer.WebUi.Pages.Cars;

public class CreateModel : PageModel
{
    private readonly CarDealerDbContext _context;

    public CreateModel(CarDealerDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Car Car { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Cars.Add(Car);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
} 