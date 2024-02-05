using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.Repositories;

public class CreditPlanRepository : BaseRepository, ICreditPlanRepository
{
    public CreditPlanRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<CreditPlan?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.CreditPlans.FirstOrDefaultAsync(x=> x.CreditPlanId == id);
    }
}
