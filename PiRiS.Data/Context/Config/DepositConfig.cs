using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiRiS.Data.Models;

namespace PiRiS.Data.Context.Config;

public class DepositConfig : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder)
    {
        builder.HasIndex(x => x.DepositNumber).IsUnique();

        builder.HasOne(x => x.MainAccount).WithMany(x => x.MainDeposits).HasForeignKey(x => x.MainAccountId);
        builder.HasOne(x => x.PercentAccount).WithMany(x => x.PercentDeposits).HasForeignKey(x => x.PercentAccountId);
    }
}
