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

    public DbSet<Report> Reports { get; set; } = null!;

    // TODO: decomposition
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(builder =>
        {
            builder.HasMany(x => x.LedEmployees).WithOne().HasForeignKey("BossId");
            builder.HasMany(x => x.Messages).WithOne().HasForeignKey("EmployeeId");
        });
        modelBuilder.Entity<Report>(builder =>
        {
            builder.HasOne(x => x.Employee);
        });
    }
}