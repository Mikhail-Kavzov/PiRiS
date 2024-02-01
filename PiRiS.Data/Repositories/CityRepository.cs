using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class CityRepository : BaseRepository, ICityRepository
{
    public CityRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Cities.ToListAsync();
    }

    public Task<IEnumerable<City>> GetListAsync(int skip, int take, Expression<Func<City, bool>>? predicate = null
        , Expression<Func<City, object>>? sort = null, bool isAscending = true)
    {
        throw new NotImplementedException();
    }
}
