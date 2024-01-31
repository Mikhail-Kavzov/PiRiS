using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiRiS.Data.Models;

namespace PiRiS.Data.Context.Config;

public class ClientConfig : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x=> x.MobilePhone).IsUnique();
        builder.HasIndex(x => x.HomePhone);
        builder.HasIndex(x=> new {x.Surname, x.FirstName, x.LastName}).IsUnique();
        builder.HasIndex(x=> x.IdentificationNumber).IsUnique();
        builder.HasIndex(x => new { x.PassportNumber, x.PassportSeries }).IsUnique();
    }
}
