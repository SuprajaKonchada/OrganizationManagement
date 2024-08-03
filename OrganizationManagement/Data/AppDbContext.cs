using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Models;

namespace OrganizationManagement.Data;

/// <summary>
/// Represents the database context for managing employees.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
}
