using AutoMapper;
using Scheduler.Application.Interfaces;
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Domain.Interfaces.UoW;

namespace Scheduler.Application.Services
{
    public class ClientService : IClientService
    {
        public readonly IClientRepository _clientRepository;
        public readonly IUnitOfWork _uow;
        public readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository,
                                  IUnitOfWork uow,
                                  IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public bool Create(ClientViewModel client)
        {
            try
            {
                _clientRepository.Create(_mapper.Map<Client>(client));

                return _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(ClientViewModel client)
        {
            try
            {
                _clientRepository.Update(_mapper.Map<Client>(client));

                return _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ClientViewModel> Get()
        {
            return _mapper.Map<List<ClientViewModel>>(_clientRepository.Query(client => !client.IsDeleted)
                                                                       .ToList());
        }

        public bool Delete(Guid id)
        {
            return _clientRepository.Delete(id);
        }
    }
}