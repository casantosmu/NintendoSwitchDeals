using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common;
using NintendoSwitchDeals.Common.Models;

namespace NintendoSwitchDeals.Web.Pages;

public class CreateModel(DealsContext context) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty] public Deal Deal { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (await context.Deals.AnyAsync(deal => deal.NintendoId == Deal.NintendoId))
        {
            ModelState.AddModelError("Deal.NintendoId",
                $"This nintendo ID already exists.");
            return Page();
        }

        context.Deals.Add(Deal);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}