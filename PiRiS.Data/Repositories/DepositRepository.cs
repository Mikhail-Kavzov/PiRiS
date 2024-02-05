using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.Repositories;

public class DepositRepository : BaseRepository, IDepositRepository
{
    public DepositRepository(BankDbContext context) : base(context)
    {
    }

    public void Create(Deposit entity)
    {
        _context.Deposits.Add(entity);
    }
}
