using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common;
using NintendoSwitchDeals.Common.Models;

namespace NintendoSwitchDeals.Web.Pages;

public class DeleteModel(DealsContext context) : PageModel
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
        else
        {
            Deal = deal;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Deal? deal = await context.Deals.FindAsync(id);
        if (deal != null)
        {
            Deal = deal;
            context.Deals.Remove(Deal);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}