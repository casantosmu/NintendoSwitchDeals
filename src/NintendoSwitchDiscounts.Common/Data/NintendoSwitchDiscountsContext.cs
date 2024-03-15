using Microsoft.EntityFrameworkCore;

using NintendoSwitchDiscounts.Common.Models;

namespace NintendoSwitchDiscounts.Common.Data;

public class NintendoSwitchDiscountsContext : DbContext
{
    public NintendoSwitchDiscountsContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "nintendo_switch_discounts.db");
    }

    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;

    private string DbPath { get; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}