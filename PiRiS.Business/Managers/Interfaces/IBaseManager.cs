using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Managers.Interfaces
{
    public interface IBaseManager
    {
        IMapper Mapper { get; }

        IUnitOfWork UnitOfWork { get; }

        ILogger Logger { get; }
    }
}
