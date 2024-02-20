using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common;
using NintendoSwitchDeals.Common.Models;

namespace NintendoSwitchDeals.Web.Pages;

public class DetailsModel(DealsContext context) : PageModel
{
    public Deal Deal { get; set; } = default!;

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
}