namespace PiRiS.Business.Managers.Interfaces;

public interface IBankManager : IBaseManager
{
    Task CloseBankDayAsync();
}
