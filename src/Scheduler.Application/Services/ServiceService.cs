using AutoMapper;
using Scheduler.Application.Interfaces;
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Domain.Interfaces.UoW;

namespace Scheduler.Application.Services
{
    public class ServiceService : IServiceService
    {
        public readonly IServiceRepository _serviceRepository;
        public readonly IUnitOfWork _uow;
        public readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository,
                                  IUnitOfWork uow,
                                  IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public bool Create(ServiceViewModel service)
        {
            try
            {
                _serviceRepository.Create(_mapper.Map<Service>(service));

                return _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(ServiceViewModel service)
        {
            try
            {
                _serviceRepository.Update(_mapper.Map<Service>(service));

                return _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ServiceViewModel> Get()
        {
            return _mapper.Map<List<ServiceViewModel>>(_serviceRepository.Query(service => !service.IsDeleted)
                                                                       .ToList());
        }

        public bool Delete(Guid id)
        {
            return _serviceRepository.Delete(id);
        }
    }
}