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

    public void Create(Transaction entity)
    {
        _context.Transactions.Add(entity);
    }

    public async Task<Transaction?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
    }

    public async Task<Transaction?> GetEntityAsync(Expression<Func<Transaction, bool>> predicate)
    {
        return await _context.Transactions.FirstOrDefaultAsync(predicate);
    }
}
