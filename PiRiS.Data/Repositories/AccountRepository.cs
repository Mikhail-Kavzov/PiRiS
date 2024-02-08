using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<Account?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x=> x.AccountId == id);
    }

    public async Task<Account?> GetEntityAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _context.Accounts.FirstOrDefaultAsync(predicate);
    }

    public void Update(Account entity)
    {
        _context.Accounts.Update(entity);
    }
}
