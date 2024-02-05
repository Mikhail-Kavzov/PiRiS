using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiRiS.Data.Models;

namespace PiRiS.Data.Context.Config;

public class DepositPlanConfig : IEntityTypeConfiguration<DepositPlan>
{
    public void Configure(EntityTypeBuilder<DepositPlan> builder)
    {
        builder.HasOne(x => x.MainAccountPlan).WithMany(x => x.MainDepositPlans).HasForeignKey(x => x.MainAccountPlanId);
        builder.HasOne(x => x.PercentAccountPlan).WithMany(x => x.PercentDepositPlans).HasForeignKey(x => x.PercentAccountPlanId);
    }
}
