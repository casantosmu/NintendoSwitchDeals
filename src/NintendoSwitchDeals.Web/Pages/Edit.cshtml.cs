using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common;
using NintendoSwitchDeals.Common.Models;

namespace NintendoSwitchDeals.Web.Pages;

public class EditModel(DealsContext context) : PageModel
{
    [BindProperty] public Deal Deal { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Deal? deal = await context.Deals.FirstOrDefaultAsync(m => m.DealId == id);
        if (deal == null)
        {
            return NotFound();
        }

        Deal = deal;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
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

        context.Attach(Deal).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DealExists(Deal.DealId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool DealExists(int id)
    {
        return context.Deals.Any(e => e.DealId == id);
    }
}