using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiRiS.Data.Models;

namespace PiRiS.Data.Context.Config;

public class CreditConfig : IEntityTypeConfiguration<Credit>
{
    public void Configure(EntityTypeBuilder<Credit> builder)
    {
        builder.HasIndex(x => x.CreditNumber).IsUnique();

        builder.HasOne(x => x.MainAccount).WithMany(x => x.MainCredits).HasForeignKey(x => x.MainAccountId);
        builder.HasOne(x=> x.PercentAccount).WithMany(x=> x.PercentCredits).HasForeignKey(x=> x.PercentAccountId);
    }
}
