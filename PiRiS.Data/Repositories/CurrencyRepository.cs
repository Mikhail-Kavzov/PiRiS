using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class CurrencyRepository : BaseRepository, ICurrencyRepository
{
    public CurrencyRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(Expression<Func<Currency, bool>> predicate)
    {
        return await _context.Currencies.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Currency>> GetAllAsync()
    {
        return await _context.Currencies.ToListAsync();
    }

    public Task<IEnumerable<Currency>> GetListAsync(int skip, int take, Expression<Func<Currency, bool>>? predicate = null, 
        Expression<Func<Currency, object>>? sort = null, bool isAscending = true)
    {
        throw new NotImplementedException();
    }
}
