using AutoMapper;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Services.Interfaces;

public interface IBaseService
{
    IMapper Mapper { get; }
    IUnitOfWork UnitOfWork { get; }
}
