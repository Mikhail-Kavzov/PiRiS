using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.Repositories;

public class CreditRepository : BaseRepository, ICreditRepository
{
    public CreditRepository(BankDbContext context) : base(context)
    {
    }

    public void Create(Credit entity)
    {
        _context.Credits.Add(entity);
    }
}
