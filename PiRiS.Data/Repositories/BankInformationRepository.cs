using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class BankInformationRepository : BaseRepository, IBankInformationRepository
{
    public BankInformationRepository(BankDbContext context) : base(context)
    {
    }

    public void Create(BankInformation entity)
    {
        _context.BankInformation.Add(entity);
    }

    public async Task<BankInformation?> GetEntityAsync(int id, bool trackChanges = true)
    {
        return await _context.BankInformation.FirstOrDefaultAsync();
    }

    public async Task<BankInformation?> GetEntityAsync(Expression<Func<BankInformation, bool>> predicate)
    {
        return await _context.BankInformation.FirstOrDefaultAsync(predicate);
    }

    public void Update(BankInformation entity)
    {
        _context.BankInformation.Update(entity);
    }
}
