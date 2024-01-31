using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Managers;

public abstract class BaseManager : IBaseManager
{
    public IMapper Mapper { get; }

    public IUnitOfWork UnitOfWork { get; }

    public ILogger Logger { get; }

    protected BaseManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
        Logger = logger;
    }
}
