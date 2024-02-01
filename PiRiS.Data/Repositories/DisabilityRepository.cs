using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class DisabilityRepository : BaseRepository, IDisabilityRepository
{
    public DisabilityRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Disability>> GetAllAsync()
    {
        return await _context.Disabilities.ToListAsync();
    }

    public Task<IEnumerable<Disability>> GetListAsync(int skip, int take, Expression<Func<Disability, bool>>? predicate = null,
        Expression<Func<Disability, object>>? sort = null, bool isAscending = true)
    {
        throw new NotImplementedException();
    }
}
