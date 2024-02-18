using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Account;

namespace PiRiS.Business.Managers.Interfaces;

public interface IAccountManager : IBaseManager
{
    Task<PaginationList<AccountDto>> GetAccountsAsync(AccountPaginationDto accountPaginationDto);
}
