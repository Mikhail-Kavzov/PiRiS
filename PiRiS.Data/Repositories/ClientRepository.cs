using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(Expression<Func<Client, bool>>? predicate = null)
    {
        IQueryable<Client> query = _context.Clients;
        if(predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    public void Create(Client entity)
    {
       _context.Clients.Add(entity);
    }

    public void Delete(Client entity)
    {
        _context.Clients.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<Client, bool>> predicate)
    {
        return await _context.Clients.AnyAsync(predicate);
    }

    public Task<IEnumerable<Client>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Client?> GetEntityAsync(int id, bool trackChanges = true)
    {
        IQueryable<Client> query = _context.Clients;
        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(x => x.ClientId == id);
    }

    public async Task<Client?> GetEntityAsync(Expression<Func<Client, bool>> predicate)
    {
        return await _context.Clients.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Client>> GetListAsync(int skip, int take)
    {
        return await _context.Clients.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<Client>> GetListAsync(int skip, int take, 
        Expression<Func<Client, bool>>? predicate = null, Expression<Func<Client, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<Client> query = _context.Clients;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }
        return await query.Skip(skip).Take(take).Include(x=> x.Disability)
            .Include(x=>x.Citizenship).Include(x=>x.City)
            .Include(x=>x.FamilyStatus).ToListAsync();
    }

    public void Update(Client entity)
    {
        _context.Clients.Update(entity);
    }
}
