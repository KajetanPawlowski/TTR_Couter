using Microsoft.EntityFrameworkCore;
using TTR_Counter_Domain.Model;

namespace TTR_Counter_Domain.Services;

public class GameService :DbContext
{
    public DbSet<Game> Games { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../TTR_Counter_Domain/ttr_counter.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
    }
}