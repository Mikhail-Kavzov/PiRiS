using PiRiS.Data.Context;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories;

public abstract class BaseRepository : IRepository
{
    protected BankDbContext _context;

    protected BaseRepository(BankDbContext context) 
    { 
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
