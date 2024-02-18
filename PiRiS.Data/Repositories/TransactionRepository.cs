using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(Expression<Func<Transaction, bool>>? predicate = null)
    {
        IQueryable<Transaction> query = _context.Transactions;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    public void Create(Transaction entity)
    {
        _context.Transactions.Add(entity);
    }

    public Task<IEnumerable<Transaction>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
    }

    public async Task<Transaction?> GetEntityAsync(Expression<Func<Transaction, bool>> predicate)
    {
        return await _context.Transactions.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Transaction>> GetListAsync(int skip, int take, Expression<Func<Transaction, bool>>? predicate = null,
        Expression<Func<Transaction, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<Transaction> query = _context.Transactions;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }
        return await query.Skip(skip).Take(take).Include(x=> x.CreditAccount).Include(x=>x.DebitAccount).AsNoTracking().ToListAsync();
    }
}
