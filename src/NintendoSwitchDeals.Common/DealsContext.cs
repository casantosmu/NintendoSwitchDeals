using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common.Models;

namespace NintendoSwitchDeals.Common;

public class DealsContext : DbContext
{
    public DbSet<Deal> Deals { get; set; } = null!;

    private string DbPath { get; }

    public DealsContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "nintendo_switch_deals.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}