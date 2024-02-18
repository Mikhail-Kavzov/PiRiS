using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class DepositRepository : BaseRepository, IDepositRepository
{
    public DepositRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(Expression<Func<Deposit, bool>>? predicate = null)
    {
        IQueryable<Deposit> query = _context.Deposits;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync();
    }

    public void Create(Deposit entity)
    {
        _context.Deposits.Add(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<Deposit, bool>> predicate)
    {
        return await _context.Deposits.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Deposit>> GetAllAsync()
    {
        return await _context.Deposits.ToListAsync();
    }

    public async Task<string> GetCurrencyNameAsync(Expression<Func<Deposit, bool>> predicate)
    {
        return await _context.Deposits.Where(predicate).Select(x => x.DepositPlan.Currency.CurrencyName).FirstOrDefaultAsync();
    }

    public async Task<Deposit?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.Deposits.Include(x => x.DepositPlan).ThenInclude(x => x.Currency).Include(x => x.Client)
            .Include(x => x.PercentAccount).ThenInclude(x=>x.AccountPlan)
            .Include(x => x.MainAccount).ThenInclude(x=>x.AccountPlan).AsNoTracking().FirstOrDefaultAsync(x => x.DepositId == id);
    }

    public async Task<Deposit?> GetEntityAsync(Expression<Func<Deposit, bool>> predicate)
    {
        return await _context.Deposits.Include(x => x.DepositPlan).ThenInclude(x => x.Currency).Include(x => x.Client)
            .Include(x => x.PercentAccount).ThenInclude(x=>x.AccountPlan)
            .Include(x => x.MainAccount).ThenInclude(x=> x.AccountPlan).AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Deposit>> GetListAsync(int skip, int take, Expression<Func<Deposit, bool>>? predicate = null,
        Expression<Func<Deposit, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<Deposit> query = _context.Deposits;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }

        return await query.Skip(skip).Take(take).Include(x => x.DepositPlan).ThenInclude(x => x.Currency).Include(x => x.Client)
            .Include(x => x.MainAccount).ThenInclude(x=>x.AccountPlan).Include(x => x.PercentAccount).ThenInclude(x=> x.AccountPlan).AsNoTracking().ToListAsync();
    }

    public void Update(Deposit entity)
    {
        _context.Deposits.Update(entity);
    }
}
