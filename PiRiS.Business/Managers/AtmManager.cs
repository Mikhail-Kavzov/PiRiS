using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto.Atm;
using PiRiS.Business.Dto.Credit;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Managers;

public class AtmManager : BaseManager, IAtmManager
{
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;

    public AtmManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<AtmManager> logger,
        ITransactionService transactionService, IAccountService accountService)
        : base(mapper, unitOfWork, logger)
    {
        _transactionService = transactionService;
        _accountService = accountService;
    }

    public async Task<CreditDto> LoginAsync(string creditCardNumber, string creditCardCode)
    {
        var credit = await UnitOfWork.CreditRepository
            .GetEntityAsync(x=> x.CreditCardNumber ==  creditCardNumber && x.CreditCardCode == creditCardCode);

        if (credit == null)
        {
            throw new ServiceException("Enter valid card credentials");
        }

        return Mapper.Map<CreditDto>(credit);
    }

    public async Task WithdrawMoneyAsync(int creditId, decimal sum)
    {
        var credit = await UnitOfWork.CreditRepository.GetEntityAsync(creditId);
        if (credit.MainAccount.Balance < sum)
        {
            throw new ServiceException("Account doesn't have enough money");
        }

        var bankAccount = await _accountService.GetBankAccountAsync();
        await _transactionService.PerformTransactionAsync(credit.MainAccount, bankAccount, sum);
        await _transactionService.WithdrawBankTransactionAsync(sum);
    }
}
