using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common;
using NintendoSwitchDeals.Common.Models;

namespace NintendoSwitchDeals.Web.Pages;

public class IndexModel(DealsContext context) : PageModel
{
    public IList<Deal> Deal { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Deal = await context.Deals.ToListAsync();
    }
}