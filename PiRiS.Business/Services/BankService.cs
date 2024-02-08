using AutoMapper;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Services;

public class BankService : BaseService, IBankService
{
    public BankService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DateTime> GetCurrentDayAsync()
    {
        var bankInfo = await UnitOfWork.BankInformationRepository.GetEntityAsync(0);
        if (bankInfo == null)
        {
            var newBankInfo = new BankInformation
            {
                CurrentDay = DateTime.UtcNow,
            };
            UnitOfWork.BankInformationRepository.Create(newBankInfo);
            await UnitOfWork.BankInformationRepository.SaveChangesAsync();
            return newBankInfo.CurrentDay;
        }
        return bankInfo.CurrentDay;
    }

    public async Task IncreaseCurrentDayAsync(int daysCount = 1)
    {
        var bankInfo = await UnitOfWork.BankInformationRepository.GetEntityAsync(0);
        bankInfo.CurrentDay = bankInfo.CurrentDay.AddDays(daysCount);
        UnitOfWork.BankInformationRepository.Update(bankInfo);
        await UnitOfWork.BankInformationRepository.SaveChangesAsync();

    }
}
