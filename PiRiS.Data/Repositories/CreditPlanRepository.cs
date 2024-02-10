using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class CreditPlanRepository : BaseRepository, ICreditPlanRepository
{
    public CreditPlanRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(Expression<Func<CreditPlan, bool>>? predicate = null)
    {
        IQueryable<CreditPlan> query = _context.CreditPlans;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    public void Create(CreditPlan entity)
    {
        _context.CreditPlans.Add(entity);
    }

    public async Task<IEnumerable<CreditPlan>> GetAllAsync()
    {
        return await _context.CreditPlans.Include(x => x.Currency).ToListAsync();
    }

    public async Task<CreditPlan?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.CreditPlans.FirstOrDefaultAsync(x => x.CreditPlanId == id);
    }

    public Task<CreditPlan?> GetEntityAsync(Expression<Func<CreditPlan, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CreditPlan>> GetListAsync(int skip, int take, Expression<Func<CreditPlan, bool>>? predicate = null,
        Expression<Func<CreditPlan, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<CreditPlan> query = _context.CreditPlans;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }
        return await query.Skip(skip).Take(take).Include(x => x.Currency)
            .Include(x => x.MainAccountPlan).Include(x => x.PercentAccountPlan).ToListAsync();
    }
}
