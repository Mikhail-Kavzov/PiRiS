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

    public async Task<bool> ExistsAsync(Expression<Func<DepositPlan, bool>> predicate)
    {
        return await _context.DepositPlans.AnyAsync(predicate);
    }

    public async Task<IEnumerable<DepositPlan>> GetAllAsync()
    {
        return await _context.DepositPlans.ToListAsync();
    }

    public async Task<DepositPlan?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.DepositPlans.FirstOrDefaultAsync(x=> x.DepositPlanId == id);
    }

    public Task<IEnumerable<DepositPlan>> GetListAsync(int skip, int take, Expression<Func<DepositPlan, bool>>? predicate = null,
        Expression<Func<DepositPlan, object>>? sort = null, bool isAscending = true)
    {
        throw new NotImplementedException();
    }
}
