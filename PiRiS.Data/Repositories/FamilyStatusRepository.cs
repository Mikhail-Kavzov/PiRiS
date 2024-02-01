using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class FamilyStatusRepository : BaseRepository, IFamilyStatusRepository
{
    public FamilyStatusRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FamilyStatus>> GetAllAsync()
    {
        return await _context.FamilyStatuses.ToListAsync();
    }

    public Task<IEnumerable<FamilyStatus>> GetListAsync(int skip, int take, Expression<Func<FamilyStatus, bool>>? predicate = null,
        Expression<Func<FamilyStatus, object>>? sort = null, bool isAscending = true)
    {
        throw new NotImplementedException();
    }
}
