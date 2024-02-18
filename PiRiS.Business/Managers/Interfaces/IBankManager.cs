using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Transaction;

namespace PiRiS.Business.Managers.Interfaces;

public interface IBankManager : IBaseManager
{
    Task CloseBankDayAsync();
    Task<PaginationList<TransactionDto>> GetTransactionsAsync(PaginationDto pagination);
}
