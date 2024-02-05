using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiRiS.Data.Models;

namespace PiRiS.Data.Context.Config;

public class CreditPlanConfig : IEntityTypeConfiguration<CreditPlan>
{
    public void Configure(EntityTypeBuilder<CreditPlan> builder)
    {
        builder.HasOne(x => x.MainAccountPlan).WithMany(x => x.MainCreditPlans).HasForeignKey(x => x.MainAccountPlanId);
        builder.HasOne(x => x.PercentAccountPlan).WithMany(x => x.PercentCreditPlans).HasForeignKey(x => x.PercentAccountPlanId);
    }
}
