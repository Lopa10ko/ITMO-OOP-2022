using DataAccessTier.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTier;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    // TODO: decomposition need
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(builder =>
        {
            builder.HasMany(x => x.LedEmployees).WithOne().HasForeignKey("BossId");
            builder.HasMany(x => x.Messages).WithOne().HasForeignKey("EmployeeId");
        });
    }
}