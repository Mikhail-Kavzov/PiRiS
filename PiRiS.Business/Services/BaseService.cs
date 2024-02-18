using AutoMapper;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Services;

public abstract class BaseService : IBaseService
{
    public IMapper Mapper { get; }

    public IUnitOfWork UnitOfWork { get; }

    protected BaseService (IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

}
