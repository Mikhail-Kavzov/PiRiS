using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class CitizenshipRepository : BaseRepository, ICitizenshipRepository
{
    public CitizenshipRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Citizenship>> GetAllAsync()
    {
        return await _context.Citizenships.ToListAsync();
    }

    public Task<IEnumerable<Citizenship>> GetListAsync(int skip, int take, Expression<Func<Citizenship, bool>>? predicate = null,
        Expression<Func<Citizenship, object>>? sort = null, bool isAscending = true)
    {
        throw new NotImplementedException();
    }
}
