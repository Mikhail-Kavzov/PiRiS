namespace PiRiS.Business.Services.Interfaces;

public interface IBankService : IBaseService
{
    Task<DateTime> GetCurrentDayAsync();
    Task IncreaseCurrentDayAsync(int daysCount = 1);
}
