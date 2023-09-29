using AutoMapper;
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;

namespace ProductionOrder.Application.AutoMapper
{
    public class DomainToViewModelMapping : Profile
    {
        public DomainToViewModelMapping()
        {
            CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(viewModel => viewModel.Services, opt => 
                    opt.MapFrom(entity => entity.AppointmentServices.Select(appointmentService => new Service(appointmentService.ServiceId, appointmentService.Service.Name))));

            CreateMap<Client, ClientViewModel>();

            CreateMap<Service, ServiceViewModel>();
        }
    }
}