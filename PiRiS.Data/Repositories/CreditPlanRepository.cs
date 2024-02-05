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

    public async Task<IEnumerable<CreditPlan>> GetAllAsync()
    {
        return await _context.CreditPlans.ToListAsync();
    }

    public async Task<CreditPlan?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.CreditPlans.FirstOrDefaultAsync(x=> x.CreditPlanId == id);
    }

    public Task<IEnumerable<CreditPlan>> GetListAsync(int skip, int take, Expression<Func<CreditPlan, bool>>? predicate = null,
        Expression<Func<CreditPlan, object>>? sort = null, bool isAscending = true)
    {
        throw new NotImplementedException();
    }
}
