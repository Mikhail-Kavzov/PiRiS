using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class AccountPlanRepository : BaseRepository, IAccountPlanRepository
{
    public AccountPlanRepository(BankDbContext context) : base(context)
    {
    }

    public void Create(AccountPlan entity)
    {
        _context.AccountPlans.Add(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<AccountPlan, bool>> predicate)
    {
        return await _context.AccountPlans.AnyAsync(predicate);
    }

    public async Task<AccountPlan?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.AccountPlans.FirstOrDefaultAsync(x=> x.AccountPlanId == id);
    }

    public async Task<AccountPlan?> GetEntityAsync(Expression<Func<AccountPlan, bool>> predicate)
    {
        return await _context.AccountPlans.AsNoTracking().FirstOrDefaultAsync(predicate);
    }
}
