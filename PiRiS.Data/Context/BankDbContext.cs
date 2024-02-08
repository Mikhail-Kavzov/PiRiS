using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context.Config;
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

    public DbSet<Credit> Credits { get; set; } = null!;

    public DbSet<CreditPlan> CreditPlans { get; set; } = null!;

    public DbSet<Deposit> Deposits { get; set; } = null!;

    public DbSet<DepositPlan> DepositPlans { get; set; } = null!;

    public DbSet<AccountPlan> AccountPlans { get; set; } = null!;

    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Currency> Currencies { get; set; } = null!;

    public DbSet<Transaction> Transactions { get; set; } = null!;

    public DbSet<BankInformation> BankInformation { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClientConfig());
        modelBuilder.ApplyConfiguration(new DepositConfig());
        modelBuilder.ApplyConfiguration(new CreditConfig());
        modelBuilder.ApplyConfiguration(new CreditPlanConfig());
        modelBuilder.ApplyConfiguration(new DepositPlanConfig());
        modelBuilder.ApplyConfiguration(new TransactionConfig());
    }

}
