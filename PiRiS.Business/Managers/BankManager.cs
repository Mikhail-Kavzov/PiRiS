using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Managers;

public class BankManager : BaseManager, IBankManager
{
    private readonly ITransactionService _transactionService;
    private readonly IBankService _bankService;

    public BankManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<BankManager> logger,
        ITransactionService transactionService, IBankService bankService) : base(mapper, unitOfWork, logger)
    {
        _transactionService = transactionService;
        _bankService = bankService;
    }

    public async Task CloseBankDayAsync()
    {
        await _transactionService.CloseDepositsForDayAsync();
        await _transactionService.CloseCreditsForDayAsync();
        await _bankService.IncreaseCurrentDayAsync();
    }
}
