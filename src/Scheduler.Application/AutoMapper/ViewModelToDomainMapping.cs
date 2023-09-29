using AutoMapper;
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;

namespace ProductionOrder.Application.AutoMapper
{
    public class ViewModelToDomainMapping : Profile
    {
        public ViewModelToDomainMapping()
        {
            CreateMap<AppointmentViewModel, Appointment>()
                .ForMember(entity => entity.AppointmentServices, opt =>
                opt.MapFrom(viewModel => viewModel.Services.Select(service => new AppointmentService(service.Id))));

            CreateMap<ClientViewModel, Client>();

            CreateMap<ServiceViewModel, Service>();
        }
    }
}