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

    public async Task<int> CountAsync(Expression<Func<Account, bool>>? predicate = null)
    {
        IQueryable<Account> query = _context.Accounts;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x=> x.AccountId == id);
    }

    public async Task<Account?> GetEntityAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _context.Accounts.Include(x=> x.AccountPlan).FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Account>> GetListAsync(int skip, int take, Expression<Func<Account, bool>>? predicate = null,
        Expression<Func<Account, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<Account> query = _context.Accounts;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }

        return await query.Skip(skip).Take(take).Include(x => x.AccountPlan).ToListAsync();
    }

    public void Update(Account entity)
    {
        _context.Accounts.Update(entity);
    }
}
