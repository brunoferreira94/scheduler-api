using AutoMapper;
using Scheduler.Application.Interfaces;
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Domain.Interfaces.UoW;
using Microsoft.EntityFrameworkCore;
using System.Data;
using NuGet.Packaging;
using System.Linq;

namespace Scheduler.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        public readonly IAppointmentRepository _appointmentRepository;
        public readonly IAppointmentServiceRepository _appointmentServiceRepository;
        public readonly IUnitOfWork _uow;
        public readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  IUnitOfWork uow,
                                  IMapper mapper,
                                  IAppointmentServiceRepository appointmentServiceRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _uow = uow;
            _appointmentServiceRepository = appointmentServiceRepository;
        }

        public bool Create(AppointmentViewModel appointmentViewModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(appointmentViewModel?.Client?.Email)
                    && _appointmentRepository.Query(appointment => appointment.Client.Email == appointmentViewModel.Client.Email).Any())
                {
                    return false;
                }

                _appointmentRepository.Create(_mapper.Map<Appointment>(appointmentViewModel));

                return _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(AppointmentViewModel appointmentViewModel)
        {
            Appointment? _appointment = _appointmentRepository.Query(appointment => appointment.Id == appointmentViewModel.Id,
                                                                    appointments => appointments.Include(appointment => appointment.AppointmentServices))
                                                                .FirstOrDefault() ?? throw new KeyNotFoundException("Id de agendamento não encontrado.");

            List<Domain.Entities.AppointmentService> _appointmentServiceToDelete =
                _appointment.AppointmentServices.Where(appointmentService => !appointmentViewModel.Services.Any(service => service.Id == appointmentService.ServiceId))
                                                .ToList();
            appointmentViewModel.Services =
                appointmentViewModel.Services.Where(service => !(_appointment.AppointmentServices.Any()
                                                            && _appointment.AppointmentServices.Any(appointmentService => appointmentService.ServiceId == service.Id)));

            _appointmentServiceRepository.Delete(_appointmentServiceToDelete);

            _mapper.Map(appointmentViewModel, _appointment);

            _appointmentRepository.Update(_appointment);
            return _uow.Commit();
        }

        public List<AppointmentViewModel> Get()
        {
            return _appointmentRepository.Query(appointment => !appointment.IsDeleted)
                                         .Select(appointment => new AppointmentViewModel(appointment.Id,
                                                                                         appointment.ScheduledDate,
                                                                                         appointment.ClientId,
                                                                                         new ClientViewModel(appointment.ClientId,
                                                                                                             appointment.Client.Name,
                                                                                                             appointment.Client.Email),
                                                                                         appointment.AppointmentServices.Select(appointmentService => new ServiceViewModel(appointmentService.ServiceId,
                                                                                                                                                                           appointmentService.Service.Name))))
                                         .ToList();
        }

        public bool Delete(Guid id)
        {
            _appointmentRepository.Delete(id);

            return _uow.Commit();
        }

        public List<AppointmentViewModel> GetAppointmentsWithin24Hours(DateTime referenceDateTime)
        {
            referenceDateTime = referenceDateTime.AddDays(-1);

            return _appointmentRepository.Query(appointment => appointment.ScheduledDate >= referenceDateTime)
                                         .Select(appointment => new AppointmentViewModel(appointment.ScheduledDate, appointment.Client.Email))
                                         .ToList();
        }
    }
}