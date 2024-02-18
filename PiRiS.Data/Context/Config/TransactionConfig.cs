using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiRiS.Data.Models;

namespace PiRiS.Data.Context.Config;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne(x => x.CreditAccount).WithMany(x => x.CreditTransactions).HasForeignKey(x => x.CreditAccountId);
        builder.HasOne(x=> x.DebitAccount).WithMany(x=> x.DebitTransactions).HasForeignKey(x=> x.DebitAccountId);
    }
}
