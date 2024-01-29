using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Models
{
  public class InventoryTrackerContext : DbContext
  {
    public DbSet<Game> Games { get; set; }

    public InventoryTrackerContext(DbContextOptions options) : base(options) { }
  }
}