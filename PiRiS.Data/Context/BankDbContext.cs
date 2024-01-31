using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Models;

namespace PiRiS.Data.Context;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Client> Clients { get; set; } = null!;

    public DbSet<FamilyStatus> FamilyStatuses { get; set; } = null!;

    public DbSet<Citizenship> Citizenships { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<Disability> Disabilities { get; set; } = null!;
    
}
