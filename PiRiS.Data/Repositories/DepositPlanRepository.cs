using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class DepositPlanRepository : BaseRepository, IDepositPlanRepository
{
    public DepositPlanRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(Expression<Func<DepositPlan, bool>>? predicate = null)
    {
        IQueryable<DepositPlan> query = _context.DepositPlans;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    public void Create(DepositPlan entity)
    {
        _context.DepositPlans.Add(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<DepositPlan, bool>> predicate)
    {
        return await _context.DepositPlans.AnyAsync(predicate);
    }

    public async Task<IEnumerable<DepositPlan>> GetAllAsync()
    {
        return await _context.DepositPlans.Include(x => x.Currency).ToListAsync();
    }

    public async Task<DepositPlan?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.DepositPlans.Include(x=> x.MainAccountPlan).Include(x=>x.PercentAccountPlan)
            .Include(x=>x.Currency).FirstOrDefaultAsync(x => x.DepositPlanId == id);
    }

    public Task<DepositPlan?> GetEntityAsync(Expression<Func<DepositPlan, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<DepositPlan>> GetListAsync(int skip, int take, Expression<Func<DepositPlan, bool>>? predicate = null,
        Expression<Func<DepositPlan, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<DepositPlan> query = _context.DepositPlans;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }

        return await query.Skip(skip).Take(take)
            .Include(x => x.Currency).Include(x => x.MainAccountPlan).Include(x => x.PercentAccountPlan).ToListAsync();
    }
}
