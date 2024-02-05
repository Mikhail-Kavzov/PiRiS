using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class CreditRepository : BaseRepository, ICreditRepository
{
    public CreditRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(Expression<Func<Credit, bool>>? predicate = null)
    {
        IQueryable<Credit> query = _context.Credits;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    public void Create(Credit entity)
    {
        _context.Credits.Add(entity);
    }

    public Task<IEnumerable<Credit>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Credit>> GetListAsync(int skip, int take, Expression<Func<Credit, bool>>? predicate = null,
        Expression<Func<Credit, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<Credit> query = _context.Credits;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }

        return await query.Skip(skip).Take(take).Include(x => x.MainAccount)
            .Include(x => x.PercentAccount).Include(x => x.CreditPlan).ToListAsync();
    }
}
