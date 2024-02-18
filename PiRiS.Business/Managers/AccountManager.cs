using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Account;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using System.Linq.Expressions;

namespace PiRiS.Business.Managers;

public class AccountManager : BaseManager, IAccountManager
{
    public AccountManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<AccountManager> logger) 
        : base(mapper, unitOfWork, logger)
    {
    }

    public async Task<PaginationList<AccountDto>> GetAccountsAsync(AccountPaginationDto accountPaginationDto)
    {
        Expression<Func<Account, bool>> predicate = null;

        if (!string.IsNullOrEmpty(accountPaginationDto.AccountNumber))
        {
            predicate = x => x.AccountNumber.StartsWith(accountPaginationDto.AccountNumber);
        }

        var totalCount = await UnitOfWork.AccountRepository.CountAsync(predicate);
        var accounts = await UnitOfWork.AccountRepository
            .GetListAsync(accountPaginationDto.Skip, accountPaginationDto.Take, predicate);

        return new PaginationList<AccountDto>
        {
            Items = Mapper.Map<List<AccountDto>>(accounts),
            TotalCount = totalCount,
        };
    }
}
