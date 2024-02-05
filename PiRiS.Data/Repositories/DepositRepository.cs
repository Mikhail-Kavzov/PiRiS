﻿using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Context;
using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories;

public class DepositRepository : BaseRepository, IDepositRepository
{
    public DepositRepository(BankDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(Expression<Func<Deposit, bool>>? predicate = null)
    {
        IQueryable<Deposit> query = _context.Deposits;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

       return await query.CountAsync();
    }

    public void Create(Deposit entity)
    {
        _context.Deposits.Add(entity);
    }

    public Task<IEnumerable<Deposit>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Deposit>> GetListAsync(int skip, int take, Expression<Func<Deposit, bool>>? predicate = null,
        Expression<Func<Deposit, object>>? sort = null, bool isAscending = true)
    {
        IQueryable<Deposit> query = _context.Deposits;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (sort != null)
        {
            query = isAscending ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }

        return await query.Skip(skip).Take(take).Include(x=> x.DepositPlan)
            .Include(x=> x.MainAccount).Include(x=> x.PercentAccount).ToListAsync();
    }
}